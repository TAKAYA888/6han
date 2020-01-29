using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath_KNMR;
using DxLibDLL;

namespace ActionGame
{
    public abstract class playerObject
    {
        public Vector2 Position;                 //座標
        public bool isDead = false;              //死亡フラグ

        protected PlayScene playScene;           //Playerの参照
        public int ImageWidth;                //画像の横ピクセル数
        public int ImageHeight;               //画像の縦ピクセル数
        protected int hitboxOffsetLeft = 0;　　　//当たり判定の左端のオフセット
        protected int hitboxOffsetRight = 0;     //当たり判定の右側のオフセット
        protected int hitboxOffsetTop = 0;       //当たり判定の上側のオフセット
        protected int hitboxOffsetBottom = 0;    //当たり判定の下側のオフセット

        float prevX;      // 1フレーム前のx座標 
        float prevY;      // 1フレーム前のy座標 
        float prevLeft;   // 1フレーム前の左端 
        float prevRight;  // 1フレーム前の右端 
        float prevTop;    // 1フレーム前の上端 
        float prevBottom; // 1フレーム前の下端 

        //コンストラクタ
        public playerObject(PlayScene playScene)
        {
            this.playScene = playScene;
        }

        //当たり判定の左側を取得
        public virtual float GetLeft()
        {
            return Position.x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public virtual void SetLeft(float left)
        {
            Position.x = left - hitboxOffsetLeft;
        }

        //右端を取得
        public virtual float GetRight()
        {
            return Position.x + ImageWidth - hitboxOffsetRight;
        }

        //右端を指定することにより位置を指定する
        public virtual void SetRight(float right)
        {
            Position.x = right + hitboxOffsetRight - ImageWidth;
        }

        //上端を取得
        public virtual float GetTop()
        {
            return Position.y + hitboxOffsetTop;
        }

        //上側を指定することにより位置を指定する
        public virtual void SetTop(float top)
        {
            Position.y = top - hitboxOffsetTop;
        }

        //下側を取得する
        public virtual float GetBottom()
        {
            return Position.y + ImageHeight - hitboxOffsetBottom;
        }

        //下側を指定することにより位置を指定する
        public virtual void SetBottom(float bottom)
        {
            Position.y = bottom + hitboxOffsetBottom - ImageHeight;
        }

        // 1フレーム前からの移動量（x方向） 
        public float GetDeltaX()
        {
            return Position.x - prevX;
        }

        // 1フレーム前からの移動量（y方向） 
        public float GetDeltaY()
        {
            return Position.y - prevY;
        }

        // 1フレーム前の左端を取得する 
        public float GetPrevLeft()
        {
            return prevLeft;
        }

        // 1フレーム前の右端を取得する 
        public float GetPrevRight()
        {
            return prevRight;
        }

        // 1フレーム前の上端を取得する 
        public float GetPrevTop()
        {
            return prevTop;
        }

        // 1フレーム前の下端を取得する 
        public float GetPrevBottom()
        {
            return prevBottom;
        }

        // 1フレーム前の場所と当たり判定を記憶する 
        public void StorePostionAndHitBox()
        {
            prevX = Position.x;
            prevY = Position.y;
            prevLeft = GetLeft();
            prevRight = GetRight();
            prevTop = GetTop();
            prevBottom = GetBottom();
        }

        //更新処理
        public abstract void Update();

        //描画処理
        public abstract void Draw();

        // 当たり判定を描画（デバッグ用） 
        public void DrawHitBox()
        {
            // 四角形を描画 
            //DX.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
            Camera.DrawLineBox(GetLeft(), GetTop(), GetRight(), GetBottom(), DX.GetColor(255, 0, 0));
        }

        public abstract void OnCollision(EnemyObject enemyObject);

        public abstract void OnCollisionI(ItemObject itemObject);

        public abstract void OnCollisionG(GimmickObject gimmickObject);

        //public abstract void OnCollisionHand(Hand hund);

        public abstract void OnCollisionP(playerObject player);

        public virtual void OnCollisionEB(EnemyBullet enemyBullet)
        {
        }

    }
}
