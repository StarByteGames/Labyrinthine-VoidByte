using System;
using VoidByte.Cheats;
using UnityEngine;

namespace VoidByte.Utilities
{
    internal class Drawing
    {
        private static Camera _gameCamera;

        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

        public static Camera GameCamera
        {
            get
            {
                if ((UnityEngine.Object)_gameCamera == null || !_gameCamera.isActiveAndEnabled)
                {
                    _gameCamera = Camera.main;
                }
                return _gameCamera;
            }
        }

        public static void DrawString(
            Vector2 position,
            string label,
            Color color,
            int fontSize,
            bool centered = true
        )
        {
            GUI.color = color;
            StringStyle.fontSize = fontSize;
            StringStyle.normal.textColor = color;
            GUIContent val = new GUIContent(label);
            Vector2 val2 = StringStyle.CalcSize(val);
            GUI.Label(new Rect(centered ? (position - val2 / 2f) : position, val2), val, StringStyle);
        }

        public static void TextWithDistance(
            Vector3 target,
            string text,
            Color color,
            Camera? relativeTo = null
        )
        {
            Camera val = relativeTo ?? GameCamera;
            Vector3 val2 = val.WorldToScreenPoint(target);
            if (
                val2.x < 0f
                || val2.x > (float)Screen.width
                || val2.y < 0f
                || val2.y > (float)Screen.height
                || val2.z > 0f
            )
            {
                float num = (float)
                    Math.Round(
                        Vector3.Distance(((Component)Laby.PlayerControl).transform.position, target),
                        1
                    );
                if (val2.z >= 0f && num < 1000f)
                {
                    DrawString(
                        new Vector2(val2.x, (float)Screen.height - val2.y),
                        text + " [" + num + "m]",
                        color,
                        12
                    );
                }
            }
        }

        public static void TextWithDistanceMonster(
            Vector3 target,
            string text,
            Camera? relativeTo = null
        )
        {
            Camera val = relativeTo ?? GameCamera;
            Vector3 val2 = val.WorldToScreenPoint(target);
            if (
                val2.x < 0f
                || val2.x > (float)Screen.width
                || val2.y < 0f
                || val2.y > (float)Screen.height
                || val2.z > 0f
            )
            {
                float num = (float)
                    Math.Round(
                        Vector3.Distance(((Component)Laby.PlayerControl).transform.position, target),
                        1
                    );
                if (val2.z >= 0f && num < 100f)
                {
                    DrawString(
                        new Vector2(val2.x, (float)Screen.height - val2.y),
                        text + " [" + num + "m]",
                        Color.red,
                        12
                    );
                }
                else if (val2.z >= 0f && num < 1000f)
                {
                    DrawString(
                        new Vector2(val2.x, (float)Screen.height - val2.y),
                        text + " [" + num + "m]",
                        Color.green,
                        12
                    );
                }
            }
        }
    }
}
