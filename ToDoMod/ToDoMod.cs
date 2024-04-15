using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ToDoMod;

/// <summary>The mod entry point.</summary>
public class ModEntry : Mod
{
    /********
     ** Properties
     ********/
    /// <summary>
    /// The mod settings.
    /// </summary>
    private ModConfig _config = null!;

    /// <summary>
    /// The saved task data.
    /// </summary>
    private ModData? _data;

    /// <summary>
    /// The mod entry point, called after the mod is first loaded.
    /// </summary>
    public override void Entry(IModHelper helper)
    {
        _config = helper.ReadConfig<ModConfig>() ?? new ModConfig();
        Helper.Events.Input.ButtonsChanged += ControlEvents_ButtonsChanged;
        Helper.Events.GameLoop.SaveLoaded += SaveEvents_AfterLoad;
    }

    /// <summary>
    /// Update the mod's config.json file from the current <see cref="_config"/>.
    /// </summary>
    private void SaveConfig()
    {
        Helper.WriteConfig(_config);
    }

    /// <summary>
    /// Update the save file's saved task list.
    /// </summary>
    private void SaveData()
    {
        Helper.Data.WriteJsonFile($"data/{Constants.SaveFolderName}.json", _data);
    }

    private void ControlEvents_ButtonsChanged(object? _, ButtonsChangedEventArgs eventArgs)
    {
        if (!Context.IsWorldReady) return;
        if (!Context.IsPlayerFree) return;

        ProcessToggleKeybind();
    }

    private void ProcessToggleKeybind()
    {
        if (!_config.ToggleListKey.JustPressed()) return;
        OpenList();
        Helper.Input.SuppressActiveKeybinds(_config.ToggleListKey);
    }

    /// <summary>The method called after the player loads their save.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    public void SaveEvents_AfterLoad(object? sender, EventArgs e)
    {
        /* If the user wants the list to load at game start, do that */
        if (_config.OpenAtStartup)
        {
            OpenList();
        }
    }

    /// <summary>
    /// Open the to do list.
    /// </summary>
    private void OpenList()
    {
        /* Read in the specific saved task list for the opened save file */
        /* Or create one if it doesn't exist yet. */
        _data = Helper.Data.ReadJsonFile<ModData>($"data/{Constants.SaveFolderName}.json") ?? new ModData();

        if (Game1.activeClickableMenu != null) Game1.exitActiveMenu();

        /* Open a to do list. */
        Game1.activeClickableMenu = new ToDoList(0, _config, SaveConfig, _data, SaveData);
    }

}