namespace BloomFilterSharp.Abstractions
{
    public interface IHashFunction
    {
        ulong Hash64(ReadOnlySpan<byte> source, ulong seed);
    }
}