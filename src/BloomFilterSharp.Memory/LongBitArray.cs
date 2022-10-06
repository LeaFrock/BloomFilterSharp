using System.Collections;
using System.Runtime.CompilerServices;

namespace BloomFilterSharp.Memory
{
    internal sealed class LongBitArray
    {
        private readonly BitArray[] b_arrays;

        public LongBitArray(long length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Length = length;
            var (Rank, Position) = Locate(length - 1);
            b_arrays = new BitArray[Rank + 1];
            for (int i = 0; i < Rank; i++)
            {
                b_arrays[i] = new BitArray(int.MaxValue);
            }
            b_arrays[^1] = new BitArray(Position + 1);
        }

        public long Length { get; }

        public bool this[long index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public bool Get(long index)
        {
            var (Rank, Position) = Locate(index);
            return b_arrays[Rank].Get(Position);
        }

        public void Set(long index, bool value)
        {
            var (Rank, Position) = Locate(index);
            b_arrays[Rank].Set(Position, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (int Rank, int Position) Locate(long index)
        {
            if (index <= int.MaxValue)
            {
                return (0, (int)index);
            }

            var count = (ulong)index + 1;
            int pos = (int)(count % int.MaxValue);
            int rank = (int)((count - (ulong)pos) / int.MaxValue);
            return pos == 0
                ? (rank - 1, int.MaxValue)
                : (rank, pos);
        }
    }
}