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

        public abstract void OnCollisionHand(Hund hund);

    }
}
