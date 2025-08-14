using UnityEngine;
using VoidByte.Utilities;
using System;

namespace VoidByte
{
    public static class ESP
    {
        public static Camera GameCamera = Camera.main;

        public static void Render()
        {
            RenderMonsters();
            RenderPlayers();
            RenderKeyPuzzle();
        }

        private static void RenderKeyPuzzle()
        {
            foreach (var key in Core.KeyPuzzle.keys)
            {
                if(key == null)
                {
                    continue;
                }

                Drawing.TextWithDistance(key.transform, key.name);
            }

            foreach (var keyLock in Core.KeyPuzzle.locks)
            {
                if(keyLock == null)
                {
                    continue;
                }

                Drawing.TextWithDistance(keyLock.transform, keyLock.name);
            }
        }

        private static void RenderPlayers()
        {
            foreach (var player in Core.GameManager.Players)
            {
                Drawing.TextWithDistance(player.transform, player.playerName);
            }
        }

        private static void RenderMonsters()
        {
            foreach (var ai in Core.AIControllers)
            {
                Drawing.TextWithDistance(ai.transform, ai.monsterType.ToString());
            }
        }
    }
}