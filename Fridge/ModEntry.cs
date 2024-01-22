using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;

namespace Fridge
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }


        private ICursorPosition cursorPos;
        private Point fridgePos;
        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e )
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window

            if (Game1.player.ActiveObject != null )
            {
                if ((Game1.player.ActiveObject.DisplayName == "Wood Sign" || Game1.player.ActiveObject.DisplayName == "Stone Sign") && e.Button.IsActionButton())
                {
                    // https://stardewvalleywiki.com/Modding:Modder_Guide/APIs/Input#ICursorPosition
                    cursorPos = this.Helper.Input.GetCursorPosition();


                    this.Monitor.Log($"MR pressed at {cursorPos.GrabTile.X} while holding sign");
                    this.Monitor.Log($"{this.Helper.Input.GetCursorPosition()}");
                    
                    this.Monitor.Log($"{Game1.currentLocation.NameOrUniqueName}");

                    // finding location of fridge, and check if it matches the grab position
                    if (Game1.currentLocation is FarmHouse house)
                    {
                        fridgePos = house.fridgePosition;
                        
                        this.Monitor.Log($"{fridgePos}", LogLevel.Debug);
                        if (fridgePos.X == cursorPos.GrabTile.X && fridgePos.Y == cursorPos.GrabTile.Y)
                        {
                            this.Helper.Input.Suppress(SButton.MouseRight);
                            this.Monitor.Log("Action Button suppressed at fridge");
                        }

                        // TODO: check for mini fridge
                        // BigCraftable | Mini-Fridge | 216

                    }

                }
                else
                {
                    this.Monitor.Log($"{Game1.player.Name} pressed {e.Button} while holding {Game1.player.ActiveObject.DisplayName}.", LogLevel.Debug);

                }
            }

        }
    }
}