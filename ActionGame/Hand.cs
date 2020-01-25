using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath_KNMR;
using MyLib;
using DxLibDLL;

namespace ActionGame
{
    public class Hand : playerObject
    {
        public enum Hit
        {
            NotHit,
            Hit,
            Retrun,
        }

        Hit hit;

        //クラス----------------------------------------------------------------------------------------------------------
        Player player;
        //----------------------------------------------------------------------------------------------------------------

        //ステータス関係変数--------------------------------------------------------------------------------------------------        
        public float playerPosY;
        public float Distance;           　　　//Playerとの距離
        public float FirstAngle;         　　　//初期角度
        public float LastAngle;          　　　//最終角度
        float VelocityX;                      //移動速度(x方向)
        float VelocityY;                      //移動速度(y方向)
        public float DistanceLimit;
        float Velocity;
        public bool HundHitFrag = false;
        bool BeforHundHitFrag = false;
        //----------------------------------------------------------------------------------------------------------------


        ////サイズ関係-------------------------------------------------------------------------             
        //-----------------------------------------------------------------------------------
        public Hand(PlayScene playScene, float x, float y) : base(playScene)
        {
            player = playScene.player;
            HundHitFrag = false;
            Position.x = x;
            Position.y = y;
            hit = Hit.NotHit;
            DistanceLimit = 960;

            if (player.PlayerStateNumber == 0)
            {
                Velocity = 10;
            }
            else
            {
                Velocity = 20;
            }

            ImageWidth = 120;             //画像の横ピクセル数
            ImageHeight = 60;             //画像の縦ピクセル数
            hitboxOffsetLeft = -5;　   　//当たり判定のオフセット
            hitboxOffsetRight = 90;       //当たり判定のオフセット
            hitboxOffsetTop = -40;        //当たり判定のオフセット
            hitboxOffsetBottom = 70;      //当たり判定のオフセット
            VelocityX = MathHelper.cos(player.playerArraw.ArrawAngle) * Velocity;
            VelocityY = MathHelper.sin(player.playerArraw.ArrawAngle) * Velocity;
            playerPosY = player.Position.y;


        }

        public override void Update()
        {
            if (player.PlayerStateNumber == 0)
            {
                DistanceLimit = 960.0f / 2.0f;
            }
            else
            {
                DistanceLimit = 960.0f;
            }
            BeforHundHitFrag = HundHitFrag;
            if (hit == Hit.NotHit)
            {
                Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));

                if (Distance > DistanceLimit || Input.GetButtonDown(DX.PAD_INPUT_2))
                {
                    hit = Hit.Retrun;
                }

                if (Distance <= 1)
                {
                    isDead = true;
                    player.HundFrag = false;
                }
            }
            else if (hit == Hit.Hit)
            {
                VelocityX = 0;
                VelocityY = 0;
            }
            else if (hit == Hit.Retrun)
            {
                float PlayerToAngle = (float)Math.Atan2(player.Position.y - Position.y, player.Position.x - Position.x);
                VelocityX = (float)Math.Cos(PlayerToAngle) * 10;
                VelocityY = (float)Math.Sin(PlayerToAngle) * 10;
            }

