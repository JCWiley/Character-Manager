using System;
using System.Collections.Generic;

namespace CharacterManager.Model.RedundantTree
{
    class InvalidIRTreeMember<T> : IRTreeMember<T>
    {
        public List<Guid> Child_Guids
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }
        public Guid Gid
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }
        public bool IsHead
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }
        public T Item
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }
        public List<Guid> Parents
        {
            get
            {
                throw new InvalidOperationException();
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        public List<IRTreeMember<T>> Child_Items
        {
            get
            {
                throw new InvalidOperationException();
            }
        }

        public void AddChild(Guid i_gid)
        {
            throw new InvalidOperationException();
        }

        public void AddParent(Guid i_gid)
        {
            throw new InvalidOperationException();
        }

        public void RemoveChild(Guid i_gid)
        {
            throw new InvalidOperationException();
        }

        public void RemoveParent(Guid i_gid)
        {
            throw new InvalidOperationException();
        }
    }
}
