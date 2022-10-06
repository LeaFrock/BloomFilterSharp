using System.Text;
using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp.Memory.Internal
{
    public class Utf8StringMemoryBloomFilter : MemoryBloomFilter<string>
    {
        public Utf8StringMemoryBloomFilter(IHashFunction hashFunction, long expectedLength, double errorRate) : base(hashFunction, expectedLength, errorRate)
        {
        }

        public override ReadOnlySpan<byte> GetBytes(string value) => Encoding.UTF8.GetBytes(value);
    }
}