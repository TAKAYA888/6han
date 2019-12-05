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


        //-----------------------------------------------------------------------------------
        public PlayerArraw(Player player,Vector2 ArrawPos)
        {
            this.player = player;
            this.ArrawPos = ArrawPos;
            ArrawAngle = 45;
        }

        public void Update()
        {
            //ArrawPos = player.PlayerPosition;

            if(Input.GetButton(DX.PAD_INPUT_5))
            {
                ArrawAngle += 1f;
            }
            else if(Input.GetButton(DX.PAD_INPUT_6))
            {
                ArrawAngle -= 1f;
            }

            //回転時の移動処理
            Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(200, 0))
                * Matrix3.createRotation(ArrawAngle)
                * Matrix3.createTranslation(player.PlayerPosition);

            ArrawPos = new Vector2(0) * NextPlayerPos;
        }

        public void Draw()
        {
            DX.DrawRotaGraphF(ArrawPos.x+100, ArrawPos.y+100, 1, MathHelper.toRadians(ArrawAngle)+MathHelper.toRadians(90), Image.PlayerArraws);
        }
    }
}
