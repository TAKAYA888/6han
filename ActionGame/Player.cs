using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;
using MyMath_KNMR;

namespace ActionGame
{
    public class Player
    {

        //ステータス一覧---------------------------------------------------------------------        
        public enum JumpState
        {
            Walk,//歩き、立ち
            Jump,//ジャンプ中
        }
        //-----------------------------------------------------------------------------------

        //ステータス関係の変数---------------------------------------------------------------
        public Vector2 PlayerPosition;            //座標
        public bool isDead = false;               //死亡フラグ(true)だと死ぬ
        float VelocityX = 0f;                     //移動速度(x方向)
        float VelocityY = 0f;　　　               //移動速度(y方向)
        public int HP = 3;                        //HP(体力)
        public bool HundFrag;                     //手がくっついたかのフラグ
        public bool BeforHundFrag = false;　　　　//一個前の手がくっついたかのフラグ
        public bool NowHundFrag = false;　　　　　//現在の手がくっついたかのフラグ（今のところはいらない）
        float Distance;　　　　　　　　　　　　　 //手とこいつの距離
        float angle = MathHelper.toRadians(45);　 //手との角度
        float FirstAngle;
        float LastAngle;
        float angleSpeed = 1.0f;

        //-----------------------------------------------------------------------------------

        //固定変数系-------------------------------------------------------------------------
        readonly float WalkSpeed = 4f;　　　　　　//歩く速さ
        readonly float Gravity = 0.6f;　　　　　　//重力加速度
        //-----------------------------------------------------------------------------------


        //サイズ関係-------------------------------------------------------------------------
        int imageWidth = 180;　　　　//画像の横ピクセル数
        int imageHeight = 240;　　　 //画像の縦ピクセル数
        int hitboxOffsetLeft = 0;　　//当たり判定のオフセット
        int hitboxOffsetRight = 0;   //当たり判定のオフセット
        int hitboxOffsetTop = 0;     //当たり判定のオフセット
        int hitboxOffsetBotton = 0;  //当たり判定のオフセット

        float prevX;                 //1フレーム前のx座標
        float prevY;                 //1フレーム前のy座標
        float prevLeft;              //1フレーム前の左端
        float prevRight;             //1フレーム前の右端
        float prevTop;               //1フレーム前の上端
        float prevBottom;            //1フレーム前の下端
        //-----------------------------------------------------------------------------------

        JumpState jumpState;         //ジャンプステートの初期化
        public PlayScene playScene;　//playSceneの宣言
        public PlayerArraw playerArraw;

        //コンストラクタ
        public Player(PlayScene playScene,float x,float y)
        {
            this.playScene = playScene;
            PlayerPosition.x = x;
            PlayerPosition.y = y;
            HundFrag = false;
            playerArraw = new PlayerArraw(this, PlayerPosition);
        }

