using System;
using System.Collections;
using System.Collections.Generic;

namespace Nodes
{
    /// <summary>
    /// The node information
    /// </summary>
    public readonly struct NodeInfo
    {
        /// <summary>
        /// The starting index of the node
        /// </summary>
        public Int32 StartIndex { get; }

        /// <summary>
        /// The length of the node to read
        /// </summary>
        public Int32 Length { get; }

        /// <summary>
        /// Constructs the node
        /// </summary>
        /// <param name="startIndex">The start index</param>
        /// <param name="length">The length to read</param>
        public NodeInfo(
            in Int32 startIndex,
            in Int32 length)
        {
            this.StartIndex = startIndex;
            this.Length = length;
        }
    }
}
