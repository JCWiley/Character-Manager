
using CharacterManager.Model.RedundantTree;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.Factories
{
    public interface IRTreeFactory<T>
    {
        Guid CreateBlankGuid();
        Guid CreateGuid();
        IDictionary<Guid, IRTreeMember<T>> CreateGuidDictionary();
        List<Guid> CreateGuidList();
        RTree<T> CreateRTree();
        IRTreeMember<T> CreateRTreeMember(RTree<T> rTree);
        List<IRTreeMember<T>> CreateMemberList();
    }
}