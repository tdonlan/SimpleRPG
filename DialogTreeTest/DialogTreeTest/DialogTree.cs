using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogTreeTest
{
    public class DialogTree
    {
        public Dictionary<int, Node> dialogNodeDictionary;

        public DialogTree(Dictionary<int, Node> dialogNodes)
        {
            this.dialogNodeDictionary = dialogNodes;

        }

        public DisplayNode getDisplayNode(int id)
        {
            return (DisplayNode)dialogNodeDictionary[id];
        }

        public ResponseNode getResponseNode(int id)
        {
            return (ResponseNode)dialogNodeDictionary[id];
        }

    }
}
