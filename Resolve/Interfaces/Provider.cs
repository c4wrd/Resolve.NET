using System;
namespace Resolve.Interfaces
{
    public interface Provider<T>
    {
        T getInstance();
    }
}
