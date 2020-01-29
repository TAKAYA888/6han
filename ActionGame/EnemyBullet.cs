using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public class EnemyBullet
    {
        const float VisibleRadiius = 8f;//見た目の半径

        public float x;//x座標
        public float y;//y座標
        public bool isDead = false;//死亡フラグ
        public float collisionRadius = 8f;//当たり判定半径
        protected int imageWidth;//画像の横ピクセル数
        protected int imageHeight;//画像の縦ピクセル数
        protected int hitboxOffsetLeft = 0;//当たり判定の左端のオフセット
        protected int hitboxOffsetRight = 0;//当たり判定の右側のオフセット
        protected int hitboxOffsetTop = 0;//当たり判定の上側のオフセット
        protected int hitboxOffsetBottom = 0;//当たり判定の下側のオフセット

        float vx;//x方向移動速度
        float vy;//y方向移動速度

        public EnemyBullet(float x, float y, float angle, float speed)
        {
            this.x = x;
            this.y = y;

            //角度からx方向の移動速度を算出
            vx = (float)Math.Cos(angle) * speed;
            //角度からy方向の移動速度を算出
            vy = (float)Math.Sin(angle) * speed;
        }

        public float GetLeft()
        {
            return x + hitboxOffsetLeft;
        }

        //左端を指定することにより位置を設定する
        public void SetLeft(float left)
        {
            x = left - hitboxOffsetLeft;
        }

        //右端を取得
        public float GetRight()
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
        public void SetTop(float top)
        {
            y = top - hitboxOffsetTop;
        }

        //下側を取得する
        public float GetBottom()
        {
            return y + imageHeight - hitboxOffsetBottom;
        }

        //下側を指定することにより位置を指定する
        public void SetBottom(float bottom)
        {
            y = bottom + hitboxOffsetBottom - imageHeight;
        }

        //更新処理
        public void Update()
        {
            //速度の分だけ移動
            x += vx;
            y += vy;

            //画面外に出たら死亡フラグを立てる
            if (y + VisibleRadiius < 0 || y - VisibleRadiius > Screen.Height ||
                x + VisibleRadiius < 0 || x - VisibleRadiius > Screen.Width)
            {
                //isDead = true;
            }
        }

        //描画処理
        public void Draw()
        {
            Camera.DrawGraph(x, y, Image.Enemy_shot);
            //あたり判定です
            Camera.DrawLineBox(x, y, x + 16, y + 16, DX.GetColor(255, 0, 0));
        }
        
        public void OnCollisionP(playerObject playerObject)
        {
            isDead = true;
        }
    }
}
