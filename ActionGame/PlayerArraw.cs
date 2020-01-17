using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath_KNMR;
using MyLib;
using DxLibDLL;

namespace ActionGame
{
    public class PlayerArraw
    {
        //ステータス関係の変数---------------------------------------------------------------

        public Vector2 ArrawPos;
        public float ArrawAngle;
        Player player;
        int Cooltimer;
        readonly int cooltime = 3;

        //-----------------------------------------------------------------------------------
        public PlayerArraw(Player player,Vector2 ArrawPos)
        {
            this.player = player;
            this.ArrawPos = ArrawPos;
            ArrawAngle = 310;
        }

        public void Update()
        {
            Cooltimer--;

            //ArrawPos = player.PlayerPosition;

            if(Input.GetButton(DX.PAD_INPUT_6) && Cooltimer < 0)
            {
                ArrawAngle += 10f;
                Cooltimer = cooltime;
            }
            else if(Input.GetButton(DX.PAD_INPUT_5) && Cooltimer < 0)
            {
                if (ArrawAngle == 0)
                {
                    ArrawAngle = 360;
                }
                ArrawAngle -= 10f;
                Cooltimer = cooltime;
            }

            ArrawAngle = Math.Abs(ArrawAngle % 360);

            //回転時の移動処理
            Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(100, 0))
                * Matrix3.createRotation(ArrawAngle)
                * Matrix3.createTranslation(player.Position);

            ArrawPos = new Vector2(0) * NextPlayerPos;
        }

        public void Draw()
        {
            //DX.DrawString(100, 100, ArrawAngle.ToString(), DX.GetColor(255, 255, 255));
            Camera.DrawRotaGraph(ArrawPos.x, ArrawPos.y, MathHelper.toRadians(ArrawAngle) + MathHelper.toRadians(90), Image.PlayerArraws,0);
        }
    }
}
