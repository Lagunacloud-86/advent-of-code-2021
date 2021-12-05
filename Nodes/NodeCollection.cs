using System;
using System.Collections;
using System.Collections.Generic;

namespace Nodes
{
    public class NodeCollection<TInput, TNodeType> 
        where TNodeType : struct
    {

        //private TNode[] _nodes = null;

        private TInput _input = default;
        //private INodeParser<TInput, TNodeType> _parser;

        //Gets the input of passed to teh node collection
        public TInput Input => _input;

        //public int Count { get; private set; }

        //public bool IsReadOnly => false;


        //private NodeCollection(
        //    TInput input, 
        //    INodeParser<TInput, TNodeType> parser)
        //{
        //    _input = input;
        //    _parser = parser;
        //}
                


        //public static NodeCollection<TInput, TNodeType> Create()
        //{

        //}


        //public void Add(TNode item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Clear()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Contains(TNode item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void CopyTo(TNode[] array, int arrayIndex)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerator<TNode> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Remove(TNode item)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
