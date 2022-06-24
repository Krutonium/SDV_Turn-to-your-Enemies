using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using Microsoft.Xna.Framework.Input;
using Netcode;
using Object = StardewValley.Object;

namespace Turn_to_your_Enemies
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }
        
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (e.Button == SButton.MouseLeft &&                //Left Click
                Game1.player.CurrentTool != null &&             //Player has a tool equipped at all
                Context.IsPlayerFree &&                         //Player is not in any menus or other things
                Game1.player.canMove &&                         //Player can move
                Game1.player.hasMenuOpen == new NetBool(false)) //Player has no menu open               
            { 
                if (Game1.player.CurrentTool.ToString() == "StardewValley.Tools.MeleeWeapon")
                {
                    // Directions:
                    // North = 0
                    // East =  1
                    // South = 2
                    // West =  3

                    float MouseX = e.Cursor.AbsolutePixels.X - Game1.player.Position.X;
                    float MouseY = e.Cursor.AbsolutePixels.Y - Game1.player.Position.Y;
                    
                    //Check if the mouse is closer to any given side
                    if (Math.Abs(MouseX) > Math.Abs(MouseY))
                    {
                        //Mouse is closer to the East or West side
                        if (MouseX > 0)
                        {
                            //Mouse is closer to the East side
                            Game1.player.faceDirection(1);
                        }
                        else
                        {
                            //Mouse is closer to the West side
                            Game1.player.faceDirection(3);
                        }
                    }
                    else
                    {
                        //Mouse is closer to the North or South side
                        if (MouseY > 0)
                        {
                            //Mouse is closer to the South side
                            Game1.player.faceDirection(2);
                        }
                        else
                        {
                            //Mouse is closer to the North side
                            Game1.player.faceDirection(0);
                        }
                    }
                }
            }
        }
    }
}