using System.Collections.Generic;
using Il2Cpp;
using Il2CppSystem.Collections.Generic;
using Il2CppValkoGames.Labyrinthine.Monsters;
using Labyrinthine.Utilities;
using UnityEngine;

namespace LabyrinthineCheat;

public static class ESP
{
    public static Camera GameCamera = Camera.main;

    public static void Render()
    {
        if (Laby.PlayerInCase)
        {
            RenderMonsters();
            RenderPlayers();
            RenderSafezones();
        }
    }

    private static void RenderSafezones()
    {
        int num = 1;
        foreach (Vector3 safezone in Laby.Safezones)
        {
            Drawing.TextWithDistance(safezone, $"Safezone {num}", Color.magenta);
            num++;
        }
    }

    private static void RenderPlayers()
    {
        foreach (PlayerNetworkSync current in Laby.GameManager.Players)
        {
            if ((UnityEngine.Object)(object)current != (UnityEngine.Object)null && (UnityEngine.Object)(object)((Component)current).transform != (UnityEngine.Object)null)
            {
                Drawing.TextWithDistance(((Component)current).transform.position, current.playerName, Color.cyan);
            }
        }
    }

    private static void RenderMonsters()
    {
        AIController[] aIControllers = Laby.AIControllers;
        foreach (AIController val in aIControllers)
        {
            if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null && (UnityEngine.Object)(object)((Component)val).transform != (UnityEngine.Object)null)
            {
                Drawing.TextWithDistanceMonster(((Component)val).transform.position, ((object)val.monsterType/*cast due to .constrained prefix*/).ToString().Replace("_", " "));
            }
        }
    }
}
