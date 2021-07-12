using CharacterManager.Model.Factories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterManager.Model.RedundantTree
{

    public class RTree<T> : IRTree<T>
    {
        private IRTreeFactory<T> factory;
        public IRTreeFactory<T> Factory
        {
            get
            {
                return factory;
            }
            set
            {
                factory = value;
            }
        }

        private IDictionary<Guid, IRTreeMember<T>> dict;
        public IDictionary<Guid, IRTreeMember<T>> Dict
        {
            get
            {
                return dict;
            }
            set
            {
                dict = value;
            }
        }

        [JsonIgnore]
        public List<IRTreeMember<T>> Heads
        {
            get
            {
                return dict.Values.Where( x => x.IsHead == true ).ToList();
            }
        }

        public RTree()
        {

        }

        public RTree(IRTreeFactory<T> i_factory)
        {
            factory = i_factory;
            dict = factory.CreateGuidDictionary();
        }

        [JsonIgnore]
        public int Count
        {
            get
            {
                return dict.Values.Count();
            }
        }

        public IRTreeMember<T> AddItem(T i_item, bool is_head = false)
        {
            IRTreeMember<T> member = factory.CreateRTreeMember( this );
            member.Item = i_item;
            member.IsHead = is_head;
            dict.Add( member.Gid, member );
            return member;
        }
        public void RemoveItem(IRTreeMember<T> i_item)
        {
            List<Guid> P = new( i_item.Parents );
            List<Guid> C = new( i_item.Child_Guids );

            foreach (Guid G in P)
            {
                RemoveChild( dict[G], i_item );
            }
            foreach (Guid G in C)
            {
                RemoveChild( i_item, dict[G] );
            }
            dict.Remove( i_item.Gid );
        }
        public void AddChild(IRTreeMember<T> Parent, IRTreeMember<T> Child)
        {
            if (Get_All_Parents( Parent ).Contains( Child ) == false)
            {
                if (Parent.Gid != Child.Gid)
                {
                    Parent.AddChild( Child.Gid );
                    Child.AddParent( Parent.Gid );
                }
            }
            else
            {
                //do nothing, adding the child would create a recursive loop
            }
        }
        public void RemoveChild(IRTreeMember<T> Parent, IRTreeMember<T> Child)
        {
            Parent.RemoveChild( Child.Gid );
            Child.RemoveParent( Parent.Gid );
        }

        public IRTreeMember<T> Get_Item(Guid i_gid)
        {
            if (dict.ContainsKey( i_gid ))
            {
                return dict[i_gid];
            }
            else if (i_gid == Guid.Empty)
            {
                return factory.CreateRTreeMember( this );
            }
            else
            {
                throw new Exception( "i_gid passed not valid" );
            }
        }
        public List<IRTreeMember<T>> Get_Immidiate_Parents(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Parents = new();
            foreach (Guid G in i_item.Parents)
            {
                Parents.Add( dict[G] );
            }
            return Parents;
        }
        public List<IRTreeMember<T>> Get_Immidiate_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = new();
            foreach (Guid G in i_item.Child_Guids)
            {
                Children.Add( dict[G] );
            }
            return Children;
        }
        public List<IRTreeMember<T>> Get_All_Parents(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Parents = Get_Immidiate_Parents( i_item );
            List<IRTreeMember<T>> Result = new();
            foreach (IRTreeMember<T> i in Parents)
            {
                Result.Add( i );
                foreach (IRTreeMember<T> j in Get_All_Parents( i ))
                {
                    if (Result.Contains( j ) == false)
                    {
                        Result.Add( j );
                    }
                }
            }
            return Result;
        }
        public List<IRTreeMember<T>> Get_All_Children(IRTreeMember<T> i_item)
        {
            List<IRTreeMember<T>> Children = Get_Immidiate_Children( i_item );
            List<IRTreeMember<T>> Result = new();
            foreach (IRTreeMember<T> i in Children)
            {
                Result.Add( i );
                foreach (IRTreeMember<T> j in Get_All_Children( i ))
                {
                    if (Result.Contains( j ) == false)
                    {
                        Result.Add( j );
                    }
                }
            }
            return Result;
        }

        public List<T> Get_All_Items()
        {
            List<T> temp = new();
            foreach (IRTreeMember<T> item in dict.Values)
            {
                temp.Add( item.Item );
            }
            return temp;
        }
        public IRTreeMember<T> GetMemberFromItem(T item)
        {
            foreach (IRTreeMember<T> target in Dict.Values)
            {
                if ((object)target.Item == (object)item)
                {
                    return target;
                }
            }
            return new InvalidIRTreeMember<T>();
        }
    }
}
