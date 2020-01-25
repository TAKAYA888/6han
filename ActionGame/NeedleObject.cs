using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    class NeedleObject:GimmickObject
    {
        public int number;

        //針オブジェクトクラス
        public NeedleObject(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 60;
            imageHeight = 60;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }
        public override void Update()
        {

        }

        public override void Draw()
        {
            int Draw = 0;

            switch (number)
            {
                case 1:
                    Draw = Image.TogeWani01;
                    break;
                case 2:
                    Draw = Image.TogeWani02;
                    break;
                case 3:
                    Draw = Image.TogeWani03;
                    break;                    
            }


            if(Draw!=0)
            {
                Camera.DrawGraph(x, y, Draw);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.TogeWani02);
            }
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playerObject is Player)
            {
                isDead = true;
            }
        }
    }
}
