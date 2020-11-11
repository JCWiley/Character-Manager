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
        public List<Guid> Children
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

        public RTreeMember(List<Guid> i_parents, List<Guid> i_children,Guid gid)
        {
            Parents = i_parents;
            Children = i_children;
            Gid = gid;
        }

        public void AddChild(Guid i_gid)
        {
            Children.Add(i_gid);
        }
        public void RemoveChild(Guid i_gid)
        {
            Children.Remove(i_gid);
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
