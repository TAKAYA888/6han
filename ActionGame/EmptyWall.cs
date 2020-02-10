using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    public class EmptyWall : ItemObject
    {
        public EmptyWall(PlayScene playScene, float x, float y) : base(playScene)
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
        }

        public override void OnCollision(playerObject playerObject)
        {
            playScene.player.SetLeft(GetRight());
        }
    }
}
