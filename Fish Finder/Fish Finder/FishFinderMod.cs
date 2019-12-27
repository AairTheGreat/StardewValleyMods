using FishFinder.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;
using System;

namespace FishFinder
{
    public class FishFinderMod : Mod
    {
        public static FishFinderMod Instance { get; private set; }
        internal ModConfig config;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            helper.Events.Display.RenderedHud += Display_RenderedHud;

            try
            {
                config = this.Helper.Data.ReadJsonFile<ModConfig>("config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");
                config = ModConfigDefaultConfig.UpdateConfigToLatest(config, "config.json");
            }
            catch  //Really the only time this is going to error is when going from old version to new version of the config file or there is a bad config file
            {
                config = ModConfigDefaultConfig.UpdateConfigToLatest(config, "config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");
            }
        }

        private void Display_RenderedHud(object sender, RenderedHudEventArgs e)
        {
            if (!config.showHudData || Game1.eventUp || !(Game1.player.CurrentTool is FishingRod))
                return;

            Color textColor = Color.White;
            SpriteFont font = Game1.smallFont;

            // Draw the panning info GUI to the screen
            float boxWidth = 0;
            float lineHeight = font.LineSpacing;
            Vector2 boxTopLeft = new Vector2(config.hudXPostion, config.hudYPostion);
            Vector2 boxBottomLeft = boxTopLeft;

            // Setup the sprite batch
            SpriteBatch batch = Game1.spriteBatch;
            batch.End();
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            Point splashPoint = Game1.player.currentLocation.fishSplashPoint.Value;
            string hudTextLine1 = "";
            string hudTextLine2 = "";
            string hudTextLine3 = "";

            if (!splashPoint.Equals(Point.Zero))
            {
                hudTextLine1 = "Found a fishing spot!";
                string fishRelativePostion = GetFishingSpotRelativePostion(splashPoint);
                long distance = GetDistanceToFishingSpot(splashPoint);
                if (config.showDistance)
                {
                    hudTextLine2 = $"In the {fishRelativePostion} direction.";
                    hudTextLine3 = $"Roughly { distance} tiles away.";
                }
            }
            else
            {
                hudTextLine1 = "No fishing spot found!";
            }

            batch.DrawStringWithShadow(font, hudTextLine1, boxBottomLeft, textColor, 1.0f);
            boxWidth = Math.Max(boxWidth, font.MeasureString(hudTextLine1).X + 5);
            boxBottomLeft += new Vector2(0, lineHeight);

            batch.DrawStringWithShadow(font, hudTextLine2, boxBottomLeft, textColor, 1.0f);
            boxWidth = Math.Max(boxWidth, font.MeasureString(hudTextLine2).X + 5);
            boxBottomLeft += new Vector2(0, lineHeight);

            batch.DrawStringWithShadow(font, hudTextLine3, boxBottomLeft, textColor, 1.0f);
            boxWidth = Math.Max(boxWidth, font.MeasureString(hudTextLine3).X + 5);
            boxBottomLeft += new Vector2(0, lineHeight);

            Texture2D box = Game1.staminaRect;
            // Draw the background rectangle DrawHelpers.WhitePixel
            batch.Draw(box, new Rectangle((int)boxTopLeft.X, (int)boxTopLeft.Y, (int)boxWidth, (int)(boxBottomLeft.Y - boxTopLeft.Y)), null,
                new Color(0, 0, 0, 0.25F), 0f, Vector2.Zero, SpriteEffects.None, 0.85F);

            batch.End();
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
        }

        private string GetFishingSpotRelativePostion(Point fishingSpot)
        {
            if (fishingSpot.X == Game1.player.getTileX())
            {
                return (fishingSpot.Y < Game1.player.getTileY()) ? "N" : "S";
            }

            if (fishingSpot.Y == Game1.player.getTileY())
            {
                return (fishingSpot.X < Game1.player.getTileX()) ? "W" : "E";
            }

            if (fishingSpot.X < Game1.player.getTileX())
            {
                return (fishingSpot.Y < Game1.player.getTileY()) ? "NW" : "SW";
            }
            else
            {
                return (fishingSpot.Y < Game1.player.getTileY()) ? "NE" : "SE";
            }
        }

        private long GetDistanceToFishingSpot(Point fishingSpot)
        {
            return (long)Math.Round(Math.Sqrt(Math.Pow(fishingSpot.X - Game1.player.getTileX(), 2) + Math.Pow(fishingSpot.Y - Game1.player.getTileY(), 2)));
        }
    }

    public static class DrawingExtensions
    {
        public static void DrawStringWithShadow(this SpriteBatch batch, SpriteFont font, string text, Vector2 position, Color color, float depth = 0F, float shadowDepth = 0.005F)
        {
            batch.DrawStringWithShadow(font, text, position, color, Color.Black, Vector2.One, depth, shadowDepth);
        }

        /// <summary>Draws a string with a shadow behind it.</summary>
        /// <param name="batch">The batch to draw with.</param>
        /// <param name="font">The font the text should use.</param>
        /// <param name="text">The string to draw.</param>
        /// <param name="position">The position of the string.</param>
        /// <param name="color">The color of the string. The shadow is black.</param>
        /// <param name="shadowColor">The color of the shadow.</param>
        /// <param name="scale">The amount to scale the size of the string by.</param>
        /// <param name="depth">The depth of the string.</param>
        /// <param name="shadowDepth">The depth of the shadow.</param>
        public static void DrawStringWithShadow(this SpriteBatch batch, SpriteFont font, string text, Vector2 position, Color color, Color shadowColor, Vector2 scale, float depth, float shadowDepth)
        {
            batch.DrawString(font, text, position + Vector2.One * Game1.pixelZoom * scale / 2f, shadowColor, 0F, Vector2.Zero, scale, SpriteEffects.None, shadowDepth);
            batch.DrawString(font, text, position, color, 0F, Vector2.Zero, scale, SpriteEffects.None, depth);
        }
    }
}
