using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    public class  SpeedUp:ItemObject
    {
        public SpeedUp(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 72;
            imageHeight = 72;
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
            Camera.DrawGraph(x, y, Image.SpeedUpItem);
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
