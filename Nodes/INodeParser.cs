using System;
using System.Collections;
using System.Collections.Generic;

namespace Nodes
{
    public interface INodeParser : IEnumerator<NodeInfo>
    {
        //Boolean ReadNext(out NodeInfo nodeInfo);

        //IEnumerable<NodeInfo> ReadAll();
    }
}
