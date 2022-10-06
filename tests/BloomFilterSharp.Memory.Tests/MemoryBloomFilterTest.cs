using BloomFilterSharp.Hashing;
using BloomFilterSharp.Memory.Internal;

namespace BloomFilterSharp.Memory.Tests
{
    public class MemoryBloomFilterTest
    {
        [Theory]
        [InlineData(10, 0.1)]
        [InlineData(1_000_000_000, 0.001)]
        public void Int32Test(long expectedLength, double errorRate)
        {
            int rand = Random.Shared.Next();
            var filter = new Int32MemoryBloomFilter(new XxHash64Function(), expectedLength, errorRate);

            filter.Append(rand);

            Assert.True(filter.Contains(rand));
        }
    }
}