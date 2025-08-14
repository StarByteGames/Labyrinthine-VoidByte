using System;
using System.Collections.Generic;
using System.Linq;
using Il2Cpp;
using Il2CppBehaviorDesigner.Runtime;
using Il2CppBehaviorDesigner.Runtime.Tasks;
using Il2CppBehaviorDesigner.Runtime.Tasks.Unity.Math;
using Il2CppCharacterCustomization;
using Il2CppObjectives;
using Il2CppRandomGeneration.Contracts;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using Il2CppValkoGames.Labyrinthine.Monsters;
using Il2CppValkoGames.Labyrinthine.Saves;
using Il2CppValkoGames.Labyrinthine.Store;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace VoidByte.Cheats
{
    public static class Hacks
    {
        public static void UnlockAllCosmetics()
        {
            CustomizationSave val = CustomizationSave.Load();
            for (ushort num = 0; num < 750; num++)
            {
                val.UnlockItem(num, false);
            }
            MelonLogger.Msg("All cosmetics unlocked.");
        }

        public static void UnlockAllMonsterTypes()
        {
            foreach (MonsterType value in System.Enum.GetValues(typeof(MonsterType)))
            {
                EquipmentSave.UnlockMonsterType(value);
                EquipmentSave.Save();
            }
            MelonLogger.Msg("All monster types unlocked.");
        }

        public static void UnlockAllMazeTypes()
        {
            foreach (MazeType value in System.Enum.GetValues(typeof(MazeType)))
            {
                EquipmentSave.UnlockMazeType(value);
                EquipmentSave.Save();
            }
            MelonLogger.Msg("All maze types unlocked.");
        }

        public static void CompleteAllObjectivesInCase()
        {
            if (Laby.CurrentSceneName.Contains("Zone"))
            {
                return;
            }
            ObjectiveManager val = UnityEngine.Object.FindObjectOfType<ObjectiveManager>();
            if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
            {
                try
                {
                    CustomizationPickup pickupCosmeticInCase = GetPickupCosmeticInCase();
                    if (pickupCosmeticInCase != null)
                    {
                        pickupCosmeticInCase.Pickup();
                    }
                    var enumerator = val.Objectives.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Il2CppSystem.Collections.Generic.KeyValuePair<string, ObjectiveUI> current = enumerator.Current;
                        val.SetObjectiveProgressL(current.Key, 100);
                        val.SetObjectiveProgressSync(current.Key, 100);
                    }
                    return;
                }
                catch (System.Exception value)
                {
                    MelonLogger.Error($"Failed to set progress for an objective: {value}");
                    return;
                }
            }
            MelonLogger.Msg("Found No Objectives");
        }

        public static CustomizationPickup GetPickupCosmeticInCase()
        {
            return UnityEngine.Object.FindObjectOfType<CustomizationPickup>();
        }

        public static void SetAllItemsCount()
        {
            for (short num = 0; num < 51; num++)
            {
                EquipmentSave.SetItemCount(num, 1000);
                EquipmentSave.Save();
            }
            MelonLogger.Msg("Successfully set all items to x1000.");
        }

        public static void SelfRevive()
        {
            Laby.PlayerControl.playerNetworkSync.Death.CmdReviveSelf();
        }

        public static void ToggleMonsters()
        {
            Laby.isAIEnabled = !Laby.isAIEnabled;
            AIController[] aIControllers = Laby.AIControllers;
            foreach (AIController val in aIControllers)
            {
                Collider component = ((Component)val).GetComponent<Collider>();
                component.isTrigger = Laby.isAIEnabled;
                component.enabled = Laby.isAIEnabled;
                BehaviorTree component2 = ((Component)val).GetComponent<BehaviorTree>();
                Il2CppSystem.Collections.Generic.List<Conditional> val2 = ((Behavior)component2).FindTasks<Conditional>();
                var enumerator = val2.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Conditional current = enumerator.Current;
                    if (!(current.GetType() == typeof(FloatComparison)))
                    {
                        ((Il2CppBehaviorDesigner.Runtime.Tasks.Task)current).disabled = !Laby.isAIEnabled;
                    }
                }
                if (Laby.isAIEnabled)
                {
                    ((Behavior)component2).EnableBehavior();
                }
                else
                {
                    ((Behavior)component2).DisableBehavior();
                }
            }
        }

        public static void AddOrRemoveCurrency(int value)
        {
            MelonLogger.Msg($"{value} Currency has been {((value >= 0) ? "added" : "removed")}!");
            CurrencyManager.AddCurrency(value, true);
        }

        public static void AddOrRemoveExperience(int value)
        {
            MelonLogger.Msg($"{value} Experience has been {((value >= 0) ? "added" : "removed")}!");
            PlayerSave.AddExperience(value, true);
        }

        public static void SetRareStamps(int value)
        {
            MelonLogger.Msg($"Set Rare tokens to {value}!");
            EquipmentSave.custom_RareTokens = value;
            EquipmentSave.Save();
        }

        public static void SetHardcoreStamps(int value)
        {
            MelonLogger.Msg($"Set Hardcore tokens to {value}!");
            EquipmentSave.custom_HardcoreTokens = value;
            EquipmentSave.Save();
        }

        public static void TeleportToSpawn()
        {
            MelonLogger.Msg("Moved the player to Spawnpoint");
            Laby.PlayerControl.playerNetworkSync.MoveToSpawnpoint(true);
        }

        public static Il2CppSystem.Collections.Generic.List<Vector3> GetAllSafezones()
        {
            Il2CppSystem.Collections.Generic.List<Vector3> list = new Il2CppSystem.Collections.Generic.List<Vector3>();
            foreach (GameObject item in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (!(UnityEngine.Object)item.name.ToLower().Contains("lightzone"))
                {
                    continue;
                }
                Collider component = item.GetComponent<Collider>();
                if ((UnityEngine.Object)(object)component != (UnityEngine.Object)null)
                {
                    Bounds bounds = component.bounds;
                    Vector3 center = bounds.center;
                    Vector3 position = Laby.PlayerControl.playerNetworkSync.SpawnpointsManager.GetSpawnpoint(0).position;
                    bounds = component.bounds;
                    if (!bounds.Contains(position))
                    {
                        list.Add(new Vector3(center.x, center.y, center.z));
                    }
                }
            }
            return list;
        }

        public static void ToggleESP()
        {
            Laby.ESPEnabled = !Laby.ESPEnabled;
            MelonLogger.Msg("ESP toggled: " + (Laby.ESPEnabled ? "enabled" : "disabled"));
        }

        public static void SetFlashlightPower(float multiplier)
        {
            PlayerControl playerControl = Laby.PlayerControl;
            object obj;
            if (playerControl == null)
            {
                obj = null;
            }
            else
            {
                PlayerNetworkSync playerNetworkSync = playerControl.playerNetworkSync;
                if (playerNetworkSync == null)
                {
                    obj = null;
                }
                else
                {
                    Flashlight flashlight = playerNetworkSync.flashlight;
                    obj = ((flashlight != null) ? flashlight.flashlightLight : null);
                }
            }
            if (!((UnityEngine.Object)obj == (UnityEngine.Object)null))
            {
                Laby.PlayerControl.playerNetworkSync.flashlight.flashlightLight.intensity = Laby.DefaultFlashlightIntensity * multiplier;
                HDAdditionalLightData val = (from l in (System.Collections.Generic.IEnumerable<Il2CppSystem.ValueTuple<HDAdditionalLightData, float>>)Laby.PlayerControl.playerNetworkSync.AllLights
                    where ((UnityEngine.Object)l.Item1).name == "Point Light"
                    select l.Item1).FirstOrDefault();
                if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
                {
                    val.intensity = Laby.DefaultPointFlashlightIntensity * multiplier;
                }
            }
        }

        public static void SetMovementSpeed(float speed)
        {
            Laby.PlayerControl.CurrentPlayerControlPreset.MovementSpeed = speed;
        }

        public static void SetJumpHeight(float height)
        {
            Laby.PlayerControl.CurrentPlayerControlPreset.JumpHeight = height;
        }
    }
}
