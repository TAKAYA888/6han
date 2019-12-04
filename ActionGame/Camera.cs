using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public static class Camera
    {
        public static float x;
        public static float y;

        public static void LookAt(float targetX)
        {
            x = targetX - Screen.Width / 2;
        }
        public static void DrawGraph(float worldX, float worldY, int handle, bool flip = false)
        {
            if (flip) DX.DrawTurnGraphF(worldX - x, worldY - y, handle);
            else DX.DrawGraphF(worldX - x, worldY - y, handle);
        }
        public static void DrawLineBox(float left, float top, float right, float bottom, uint color)
        {
            DX.DrawBox(
                (int)(left - x + 0.5f),
                (int)(top - y + 0.5f),
                (int)(right - x + 0.5f),
                (int)(bottom - y + 0.5f),
                color,
                DX.FALSE);
        }
    }
}

