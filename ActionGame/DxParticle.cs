using DxLibDLL;
using System;
using MyLib;
using MyMath_KNMR;


namespace ActionGame
{
    /// <summary>
    /// パーティクルの状態種別
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 発生前の待機状態。処理や描画はしない
        /// </summary>
        Sleep,
        /// <summary>
        ///  通常の状態
        /// </summary>
        Active,
        /// <summary>
        /// 死んだ状態。除去対象
        /// </summary>
        Dead,
    }

    //パーティクルの粒子一粒を表すクラス
    public class DxParticle
    {
        //public bool isDead = false;                     //死んでいるフラグ
        public float positionX;                         //中心座標のｘ
        public float positionY;                         //中心座標のｙ
        public int lifeSpan;                            //寿命（フレーム）
        public int imageHandle;                         //画像ハンドル
        public float Vy;                                //縦の移動速度
        public float Vx;                                //横の移動速度
        public float forceX;                            //横方向の外力(風など)
        public float forceY;                            //縦方向の外力（重力や浮力）
        public float startScale = 1f;                   // 開始時の拡大率
        public float endScale = 1f;                     // 終了時の拡大率
        public int red = 255;                           // 赤
        public int green = 255;                         // 緑
        public int blue = 255;                          // 青
        //public int startAlpha = 255;                  //開始時のアルファ値
        //public int endAlpha = 255;                    //終了時のアルファ値
        public float angle;                             //画像の向き(ラジアン)
        public float angularVelocity;                   // 回転速度（ラジアン/フレーム）
        public float angularDamp = 1f;                  // 回転速度の減衰（維持率）
        public float damp = 1.0f;                       //空気抵抗による速度の減衰（速度の維持率）
        public int blendMode = DX.DX_BLENDMODE_ALPHA;   //ブレンドモード
        public int delay = 0;                           // 発生までの遅延時間（フレーム）
        public State state = State.Sleep;               // パーティクルの状態
        public int alpha = 255;                         // フェードインもフェードアウトもしていないときの基本のアルファ値 
        public float fadeInTime = 0f;                   // フェードインにかける時間（0～1で指定） 
        public float fadeOutTime = 0f;                  // フェードアウトにかける時間（0～1で指定） 
        public Action<DxParticle> OnUpdate;               // Update時に実行したい処理
        public Action<DxParticle> OnDeath;                // 死ぬときに実行したい処理

        private int age = 0;                            //生まれてからの経過時間
        //private float scale;                          // 現在の拡大率
        //private int alpha = 255;                      //現在のアルファ値

        public void Update()
        {
            // 発生までの遅延時間が設定されている場合は、何もせず待機
            if (delay > 0)
            {
                delay--; // デクリメント
                return; // 何もしないで終了
            }

            ++age;      //経過時間をインクリメント

            //寿命を超えたら死亡する
            if (age > lifeSpan)
            {
                state = State.Dead;

                // 死亡時のカスタム処理
                if (OnDeath != null)
                    OnDeath(this);

                return;
            }

            //進捗率
            //float progressRate = (float)age / lifeSpan;

            //拡大率を計算
            //scale = MyMath.Lerp(startScale, endScale, progressRate);

            //外力を適用
            Vy += forceY;
            Vx += forceX;

            //空気抵抗による速度の減衰
            Vx *= damp;
            Vy *= damp;

            //移動速度を座標に足す
            positionX += Vx;
            positionY += Vy;

            // 回転速度の減衰
            angularVelocity *= angularDamp;

            // 回転速度の分だけ回転
            angle += angularVelocity;

            // カスタムUpdate処理を実行
            if (OnUpdate != null)
                OnUpdate(this);

            // アルファ値の計算
            //alpha = (int)MyMath.Lerp(startAlpha, endAlpha, progressRate);
        }

        public void Draw()
        {
            // アクティブじゃないパーティクルは描画しない
            if (state != State.Active) return;

            // 色を指定
            DX.SetDrawBright(red, green, blue);
            //アルファ値を設定
            DX.SetDrawBlendMode(blendMode, alpha);

            // 進捗率 
            float progressRate = (float)age / lifeSpan;

            // 拡大率を計算 
            float scale = MyMath.Lerp(startScale, endScale, progressRate);

            // アルファ値の計算 
            int currentAlpha = (int)(Math.Min(Math.Min(progressRate / fadeInTime, (1f - progressRate) / fadeOutTime), 1f) * alpha);

            //描画する
            DX.DrawRotaGraphFastF(positionX, positionY, currentAlpha, angle, imageHandle);

            // アルファ値を元に戻す
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            // 色を元に戻す
            DX.SetDrawBright(255, 255, 255);
        }

        /// <summary>
        /// 現在のスケール
        /// </summary>
        public float currentScale
        {
            get
            {
                float progressRate = (float)age / lifeSpan;
                return MyMath.Lerp(startScale, endScale, progressRate);
            }
        }

        /// <summary>
        /// 現在のアルファ値
        /// </summary>
        public int currentAlpha
        {
            get
            {
                float progressRate = (float)age / lifeSpan;
                return (int)(Math.Min(Math.Min(progressRate / fadeInTime, (1f - progressRate) / fadeOutTime), 1f) * alpha);
            }
        }
    }
}
