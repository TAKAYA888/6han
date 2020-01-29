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

        public enum State
        {
            Normal,
            Speed,
        }
        //-----------------------------------------------------------------------------------

        //ステータス関係の変数---------------------------------------------------------------               
        float VelocityX = 0f;                           //移動速度(x方向)
        float VelocityY = 0f;　　　                     //移動速度(y方向)
        //float flyVelocityX = 0f;
        public int PlayerStateNumber;
        public int HP;                                  //HP(体力)
        public bool HundFrag;                           //手がくっついたかのフラグ
        public bool BeforHundFrag = false;　　　      　//一個前の手がくっついたかのフラグ
        public bool FrastHandAngleFrag = false;　　　 　//手のくっついた時の角度が90度以上かそうでないかの判断を行うためのフラグ
        //float Distance;　　　　　　　　　　　　　       //手とこいつの距離
        float angle = MathHelper.toRadians(315);     　 //手との角度
        float FirstAngle;                               //角度を制限するための変数
        float LastAngle;　　　　　　　　　　　　     　 //角度を制限するための変数
        float angleSpeed;　　　　　　　　            　 //角度を変えるスピード
        int mutekiTimer;                                //無的時間を管理するための変数                
        public int HandDestroyTimer = 0;                //手がPlayerに当たっても消さないためのタイマー
        public int AngleSpeedStopTimer = 0;
        float DistanceSpeed;                            //手とプレイヤーの距離を縮める速さ
        int AnimationTimer;                             //アニメーション用のタイマー
        int EndTimer;
        public static int ScorePoint = 0;               //スコアポイントの変数


        public int haveWoolenYarn = 0;                  //持っている毛糸の数

        //-----------------------------------------------------------------------------------

        //固定変数系-------------------------------------------------------------------------
        readonly int MaxHP = 3;
        readonly float WalkSpeed = 4f;　　　　　　//歩く速さ
        readonly float Gravity = 0.6f;　　　　　　//重力加速度
        readonly float MaxFallSpeed = 12;         //落ちる速度の最高速
        readonly int mutekitime = 60;             //無的時間の長さ
        public int HandDestroyTime = 20;          //発射後どのくらいで手を消すか？
        public readonly int AngleSpeedStopTime = 60;
        //-----------------------------------------------------------------------------------


        //フラグ関係--------------------------------------------------------------------------------------------------------------
        bool UpAndDownHit = false;                      //上下が当たっているかどうかの確認です
        bool LeftAndRightHit = false;                   //左右が当たっているかどうかの確認です
        bool gennsokuFrag = true;                       //振り子運動中にどっちの方向に進んでいるかを判断するためのフラグ
        bool FacingRight = true;
        //------------------------------------------------------------------------------------------------------------------------        

        public static State state;
        //JumpState jumpState;                //ジャンプステートの初期化
        //public PlayScene playScene;       　//playSceneの宣言
        public PlayerArraw playerArraw;     //矢印の宣言
        public Hand hand;
        GimmickObject groundObject = null; // 乗っている床オブジェクト 

        //コンストラクタ
        public Player(PlayScene playScene, float x, float y) : base(playScene)
        {
            //this.playScene = playScene;　　　　　　　　　　　　　　　//PlaySceneの受け取り
            Position.x = x;　　　　　　　　　　　　　　　　　　　　　//初期座標の設定
            Position.y = y;　　　　　　　　　　　　　　　　　　　　　//上と同じ
            HundFrag = false;　　　　　　　　　　　　　　　　　　　　//最初のハンドフラグの設定
            playerArraw = new PlayerArraw(this, Position);  　　　　 //矢印の生成
            state = State.Normal;
            PlayerStateNumber = 0;
            HP = MaxHP;
            ScorePoint = 0;
            //サイズ関係-------------------------------------------------------------------------
            ImageWidth = 110;    　　　　　　　　　　　　　　 　　　　//画像の横ピクセル数
            ImageHeight = 180;   　　　　　　　　　　　　　　　　　　 //画像の縦ピクセル数
            hitboxOffsetLeft = -ImageWidth / 2+20 ; 　　　　　　　        //当たり判定のオフセット
            hitboxOffsetRight = ImageWidth / 2+20;                       //当たり判定のオフセット
            hitboxOffsetTop = -ImageHeight / 2 +25;                       //当たり判定のオフセット
            hitboxOffsetBottom = ImageHeight / 2;                     //当たり判定のオフセット
        }

        //毎フレームの更新処理
        public override void Update()
        {
            if (HP < 0)
            {
                HP = 0;

            }

            if (HP != 0)
            {

                if(PlayerStateNumber==1)
                {
                    state = State.Speed;
                }
                ////HPが無くなると死ぬ
                //if (HP <= 0)
                //{
                //    isDead = true;
                //}

                //速度をリセットする
                VelocityX = 0;

                //無的時間のカウントダウン
                mutekiTimer--;

                //0以下にならないようにする、時間を0にする
                if (mutekiTimer < 0) mutekiTimer = 0;

                //
                //if (BeforHundFrag && !HundFrag)
                //{
                //    flyVelocityX = MathHelper.cos(angle) * WalkSpeed * 4;
                //}

                //手を殺すためのタイマーでてきてすぐに殺さないため
                HandDestroyTimer--;
                //0以下にしない            
                HandDestroyTimer = HandDestroyTimer < 0 ? 0 : HandDestroyTimer;

                AngleSpeedStopTimer--;

                if (AngleSpeedStopTimer < 0)
                {
                    AngleSpeedStopTimer = 0;
                }

                AnimationTimer++;

                //一個前のハンドフラグを代入
                BeforHundFrag = HundFrag;
                //ゲーム上に手が存在しなかったら
                if (!HundFrag)
                {
                    if (Input.GetButton(DX.PAD_INPUT_LEFT))
                    {
                        //左が押されたら、速度に「WalkSpeed」を引く
                        VelocityX += -WalkSpeed;
                        //アニメーションカウントアップ


                        FacingRight = false;
                    }
                    else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                    {
                        //左が押されたら、速度に「WalkSpeed」を引く                    
                        VelocityX += WalkSpeed;

                        FacingRight = true;
                    }
                    else
                    {
                        //速度を0にする
                        VelocityX += 0;
                        if (HP != 1)
                        {
                            AnimationTimer = 0;
                        }
                    }

                    if (Input.GetButtonDown(DX.PAD_INPUT_3) && haveWoolenYarn != 0)
                    {
                        HP = MaxHP;
                        haveWoolenYarn--;
                        Game.particleManager.Heal(Position.x, Position.y);
                    }

                    // 重力による落下 
                    VelocityY += Gravity;
                }
                else
                {
                    //腕が壁とくっついたら
                    //かつ角度が90以上なら　：　90だとradが0になるから
                    if (hand.HundHitFrag)
                    {
                        //手とPlayerの距離を縮めています
                        if (hand.Distance > 100)
                        {
                            //手とPlayerの距離を縮めています
                            hand.Distance -= DistanceSpeed;
                        }
                        else
                        {
                            //これ以上短くしない
                            hand.Distance = 100;
                        }

                        if (angle % 90 != 0)
                        {
                            //振り子の減衰についてのプログラミング
                            PendulumDecay();
                        }

                        //角度の変更
                        angle += angleSpeed;

                        //回転時の移動処理
                        Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(hand.Distance, 0))
                            * Matrix3.createRotation(angle)
                            * Matrix3.createTranslation(hand.Position);

                        //座標の設定
                        Vector2 NextPosition = new Vector2(0) * NextPlayerPos;

                        //移動量に足す
                        VelocityX = NextPosition.x - Position.x;
                        VelocityY = NextPosition.y - Position.y;

                        if (Input.GetButtonDown(DX.PAD_INPUT_3))
                        {
                            DistanceSpeed = 10;
                        }

                        //if(Input.GetButton(DX.PAD_INPUT_LEft
                                            //))

                        //手を消す
                        if (Input.GetButtonDown(DX.PAD_INPUT_2))
                        {
                            HundFrag = false;
                            hand.HundHitFrag = false;
                            hand.isDead = true;
                        }
                    }

                    // 重力による落下 
                    VelocityY += Gravity / 10;
                }
                if (VelocityY >= MaxFallSpeed)
                {
                    VelocityY = MaxFallSpeed;
                }

                //入力処理
                HundleInput();

                //縦移動
                MoveY();
                //横移動
                MoveX();

                //矢印の更新処理
                playerArraw.Update();

                // 乗っている床の情報を破棄 
                groundObject = null;

                Camera.LookAt(Position);
            }
            else
            {
                EndTimer++;

                if (EndTimer > 240)
                {
                    isDead = true;
                }
            }
        }

        //入力関係の処理を行います
        void HundleInput()
        {
            //手の射出
            if (Input.GetButtonDown(DX.PAD_INPUT_10) || Input.GetButtonDown(DX.PAD_INPUT_1) && !HundFrag)
            {
                Hand hand_A = new Hand(playScene, Position.x, Position.y);

                hand = hand_A;

                playScene.playerObjects.Add(hand_A);

                HandDestroyTimer = HandDestroyTime;

                //手を縮めるスピード
                DistanceSpeed = 10;

                gennsokuFrag = true;

                //回転時の移動処理
                Matrix3 NextPlayerPos = Matrix3.createTranslation(new Vector2(50, 0))
                    * Matrix3.createRotation(playerArraw.ArrawAngle)
                    * Matrix3.createTranslation(Position);

                Vector2 effectpos = new Vector2(0) * NextPlayerPos;

                Game.particleManager.ShockWave(effectpos.x, effectpos.y, MathHelper.toRadians(playerArraw.ArrawAngle));

                SE.Play(SE.ArmShotSE);

                //初期角度
                angle = playerArraw.ArrawAngle + 180.0f;
                //ハンドフラグをTrueに
                HundFrag = true;
                //角度を360度以上にならないように制限
                angle = angle % 360;
                //flyVelocityX = 0;

                //角度の初期設定
                if (angle < 90)
                {
                    FrastHandAngleFrag = true;
                    FirstAngle = angle % 360;
                    LastAngle = (FirstAngle + (90 - FirstAngle % 90) * 2) % 360;
                    angleSpeed = 0.1f;
                }
                else if (angle % 90 == 0)
                {
                    angleSpeed = 0;
                }
                else
                {
                    FrastHandAngleFrag = false;
                    FirstAngle = (playerArraw.ArrawAngle + 180.0f) % 360;
                    LastAngle = (FirstAngle - (FirstAngle % 90) * 2) % 360;
                    angleSpeed = -0.1f;
                }
            }
        }

        void MoveX()
        {
            //当たってない状態にもどす
            LeftAndRightHit = false;

            // 床オブジェクトに乗っている場合は、その床の移動量だけ移動させる 
            if (groundObject != null)
            {
                VelocityX += groundObject.GetDeltaX();
            }

            //positionに速度を足す
            Position.x = Position.x + VelocityX/* + flyVelocityX*/;

            //当たり判定の四隅の座標を取得
            float left = GetLeft() + 0.01f;
            float right = GetRight() - 0.01f;
            float top = GetTop();
            float middle1 = top + 30;
            float middle2 = top + 30 * 2;
            float middle3 = top + 30 * 3;
            float bottom = GetBottom() - 0.01f;

            //左端が壁にめり込んでいるか？
            if (playScene.map.IsWall(left, top) || //左上が壁か？
                playScene.map.IsWall(left, middle1) ||//左真ん中は壁か？
                playScene.map.IsWall(left, middle2) ||
                playScene.map.IsWall(left, middle3) ||
                playScene.map.IsWall(left, bottom))   //左下が壁か？
            {
                if (HandDestroyTimer <= 0)
                {
                    float _wallRight = left - left % Map.CellSize + Map.CellSize;//壁の右端
                    SetLeft(_wallRight);//プレイヤーの左端を右の壁に沿わす
                }
                //フラグをオンにする
                LeftAndRightHit = true;
            }
            //右端が壁にめりこんでいるか？
            else if (
                playScene.map.IsWall(right, top) ||　　　//左上が壁か？
                playScene.map.IsWall(right, middle1) ||
                playScene.map.IsWall(right, middle2) ||  //左真ん中は壁か？
                playScene.map.IsWall(right, middle3) ||
                playScene.map.IsWall(right, bottom))     //左下が壁か？
            {
                if (HandDestroyTimer <= 0)
                {
                    float wallLeft = right - right % Map.CellSize;//壁の左端
                    SetRight(wallLeft);//プレイヤーの左端を壁の右端に沿わす
                }
                //フラグをオンにする
                LeftAndRightHit = true;
            }

            //左右が壁にあたっているか？
            if (LeftAndRightHit)
            {
                VelocityX = 0;
                angleSpeed = -angleSpeed;
                gennsokuFrag = !gennsokuFrag;

                if (angle > 135.0f && angle < 45.0f)
                {
                    //これ以上距離を縮めないようにする
                    DistanceSpeed = 0;
                }
            }
        }

        void MoveY()
        {
            // 床オブジェクトに乗っている場合は、その床の移動量だけ移動させる 
            if (groundObject != null)
            {
                VelocityY += groundObject.GetDeltaY();
            }

            // 縦に移動する 
            Position.y += VelocityY;

            // 着地したかどうか 
            bool grounded = false;

            // 当たり判定の四隅の座標を取得 
            float left = GetLeft();
            float right = GetRight() - 0.01f;
            float Center1 = left + 30.0f;
            float Center2 = left + 30.0f * 2.0f;
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
                DistanceSpeed = 0;
            }
            // 下端が壁にめりこんでいるか？ 
            else if (
                playScene.map.IsWall(left, bottom) || // 左下が壁か？ 
                playScene.map.IsWall(Center1, bottom) ||
                playScene.map.IsWall(Center2, bottom) ||
                playScene.map.IsWall(right, bottom))   // 右下が壁か？ 
            {
                grounded = true;
            }

            if (grounded) // もし着地してたら 
            {
                float wallTop = bottom - bottom % Map.CellSize - 0.1f; // 床のy座標 
                SetBottom(wallTop); // プレイヤーの足元を床の高さに沿わす 
                VelocityY = 0; // 縦の移動速度を0に                 
                //flyVelocityX = 0;
                if (hand != null && hand.HundHitFrag && AngleSpeedStopTimer <= 0)
                {
                    angleSpeed = 0;
                }
            }
        }


        void PendulumDecay()
        {
            if (FrastHandAngleFrag)
            {
                //角速度変更の変更
                if (LastAngle < angle || FirstAngle > angle)
                {
                    angleSpeed = -angleSpeed;
                    gennsokuFrag = !gennsokuFrag;
                }

                //角度で減速する
                //稼働域の半分　中間の角度を求める
                float rad = (LastAngle - FirstAngle) / 2;

                //左方向に進むとき
                if (!gennsokuFrag)
                {
                    if (LastAngle - rad >= angle)
                    {
                        angleSpeed = angleSpeed - (angleSpeed * 2 / rad);
                    }
                    if (FirstAngle + rad < angle)
                    {
                        angleSpeed = angleSpeed + (angleSpeed * 2 / rad);
                    }
                }
                else
                {
                    if (LastAngle - rad >= angle)
                    {
                        angleSpeed = angleSpeed + (angleSpeed * 2 / rad);
                    }
                    if (FirstAngle + rad < angle)
                    {
                        angleSpeed = angleSpeed - (angleSpeed * 2 / rad);
                    }
                }

                //if (Input.GetButton(DX.PAD_INPUT_LEFT))
                //{
                //    angleSpeed -= 0.01f;
                //}
                //else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                //{
                //    angleSpeed += 0.01f;
                //}

                if (angleSpeed > -0.1f && angleSpeed < 0.1f)
                {
                    if (FirstAngle + rad < angle && gennsokuFrag)
                    {
                        angleSpeed = -angleSpeed;
                        gennsokuFrag = !gennsokuFrag;
                    }
                    else if (LastAngle - rad >= angle && !gennsokuFrag)
                    {
                        angleSpeed = -angleSpeed;
                        gennsokuFrag = !gennsokuFrag;
                    }
                }
            }
            else
            {
                //上と同じ
                if (LastAngle > angle || FirstAngle < angle)
                {
                    angleSpeed = -angleSpeed;
                    gennsokuFrag = !gennsokuFrag;
                }

                //角度で減速する
                //稼働域の半分　中間の角度を求める
                float rad = (FirstAngle - LastAngle) / 2;

                //右方向に進むとき
                if (gennsokuFrag)
                {
                    if (LastAngle + rad >= angle)
                    {
                        angleSpeed = angleSpeed - (angleSpeed * 2 / rad);
                    }
                    if (FirstAngle - rad < angle)
                    {
                        angleSpeed = angleSpeed + (angleSpeed * 2 / rad);
                    }
                }
                else
                {
                    if (LastAngle + rad >= angle)
                    {
                        angleSpeed = angleSpeed + (angleSpeed * 2 / rad);
                    }
                    if (FirstAngle - rad < angle)
                    {
                        angleSpeed = angleSpeed - (angleSpeed * 2 / rad);
                    }
                }

                if (angleSpeed > -0.1f && angleSpeed < 0.1f)
                {
                    if (FirstAngle - rad < angle && !gennsokuFrag)
                    {
                        angleSpeed = -angleSpeed;
                        gennsokuFrag = !gennsokuFrag;
                    }
                    else if (LastAngle + rad >= angle && gennsokuFrag)
                    {
                        angleSpeed = -angleSpeed;
                        gennsokuFrag = !gennsokuFrag;
                    }
                }

                //if (Input.GetButton(DX.PAD_INPUT_LEFT))
                //{
                //    angleSpeed += 0.01f;
                //}
                //else if (Input.GetButton(DX.PAD_INPUT_RIGHT))
                //{
                //    angleSpeed -= 0.01f;
                //}
            }
        }

        //描画処理
        public override void Draw()
        {
            //右向きにするかどうかの変数
            int FacingRightNow;

            //bool分が入らなかったのでint型にしています
            if (FacingRight)
            {
                FacingRightNow = 0;
            }
            else
            {
                FacingRightNow = 1;
            }

            //何番目の画像を使うか判断するための変数です
            int PlayerImageNumber = 0;

            //手を射出しているかどうか
            if (HundFrag)
            {
                PlayerImageNumber = 4;
                if (HP <= MaxHP * 2 / 3)
                {
                    PlayerImageNumber++;
                }
            }
            //歩いてるかどうか
            else if (VelocityX != 0)
            {
                PlayerImageNumber = AnimationTimer / 10 % 4 + 8;
                if (HP <= MaxHP * 2 / 3)
                {
                    PlayerImageNumber += 4;
                }
            }
            else if (HP == 0)
            {
                PlayerImageNumber = 17;
            }
            else
            {
                PlayerImageNumber = 0;
                if (HP <= MaxHP * 2 / 3)
                {
                    PlayerImageNumber++;
                }
            }


            //無敵時に点滅するif分
            if (mutekiTimer / 5 % 3 < 2 && HP != 1)
            {
                if (state == State.Normal)
                {
                    Camera.DrawRotaGraph(Position.x, Position.y, 1, 0, Image.PlayerImage01[PlayerImageNumber], FacingRightNow);
                }
                else if (state == State.Speed)
                {
                    Camera.DrawRotaGraph(Position.x, Position.y, 1, 0, Image.PlayerImage02[PlayerImageNumber], FacingRightNow);
                }
            }
            else if (HP == 1)
            {
                if (AnimationTimer / 10 % 3 < 2)
                {
                    if (state == State.Normal)
                    {
                        Camera.DrawRotaGraph(Position.x, Position.y, 1, 0, Image.PlayerImage01[PlayerImageNumber], FacingRightNow);
                    }
                    else if (state == State.Speed)
                    {
                        Camera.DrawRotaGraph(Position.x, Position.y, 1, 0, Image.PlayerImage02[PlayerImageNumber], FacingRightNow);
                    }
                }
                else
                {
                    Camera.DrawRotaGraph(Position.x, Position.y, 1, 0, Image.PlayerImage03[PlayerImageNumber], FacingRightNow);
                }
            }

            if (hand != null && !hand.isDead)
            {
                Camera.DrawLine(Position, hand.Position);
                hand.Draw();
            }

            //矢印の描画
            playerArraw.Draw();
        }

        //あたり判定？
        public override void OnCollision(EnemyObject enemy)//あたり判定の対象)
        {
            if (mutekiTimer <= 0)
            {
                HP -= 1;
                mutekiTimer = mutekitime;
                if (HP == 0)
                {
                    HundFrag = false;
                    Game.particleManager.Slash(Position.x, Position.y, MathHelper.toRadians(45.0f));
                }
            }
            
        }

        public override void OnCollisionI(ItemObject item)//あたり判定の対象)
        {
            if (item is WoolenYarn)
            {
                haveWoolenYarn += 1;
                ScorePoint += 1000; //スコア
            }
            else if (item is SpeedUp)
            {
                state = State.Speed;
                PlayerStateNumber = 1;
                ScorePoint += 1000; //スコア
            }
           
        }

        public override void OnCollisionG(GimmickObject needle)
        {
            if (needle is NeedleObject)
            {
                VelocityX = 0;
                HundFrag = false;
                HP = 0;//無理やり即死させますスミマセン
                Game.particleManager.Claw(Position.x, Position.y);
            }
            else if (needle is MoveFloorObject)
            {
                groundObject = needle; // この床オブジェクトを覚えておく 
                if (GetPrevBottom() <= needle.GetPrevTop())
                {
                    SetBottom(needle.GetPrevTop());
                    VelocityY = 0;
                }
                //else if (GetPrevTop() <= needle.GetPrevBottom())
                //{
                //    VelocityY = 0;
                //    SetTop(needle.GetPrevBottom());
                //}
                //else if (GetPrevLeft() <= needle.GetPrevRight())
                //{
                //    SetLeft(needle.GetPrevRight());
                //    VelocityX = 0;
                //}
                //else if (GetPrevRight() <= needle.GetPrevLeft())
                //{
                //    SetRight(needle.GetPrevLeft());
                //    VelocityX = 0;
                //}                
            }
            else if (needle is KeyDoor)
            {
                if (GetPrevBottom() <= needle.GetPrevTop())
                {
                    //SetBottom(needle.GetPrevTop() - 1f);
                    VelocityY = 0;
                    //flyVelocityX = 0;
                }
            }
        }

        public override void OnCollisionP(playerObject playerObject)
        {
            if (playerObject is Hand)
            {
                if (HandDestroyTimer <= 0)
                {
                    HundFrag = false;
                    hand.isDead = true;
                }
            }
        }

        public void DrawHitPoint()
        {
            Camera.DrawHitBoxPoint((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 255, 255));
        }
    }
}