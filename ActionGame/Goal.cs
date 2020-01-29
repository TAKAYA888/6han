using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public class Goal : ItemObject
    {
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
            
        }

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.Goal[1]);
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playScene.player.haveWoolenYarn >= 1)
            {
               Game.ChangeScene(new GameClearScene());
            }
            else 
            {

            }
           
        }
    }
}
