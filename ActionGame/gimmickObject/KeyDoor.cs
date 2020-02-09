using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionGame
{
    class KeyDoor : GimmickObject
    {        
        public int DoorNunber;

        public KeyDoor(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            imageWidth = 60;
            imageHeight = 60;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
            openFrag = false;
        }


        public override void Update()
        {
            for (int i = 0; i < playScene.keys.Count(); i++)
            {
                key nowkey = playScene.keys[i];

                if (nowkey.KeyNunber == DoorNunber)
                {
                    if (nowkey.openFrag)
                    {
                        openFrag = true;
                    }
                }
                else continue;
            }
        }

        public override void Draw()
        {
            switch (DoorNunber)
            {
                case 0:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick4[0]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick4[2]);
                    }
                    break;
                case 1:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick1[2]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick1[0]);
                    }
                    break;
                case 2:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick2[0]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick2[2]);
                    }
                    break;
                case 3:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick3[0]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick3[2]);
                    }
                    break;
                case 4:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick3[0]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick3[0]);
                    }
                    break;
                case 5:
                    if (openFrag)
                    {
                        //Camera.DrawGraph(x, y, Image.Gimmick3[0]);
                    }
                    else
                    {
                        Camera.DrawGraph(x, y, Image.Gimmick3[0]);
                    }
                    break;
            }

        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playerObject is Player)
            {
                if (!openFrag && playerObject.GetPrevBottom() <= GetPrevTop())
                {
                    playerObject.SetBottom(GetPrevTop() - 1f);
                }
                if (!openFrag && playerObject.GetPrevLeft() >= GetPrevRight())
                {
                    playerObject.SetLeft(GetPrevRight());
                }
                else if (!openFrag && playerObject.GetPrevRight() <= GetPrevLeft())
                {
                    playerObject.SetRight(GetPrevLeft());
                }
            }
        }
    }
}
