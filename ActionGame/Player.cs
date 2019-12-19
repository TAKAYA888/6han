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
    public class Player : playerObject
    {
        //ステータス一覧---------------------------------------------------------------------        
        public enum JumpState
        {
            Walk,//歩き、立ち
            Jump,//ジャンプ中
        }
        //-----------------------------------------------------------------------------------

        //ステータス関係の変数---------------------------------------------------------------               
        float VelocityX = 0f;                     //移動速度(x方向)
        float VelocityY = 0f;　　　               //移動速度(y方向)
        float flyVelocityX = 0f;
        public int HP = 1;                        //HP(体力)
        public bool HundFrag;                     //手がくっついたかのフラグ
        public bool BeforHundFrag = false;　　　　//一個前の手がくっついたかのフラグ
        public bool NowHundFrag = false;　　　　　//現在の手がくっついたかのフラグ（今のところはいらない）
        float Distance;　　　　　　　　　　　　　 //手とこいつの距離
        float angle = MathHelper.toRadians(315);　 //手との角度
        float FirstAngle;                          //角度を制限するための変数
        float LastAngle;　　　　　　　　　　　　　 //角度を制限するための変数
        float angleSpeed = 1.0f;　　　　　　　　　 //角度を変えるスピード
        int mutekiTimer;　　　　　　　　　　　　　 //無的時間を管理するための変数
        int handCount;

        public int haveWoolenYarn = 0;　　　　　　　　　　//持っている毛糸の数

        //-----------------------------------------------------------------------------------

        //固定変数系-------------------------------------------------------------------------
        readonly float WalkSpeed = 4f;　　　　　　//歩く速さ
        readonly float Gravity = 0.6f;　　　　　　//重力加速度
        readonly float MaxFallSpeed = 12;
        readonly int mutekitime = 60;　　　　　　 //無的時間の長さ
        //-----------------------------------------------------------------------------------


        //サイズ関係-------------------------------------------------------------------------       
        //float prevX;                 //1フレーム前のx座標
        //float prevY;                 //1フレーム前のy座標
        //float prevLeft;              //1フレーム前の左端
        //float prevRight;             //1フレーム前の右端
        //float prevTop;               //1フレーム前の上端
        //float prevBottom;            //1フレーム前の下端
        //-----------------------------------------------------------------------------------

        JumpState jumpState;         //ジャンプステートの初期化
        public PlayScene playScene;　//playSceneの宣言
        public PlayerArraw playerArraw;//矢印の宣言

        //コンストラクタ
        public Player(PlayScene playScene, float x, float y):base(playScene)
        {
            this.playScene = playScene;　　　　　　　　　　　　　　　//PlaySceneの受け取り
            Position.x = x;　　　　　　　　　　　　　　　　　　//初期座標の設定
            Position.y = y;　　　　　　　　　　　　　　　　　　//上と同じ
            HundFrag = false;　　　　　　　　　　　　　　　　　　　　//最初のハンドフラグの設定
            playerArraw = new PlayerArraw(this, Position);   //矢印の生成
                                                                   //サイズ関係-------------------------------------------------------------------------
            ImageWidth = 180;    //画像の横ピクセル数
            ImageHeight = 240;    //画像の縦ピクセル数
            hitboxOffsetLeft = 0;  //当たり判定のオフセット
            hitboxOffsetRight = 0;   //当たり判定のオフセット
            hitboxOffsetTop = 0;     //当たり判定のオフセット
            hitboxOffsetBottom = 0;  //当たり判定のオフセット
        }

        //毎フレームの更新処理
        public override void Update()
        {
            if(HP<=0)
            {
                isDead = true;
            }
            Camera.LookAt(Position);
            //無的時間のカウントダウン
            mutekiTimer--;
            //0以下にならないようにする
            if (mutekiTimer < 0)
            {
                //時間を0にする
                mutekiTimer = 0;
            }
            if (BeforHundFrag && !HundFrag)
            {
                flyVelocityX = MathHelper.cos(angle) * WalkSpeed * 4;
            }

            //一個前のハンドフラグを代入
            BeforHundFrag = HundFrag;
            //ゲーム上に手が存在しなかったら
            if (!HundFrag)
            {
                // 重力による落下 
                VelocityY += Gravity;                
            }           
            else
            {                
                //腕が壁とくっついたら
                if (playScene.hund.HundHitFrag)
                {

                    //手とPlayerの距離を縮めています
                    if (playScene.hund.Distance > 100)
                    {
                        //手とPlayerの距離を縮めています
                        playScene.hund.Distance -= 8f;
                    }
                    else
                    {
                        //これ以上短くしない
                        playScene.hund.Distance =100;
                    }
                    if (NowHundFrag)
                    {
                        //角速度変更の変更
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
                        //上と同じ
                        if (LastAngle > angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                        else if (FirstAngle < angle)
                        {
                            angleSpeed = -angleSpeed;
                        }
                    }

                    //角度の変更
                    angle += angleSpeed;

                    //回転時の移動処理
                    Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(playScene.hund.Distance, 0))
                        * Matrix3.createRotation(angle)
                        * Matrix3.createTranslation(playScene.hund.Position);

                    //座標の設定
                    Position = new Vector2(0) * NextPlayerPos;

                    //埋まらないように
                    if (playScene.hund.playerPosY < Position.y)
                    {
                        Position.y = playScene.hund.playerPosY - 1.0f;
                    }
                }
            }
            //入力処理
            HundleInput();

            //横移動
            MoveX();
            //縦移動
            MoveY();

            //矢印の更新処理
            playerArraw.Update();

            Camera.LookAt(Position);
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

            if (Input.GetButtonDown(DX.PAD_INPUT_2))
            {
                playScene.hund = null;
                HundFrag = false;
            }

            //手の射出
            if (Input.GetButtonDown(DX.PAD_INPUT_10))
            {                
                //初期角度
                angle = playerArraw.ArrawAngle + 180.0f;
                //ハンドフラグをTrueに
                HundFrag = true;
                //角度を360度以上にならないように制限
                angle = angle % 360;
                //速度を止める
                VelocityX = 0;
                VelocityY = 0;
                flyVelocityX = 0;

                //角度の初期設定
                if (angle < 90)
                {
                    NowHundFrag = true;
                    FirstAngle = (playerArraw.ArrawAngle + 180.0f) % 360;
                    LastAngle = (FirstAngle + (90 - FirstAngle % 90) * 2) % 360;
                    angleSpeed = 1;
                }
                else if (angle % 90 == 0)
                {
                    angleSpeed = 0;
                }
                else
                {
                    NowHundFrag = false;
                    FirstAngle = (playerArraw.ArrawAngle + 180.0f) % 360;
                    LastAngle = (FirstAngle - (FirstAngle % 90) * 2) % 360;
                    angleSpeed = -1;
                }
            }
        }

        void MoveX()
        {
            Position.x = Position.x + VelocityX + flyVelocityX;
            //当たり判定の四隅の座標を取得
            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle1 = top + 30;
            float middle2 = top + 30 * 2;
            float middle3 = top + 30 * 3;
            float middle4 = top + 30 * 4;
            float middle5 = top + 30 * 5;
            float middle6 = top + 30 * 6;
            float middle7 = top + 30 * 7;
            float bottom = GetBottom() - 0.01f;

            //左端が壁にめり込んでいるか？
            if (playScene.map.IsWall(left, top) || //左上が壁か？
                playScene.map.IsWall(left, middle1) ||//左真ん中は壁か？
                playScene.map.IsWall(left, middle2) ||
                playScene.map.IsWall(left, middle3) ||
                playScene.map.IsWall(left,middle4) ||
                playScene.map.IsWall(left,middle5) ||
                playScene.map.IsWall(left,middle6)||
                playScene.map.IsWall(left,middle7)||
                playScene.map.IsWall(left, bottom))   //左下が壁か？
            {
                //if(!HundFrag)
                //{
                //    float _wallRight = left - left % Map.CellSize + Map.CellSize;//壁の右端
                //    SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
                //}
                float _wallRight = left - left % Map.CellSize + Map.CellSize;//壁の右端
                SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
                angleSpeed = -angleSpeed;
            }
            //右端が壁にめりこんでいるか？
            else if (
                playScene.map.IsWall(right, top) ||　　　//左上が壁か？
                playScene.map.IsWall(right, middle1) ||
                playScene.map.IsWall(right, middle2) ||//左真ん中は壁か？
                playScene.map.IsWall(right, middle3) ||
                playScene.map.IsWall(right,middle4)||
                playScene.map.IsWall(right,middle5)||
                playScene.map.IsWall(right,middle6)||
                playScene.map.IsWall(right,middle7)||
                playScene.map.IsWall(right, bottom))     //左下が壁か？
            {
                //if (!HundFrag)
                //{
                //    
                //}
                float wallLeft = right - right % Map.CellSize;//壁の左端
                SetRight(wallLeft);//プレイヤーの左端を壁の右端に沿わす
                angleSpeed = -angleSpeed;
            }
        }

        void MoveY()
        {
            // 縦に移動する 
            Position.y += VelocityY;

            // 着地したかどうか 
            bool grounded = false;

            // 当たり判定の四隅の座標を取得 
            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float Center1 = left + 45f;
            float Center2 = left + 45 * 2.0f;
            float Center3 = left + 45 * 3.0f;
            float top = GetTop();
            float bottom = GetBottom() - 0.01f;

            // 上端が壁にめりこんでいるか？ 
            if (playScene.map.IsWall(left, top) || // 左上が壁か？ 
                playScene.map.IsWall(Center1, top) ||
                playScene.map.IsWall(Center2, top) ||
                playScene.map.IsWall(Center3, top) ||
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
                playScene.map.IsWall(Center3, bottom) ||
                playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                if (!HundFrag)
                {
                    grounded = true; // 着地した 
                }
                else
                {
                    grounded = false;
                }
            }

            if (grounded) // もし着地してたら 
            {
                float wallTop = bottom - bottom % Map.CellSize; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                VelocityY = 0; // 縦の移動速度を0に 
                jumpState = JumpState.Walk;
                flyVelocityX = 0;
                if (HundFrag && playScene.hund.HundHitFrag)
                {
                    HundFrag = false;
                    playScene.hund = null;
                }
            }
            else // 着地してなかったら 
            {
                jumpState = JumpState.Jump; // 状態をジャンプ中に 
            }
        }

        //描画処理
        public override void Draw()
        {
            if (mutekiTimer % 6 < 4)
            {
                Camera.DrawGraph(Position.x, Position.y, Image.PlayerImage01);
            }
            playerArraw.Draw();
        }

        //あたり判定？
        public override void OnCollision(EnemyObject enemy)//あたり判定の対象)
        {
            if (mutekiTimer <= 0)
            {
                HP -= 1;
                mutekiTimer = mutekitime;
            }
        }

        public override void OnCollisionI(ItemObject item)//あたり判定の対象)
        {
            if (item is WoolenYarn)
            {
                haveWoolenYarn += 1;
            }
        }

        public void OnCollisionG(GimmickObject needle)
        {
            if (needle is NeedleObject)
            {
                HP -= 10;//無理やり即死させますスミマセン
            }
        }        

        public void DrawHitPoint()
        {

            Camera.DrawHitBoxPoint((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 255, 255));            
        }
    }
}

