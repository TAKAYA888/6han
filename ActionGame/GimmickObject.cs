using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public abstract class GimmickObject
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
        public bool openFrag;               

        float prevX;      // 1フレーム前のx座標
        float prevY;      // 1フレーム前のy座標
        float prevLeft;   // 1フレーム前の左端
        float prevRight;  // 1フレーム前の右端
        float prevTop;    // 1フレーム前の上端
        float prevBottom; // 1フレーム前の下端

        //コンストラクタ
        public GimmickObject(PlayScene playScene)
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


        //1フレーム前からの移動量(x方向)
        public float GetDeltaX()
        {
            return x - prevX;
        }

        //1フレーム前からの移動量(y方向)
        public float GetDeltaY()
        {
            return y - prevY;
        }

        //1フレーム前の左側を取得
        public float GetPrevLeft()
        {
            return prevLeft;
        }

        //1フレーム前の右側を取得
        public float GetPrevRight()
        {
            return prevRight;
        }

        //1フレーム前の上側を取得
        public float GetPrevTop()
        {
            return prevTop;
        }

        //1フレーム前の下側を取得
        public float GetPrevBottom()
        {
            return prevBottom;
        }

        //1フレーム前の場所と当たり判定を記憶する
        public void StorePostionAndHitBox()
        {
            prevX = x;
            prevY = y;
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
            Camera.DrawLineBox((int)GetLeft(), (int)GetTop(), (int)GetRight(), (int)GetBottom(), DX.GetColor(255, 0, 0));
        }

        //他のオブジェクトと衝突したときに呼ばれる
        public abstract void OnCollision(playerObject playerObject);

        //画面内に映っているか？
        //public virtual bool IsVisible()
        //{
        //    return MyMath.RectRectIntersect(
        //        x, y, x + imageWidth, y + imageHeight,
        //        Camera.x, Camera.y, Camera.x + Screen.Width, Camera.y + Screen.Height);
        //}
    }
}
