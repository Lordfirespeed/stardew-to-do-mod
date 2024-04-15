using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI.Utilities;

namespace ToDoMod
{
    class ModConfig
    {
        public KeybindList ToggleListKey { get; set; } = KeybindList.Parse("F2");

        public bool UseLargerFont { get; set; } = false;

        public bool OpenAtStartup { get; set; } = false;
    }
}