        //毎フレームの更新処理
        public void Update()
        {
            playerArraw.Update();            
            //一個前のハンドフラグを代入
            BeforHundFrag = HundFrag;
            //ゲーム上に手が存在しなかったら
            if (!HundFrag)
            {
                //入力処理
                HundleInput();

                // 重力による落下 
                VelocityY += Gravity;

                //横移動
                MoveX();
                //縦移動
                MoveY();
            }
            else
            {
                //腕が壁とくっついたら
                if (playScene.hund.HundHitFrag)
                {
                    //手とPlayerの距離を縮めています
                    if (playScene.hund.Distance > 0)
                    {
                        playScene.hund.Distance -= 4f;
                    }
                    else
                    {
                        playScene.hund.Distance = 0;
                    }
                    if (NowHundFrag)
                    {
                        if (LastAngle < angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                        else if (FirstAngle > angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                    }
                    else
                    {
                        if (LastAngle > angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                        else if (FirstAngle < angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                    }
                    
                    angle += angleSpeed;

                    //回転時の移動処理
                    Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(playScene.hund.Distance, 0))
                        * Matrix3.createRotation(angle)
                        * Matrix3.createTranslation(playScene.hund.Position);

                    PlayerPosition = new Vector2(0) * NextPlayerPos;
                }
            }
        }

        //入力関係の処理を行います
        void HundleInput()
        {
            if (Input.GetButton(DX.PAD_INPUT_LEFT))
            {
                //左が押されたら、速度に「WalkSpeed」を引く
                VelocityX = -WalkSpeed;
            }
            else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
            {
                //左が押されたら、速度に「WalkSpeed」を引く
                VelocityX = WalkSpeed;
            }
            else
            {
                //速度を0にする
                VelocityX = 0;
            }

            //手の射出
            if (Input.GetButtonDown(DX.PAD_INPUT_10))
            {
                playScene.hund = new Hund(this, PlayerPosition.x, PlayerPosition.y);
                angle = playerArraw.ArrawAngle + 180.0f;                
                HundFrag = true;

                if(angle < 90)
                {
                    NowHundFrag = true;
                    FirstAngle = playerArraw.ArrawAngle + 180.0f;
                    LastAngle = playerArraw.ArrawAngle + 180.0f + (90 - (playerArraw.ArrawAngle + 180) % 91) * 2;
                }
                else 
                {
                    NowHundFrag = false;
                    FirstAngle = playerArraw.ArrawAngle + 180.0f;
                    LastAngle = playerArraw.ArrawAngle + 180.0f - (90 - (playerArraw.ArrawAngle + 180) % 91) * 2;
                }
            }
        }

        void MoveX()
        {
            PlayerPosition.x += VelocityX;
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
                SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
            }
            //右端が壁にめりこんでいるか？
            else if (
                playScene.map.IsWall(right, top) ||　　　//左上が壁か？
                playScene.map.IsWall(right, middle) ||     //左真ん中は壁か？
                playScene.map.IsWall(right, bottom))     //左下が壁か？
            {

                float wallLeft = right - right % Map.CellSize;//壁の左端
                SetRight(wallLeft);//プレイヤーの左端を壁の右端に沿わす
            }
        }

        void MoveY()
        {
            // 縦に移動する 
            PlayerPosition.y += VelocityY;            

            // 着地したかどうか 
            bool grounded = false;

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
            }
            // 下端が壁にめりこんでいるか？ 
            else if (
                playScene.map.IsWall(left, bottom) || // 左下が壁か？ 
                playScene.map.IsWall(Center1, bottom) ||
                playScene.map.IsWall(Center2, bottom) ||
                playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                grounded = true; // 着地した 
            }

            if (grounded) // もし着地してたら 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                jumpState = JumpState.Walk;
            }
            else // 着地してなかったら 
            {
                jumpState = JumpState.Jump; // 状態をジャンプ中に 
            }
        }

        //描画処理
        public void Draw()
        {
            DX.DrawGraphF(PlayerPosition.x, PlayerPosition.y, Image.PlayerImage01);
            playerArraw.Draw();
        }

        //あたり判定？
        public void OnCollision()//あたり判定の対象)
        {

        }

        //当たり判定の左端を取得
        public virtual float GetLeft()
        {
            return PlayerPosition.x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public virtual void SetLeft(float left)
        {
            PlayerPosition.x = left - hitboxOffsetLeft;
        }

        //当たり判定の右端を取得
        public virtual float GetRight()
        {
            return PlayerPosition.x + imageWidth - hitboxOffsetRight;
        }

        //右端を指定することにより位置を設定する
        public virtual void SetRight(float right)
        {
            PlayerPosition.x = right + hitboxOffsetRight - imageWidth;
        }

        //当たり判定の上端を取得
        public virtual float GetTop()
        {
            return PlayerPosition.y + hitboxOffsetTop;
        }

        //上端を指定することにより位置を設定する
        public virtual void SetTop(float top)
        {
            PlayerPosition.y = top - hitboxOffsetTop;
        }

        //当たり判定の下端を取得する
        public virtual float GetBottom()
        {
            return PlayerPosition.y + imageHeight - hitboxOffsetBotton;
        }

        //下端を指定することにより位置を設定する
        public virtual void SetBottom(float bottom)
        {
            PlayerPosition.y = bottom + hitboxOffsetBotton - imageHeight;
        }
    }
}

