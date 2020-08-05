using System;
using System.Collections.Generic;
using System.IO;
using Nessos.Effects;

namespace Eff_Fx.FileFx
{
    // (Types capturing individual effects)

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
