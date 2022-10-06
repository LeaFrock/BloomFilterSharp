using System.IO.Hashing;
using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp.Hashing
{
    public class XxHash64Function : IHashFunction
    {
        public ulong Hash64(ReadOnlySpan<byte> source, ulong seed)
        {
            // Waiting for https://github.com/dotnet/runtime/issues/76279
            var bytes = XxHash64.Hash(source, (long)seed);
            return BitConverter.ToUInt64(bytes);
        }
    }
}