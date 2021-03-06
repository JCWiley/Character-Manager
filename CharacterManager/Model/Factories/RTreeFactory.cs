﻿using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.Factories
{
    public class RTreeFactory<T> : IRTreeFactory<T>
    {
        public RTreeFactory()
        {

        }

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

        public IRTreeMember<T> CreateRTreeMember(RTree<T> rTree)
        {
            return new RTreeMember<T>( CreateGuidList(), CreateGuidList(), CreateGuid(), rTree );
        }

        public RTree<T> CreateRTree()
        {
            return new RTree<T>( this );
        }
    }
}
