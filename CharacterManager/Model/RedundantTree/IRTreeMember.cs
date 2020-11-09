using System;
using System.Collections.Generic;

namespace Character_Manager.Model.RedundantTree
{
    public interface IRTreeMember<T>
    {
        List<Guid> Children { get; set; }
        Guid Gid { get; set; }
        bool IsHead { get; set; }
        T Item { get; set; }
        List<Guid> Parents { get; set; }

        void AddChild(Guid i_gid);
        void AddParent(Guid i_gid);
        void RemoveChild(Guid i_gid);
        void RemoveParent(Guid i_gid);
    }
}