using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    class Enemy1:EnemyObject
    {
        public Enemy1(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 180;
            imageHeight = 180;
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

        }

        public override void OnCollision(ItemObject other)
        {
            // if (other is Player)
            // {
            //     isDead = true;
            // }
        }
    }
}
