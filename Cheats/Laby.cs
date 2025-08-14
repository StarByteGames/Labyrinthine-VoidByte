using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Il2Cpp;
using Il2CppCharacterCustomization;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppValkoGames.Labyrinthine.Monsters;
using MelonLoader;
using UnityEngine;

namespace VoidByte.Cheats
{
    public class Laby : MelonMod
    {
        public static bool PlayerInCase = false;

        public static bool ESPEnabled = false;

        public static bool isAIEnabled = true;

        public static float FlashlightMultiplier = 1f;

        public static float DefaultFlashlightIntensity = 47.7f;

        public static float DefaultPointFlashlightIntensity = 30f;

        public static float JumpHeight = 1.8f;

        public static float MovementSpeed = 4.4f;

        public static int? CurrentSceneIndex;

        public static string? CurrentSceneName;

        public static List<Vector3> Safezones = new List<Vector3>();

        private bool showMenu = false;

        private Menu menu = new Menu();

        public static Camera? GameCamera { get; set; }

        public static GameManager? GameManager { get; set; }

        public static PlayerControl? PlayerControl { get; set; }

        public static AIController[]? AIControllers { get; set; }

        public override void OnInitializeMelon()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                MelonLogger.Error($"Unhandled Exception: {e.ExceptionObject}");
            };
            TaskScheduler.UnobservedTaskException += delegate(object? sender, UnobservedTaskExceptionEventArgs e)
            {
                MelonLogger.Error($"Unobserved Task Exception: {e.Exception}");
                e.SetObserved();
            };
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            ((MelonBase)this).LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded!");
            CurrentSceneIndex = buildIndex;
            CurrentSceneName = sceneName;
            if (buildIndex >= 4 && buildIndex != 8)
            {
                MelonCoroutines.Start(CollectCaseGameObjectsAndData());
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            MovementSpeed = 4.4f;
            JumpHeight = 1.8f;
            if (PlayerInCase)
            {
                Hacks.SetMovementSpeed(MovementSpeed);
                Hacks.SetJumpHeight(JumpHeight);
            }
            ESPEnabled = false;
            PlayerInCase = false;
            isAIEnabled = true;
            FlashlightMultiplier = 1f;
            AIControllers = null;
            GameManager = null;
            PlayerControl = null;
            GameCamera = null;
            Safezones.Clear();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown((KeyCode)277))
            {
                showMenu = !showMenu;
            }
        }

        public override void OnGUI()
        {
            if (CurrentSceneIndex.HasValue && !(CurrentSceneIndex < 2))
            {
                if (showMenu)
                {
                    menu.StartMenu();
                }
                if (ESPEnabled)
                {
                    ESP.Render();
                }
            }
        }

        private IEnumerator CollectCaseGameObjectsAndData()
        {
            while ((UnityEngine.Object)(object)GameManager == (UnityEngine.Object)null)
            {
                GameManager = UnityEngine.Object.FindObjectOfType<GameManager>();
                yield return (object)new WaitForSeconds(0.5f);
            }
            while ((UnityEngine.Object)(object)PlayerControl == (UnityEngine.Object)null)
            {
                PlayerControl = UnityEngine.Object.FindObjectOfType<PlayerControl>();
                yield return (object)new WaitForSeconds(0.5f);
            }
            MelonLogger.Msg("All required game objects collected!");
            PlayerInCase = true;
            Safezones.AddRange(new List<Vector3>(Hacks.GetAllSafezones().ToArray()));
            MelonLogger.Msg($"Found {Safezones?.Count} safe zones!");
            GetInfoCosmeticInCase();
            while (AIControllers == null || AIControllers.Length == 0)
            {
                AIControllers = UnityEngine.Object.FindObjectsOfType<AIController>().ToArray();
                yield return (object)new WaitForSeconds(0.5f);
            }
            MelonLogger.Msg("All optional game objects collected!");
        }

        private void GetInfoCosmeticInCase()
        {
            CustomizationPickup pickupCosmeticInCase = Hacks.GetPickupCosmeticInCase();
            if ((UnityEngine.Object)(object)pickupCosmeticInCase != (UnityEngine.Object)null)
            {
                string value = ((UnityEngine.Object)pickupCosmeticInCase).name.Replace("CPickup - ", "").Replace("Customization Pickup - ", "").Replace("(Clone)", "")
                    .Replace("_", " ")
                    .Trim();
                MelonLogger.Msg($"Cosmetic in this case if any: {value} with itemID {pickupCosmeticInCase.itemID}");
            }
        }
    }
}
