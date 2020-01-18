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
                isDead = true;
            }
        }

        //描画処理
        public void Draw()
        {
            DX.DrawRotaGraphF(x, y, 1, 0f, Image.Enemy_shot);
        }
    }
}
