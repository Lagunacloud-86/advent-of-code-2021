using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class ChunkTree
    {

        private static Dictionary<Char, ChunkNodeType> _tagTypes = new Dictionary<char, ChunkNodeType>
        {
            { '(', ChunkNodeType.ChunkOpen },
            { '[', ChunkNodeType.ChunkOpen },
            { '{', ChunkNodeType.ChunkOpen },
            { '<', ChunkNodeType.ChunkOpen },
            { ')', ChunkNodeType.ChunkClose },
            { ']', ChunkNodeType.ChunkClose },
            { '}', ChunkNodeType.ChunkClose },
            { '>', ChunkNodeType.ChunkClose },
        };

        private static Dictionary<Char, Char> _tagClosers = new Dictionary<char, Char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        private List<ChunkNode> _chunkNodes = new List<ChunkNode>();
        private String _input = null;

        public ChunkTree(in String line)
        {
            _input = line;
            for (Int32 c = 0; c < line.Length; ++c)
            {
                if (_tagTypes.ContainsKey(line[c]))
                    _chunkNodes.Add(new ChunkNode(in c, _tagTypes[line[c]]));
            }
        }




        public String AutocompleteSyntaxClosers()
        {
            UInt64 count = 0;
            Stack<char> chunkStack = new Stack<char>();
            for (Int32 i = 0; i < _chunkNodes.Count; ++i)
            {
                char currentTag = _input[_chunkNodes[i].Index];
                if (_chunkNodes[i].ChunkType == ChunkNodeType.ChunkOpen)
                {
                    chunkStack.Push(currentTag);
                }
                else if (_chunkNodes[i].ChunkType == ChunkNodeType.ChunkClose)
                {
                    char openTag = chunkStack.Pop();
                }
            }


            StringBuilder stringBuilder = new StringBuilder();
            while(chunkStack.Count > 0)
            {
                char openTag = chunkStack.Pop();
                stringBuilder.Append(_tagClosers[openTag]);
            }
            return stringBuilder.ToString();
        }


        public Boolean FindWrongCloseBracket(out char bracketTag)
        {
            bracketTag = '\0';
            Stack<char> chunkStack = new Stack<char>();
            for (Int32 i = 0; i < _chunkNodes.Count; ++i)
            {
                char currentTag = _input[_chunkNodes[i].Index];
                if (_chunkNodes[i].ChunkType == ChunkNodeType.ChunkOpen)
                {
                    chunkStack.Push(currentTag);
                }
                else if (_chunkNodes[i].ChunkType == ChunkNodeType.ChunkClose)
                {
                    char openTag = chunkStack.Pop();
                    if (_tagClosers[openTag] != currentTag)
                    {
                        bracketTag = currentTag;
                        return true;
                    }
                    //if (openTag != currentTag && currentTag == syntax)
                    //{
                    //    count++;
                    //}
                }
            }
            return false;
        }


    }
    
}
