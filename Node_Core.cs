using System;
using System.Collections.Generic;
using System.Text;

namespace TrieDataStructure
{
    abstract class Node_Core
    {
        //Properties.
        protected char value { get; set; }
        protected List<Node> children { get; set; }
        protected int numOfChildren { get; set; }
        protected bool isFinal { get; set; }

    }
}
