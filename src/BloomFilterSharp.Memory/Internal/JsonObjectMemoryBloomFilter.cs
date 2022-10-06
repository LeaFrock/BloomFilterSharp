using System.Text.Json;
using BloomFilterSharp.Abstractions;

namespace BloomFilterSharp.Memory.Internal
{
    public class JsonObjectMemoryBloomFilter : MemoryBloomFilter<object>
    {
        public JsonObjectMemoryBloomFilter(IHashFunction hashFunction, long expectedLength, double errorRate) : base(hashFunction, expectedLength, errorRate)
        {
        }

        public JsonSerializerOptions? SerializerOptions { get; set; }

        public override ReadOnlySpan<byte> GetBytes(object value) => JsonSerializer.SerializeToUtf8Bytes(value, value.GetType(), SerializerOptions);
    }
}