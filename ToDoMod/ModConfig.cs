using StardewModdingAPI.Utilities;

namespace ToDoMod;

class ModConfig
{
    public KeybindList ToggleListKey { get; set; } = KeybindList.Parse("F2");

    public bool UseLargerFont { get; set; } = false;

    public bool OpenAtStartup { get; set; } = false;
}