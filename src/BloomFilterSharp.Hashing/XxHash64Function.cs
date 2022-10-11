using System.Buffers.Binary;
using System.IO.Hashing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp.Hashing
{
    public class XxHash64Function : IHashFunction
    {
        public ulong Hash64(ReadOnlySpan<byte> source, ulong seed)
        {
            // Waiting for https://github.com/dotnet/runtime/issues/76279
            Unsafe.SkipInit(out ulong hash);
            XxHash64.TryHash(source, MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref hash, 1)), out _, (long)seed);
            var hashCode = BitConverter.IsLittleEndian ? hash : BinaryPrimitives.ReverseEndianness(hash);
            return hashCode;
        }
    }
}