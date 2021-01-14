using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace TimeFreezesAtMidnight
{
    public class ModEntry : Mod
    {
        private ModConfig config;

        public override void Entry(IModHelper helper)
        {
            config = Helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            helper.Events.GameLoop.TimeChanged += GameLoop_TimeChanged;
            helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        }

        private void GameLoop_UpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady || !Context.IsMainPlayer || !config.Enabled || config.UseOldMethod)
                return;

            if (Game1.timeOfDay > config.TimeFreezesAt)
                Game1.timeOfDay = config.TimeFreezesAt;

            if (Game1.timeOfDay == config.TimeFreezesAt)
                Game1.gameTimeInterval = 0;
        }

        private void GameLoop_TimeChanged(object sender, TimeChangedEventArgs e)
        {
            if (!Context.IsWorldReady || !Context.IsMainPlayer || !config.Enabled || !config.UseOldMethod)
                return;

            if (e.NewTime > config.TimeFreezesAt)
                Game1.timeOfDay = config.TimeFreezesAt;
        }

        private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            RegisterControlsGenericModConfigMenu(config);
        }

        private void RegisterControlsGenericModConfigMenu(ModConfig config)
        {
            var api = Helper.ModRegistry.GetApi<IGenericModConfigMenuAPI>("spacechase0.GenericModConfigMenu");
            if (api is null)
            {
                Monitor.Log("Generic Mod Config Menu not installed. No integration needed", LogLevel.Info);
                return;
            }

            api.RegisterModConfig(ModManifest, () => config = new ModConfig(), () => Helper.WriteConfig(config));
            api.RegisterLabel(ModManifest, "Options", "");
            api.RegisterSimpleOption(ModManifest, "Enabled", "Enables or disables the mod",
                () => config.Enabled,
                val => config.Enabled = val);
            api.RegisterSimpleOption(ModManifest, "Time freezes at", "Indicates at what point should the game prevent time from advancing",
                () => TimeHelper.GameTimeToHumanReadableTime(config.TimeFreezesAt),
                val => config.TimeFreezesAt = TimeHelper.HumanReadableTimeToGameTime(val));
            api.RegisterSimpleOption(ModManifest, "Use old method", "Doesn't actually freeze time but reverts it back whenever time goes over the value above",
                () => config.UseOldMethod,
                val => config.UseOldMethod = val);

            //api.RegisterLabel(ModManifest, "", "");
            //api.RegisterLabel(ModManifest, "", "");
            //api.RegisterLabel(ModManifest, "PS: The game stores time as an integer between 0600", "");
            //api.RegisterLabel(ModManifest, "and 2600, where numbers 1300 to 2400 correspond to", "");
            //api.RegisterLabel(ModManifest, "1PM to 12AM, 1AM is treated as 2500, and 2AM, 2600.", "");
        }
    }
}
