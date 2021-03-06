﻿using System;
using System.Collections.Generic;

namespace CharacterManager.Model.RedundantTree
{
    public interface IRTreeMember<T>
    {
        List<Guid> Child_Guids { get; set; }
        Guid Gid { get; set; }
        bool IsHead { get; set; }
        T Item { get; set; }
        List<Guid> Parents { get; set; }
        public List<IRTreeMember<T>> Child_Items { get; }


        void AddChild(Guid i_gid);
        void AddParent(Guid i_gid);
        void RemoveChild(Guid i_gid);
        void RemoveParent(Guid i_gid);
    }
}