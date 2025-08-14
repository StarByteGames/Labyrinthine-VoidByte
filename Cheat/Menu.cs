using System;
using System.Text.RegularExpressions;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppRandomGeneration.Contracts;
using Il2CppSystem.Collections.Generic;
using Il2CppValkoGames.Labyrinthine.Monsters;
using Il2CppValkoGames.Labyrinthine.Saves;
using MelonLoader;
using UnityEngine;
using static UnityEngine.GUI;

namespace LabyrinthineCheat;

public class Menu
{
    private Rect windowRect = new Rect(50f, 50f, 300f, 400f);

    private Rect windowMonsterTeleportRect = new Rect(350f, 50f, 300f, 400f);

    private Rect windowPlayerTeleportRect = new Rect(650f, 50f, 300f, 400f);

    private string xCoords = "0";

    private string zCoords = "0";

    private string yCoords = "0";

    private string currencyInput = "100";

    private string experienceInput = "1000";

    private bool initializeStampValues = true;

    private string rareStampInput = "0";

    private string hardcoreStampInput = "0";

    private GUIStyle? titleStyle;

    private GUIStyle? buttonStyle;

    private GUIStyle? sliderStyle;

    public void StartMenu()
    {
        GUIStyle val = new GUIStyle(GUI.skin.button)
        {
            fontSize = 14,
            fontStyle = (FontStyle)1
        };
        val.normal.textColor = Color.white;
        buttonStyle = val;
        GUIStyle val2 = new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            fontStyle = (FontStyle)1
        };
        val2.normal.textColor = Color.cyan;
        titleStyle = val2;
        sliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
        windowRect = GUILayout.Window(0, windowRect, DelegateSupport.ConvertDelegate<WindowFunction>((Delegate)new Action<int>(DrawMenu)), "Labyrinthine Menu", (GUILayoutOption[])(object)new GUILayoutOption[2]
        {
            GUILayout.ExpandHeight(true),
            GUILayout.ExpandWidth(true)
        });
        if (Laby.PlayerInCase)
        {
            windowMonsterTeleportRect = GUILayout.Window(1, windowMonsterTeleportRect, DelegateSupport.ConvertDelegate<WindowFunction>((Delegate)new Action<int>(DrawMonsterTeleportMenu)), "Monster Teleport Menu", (GUILayoutOption[])(object)new GUILayoutOption[2]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            });
            windowPlayerTeleportRect = GUILayout.Window(2, windowPlayerTeleportRect, DelegateSupport.ConvertDelegate<WindowFunction>((Delegate)new Action<int>(DrawPlayerTeleportMenu)), "Player Teleport Menu", (GUILayoutOption[])(object)new GUILayoutOption[2]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            });
        }
    }

    private void DrawMenu(int windowID)
    {
        GUILayout.Label(" ~ Made by DevStarByte ~", titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
        GUILayout.Space(10f);
        if (Laby.CurrentSceneIndex == 2 || Laby.CurrentSceneIndex == 8)
        {
            if (GUILayout.Button("Unlock all cosmetics", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.UnlockAllCosmetics();
            }
            if (GUILayout.Button("Unlock all monster types", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.UnlockAllMonsterTypes();
            }
            if (GUILayout.Button("Unlock all maze types", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.UnlockAllMazeTypes();
            }
            if (GUILayout.Button("Have all items x1000", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.SetAllItemsCount();
            }
            GUILayout.BeginHorizontal((Il2CppReferenceArray<GUILayoutOption>)null);
            currencyInput = NumberInput(currencyInput);
            if (GUILayout.Button("Give Currency", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.AddOrRemoveCurrency(int.Parse(currencyInput));
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal((Il2CppReferenceArray<GUILayoutOption>)null);
            experienceInput = NumberInput(experienceInput);
            if (GUILayout.Button("Give Experience", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.AddOrRemoveExperience(int.Parse(experienceInput));
            }
            GUILayout.EndHorizontal();
            if (initializeStampValues)
            {
                hardcoreStampInput = EquipmentSave.GetContractTypeTokenCount((ContractType)2).ToString();
                rareStampInput = EquipmentSave.GetContractTypeTokenCount((ContractType)1).ToString();
                initializeStampValues = false;
            }
            GUILayout.BeginHorizontal((Il2CppReferenceArray<GUILayoutOption>)null);
            rareStampInput = NumberInput(rareStampInput);
            if (GUILayout.Button("Set Rare stamps", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.SetRareStamps(int.Parse(rareStampInput));
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal((Il2CppReferenceArray<GUILayoutOption>)null);
            hardcoreStampInput = NumberInput(hardcoreStampInput);
            if (GUILayout.Button("Set Hardcore stamps", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.SetHardcoreStamps(int.Parse(hardcoreStampInput));
            }
            GUILayout.EndHorizontal();
        }
        else if (Laby.PlayerInCase)
        {
            if (GUILayout.Button("Complete Case", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.CompleteAllObjectivesInCase();
            }
            if (GUILayout.Button("Self Revive", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.SelfRevive();
            }
            if (GUILayout.Button(Laby.isAIEnabled ? "Disable Monsters" : "Enable Monsters", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.ToggleMonsters();
            }
            if (GUILayout.Button("Toggle ESP", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Hacks.ToggleESP();
            }
            GUILayout.Space(5f);
            GUILayout.Label("Flashlight power: " + Laby.FlashlightMultiplier.ToString("F2"), titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
            float flashlightMultiplier = Laby.FlashlightMultiplier;
            Laby.FlashlightMultiplier = GUILayout.HorizontalSlider(Laby.FlashlightMultiplier, 1f, 500f, sliderStyle, GUI.skin.horizontalSliderThumb, Array.Empty<GUILayoutOption>());
            if (Mathf.Abs(flashlightMultiplier - Laby.FlashlightMultiplier) > Mathf.Epsilon)
            {
                Hacks.SetFlashlightPower(Laby.FlashlightMultiplier);
            }
            GUILayout.Label("Movement Speed: " + Laby.MovementSpeed.ToString("F2"), titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
            float movementSpeed = Laby.MovementSpeed;
            Laby.MovementSpeed = GUILayout.HorizontalSlider(Laby.MovementSpeed, 4.4f, 50f, sliderStyle, GUI.skin.horizontalSliderThumb, Array.Empty<GUILayoutOption>());
            if (Mathf.Abs(movementSpeed - Laby.MovementSpeed) > Mathf.Epsilon)
            {
                Hacks.SetMovementSpeed(Laby.MovementSpeed);
            }
            GUILayout.Label("Jump Height: " + Laby.JumpHeight.ToString("F2"), titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
            float jumpHeight = Laby.JumpHeight;
            Laby.JumpHeight = GUILayout.HorizontalSlider(Laby.JumpHeight, 1.8f, 50f, sliderStyle, GUI.skin.horizontalSliderThumb, Array.Empty<GUILayoutOption>());
            if (Mathf.Abs(jumpHeight - Laby.JumpHeight) > Mathf.Epsilon)
            {
                Hacks.SetJumpHeight(Laby.JumpHeight);
            }
        }
        else
        {
            GUILayout.Label("Loading...", (Il2CppReferenceArray<GUILayoutOption>)null);
        }
        GUI.DragWindow();
    }

    private void DrawMonsterTeleportMenu(int windowID)
    {
        GUILayout.Label("Monster list", titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
        AIController[] aIControllers = Laby.AIControllers;
        foreach (AIController val in aIControllers)
        {
            string text = ((object)val.MonsterType/*cast due to .constrained prefix*/).ToString().Replace("_", " ");
            if (GUILayout.Button(text, buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Transform transform = ((Component)val).transform;
                transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                Laby.PlayerControl.playerNetworkSync.MoveToTransform(((Component)val).transform);
            }
        }
        GUI.DragWindow();
    }

    private void DrawPlayerTeleportMenu(int windowID)
    {
        Vector3 position = ((Component)Laby.PlayerControl.playerNetworkSync).transform.position;
        GUILayout.Label($"Current Coords | X: {Mathf.RoundToInt(position.x)}, Y: {Mathf.RoundToInt(position.y)}, Z: {Mathf.RoundToInt(position.z)}", (Il2CppReferenceArray<GUILayoutOption>)null);
        GUILayout.BeginHorizontal((Il2CppReferenceArray<GUILayoutOption>)null);
        xCoords = NumberInput(xCoords);
        yCoords = NumberInput(yCoords);
        zCoords = NumberInput(zCoords);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Teleport To Coords", buttonStyle, Array.Empty<GUILayoutOption>()))
        {
            try
            {
                Laby.PlayerControl.playerNetworkSync.MoveToPosition(new Vector3((float)int.Parse(xCoords), (float)int.Parse(yCoords), (float)int.Parse(zCoords)), default(Quaternion));
            }
            catch
            {
                MelonLogger.Error("Couldn't teleport you to those coords");
            }
        }
        if (GUILayout.Button("Teleport to Spawnpoint", buttonStyle, Array.Empty<GUILayoutOption>()))
        {
            Hacks.TeleportToSpawn();
        }
        GUILayout.Space(10f);
        GUILayout.Label("Player list", titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
        var enumerator = Laby.GameManager.Players.GetEnumerator();
        while (enumerator.MoveNext())
        {
            PlayerNetworkSync current = enumerator.Current;
            if (GUILayout.Button(current.playerName, buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                Transform transform = ((Component)current).transform;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Laby.PlayerControl.playerNetworkSync.MoveToTransform(((Component)current).transform);
            }
        }
        GUILayout.Label("Safezone list", titleStyle, (Il2CppReferenceArray<GUILayoutOption>)null);
        if (Laby.Safezones != null && Laby.Safezones.Count > 0)
        {
            if (GUILayout.Button("Teleport to Random Safezone", buttonStyle, Array.Empty<GUILayoutOption>()))
            {
                int index = UnityEngine.Random.Range(0, Laby.Safezones.Count);
                Laby.PlayerControl.playerNetworkSync.MoveToPosition(Laby.Safezones[index], default(Quaternion));
            }
            int num = 1;
            foreach (Vector3 safezone in Laby.Safezones)
            {
                if (GUILayout.Button($"Safezone {num}", buttonStyle, Array.Empty<GUILayoutOption>()))
                {
                    Laby.PlayerControl.playerNetworkSync.MoveToPosition(safezone, default(Quaternion));
                }
                num++;
            }
        }
        else
        {
            GUILayout.Label("Searching for safehouses...", (Il2CppReferenceArray<GUILayoutOption>)null);
        }
        GUI.DragWindow();
    }

    private string NumberInput(string input)
    {
        return Regex.Replace(GUILayout.TextField(input, 25, Array.Empty<GUILayoutOption>()), "(?!^-)[^0-9]", "");
    }
}
