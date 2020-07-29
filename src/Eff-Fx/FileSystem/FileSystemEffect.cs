using Nessos.Effects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Eff_Fx.FileSystem
{
	public static class FileSystemEffect
	{
		public static GetPhysicalPathEffect ToPhysicalPath((string path, FileKind kind) info) => new GetPhysicalPathEffect(info.path, info.kind);

		public static GetFileExistsEffect Exists((string path, FileKind kind) info) => new GetFileExistsEffect(info.path, info.kind);

		public static GetFileCreationTimeEffect CreationTime((string path, FileKind kind) info) => new GetFileCreationTimeEffect(info.path, info.kind);

		public static GetFileLastWriteTimeEffect LastWriteTime((string path, FileKind kind) info) => new GetFileLastWriteTimeEffect(info.path, info.kind);

		public static DeleteFileEffect Delete((string path, FileKind kind) info) => new DeleteFileEffect(info.path, info.kind);
	}

	public static class FileEffect
	{
		public static FileKind Kind => FileKind.File;

		// General effects

		public static GetPhysicalPathEffect ToPhysicalPath(string path) => new GetPhysicalPathEffect(path, Kind);

		public static GetFileExistsEffect Exists(string path) => new GetFileExistsEffect(path, Kind);

		public static GetFileCreationTimeEffect CreationTime(string path) => new GetFileCreationTimeEffect(path, Kind);

		public static GetFileLastWriteTimeEffect LastWriteTime(string path) => new GetFileLastWriteTimeEffect(path, Kind);

		public static DeleteFileEffect Delete(string path) => new DeleteFileEffect(path, Kind);

		// File-specific effects

		public static GetFileLengthEffect Length(string path) => new GetFileLengthEffect(path);

		public static GetFileInputStreamEffect OpenRead(string path) => new GetFileInputStreamEffect(path);

		public static GetFileOutputStreamEffect OpenWrite(string path) => new GetFileOutputStreamEffect(path);
	}

	public static class DirectoryIO
	{
		public static FileKind Kind => FileKind.Directory;

		// General effects

		public static GetPhysicalPathEffect ToPhysicalPath(string path) => new GetPhysicalPathEffect(path, Kind);

		public static GetFileExistsEffect Exists(string path) => new GetFileExistsEffect(path, Kind);

		public static GetFileCreationTimeEffect CreationTime(string path) => new GetFileCreationTimeEffect(path, Kind);

		public static GetFileLastWriteTimeEffect LastWriteTime(string path) => new GetFileLastWriteTimeEffect(path, Kind);

		public static DeleteFileEffect Delete(string path) => new DeleteFileEffect(path, Kind);

		// Directory-specific effects

		public static EnumerateDirectoryEffect Enumerate(string path, FileKind? findKind = null) => new EnumerateDirectoryEffect(path, findKind);

		public static EnumerateDirectoryEffect EnumerateFiles(string path) => Enumerate(path, findKind: FileKind.File);

		public static EnumerateDirectoryEffect EnumerateDirectories(string path) => Enumerate(path, findKind: FileKind.Directory);
	}

	// (Types capturing individual effects)

	public enum FileKind
	{
		File,
		Directory,
	}

	public abstract class FilePathEffect<T> : Effect<T>
	{
		public FilePathEffect(string path, FileKind kind)
		{
			Path = path;
			Kind = kind;
		}

		public string Path { get; }

		public FileKind Kind { get; }
	}

	public class EnumerateDirectoryEffect : FilePathEffect<IEnumerable<(string path, FileKind kind)>>
	{
		public EnumerateDirectoryEffect(string path, FileKind? kind = null) : base(path, FileKind.Directory)
		{
			FindKind = kind;
		}

		public FileKind? FindKind { get; }
	}

	public class GetPhysicalPathEffect : FilePathEffect<string?>
	{
		public GetPhysicalPathEffect(string path, FileKind kind) : base(path, kind) { }
	}

	public class GetFileExistsEffect : FilePathEffect<bool>
	{
		public GetFileExistsEffect(string path, FileKind kind) : base(path, kind) { }
	}

	public class GetFileLengthEffect : FilePathEffect<long>
	{
		public GetFileLengthEffect(string path) : base(path, FileKind.File) { }
	}

	public class GetFileInputStreamEffect : FilePathEffect<Stream>
	{
		public GetFileInputStreamEffect(string path) : base(path, FileKind.File) { }
	}

	public class GetFileOutputStreamEffect : FilePathEffect<Stream>
	{
		public GetFileOutputStreamEffect(string path) : base(path, FileKind.File) { }
	}

	public class GetFileCreationTimeEffect : FilePathEffect<DateTimeOffset>
	{
		public GetFileCreationTimeEffect(string path, FileKind kind) : base(path, kind) { }
	}

	public class GetFileLastWriteTimeEffect : FilePathEffect<DateTimeOffset>
	{
		public GetFileLastWriteTimeEffect(string path, FileKind kind) : base(path, kind) { }
	}

	public class DeleteFileEffect : FilePathEffect<Unit>
	{
		public DeleteFileEffect(string path, FileKind kind) : base(path, kind) { }
	}
}
