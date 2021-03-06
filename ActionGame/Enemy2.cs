﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    class Enemy2 : EnemyObject
    {
        enum State
        {
            Stop,
            Move
        }

        State state = State.Stop;

        int HP = 2;//HP
        float diff1 = 0;//プレイヤーXとエネミーXの差
        float diff2 = 0;//プレイヤーYとエネミーYの差
        const float Gravity = 0.6f; // 重力 
        const float MaxFallSpeed = 12f;   // 最大落下速度 
        float vx = -6; // 横移動速度 
        float vy = 0; // 縦移動速度
        bool Right;
        //GimmickObject groundObject = null; // 乗っている床オブジェクト
        //Direction direction = Direction.Right; // 向いている方向

        public Enemy2(PlayScene playScene, float x, float y) : base(playScene)
        {
            this.x = x;
            this.y = y;
            Right = false;

            imageWidth = 180;
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

            float playerX = player.Position.x;
            float playerY = player.Position.y;

            vy += Gravity;

            if (x > playerX)
            {
                diff1 = x - playerX;
            }
            else
            {
                diff1 = playerX - x;
            }
            if (y > playerY)
            {
                diff2 = y - playerY;
            }
            else
            {
                diff2 = playerY - y;
            }

            if (diff1 <= 60 * 6 && diff2 <= 60 * 7)
            {
                state = State.Move;
            }

            if(vx>=0)
            {
                Right = true;
            }
            else
            {
                Right = false;
            }

            if (state == State.Stop)
            {

            }
            if (state == State.Move)
            {
                MoveX();

            }

            MoveY();

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
            y += vy;

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
            if (state == State.Stop)
            {
                Camera.DrawGraph(x, y, Image.EnemyImage02,Right);
            }
            else
            {
                Camera.DrawGraph(x, y, Image.EnemyImage02_1,Right);
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
        public override void OncollisionG(GimmickObject gimmickObject)
        {
            if(gimmickObject is NeedleObject)
            {
                isDead = true;
            }
        }
    }
}
