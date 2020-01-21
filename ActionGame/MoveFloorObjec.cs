using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    class MoveFloorObject : GimmickObject
    {
        int counter = 0;

        public MoveFloorObject(PlayScene playScene, float x, float y) : base(playScene)
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
            counter++;

            if (counter < 200)
            {
                x += 1f; // 右に移動
            }
            else if (counter < 400)
            {
                x -= 1f; // 左に移動
            }

            if (counter == 400)
            {
                counter = 0;
            }
        }

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.MoveFloor);
        }

        public override void OnCollision(playerObject playerObject)
        {            
        }
    }
}
