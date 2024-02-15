using UnityEngine;
using Verse;

namespace abliger
{
    [StaticConstructorOnStartup]
    public class WeatherOverlay_BloodRain : SkyOverlay
    {
        public WeatherOverlay_BloodRain()
        {
            LongEventHandler.ExecuteWhenFinished(() =>
            {
                this.worldOverlayMat = MaterialPool.MatFrom(ContentFinder<Texture2D>.Get("Weather/BloodOverlayRain", true), ShaderDatabase.Transparent, Color.white);
                this.worldOverlayPanSpeed1 = 0.015f;
                this.worldPanDir1 = new Vector2(-0.25f, -1f);
                this.worldPanDir1.Normalize();
                this.worldOverlayPanSpeed2 = 0.022f;
                this.worldPanDir2 = new Vector2(-0.24f, -1f);
                this.worldPanDir2.Normalize();
            });
        }
    }
}
    