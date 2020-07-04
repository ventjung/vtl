using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace VTL
{
    public class Node
    {
        public List<Node> Children { get; set; } = new List<Node>();

        public string Value { get; set; }
    }
}
