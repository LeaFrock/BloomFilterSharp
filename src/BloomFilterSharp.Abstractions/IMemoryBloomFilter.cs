namespace BloomFilterSharp.Abstractions
{
    public interface IMemoryBloomFilter
    {
        IHashFunction HashFunc { get; }

        long ExpectedLength { get; }

        double ErrorRate { get; }

        long Capacity { get; }

        int HashingCount { get; }

        void Append(ReadOnlySpan<byte> data);

        bool Contains(ReadOnlySpan<byte> data);
    }
}