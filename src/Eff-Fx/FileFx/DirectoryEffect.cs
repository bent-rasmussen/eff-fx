namespace Eff_Fx.FileFx
{
    /// <summary>
    /// Directory effect.
    /// </summary>
	public static class DirectoryEffect
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
}
