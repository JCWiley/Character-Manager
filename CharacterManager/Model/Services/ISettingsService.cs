﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.Model.Services
{
    public interface ISettingsService
    {
        public string LastUsedPath { get; set; }
    }
}