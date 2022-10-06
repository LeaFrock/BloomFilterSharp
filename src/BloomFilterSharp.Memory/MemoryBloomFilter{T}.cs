using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp
{
    public abstract class MemoryBloomFilter<T> : MemoryBloomFilter
    {
        public MemoryBloomFilter(IHashFunction hashFunction, long expectedLength, double errorRate) : base(hashFunction, expectedLength, errorRate)
        {
        }

        public abstract ReadOnlySpan<byte> GetBytes(T value);

        public void Append(T value) => Append(GetBytes(value));

        public bool Contains(T value) => Contains(GetBytes(value));
    }
}