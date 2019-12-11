using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    class NeedleObject:GimmickObject
    {
        //針オブジェクトクラス
        public NeedleObject(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 120;
            imageHeight = 120;
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
            Camera.DrawGraph(x, y, Image.TogeWani02);
        }

        public override void OnCollision(Player player)
        {
            isDead = true;
            // if (other is Player)
            // {
            //     isDead = true;
            // }
        }
    }
}
