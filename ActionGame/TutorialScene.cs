using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;
using DxLibDLL;
using MyMath_KNMR;

namespace ActionGame
{
    //チュートリアルシーン
    public class TutorialScene : ItemObject
    {
        public TutorialScene(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 60;
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
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playerObject is Player)
            {
                playScene.stageLevel += 1;
                isDead = true;
            }


        }
    }
}

