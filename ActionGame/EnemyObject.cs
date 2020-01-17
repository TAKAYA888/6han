using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public abstract class EnemyObject
    {

        public float x;//x座標
        public float y;//y座標
        public bool isDead = false;//死亡フラグ

        protected PlayScene playScene;//playsceneの参照
        protected int imageWidth;//画像の横ピクセル数
        protected int imageHeight;//画像の縦ピクセル数
        protected int hitboxOffsetLeft = 0;//当たり判定の左端のオフセット
        protected int hitboxOffsetRight = 0;//当たり判定の右側のオフセット
        protected int hitboxOffsetTop = 0;//当たり判定の上側のオフセット
        protected int hitboxOffsetBottom = 0;//当たり判定の下側のオフセット

        //コンストラクタ
        public EnemyObject(PlayScene playScene)
        {
            this.playScene = playScene;
        }

        //当たり判定の左側を取得
        public virtual float GetLeft()
        {
            return x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public virtual void SetLeft(float left)
        {
            x = left - hitboxOffsetLeft;
        }

        //右端を取得
        public virtual float GetRight()
        {
            return x + imageWidth - hitboxOffsetRight;
        }

        //右端を指定することにより位置を指定する
        public virtual void SetRight(float right)
        {
            x = right + hitboxOffsetRight - imageWidth;
        }

        //上端を取得
        public virtual float GetTop()
        {
            return y + hitboxOffsetTop;
        }

        //上側を指定することにより位置を指定する
        public virtual void SetTop(float top)
        {
            y = top - hitboxOffsetTop;
        }

        //下側を取得する
        public virtual float GetBottom()
        {
            return y + imageHeight - hitboxOffsetBottom;
        }

        //下側を指定することにより位置を指定する
        public virtual void SetBottom(float bottom)
        {
            y = bottom + hitboxOffsetBottom - imageHeight;
        }


        //更新処理
        public abstract void Update(Player player);

        //描画処理
        public abstract void Draw();

        // 当たり判定を描画（デバッグ用） 
        public void DrawHitBox()
        {
            // 四角形を描画 
            Camera.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
            //DX.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
        }

        public abstract void miniMapDraw();

        //他のオブジェクトと衝突したときに呼ばれる
        public abstract void OnCollision(playerObject player);

        public abstract void OnCollisionH(Hand hund);

        //画面内に映っているか？
        //public virtual bool IsVisible()
        //{
        //    return MyMath.RectRectIntersect(
        //        x, y, x + imageWidth, y + imageHeight,
        //        Camera.x, Camera.y, Camera.x + Screen.Width, Camera.y + Screen.Height);
        //}
    }
}
