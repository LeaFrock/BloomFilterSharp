using BloomFilterSharp.Abstractions;
using BloomFilterSharp.Memory;

namespace BloomFilterSharp
{
    public class MemoryBloomFilter : IMemoryBloomFilter
    {
        private readonly LongBitArray _bits;

        public MemoryBloomFilter(IHashFunction hashFunction, long expectedLength, double errorRate)
        {
            HashFunc = hashFunction;
            ExpectedLength = expectedLength;
            ErrorRate = errorRate;
            Capacity = (long)Math.Ceiling(-(expectedLength * Math.Log(errorRate)) / Math.Pow(Math.Log(2), 2));

            if (Capacity < 1)
            {
                throw new ArgumentException("Value of arguments not supported");
            }

            HashingCount = (int)Math.Ceiling(Math.Log(2) * Capacity / ExpectedLength);

            if (HashingCount < 1)
            {
                throw new ArgumentException("Value of arguments not supported");
            }

            _bits = new(Capacity);
        }

        public IHashFunction HashFunc { get; }

        public long ExpectedLength { get; }

        public double ErrorRate { get; }

        public long Capacity { get; }

        public int HashingCount { get; }

        public void Append(ReadOnlySpan<byte> data)
        {
            ulong seed = default;
            for (int i = 0; i < HashingCount; i++)
            {
                seed = HashFunc.Hash64(data, seed);
                var pos = (long)(seed % (ulong)Capacity);
                _bits.Set(pos, true);
            }
        }

        public bool Contains(ReadOnlySpan<byte> data)
        {
            ulong seed = default;
            for (int i = 0; i < HashingCount; i++)
            {
                seed = HashFunc.Hash64(data, seed);
                var pos = (long)(seed % (ulong)Capacity);
                if (!_bits.Get(pos))
                {
                    return false;
                }
            }
            return true;
        }
    }
}