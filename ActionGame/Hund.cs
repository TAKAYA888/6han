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
    public class Hund
    {
        public enum Hit
        {
            NotHit,
            Hit,
        }

        Hit hit;

        //クラス----------------------------------------------------------------------------------------------------------
        Player player;
        //----------------------------------------------------------------------------------------------------------------

        //ステータス関係変数--------------------------------------------------------------------------------------------------

        public Vector2 Position = new Vector2(0, 0); 　//現在地
        public float playerPosY;
        public float Distance;           　　　//Playerとの距離
        public float FirstAngle;         　　　//初期角度
        public float LastAngle;          　　　//最終角度
        float VelocityX ;                      //移動速度(x方向)
        float VelocityY ;                      //移動速度(y方向)
        public bool HundHitFrag = false;
        bool BeforHundHitFrag = false;

        //----------------------------------------------------------------------------------------------------------------


        //サイズ関係-------------------------------------------------------------------------
        int imageWidth = 120;//画像の横ピクセル数
        int imageHeight = 60;//画像の縦ピクセル数
        int hitboxOffsetLeft = 0;　　//当たり判定のオフセット
        int hitboxOffsetRight = 0;   //当たり判定のオフセット
        int hitboxOffsetTop = 0;     //当たり判定のオフセット
        int hitboxOffsetBotton = 0;  //当たり判定のオフセット

        float prevX;           //1フレーム前のx座標
        float prevY;           //1フレーム前のy座標
        float prevLeft;        //1フレーム前の左端
        float prevRight;       //1フレーム前の右端
        float prevTop;         //1フレーム前の上端
        float prevBottom;      //1フレーム前の下端
        //-----------------------------------------------------------------------------------
        public Hund(Player player,float x,float y)
        {
            this.player = player;
            Position.x = x;
            Position.y = y;
            hit = Hit.NotHit;
            VelocityX = MathHelper.cos(player.playerArraw.ArrawAngle) * 10;
            VelocityY = MathHelper.sin(player.playerArraw.ArrawAngle) * 10;
            playerPosY = player.PlayerPosition.y;
        }

        public void Update()
        {            
            BeforHundHitFrag = HundHitFrag;
            if(hit==Hit.NotHit)
            {
                MoveX();
                MoveY();

                Distance = (float)Math.Sqrt(
                    (player.PlayerPosition.x - Position.x)
                    * (player.PlayerPosition.x - Position.x)
                    + (player.PlayerPosition.y - Position.y)
                    * (player.PlayerPosition.y - Position.y));

                if(Distance>960.0f)
                {
                    VelocityX = -VelocityX;
                    VelocityY = -VelocityY;
                }                               

                if (Distance<=1)
                {
                    player.playScene.hund = null;
                    player.HundFrag = false;
                }
            }
            else
            {
                VelocityX = 0;
                VelocityY = 0;
            }            
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
            if (player.playScene.map.IsWall(left, top) || //左上が壁か？
                player.playScene.map.IsWall(left, middle) ||//左真ん中は壁か？
                player.playScene.map.IsWall(left, bottom))   //左下が壁か？
            {
                float _wallRight = left - left % Map.CellSize + Map.CellSize;//壁の右端
                hit = Hit.Hit;
                SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
                HundHitFrag = true;
                Distance = (float)Math.Sqrt(
                    (player.PlayerPosition.x - Position.x)
                    * (player.PlayerPosition.x - Position.x)
                    + (player.PlayerPosition.y - Position.y)
                    * (player.PlayerPosition.y - Position.y));
                
            }
            //右端が壁にめりこんでいるか？
            else if (
                player.playScene.map.IsWall(right, top) ||　　　//左上が壁か？
                player.playScene.map.IsWall(right, middle) ||     //左真ん中は壁か？
                player.playScene.map.IsWall(right, bottom))     //左下が壁か？
            {
                hit = Hit.Hit;
                float wallLeft = right - right % Map.CellSize;//壁の左端
                SetRight(wallLeft);//プレイヤーの左端を壁の右端に沿わす
                HundHitFrag = true;
                Distance = (float)Math.Sqrt(
                    (player.PlayerPosition.x - Position.x)
                    * (player.PlayerPosition.x - Position.x)
                    + (player.PlayerPosition.y - Position.y)
                    * (player.PlayerPosition.y - Position.y));
                
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
            if (player.playScene.map.IsWall(left, top) || // 左上が壁か？ 
                player.playScene.map.IsWall(Center1, top) ||
                player.playScene.map.IsWall(Center2, top) ||
                player.playScene.map.IsWall(right, top))   // 右上が壁か？ 
            {
                float wallBottom = top - top % Map.CellSize + Map.CellSize; // 天井のy座標 
                SetTop(wallBottom); // プレイヤーの頭を天井に沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                hit = Hit.Hit;
                HundHitFrag = true;
                Distance = (float)Math.Sqrt(
                    (player.PlayerPosition.x - Position.x)
                    * (player.PlayerPosition.x - Position.x)
                    + (player.PlayerPosition.y - Position.y)
                    * (player.PlayerPosition.y - Position.y));
               
            }
            // 下端が壁にめりこんでいるか？ 
            else if (
                player.playScene.map.IsWall(left, bottom) || // 左下が壁か？ 
                player.playScene.map.IsWall(Center1, bottom) ||
                player.playScene.map.IsWall(Center2, bottom) ||
                player.playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                hit = Hit.Hit;
                HundHitFrag = true;
                Distance = (float)Math.Sqrt(
                    (player.PlayerPosition.x - Position.x)
                    * (player.PlayerPosition.x - Position.x)
                    + (player.PlayerPosition.y - Position.y)
                    * (player.PlayerPosition.y - Position.y));
                
            }            
        }

        public void Draw()
        {
            Camera.DrawRotaGraph(Position.x, Position.y, 0, Image.PlayerHand);
            //DX.DrawString(100, 100, player.playerArraw.ArrawAngle.ToString(), DX.GetColor(255, 255, 255));
            Camera.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
        }

        //当たり判定の左端を取得
        public virtual float GetLeft()
        {
            return Position.x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public virtual void SetLeft(float left)
        {
            Position.x = left - hitboxOffsetLeft;
        }

        //当たり判定の右端を取得
        public virtual float GetRight()
        {
            return Position.x + imageWidth - hitboxOffsetRight;
        }

        //右端を指定することにより位置を設定する
        public virtual void SetRight(float right)
        {
            Position.x = right + hitboxOffsetRight - imageWidth;
        }

        //当たり判定の上端を取得
        public virtual float GetTop()
        {
            return Position.y + hitboxOffsetTop;
        }

        //上端を指定することにより位置を設定する
        public virtual void SetTop(float top)
        {
            Position.y = top - hitboxOffsetTop;
        }

        //当たり判定の下端を取得する
        public virtual float GetBottom()
        {
            return Position.y + imageHeight - hitboxOffsetBotton;
        }

        //下端を指定することにより位置を設定する
        public virtual void SetBottom(float bottom)
        {
            Position.y = bottom + hitboxOffsetBotton - imageHeight;
        }
    }
}

