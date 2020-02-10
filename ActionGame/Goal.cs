using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyMath_KNMR;

namespace ActionGame
{
    public class Goal : ItemObject
    {
        public bool Clear = false;
        int timer = 0;
        public Goal(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 180;
            imageHeight = 240;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }
        public override void Update()
        {
            //if (Clear == true)
            //{
            //    timer++;
            //    if (timer >= 100)
            //    {
            //        Game.ChangeScene(new GameClearScene());
            //        timer = 0;
            //    }
            //}
        }

        public override void Draw()
        {
            //Camera.DrawGraph(x, y, Image.Goal[1]);
            //アニメーション処理
            if (Clear == true)
            {
                timer++;
               
                if (timer == 3)
                {
                    Game.particleManager.Heal(x+imageWidth/2, y+imageHeight/2);
                }

                if (timer >= 5&&timer<=60)
                {

                    Camera.DrawGraph(x, y, Image.Goal[5]);

                }

                if (timer >= 60&&timer<=100)
                {
                    Camera.DrawGraph(x, y, Image.Goal[6]);
                }

                if (timer >= 100)
                {
                    Camera.DrawGraph(x, y, Image.Goal[7]);
                }

                if (timer >= 140)
                {
                    Game.particleManager.Stars(x, y); 　//パーティクルです。
                    Game.ChangeScene(new GameClearScene());
                    timer = 0;
                }              
            }
            else
            {
                Camera.DrawGraph(x, y, Image.Goal[1]);
            }
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playScene.player.haveWoolenYarn >= 1)
            {
                Clear = true;
            }
            else
            {

            }

        }
    }
}
