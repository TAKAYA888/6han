using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

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
        public float x;             //x座標
        public float y;             //y座標
        public bool isDead = false; //死亡フラグ(true)だと死ぬ
        float VelocityX = 0f;       //移動速度(x方向)
        float VelocityY = 0f;　　　 //移動速度(y方向)
        public int HP = 3;          //HP(体力)

        //-----------------------------------------------------------------------------------

        //固定変数系-------------------------------------------------------------------------
        readonly float WalkSpeed = 4f;
        readonly float Gravity = 0.6f;
        //-----------------------------------------------------------------------------------


        //サイズ関係-------------------------------------------------------------------------
        int imageWidth = 180;//画像の横ピクセル数
        int imageHeight = 240;//画像の縦ピクセル数
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

        JumpState jumpState;
        PlayScene playScene;
        
        //コンストラクタ
        public Player(PlayScene playScene,float x,float y)
        {
            this.playScene = playScene;
            this.x = x;
            this.y = y;
        }

        //毎フレームの更新処理
        public void Update()
        {
            HundleInput();

            // 重力による落下 
            VelocityY += Gravity;            

            MoveX();
            MoveY();
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
        }

        void MoveX()
        {
            x += VelocityX;
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
            y += VelocityY;            

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
            DX.DrawBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0),1);
        }

        //あたり判定？
        public void OnCollision()//あたり判定の対象)
        {

        }


        //当たり判定の左端を取得
        public virtual float GetLeft()
        {
            return x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public virtual void SetLeft(float left)
        {
            x = left - hitboxOffsetLeft;
        }

        //当たり判定の右端を取得
        public virtual float GetRight()
        {
            return x + imageWidth - hitboxOffsetRight;
        }

        //右端を指定することにより位置を設定する
        public virtual void SetRight(float right)
        {
            x = right + hitboxOffsetRight - imageWidth;
        }

        //当たり判定の上端を取得
        public virtual float GetTop()
        {
            return y + hitboxOffsetTop;
        }

        //上端を指定することにより位置を設定する
        public virtual void SetTop(float top)
        {
            y = top - hitboxOffsetTop;
        }

        //当たり判定の下端を取得する
        public virtual float GetBottom()
        {
            return y + imageHeight - hitboxOffsetBotton;
        }

        //下端を指定することにより位置を設定する
        public virtual void SetBottom(float bottom)
        {
            y = bottom + hitboxOffsetBotton - imageHeight;
        }
    }
}
