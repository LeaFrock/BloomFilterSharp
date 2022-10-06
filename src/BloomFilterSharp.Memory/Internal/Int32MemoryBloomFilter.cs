using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp.Memory.Internal
{
    public class Int32MemoryBloomFilter : MemoryBloomFilter<int>
    {
        public Int32MemoryBloomFilter(IHashFunction hashFunction, long expectedLength, double errorRate) : base(hashFunction, expectedLength, errorRate)
        {
        }

        public override ReadOnlySpan<byte> GetBytes(int value) => BitConverter.GetBytes(value);
    }
}