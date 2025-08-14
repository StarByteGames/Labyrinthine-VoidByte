using System.Linq;
using Il2CppValkoGames.Labyrinthine.Store;

namespace VoidByte
{
    public static class Hacks
    {
        // Flashlights
        public static float FlashlightMultiplier = 1f;
        public static float DefaultFlashlightIntensity = 47.7f;
        public static float DefaultPointFlashlightIntensity = 30f;

        public static void Listen()
        {
            SetFlashlightPower();
        }

        public static void UlimitedCurrency()
        {
            CurrencyManager.AddCurrency(99999, false);
        }

        public static void SetFlashlightPower()
        {
            if(Core.PlayerControl?.playerNetworkSync?.flashlight?.flashlightLight == null)
            {
                return;
            }

            Core.PlayerControl.playerNetworkSync.flashlight.flashlightLight.intensity = DefaultFlashlightIntensity * FlashlightMultiplier;

            var spotLight = Core.PlayerControl.playerNetworkSync.AllLights
                .Where(l => l.Item1.name == "Point Light")
                .Select(l => l.Item1)
                .FirstOrDefault();

            if(spotLight)
            {
                spotLight.intensity = DefaultPointFlashlightIntensity * FlashlightMultiplier;
            }
        }
    }
}