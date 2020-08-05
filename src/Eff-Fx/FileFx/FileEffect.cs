using Nessos.Effects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Eff_Fx.FileFx
{
    /// <summary>
    /// File effect.
    /// </summary>
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
}
