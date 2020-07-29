using Nessos.Effects;
using Nessos.Effects.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eff_Fx.FileSystem
{
	public abstract class PhysicalFileSystemHandlerBase : EffectHandler
	{
		public PhysicalFileSystemHandlerBase(string root)
		{
			Root = root;
		}

		public string Root { get; }

		public string ResolvePath(string path) => Path.Combine(Root, path);

		public string ResolvePath<T>(FilePathEffect<T> effect) => ResolvePath(effect.Path);
	}

	// Note: these deal with resources, i.e. they need to be disposed; this could also be modelled 
	// in such a way that streams are also types with effects, so IO has no meaning outside effect 
	// handler scope.

	public class PhysicalFileSystemHandler : PhysicalFileSystemHandlerBase
	{
		public PhysicalFileSystemHandler(string root) : base(root) { }

		public override ValueTask Handle<TResult>(EffectAwaiter<TResult> awaiter)
		{
			switch (awaiter)
			{
				// General effects

				case EffectAwaiter<string?> { Effect: GetPhysicalPathEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						awtr.SetResult(resolvedPath);
					}
					break;

				case EffectAwaiter<bool> { Effect: GetFileExistsEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						switch (info.Kind)
						{
							case FileKind.File:
								awtr.SetResult(File.Exists(resolvedPath));
								break;
							case FileKind.Directory:
								awtr.SetResult(Directory.Exists(resolvedPath));
								break;
						}
					}
					break;

				case EffectAwaiter<DateTimeOffset> { Effect: GetFileCreationTimeEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						switch (info.Kind)
						{
							case FileKind.File:
								awtr.SetResult(File.GetCreationTime(resolvedPath));
								break;
							case FileKind.Directory:
								awtr.SetResult(Directory.GetCreationTime(resolvedPath));
								break;
						}
					}
					break;

				case EffectAwaiter<DateTimeOffset> { Effect: GetFileLastWriteTimeEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						switch (info.Kind)
						{
							case FileKind.File:
								awtr.SetResult(File.GetLastWriteTime(resolvedPath));
								break;
							case FileKind.Directory:
								awtr.SetResult(Directory.GetLastWriteTime(resolvedPath));
								break;
						}
					}
					break;

				case EffectAwaiter<Unit> { Effect: DeleteFileEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						switch (info.Kind)
						{
							case FileKind.File:
								File.Delete(resolvedPath);
								awtr.SetResult(Unit.Value);
								break;
							case FileKind.Directory:
								Directory.Delete(resolvedPath);
								awtr.SetResult(Unit.Value);
								break;
						}
					}
					break;

				// File-specific effects

				case EffectAwaiter<long> { Effect: GetFileLengthEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						var length = new FileInfo(resolvedPath).Length;
						awtr.SetResult(length);
					}
					break;

				case EffectAwaiter<Stream> { Effect: GetFileInputStreamEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						var stream = File.OpenRead(resolvedPath);
						awtr.SetResult(stream);
					}
					break;

				case EffectAwaiter<Stream> { Effect: GetFileOutputStreamEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info);
						var stream = File.OpenWrite(resolvedPath);
						awtr.SetResult(stream);
					}
					break;

				// Directory-specific effects

				case EffectAwaiter<IEnumerable<(string, FileKind)>> { Effect: EnumerateDirectoryEffect info } awtr:
					{
						var resolvedPath = ResolvePath(info.Path);
						string Localize(string path) => Path.Combine(info.Path, Path.GetFileName(path));
						(string, FileKind) LocalizeToFile(string path) => (Localize(path), FileKind.File);
						(string, FileKind) LocalizeToDirectory(string path) => (Localize(path), FileKind.Directory);
						switch (info.FindKind)
						{
							case FileKind.File:
								awtr.SetResult(Directory.EnumerateFiles(resolvedPath).Select(LocalizeToFile));
								break;
							case FileKind.Directory:
								awtr.SetResult(Directory.EnumerateDirectories(resolvedPath).Select(LocalizeToDirectory));
								break;
							case null:
								var dirs = Directory.EnumerateDirectories(resolvedPath).Select(LocalizeToDirectory);
								var files = Directory.EnumerateFiles(resolvedPath).Select(LocalizeToFile);
								var allFiles = Enumerable.Concat(dirs, files);
								awtr.SetResult(allFiles);
								break;
						}
					}
					break;

				// Unhandled effects

				default:
					throw new NotSupportedException(awaiter.Effect.GetType().Name);
			}

			return default;
		}
	}
}