            MoveX();
            MoveY();
        }

        void MoveX()
        {
            Position.x += VelocityX;
            //当たり判定の四隅の座標を取得
            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle = top + 32;
            float bottom = GetBottom() - 0.01f;

            //左端が壁にめり込んでいるか？
            if (playScene.map.IsWall(left, top) || //左上が壁か？
                playScene.map.IsWall(left, middle) ||//左真ん中は壁か？
                playScene.map.IsWall(left, bottom))   //左下が壁か？
            {
                float _wallRight = left - left % Map.CellSize + Map.CellSize;//壁の右端
                hit = Hit.Hit;
                SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
                HundHitFrag = true;
                player.AngleSpeedStopTimer = player.AngleSpeedStopTime;
                Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));

            }
            //右端が壁にめりこんでいるか？
            else if (
                playScene.map.IsWall(right, top) ||　　　//左上が壁か？
                playScene.map.IsWall(right, middle) ||     //左真ん中は壁か？
                playScene.map.IsWall(right, bottom))     //左下が壁か？
            {
                hit = Hit.Hit;
                float wallLeft = right - right % Map.CellSize;//壁の左端
                SetRight(wallLeft);//プレイヤーの左端を壁の右端に沿わす
                HundHitFrag = true;
                player.AngleSpeedStopTimer = player.AngleSpeedStopTime;
                Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));
            }
        }

        void MoveY()
        {
            // 縦に移動する 
            Position.y += VelocityY;

            // 当たり判定の四隅の座標を取得 
            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float Center1 = left + 16.25f;
            float Center2 = left + 16.25f * 2.0f;
            float top = GetTop();
            float bottom = GetBottom() - 0.01f;

            // 上端が壁にめりこんでいるか？ 
            if (playScene.map.IsWall(left, top) || // 左上が壁か？ 
                playScene.map.IsWall(Center1, top) ||
                playScene.map.IsWall(Center2, top) ||
                playScene.map.IsWall(right, top))   // 右上が壁か？ 
            {
                float wallBottom = top - top % Map.CellSize + Map.CellSize; // 天井のy座標 
                SetTop(wallBottom); // プレイヤーの頭を天井に沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                hit = Hit.Hit;
                HundHitFrag = true;
                player.AngleSpeedStopTimer = player.AngleSpeedStopTime;
                if (player.playerArraw.ArrawAngle != 90 * 3)
                {
                    Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));
                }
                else
                {
                    Distance = Math.Abs(Position.y - player.Position.y + (player.ImageHeight / 2));
                }
            }
            // 下端が壁にめりこんでいるか？ 
            else if (
                playScene.map.IsWall(left, bottom) || // 左下が壁か？ 
                playScene.map.IsWall(Center1, bottom) ||
                playScene.map.IsWall(Center2, bottom) ||
                playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                hit = Hit.Hit;
                HundHitFrag = true;
                player.AngleSpeedStopTimer = player.AngleSpeedStopTime;
                Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));
            }
        }

        public override void Draw()
        {
            if (player.PlayerStateNumber == 0)
            {
                Camera.DrawRotaGraph(Position.x, Position.y, 1.0f, 180, Image.PlayerHand, 1);
            }
            else
            {
                Camera.DrawRotaGraph(Position.x, Position.y, 1.0f, 180, Image.PlayerHand2, 1);
            }
            //DX.DrawString(100, 100, player.playerArraw.ArrawAngle.ToString(), DX.GetColor(255, 255, 255));
            Camera.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
        }

        public override void OnCollision(EnemyObject enemyObject)
        {
            player.HundFrag = false;
            isDead = true;
            Player.ScorePoint += 1000;
        }

        public override void OnCollisionI(ItemObject itemObject)
        {
            if (itemObject is Grounds)
            {
                hit = Hit.Retrun;

            }
            else if (itemObject is WoolenYarn)
            {
                player.haveWoolenYarn++;
                Player.ScorePoint += 1000;
            }
            else if (itemObject is SpeedUp)
            {
                player.PlayerStateNumber = 1;
                Player.ScorePoint += 1000;
            }
        }

        public override void OnCollisionP(playerObject playerObject)
        {
        }

        public override void OnCollisionG(GimmickObject gimmickObject)
        {
            if (gimmickObject is key && !HundHitFrag)
            {
                HundHitFrag = true;
                hit = Hit.Hit;
                player.AngleSpeedStopTimer = player.AngleSpeedStopTime;
                Distance = (float)Math.Sqrt(
                    (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    * (player.Position.x + (player.ImageWidth / 2) - Position.x + ImageWidth / 2)
                    + (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2)
                    * (player.Position.y + (player.ImageHeight / 2) - Position.y + ImageHeight / 2));
            }
        }
    }
}

