using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath_KNMR;

namespace ActionGame
{
    //パーティクルを管理するクラス
    public class ParticleManager
    {
        //パーティクルを入れておくためのリスト
        List<DxParticle> particles = new List<DxParticle>();

        //全パーティクルを更新する
        public void Update()
        {
            foreach (DxParticle particle in particles)
            {
                particle.Update();
            }

            //死んでいるパーティクルはリストから除去
            particles.RemoveAll(p => p.isDead);
        }

        //全てのパーティクルを描画する
        public void Draw()
        {
            foreach (DxParticle particle in particles)
            {
                particle.Draw();
            }
        }


        //以下パーティカルの種類-----------------------------------------------------------------------------------------------------
        //噴水
        public void Fountain(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = MyLib.MyRandom.Range(40, 70),
                imageHandle = Image.particleDot1,
                Vy = MyLib.MyRandom.Range(-7, -4),
                Vx = MyLib.MyRandom.PlusMinus(1.5f),
                forceY = 0.15f,
                startScale = 0.5f,
                endScale = 0f,
                red = 50,
                green = 50,
                blue = 255,
            });
        }

        //衝撃は
        public void ShockWave(float x, float y, float angle)
        {
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 10,
                imageHandle = Image.particleRing4,
                startScale = 0.15f,
                endScale = 0.8f,
                endAlpha = 0,
                angle = angle,
            });
        }

        //火花エフェクト
        public void Spark(float x, float y, float Shotangle)
        {
            for (int i = 0; i < 30; i++)
            {
                Shotangle += MyLib.MyRandom.PlusMinus(5f);
                float speed = MyLib.MyRandom.Range(4f, 17f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(22, 35),
                        imageHandle = Image.particleDot1,
                        Vx = MathHelper.cos(Shotangle) * speed,
                        Vy = MathHelper.sin(Shotangle) * speed,
                        forceY = 0.13f,
                        startScale = 0.1f,
                        endScale = 0.05f,
                        red = 255,
                        green = 255,
                        blue = 0,
                        endAlpha = 0,
                        damp = 0.95f,
                    });
            }
        }

        //スチーム
        public void Steam(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = MyLib.MyRandom.Range(40, 90),
                imageHandle = Image.particleSteam,
                Vx = MyLib.MyRandom.PlusMinus(0.5f),
                Vy = MyLib.MyRandom.Range(-6f, -9f),
                forceX = MyLib.MyRandom.Range(0.03f, 0.07f),
                startScale = MyLib.MyRandom.Range(0.1f, 0.15f),
                endScale = MyLib.MyRandom.Range(0.3f, 0.8f),
                startAlpha = MyLib.MyRandom.Range(120, 170),
                endAlpha = 0,
                angle = MyLib.MyRandom.PlusMinus(MyMath_KNMR.MathHelper.pi),
                angularVelocity = MyLib.MyRandom.PlusMinus(0.13f),
                angularDamp = 0.98f,
                damp = 0.935f,
            });
        }

        //火
        public void Fire(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x + MyLib.MyRandom.PlusMinus(8),
                positionY = y + MyLib.MyRandom.PlusMinus(8),
                lifeSpan = MyLib.MyRandom.Range(30, 60),
                imageHandle = Image.particleFire,
                Vx = MyLib.MyRandom.PlusMinus(2f),
                Vy = MyLib.MyRandom.PlusMinus(2f),
                forceY = -0.07f,
                startScale = MyLib.MyRandom.Range(0.3f, 0.8f),
                endScale = MyLib.MyRandom.Range(0.2f, 0.4f),
                startAlpha = MyLib.MyRandom.Range(170, 255),
                endAlpha = 0,
                angle = MyLib.MyRandom.PlusMinus(MyMath_KNMR.MathHelper.pi),
                angularVelocity = MyLib.MyRandom.PlusMinus(0.12f),
                angularDamp = 0.97f,
                damp = 0.97f,
            });

        }
    }
}
