﻿using System.Collections.Generic;

namespace AppLogistics.Components.Extensions
{
    public class MvcTree
    {
        public List<MvcTreeNode> Nodes { get; set; }
        public HashSet<int> SelectedIds { get; set; }

        public MvcTree()
        {
            Nodes = new List<MvcTreeNode>();
            SelectedIds = new HashSet<int>();
        }
    }
}
