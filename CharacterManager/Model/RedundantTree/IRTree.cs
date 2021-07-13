using CharacterManager.Model.Factories;
using System;
using System.Collections.Generic;

namespace CharacterManager.Model.RedundantTree
{
    public interface IRTree<T>
    {
        public IRTreeFactory<T> Factory { get; set; }

        public IDictionary<Guid, IRTreeMember<T>> Dict { get; set; }

        public List<IRTreeMember<T>> Heads { get; }

        public int Count { get; }

        public IRTreeMember<T> AddItem(T i_item, bool is_head = false);
        public void RemoveItem(IRTreeMember<T> i_item);
        public void AddChild(IRTreeMember<T> Parent, IRTreeMember<T> Child);
        public void RemoveChild(IRTreeMember<T> Parent, IRTreeMember<T> Child);

        public IRTreeMember<T> Get_Item(Guid i_gid);
        public List<IRTreeMember<T>> Get_Immidiate_Parents(IRTreeMember<T> i_item);
        public List<IRTreeMember<T>> Get_Immidiate_Children(IRTreeMember<T> i_item);
        public List<IRTreeMember<T>> Get_All_Parents(IRTreeMember<T> i_item);
        public List<IRTreeMember<T>> Get_All_Children(IRTreeMember<T> i_item);

        public List<T> Get_All_Items();
        public IRTreeMember<T> GetMemberFromItem(T item);
    }
}
