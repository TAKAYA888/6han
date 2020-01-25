using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyMath_KNMR;

namespace ActionGame
{
    class Enemy3 : EnemyObject
    {
        int counter = 0;

        enum State
        {
            Stop,
            Move
        }
        State state = State.Stop;

        int HP = 3;//HP
        const float Gravity = 0.6f; // 重力 
        const float MaxFallSpeed = 12f;   // 最大落下速度 
        float vx = 0; // 横移動速度 
        float vy = 0; // 縦移動速度


        public Enemy3(PlayScene playScene, float x, float y) : base(playScene)
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
        public override void Update(Player player)
        {
            counter++;

            if (HP <= 0)
            {
                isDead = true;
            }

            if (counter < 200)
            {
                state = State.Stop;
            }
            else if (counter < 400)
            {
                state = State.Move;
            }

            if (counter == 400)
            {
                counter = 0;
            }

            if (state == State.Stop)
            {

            }
            if (state == State.Move)
            {
                if (counter % 50 == 0)
                {
                    playScene.enemyBullets.Add(new EnemyBullet(x, y, 180f * MyMath.Deg2Rad, 8f));
                }
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

            if (playScene.map.IsWall(left, top) ||    // 左上が壁か？ 
                playScene.map.IsWall(left, middle) || // 左中が壁か？ 
                playScene.map.IsWall(left, bottom))   // 左下が壁か？ 

            {
                float wallRight = left - left % Map.CellSize + Map.CellSize; // 壁の右端 
                SetLeft(wallRight); // 敵の左端を壁の右端に沿わす
                vx = -vx;
                //direction = Direction.Left; // 左向きにする
            }
            else if (
              playScene.map.IsWall(right, top) ||   // 右上が壁か？ 
              playScene.map.IsWall(right, middle) || // 右中が壁か？ 
              playScene.map.IsWall(right, bottom))  // 右下が壁か？ 
            {
                float wallLeft = right - right % Map.CellSize; // 壁の左端 
                SetRight(wallLeft); // 敵の右端を壁の左端に沿わす 
                vx = -vx;
                //direction = Direction.Right; // 右向きにする
            }

        }
        void MoveY()
        {
            //y += vy;

            bool grounded = false;

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
            else if (
                playScene.map.IsWall(left, bottom) || // 左下が壁か？ 
                playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                grounded = true; // 着地した 
            }

            if (grounded) // もし着地してたら 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                vy = 0; // 縦の移動速度を0に 
            }
        }

        public override void Draw()
        {
            //bool flip = direction == Direction.Left;
            if (state == State.Stop)
            {
                Camera.DrawGraph(x, y, Image.EnemyImage03);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.EnemyImage03_1);
            }
        }

        public override void OnCollision(playerObject playerObject)
        {
            if (playerObject is Hand)
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
            DX.DrawRotaGraphF(x / 1.5f, y / 2 + 200, 0.2, 0, Image.EnemyImage01);
        }
    }
}