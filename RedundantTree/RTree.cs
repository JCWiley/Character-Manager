using Character_Manager.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_Manager.RedundantTree
{
    public class RTree<T>
    {
        private readonly IRTreeFactory<T> factory;

        private readonly IDictionary<Guid, IRTreeMember<T>> dict;

        public List<IRTreeMember<T>> Heads
        {
            get
            {
                return (List<IRTreeMember<T>>)dict.Values.Where(x => x.IsHead == true);
            }
        }
        public RTree(IDictionary<Guid, IRTreeMember<T>> i_dict, IRTreeFactory<T> i_factory)
        {
            dict = i_dict;
            factory = i_factory;
        }

        public int Count
        {
            get
            {
                return dict.Values.Count();
            }
        }

        public IRTreeMember<T> AddItem(T i_item, bool is_head)
        {
            IRTreeMember<T> member = factory.CreateRTreeMember();
            member.Item = i_item;
            member.IsHead = is_head;
            dict.Add(member.Gid, member);
            return member;
        }
        public void RemoveItem(IRTreeMember<T> i_item)
        {
            List<Guid> P = new List<Guid>(i_item.Parents);
            List<Guid> C = new List<Guid>(i_item.Children);

            foreach (Guid G in P)
            {
                RemoveChild(dict[G], i_item);
            }
            foreach(Guid G in C)
            {
                RemoveChild(i_item, dict[G]);
            }
            dict.Remove(i_item.Gid);
        }
        public void AddChild(IRTreeMember<T> Parent, IRTreeMember<T> Child)
        {
            if(Get_All_Parents(Parent).Contains(Child) == false)
            {
                if(Parent.Gid != Child.Gid)
                {
                    Parent.AddChild(Child.Gid);
                    Child.AddParent(Parent.Gid);
                }
            }
            else
            {
                //do nothing, adding the child would create a recursive loop
            }
        }
        public void RemoveChild(IRTreeMember<T> Parent, IRTreeMember<T> Child)
        {
            Parent.RemoveChild(Child.Gid);
            Child.RemoveParent(Parent.Gid);
        }

        public IRTreeMember<T> Get_Item(Guid i_gid)
        {
            return dict[i_gid];
        }
        public List<IRTreeMember<T>> Get_Immidiate_Parents(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Parents = factory.CreateMemberList();
            foreach(Guid G in i_item.Parents)
            {
                Parents.Add(dict[G]);
            }
            return Parents;
        }
        public List<IRTreeMember<T>> Get_Immidiate_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = factory.CreateMemberList();
            foreach (Guid G in i_item.Children)
            {
                Children.Add(dict[G]);
            }
            return Children;
        }
        public List<IRTreeMember<T>> Get_All_Parents(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Parents = Get_Immidiate_Parents(i_item);
            foreach(IRTreeMember<T> i in Parents)
            {
                foreach(IRTreeMember<T> j in Get_All_Parents(i))
                {
                    if(Parents.Contains(j) == false)
                    {
                        Parents.Add(j);
                    }
                }
            }
            return Parents;
        }
        public List<IRTreeMember<T>> Get_All_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = Get_Immidiate_Children(i_item);
            foreach (IRTreeMember<T> i in Children)
            {
                foreach (IRTreeMember<T> j in Get_All_Children(i))
                {
                    if (Children.Contains(j) == false)
                    {
                        Children.Add(j);
                    }
                }
            }
            return Children;
        }

    }
}
