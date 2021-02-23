using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.RedundantTree
{
    public class RTreeMember<T> : IRTreeMember<T>
    {
        private Guid gid;
        public Guid Gid
        {
            get
            {
                return gid;
            }
            set
            {
                gid = value;
            }
        }

        private List<Guid> parents;
        public List<Guid> Parents
        {
            get
            {
                return parents;
            }
            set
            {
                parents = value;
            }
        }

        private List<Guid> children;
        public List<Guid> Child_Guids
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
            }
        }

        private T item;
        public T Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        private bool ishead;
        public bool IsHead
        {
            get
            {
                return ishead;
            }
            set
            {
                ishead = value;
            }
        }

        private RTree<T> parentrtree;
        public RTree<T> ParentRTree
        {
            get
            {
                return parentrtree;
            }
            set
            {
                parentrtree = value;
            }
        }

        [JsonIgnore]
        public List<IRTreeMember<T>> Child_Items
        {
            get
            {
                List<IRTreeMember<T>> Items = new List<IRTreeMember<T>>();
                foreach (Guid G in Child_Guids)
                {
                    Items.Add(ParentRTree.Get_Item(G));
                }
                return Items;
            }
        }

        public RTreeMember(List<Guid> i_parents, List<Guid> i_children,Guid gid,RTree<T> rTree)
        {
            Parents = i_parents;
            Child_Guids = i_children;
            Gid = gid;
            ParentRTree = rTree;
        }

        public void AddChild(Guid i_gid)
        {
            Child_Guids.Add(i_gid);
        }
        public void RemoveChild(Guid i_gid)
        {
            Child_Guids.Remove(i_gid);
        }

        public void AddParent(Guid i_gid)
        {
            Parents.Add(i_gid);
        }
        public void RemoveParent(Guid i_gid)
        {
            Parents.Remove(i_gid);
        }
    }
}
