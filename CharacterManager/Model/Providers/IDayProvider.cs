﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Providers
{
    public interface IDayProvider
    {
        int CurrentDay { get; }

    }
}