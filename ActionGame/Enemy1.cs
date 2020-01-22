using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    class Enemy1:EnemyObject
    {
        int HP = 1;//HP
        const float Gravity = 0.6f; // 重力 
        const float MaxFallSpeed = 12f;   // 最大落下速度 
        float vx = -1; // 横移動速度 
        float vy = 0; // 縦移動速度
        //GimmickObject groundObject = null; // 乗っている床オブジェクト
        //Direction direction = Direction.Right; // 向いている方向

        public Enemy1(PlayScene playScene, float x, float y) : base(playScene)
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
        public override void Update(Player player)
        {
            if (HP <= 0)
            {
                isDead = true;
            }
            vy += Gravity;
            MoveY();
            MoveX();                        
        }

        void MoveX()
        {
            x += vx;

            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle = top + 32;
            float bottom = GetBottom() - 0.01f;
            float bottom2 = bottom + 0.01f;

            if (playScene.map.IsWall(left, top) ||    // 左上が壁か？ 
                playScene.map.IsWall(left, middle) || // 左中が壁か？ 
                playScene.map.IsWall(left, bottom) //||  // 左下が壁か？ 
                /*!playScene.map.IsWall(left, bottom2*/)// 左下の床がないか？

            {
                float wallRight = left - left % Map.CellSize + Map.CellSize; // 壁の右端 
                SetLeft(wallRight); // 敵の左端を壁の右端に沿わす
                vx = -vx;
                //direction = Direction.Left; // 左向きにする
            }
            else if (
              playScene.map.IsWall(right, top) ||   // 右上が壁か？ 
              playScene.map.IsWall(right, middle) || // 右中が壁か？ 
              playScene.map.IsWall(right, bottom) //||  // 右下が壁か？
              /*!playScene.map.IsWall(right, bottom2)*/) // 右下の床がないか？
            {
                float wallLeft = right - right % Map.CellSize; // 壁の左端 
                SetRight(wallLeft); // 敵の右端を壁の左端に沿わす 
                vx = -vx;
                //direction = Direction.Right; // 右向きにする
            }

        }
        void MoveY()
        {
            y += vy;

            bool grounded1 = false;
            bool grounded2 = false;

            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float bottom = GetBottom() - 0.01f;


            if (playScene.map.IsWall(left, top) || // 左上が壁か？ 
                playScene.map.IsWall(right, top))   // 右上が壁か？ 
            {
                float wallBottom = top - top % Map.CellSize + Map.CellSize; // 天井のy座標 
                SetTop(wallBottom); // 敵の頭を天井に沿わす 
                vy = 0; // 縦の移動速度を0に 
            }
            if (playScene.map.IsWall(left, bottom))   // 右下が壁か？ 
            {
                grounded1 = true; // 着地した
            }
            if (playScene.map.IsWall(right, bottom)) // 左下が壁か？ 
            {
                grounded2 = true; // 着地した
            }

            if (grounded1 || grounded2) // もし着地してたら 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); //足元を床の高さに沿わす 
                vy = 0; // 縦の移動速度を0に 
            }

            if((grounded1&&!grounded2)||(!grounded1&&grounded2))
            {
                vx = -vx;
            }
        }

        public override void Draw()
        {
            //bool flip = direction == Direction.Left;
            Camera.DrawGraph(x, y, Image.EnemyImage01);
        }

        public override void OnCollision(playerObject playerObject)
        {
            if(playerObject is Hand)
            {
                SE.Play(SE.EnemyDamageSE);
                HP -= 1;
                if (HP <= 0)
                {
                    isDead = true; // 死亡 
                }
            }
        }

        public override void OnCollisionH(Hand hund)
        {
            SE.Play(SE.EnemyDamageSE);
            HP -= 1;
            if (HP <= 0)
            {
                isDead = true; // 死亡 
            }
        }

        public override void miniMapDraw()
        {
            DX.DrawRotaGraphF(x / 1.5f, y / 2+200, 0.2, 0, Image.EnemyImage01);
        }
    }
}
