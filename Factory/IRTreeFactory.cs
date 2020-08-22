using Character_Manager.RedundantTree;
using System;
using System.Collections.Generic;

namespace Character_Manager.Factory
{
    public interface IRTreeFactory<T>
    {
        Guid CreateBlankGuid();
        Guid CreateGuid();
        IDictionary<Guid, IRTreeMember<T>> CreateGuidDictionary();
        List<Guid> CreateGuidList();
        RTree<T> CreateRTree();
        IRTreeMember<T> CreateRTreeMember();
        List<IRTreeMember<T>> CreateMemberList();
    }
}