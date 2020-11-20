﻿using CharacterManager.Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CharacterManager.Model.RedundantTree
{
    public class RTree<T>
    {
        private readonly IRTreeFactory<T> factory;
        private readonly IDictionary<Guid, IRTreeMember<T>> dict;

        public List<IRTreeMember<T>> Heads
        {
            get
            {
                return dict.Values.Where(x => x.IsHead == true).ToList();
            }
        }
        public RTree(IRTreeFactory<T> i_factory)
        {
            factory = i_factory;
            dict = factory.CreateGuidDictionary();
        }

        public int Count
        {
            get
            {
                return dict.Values.Count();
            }
        }

        public IRTreeMember<T> AddItem(T i_item, bool is_head = false)
        {
            IRTreeMember<T> member = factory.CreateRTreeMember(this);
            member.Item = i_item;
            member.IsHead = is_head;
            dict.Add(member.Gid, member);
            return member;
        }
        public void RemoveItem(IRTreeMember<T> i_item)
        {
            List<Guid> P = new List<Guid>(i_item.Parents);
            List<Guid> C = new List<Guid>(i_item.Child_Guids);

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
            List<IRTreeMember<T>> Parents = new List<IRTreeMember<T>>();
            foreach (Guid G in i_item.Parents)
            {
                Parents.Add(dict[G]);
            }
            return Parents;
        }
        public List<IRTreeMember<T>> Get_Immidiate_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = new List<IRTreeMember<T>>();
            foreach (Guid G in i_item.Child_Guids)
            {
                Children.Add(dict[G]);
            }
            return Children;
        }
        public List<IRTreeMember<T>> Get_All_Parents(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Parents = Get_Immidiate_Parents(i_item);
            List<IRTreeMember<T>> Result = new List<IRTreeMember<T>>();
            foreach (IRTreeMember<T> i in Parents)
            {
                Result.Add(i);
                foreach (IRTreeMember<T> j in Get_All_Parents(i))
                {
                    if(Result.Contains(j) == false)
                    {
                        Result.Add(j);
                    }
                }
            }
            return Result;
        }
        public List<IRTreeMember<T>> Get_All_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = Get_Immidiate_Children(i_item);
            List<IRTreeMember<T>> Result = new List<IRTreeMember<T>>();
            foreach (IRTreeMember<T> i in Children)
            {
                Result.Add(i);
                foreach (IRTreeMember<T> j in Get_All_Children(i))
                {
                    if (Result.Contains(j) == false)
                    {
                        Result.Add(j);
                    }
                }
            }
            return Result;
        }
    }
}
