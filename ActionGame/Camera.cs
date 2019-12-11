using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyMath_KNMR;

namespace ActionGame
{
    public static class Camera
    {
        public static float x;
        public static float y;

        public static void LookAt(Vector2 pos)
        {
            x = pos.x - 1920/2;
            y = pos.y - 1080/2;
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

        public static void DrawRotaGraph(float _x,float _y,float angle,int handle,int trun)
        {
            DX.DrawRotaGraphF(_x - x, _y - y, 1, angle, handle,1,trun);
        }

        public static void DrawLine(Vector2 pos1,Vector2 pos2)
        {
            DX.DrawLine((int)(pos1.x - x), (int)(pos1.y - y), (int)(pos2.x - x), (int)(pos2.y - y), DX.GetColor(0, 0, 0));
        }
    }
}

