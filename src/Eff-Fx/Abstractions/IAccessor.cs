using System.Threading.Tasks;

namespace Eff_Fx.Abstractions
{
    /// <summary>
    /// Read accessor.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public interface IReadAccessor<T>
    {
        ValueTask<T> ReadAsync();
    }

    /// <summary>
    /// Write accessor.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public interface IWriteAccessor<T>
    {
        ValueTask WriteAsync(T value);
    }

    /// <summary>
    /// Read/write accessor.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public interface IAccessor<T> : IReadAccessor<T>, IWriteAccessor<T>
    {
    }
}
