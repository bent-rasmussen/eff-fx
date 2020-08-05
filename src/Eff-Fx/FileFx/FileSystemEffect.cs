namespace Eff_Fx.FileFx
{
    /// <summary>
    /// File system effect.
    /// </summary>
	public static class FileSystemEffect
    {
        public static GetPhysicalPathEffect ToPhysicalPath((string path, FileKind kind) info) => new GetPhysicalPathEffect(info.path, info.kind);

        public static GetFileExistsEffect Exists((string path, FileKind kind) info) => new GetFileExistsEffect(info.path, info.kind);

        public static GetFileCreationTimeEffect CreationTime((string path, FileKind kind) info) => new GetFileCreationTimeEffect(info.path, info.kind);

        public static GetFileLastWriteTimeEffect LastWriteTime((string path, FileKind kind) info) => new GetFileLastWriteTimeEffect(info.path, info.kind);

        public static DeleteFileEffect Delete((string path, FileKind kind) info) => new DeleteFileEffect(info.path, info.kind);
    }
}
