using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using TimeFreezesAtMidnight.Helpers;
using TimeFreezesAtMidnight.Interfaces;
using TimeFreezesAtMidnight.Settings;

namespace TimeFreezesAtMidnight;
internal sealed class ModEntry : Mod
{
    private ModConfig config = new();

    public override void Entry(IModHelper helper)
    {
        config = Helper.ReadConfig<ModConfig>();
        helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
        helper.Events.GameLoop.TimeChanged += GameLoop_TimeChanged;
        helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
    }

    private void GameLoop_UpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (!Context.IsWorldReady || !Context.IsMainPlayer || !config.Enabled || config.UseOldMethod)
            return;

        if (Game1.timeOfDay > config.TimeFreezesAt)
            Game1.timeOfDay = config.TimeFreezesAt;

        if (Game1.timeOfDay == config.TimeFreezesAt)
            Game1.gameTimeInterval = 0;
    }

    private void GameLoop_TimeChanged(object? sender, TimeChangedEventArgs e)
    {
        if (!Context.IsWorldReady || !Context.IsMainPlayer || !config.Enabled || config.UseOldMethod)
            return;

        if (e.NewTime > config.TimeFreezesAt)
            Game1.timeOfDay = config.TimeFreezesAt;
    }

    private void GameLoop_GameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        RegisterControlsGenericModConfigMenu(config);
    }

    private void RegisterControlsGenericModConfigMenu(ModConfig config)
    {
        var api = Helper.ModRegistry.GetApi<IGenericModConfigMenuAPI>("spacechase0.GenericModConfigMenu");
        if (api is null)
        {
            Monitor.Log("Generic Mod Config Menu not installed. Skipping integration.", LogLevel.Info);
            return;
        }

        api.Register(ModManifest,
            reset: () => config = new ModConfig(),
            save: () => Helper.WriteConfig(config)
        );
        api.AddSectionTitle(ModManifest, text: () => "Options");
        api.AddBoolOption(ModManifest,
            name: () => "Enabled",
            tooltip: () => "Enables or disables the mod",
            getValue: () => config.Enabled,
            setValue: val => config.Enabled = val);
        api.AddTextOption(ModManifest,
            name: () => "Time freezes at",
            tooltip: () => "Set at what point the game should prevent time from advancing",
            getValue: () => TimeHelper.GameTimeToHumanReadableTime(config.TimeFreezesAt),
            setValue: val => config.TimeFreezesAt = TimeHelper.HumanReadableTimeToGameTime(val));
        api.AddBoolOption(ModManifest,
            name: () => "Use legacy method",
            tooltip: () => "Not recommended",
            getValue: () => config.UseOldMethod,
            setValue: val => config.UseOldMethod = val);
        api.AddParagraph(ModManifest,
            text: () => "PS: The legacy method, rather than freezing time, kept checking whenever " +
            "the clock went over the set time, reverting it back each time. This caused " +
            "problems if you picked something close to 2AM, ticking JUST over it and " +
            "causing the player to collapse into the next day.");
    }
}
