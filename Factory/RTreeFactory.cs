﻿using Character_Manager.RedundantTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.Factory
{
    public class RTreeFactory<T> : IRTreeFactory<T>
    {
        public Guid CreateGuid()
        {
            return Guid.NewGuid();
        }
        public Guid CreateBlankGuid()
        {
            return Guid.Empty;
        }

        public List<Guid> CreateGuidList()
        {
            return new List<Guid>();
        }
        public List<IRTreeMember<T>> CreateMemberList()
        {
            return new List<IRTreeMember<T>>();
        }

        public IDictionary<Guid, IRTreeMember<T>> CreateGuidDictionary()
        {
            return new Dictionary<Guid, IRTreeMember<T>>();
        }

        public IRTreeMember<T> CreateRTreeMember()
        {
            return new RTreeMember<T>(CreateGuidList(), CreateGuidList(),this);
        }

        public RTree<T> CreateRTree()
        {
            return new RTree<T>(CreateGuidDictionary(),this);
        }
    }
}
