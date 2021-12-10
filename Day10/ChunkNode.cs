using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public enum ChunkNodeType
    {
        ChunkOpen,
        ChunkClose
    }

    public readonly struct ChunkNode : IEquatable<ChunkNode>
    {

        public Int32 Index { get; }

        public ChunkNodeType ChunkType { get; }


        public ChunkNode(in Int32 startIndex, ChunkNodeType chunkType)
        {
            this.Index = startIndex;
            this.ChunkType = chunkType;
        }

        public bool Equals(ChunkNode other)
        {
            return this.Index == other.Index && 
                this.ChunkType == other.ChunkType;
        }


        public override int GetHashCode()
        {
            const uint hash = 0x9e3779b9;
            var seed = this.Index + hash;
            seed ^= ((Int32)this.ChunkType) + hash + (seed << 6) + (seed >> 2);
            return (int)seed;
        }

        public override bool Equals(object obj) => obj is ChunkNode other && (this.Index == other.Index && this.ChunkType == other.ChunkType);
    }
    
}
