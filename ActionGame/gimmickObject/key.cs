using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    public class key : GimmickObject
    {
        public int KeyNunber;

        public key(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            imageWidth = 40;
            imageHeight = 60;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
            openFrag = false;
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            if (openFrag)
            {
                Camera.DrawGraph(x, y, Image.KeyImage[1]);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.KeyImage[0]);
            }
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playerObject is Hand && !openFrag)
            {
                SE.Play(SE.keySE);
                openFrag = true;
            }
        }
    }
}


