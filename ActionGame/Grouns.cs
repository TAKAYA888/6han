using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    public class Grounds : ItemObject
    {
        public Grounds(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 3840;
            imageHeight = 64;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 50;
            hitboxOffsetBottom = 0;
        }
        public override void Update()
        {

        }

        public override void Draw()
        {

        }

        public override void OnCollision(playerObject playerObject)
        {
            //isDead = true;
            //if (playerObject is Player)
            //{
            //    isDead = true;
            //}
        }
    }
}
