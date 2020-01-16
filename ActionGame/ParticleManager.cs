using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath_KNMR;
using DxLibDLL;
using MyLib;

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
            int count = particles.Count;

            for (int i = 0; i < count; i++)
            {
                particles[i].Update();
            }

            // 死んでる粒子はリストから除去
            particles.RemoveAll(p => p.state == State.Dead);
        }

        //全てのパーティクルを描画する
        public void Draw()
        {
            foreach (DxParticle particle in particles)
            {
                particle.Draw();
            }

            // アルファ値を元に戻す
            DxHelper.SetBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            // 色を元に戻す
            DxHelper.SetColor(255, 255, 255);
        }


        //以下パーティカルの種類-----------------------------------------------------------------------------------------------------
        public void Splash(float x, float y)
        {
            particles.Add(
                new DxParticle()
                {
                    positionX = x,
                    positionY = y,
                    lifeSpan = MyLib.MyRandom.Range(40, 70),
                    imageHandle = Image.particleDot1,
                    Vy = MyLib.MyRandom.Range(-4f, -7f),
                    Vx = MyLib.MyRandom.PlusMinus(1.5f),
                    forceY = 0.15f,
                    startScale = 0.5f,
                    endScale = 0f,
                    red = 170,
                    green = 170,
                    blue = 255,
                });
        }

        public void Spark(float x, float y, float angle)
        {
            for (int i = 0; i < 30; i++)
            {
                angle += MyLib.MyRandom.PlusMinus(0.04f);
                float speed = MyLib.MyRandom.Range(4f, 17f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(22, 35),
                        imageHandle = Image.particleDot1,
                        Vx = (float)Math.Cos(angle) * speed,
                        Vy = (float)Math.Sin(angle) * speed,
                        forceY = 0.13f,
                        damp = 0.95f,
                        startScale = 0.1f,
                        endScale = 0.05f,
                        red = 255,
                        green = 255,
                        blue = 0,
                        fadeOutTime = 1f,
                    });
            }
        }

        public void Fire(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x + MyLib.MyRandom.PlusMinus(8),
                positionY = y + MyLib.MyRandom.PlusMinus(8),
                lifeSpan = MyLib.MyRandom.Range(30, 60),
                startScale = MyLib.MyRandom.Range(0.3f, 0.8f),
                endScale = MyLib.MyRandom.Range(0.2f, 0.4f),
                imageHandle = Image.particleFire,
                alpha = MyLib.MyRandom.Range(170, 255),
                fadeOutTime = 1f,
                blendMode = DX.DX_BLENDMODE_ADD,
                forceY = -0.07f,
                Vx = MyLib.MyRandom.PlusMinus(2f),
                Vy = MyLib.MyRandom.PlusMinus(2f),
                damp = 0.97f,
                angle = MyLib.MyRandom.PlusMinus(MathHelper.pi),
                angularVelocity = MyLib.MyRandom.PlusMinus(0.12f),
                angularDamp = 0.97f,
            });
        }

        public void Explosion(float x, float y)
        {
            for (int i = 0; i < 70; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
                float speed = MyLib.MyRandom.Range(0f, 8f);
                float startScale = MyLib.MyRandom.Range(0.5f, 1.3f);


                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(15, 45),
                        imageHandle = Image.particleFire,
                        Vx = (float)Math.Cos(angle) * speed,
                        Vy = (float)Math.Sin(angle) * speed,
                        damp = 0.88f,
                        startScale = startScale,
                        endScale = startScale * 0.75f,
                        fadeOutTime = 1f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        angle = MyLib.MyRandom.PlusMinus(3.14f),
                        angularVelocity = MyLib.MyRandom.PlusMinus(0.15f),
                        angularDamp = 0.94f,
                    });
            }
        }

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
                alpha = MyLib.MyRandom.Range(120, 170),
                fadeOutTime = 1f,
                angle = MyLib.MyRandom.PlusMinus(MathHelper.pi),
                angularVelocity = MyLib.MyRandom.PlusMinus(0.13f),
                angularDamp = 0.98f,
                damp = 0.935f,
            });
        }

        public void Smoke(float x, float y)
        {
            for (int i = 0; i < 30; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionY = x + MyLib.MyRandom.PlusMinus(5),
                    positionX = y + MyLib.MyRandom.PlusMinus(2),
                    lifeSpan = MyLib.MyRandom.Range(15, 40),
                    imageHandle = Image.particleSteam,
                    Vx = MyLib.MyRandom.PlusMinus(3f),
                    Vy = MyLib.MyRandom.PlusMinus(0.7f) + -0.5f,
                    damp = 0.93f,
                    forceY = 0.02f,
                    startScale = 0.15f,
                    endScale = 0.3f,
                    alpha = 170,
                    fadeOutTime = 1f,
                    angle = MyLib.MyRandom.PlusMinus(3.14f),
                    angularVelocity = MyLib.MyRandom.PlusMinus(0.05f),
                    angularDamp = 0.98f,
                });
            }
        }

        public void PickupItem(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 12,
                imageHandle = Image.particleRing2,
                startScale = 0.15f,
                endScale = 0.35f,
                alpha = 150,
                fadeOutTime = 1f,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 170,
            });

            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 24,
                imageHandle = Image.particleGlitter1,
                startScale = 1f,
                endScale = 0.4f,
                fadeOutTime = 1f,
                blendMode = DX.DX_BLENDMODE_ADD,
                blue = 100,
            });

            for (int i = 0; i < 7; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionY = x + MyLib.MyRandom.PlusMinus(10f),
                    positionX = y + MyLib.MyRandom.PlusMinus(10f),
                    lifeSpan = MyLib.MyRandom.Range(20, 50),
                    imageHandle = Image.particleGlitter1,
                    startScale = 0.6f,
                    endScale = 0.1f,
                    alpha = 190,
                    fadeOutTime = 1f,
                    blendMode = DX.DX_BLENDMODE_ADD,
                    blue = 100,
                    Vx = MyLib.MyRandom.PlusMinus(2f),
                    Vy = MyLib.MyRandom.Range(-3, -7f),
                    damp = 0.96f,
                    forceY = 0.15f,
                });
            }
        }

        public void ShockWave(float x, float y,float angle)
        {
            particles.Add(new DxParticle()
            {
                positionY = x,
                positionX = y,
                lifeSpan = 10,
                imageHandle = Image.particleRing4,
                startScale = 0.15f,
                endScale = 0.8f,
                fadeOutTime = 1f,
                angle = angle,
            });
        }

        public void Blood(float x, float y)
        {

            for (int i = 0; i < 40; i++)
            {
                float angle = 3.14f / 180f * (225f + MyLib.MyRandom.PlusMinus(4f));
                float speed = MyLib.MyRandom.Range(3f, 4.5f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(8, 20),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.2f,
                        endScale = 0.1f,
                        red = 170,
                        green = 0,
                        blue = 0,
                    });
            }

            for (int i = 0; i < 40; i++)
            {
                float angle = 3.14f / 180f * (-65f + MyLib.MyRandom.PlusMinus(4f));
                float speed = MyLib.MyRandom.Range(3f, 5.5f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(10, 30),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.2f,
                        endScale = 0.1f,
                        red = 170,
                        green = 0,
                        blue = 0,
                    });
            }

            for (int i = 0; i < 20; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(3.14f);
                float speed = MyLib.MyRandom.Range(0.2f, 3.5f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(10, 20),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.98f,
                        startScale = 0.3f,
                        endScale = 0.2f,
                        red = 170,
                        green = 0,
                        blue = 0,
                        alpha = 150,
                        fadeOutTime = 1f,
                    });
            }
        }

        public void Stars(float x, float y)
        {
            for (int i = 0; i < 100; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(3.14f);
                float speed = MyLib.MyRandom.Range(1.5f, 7f);
                float scale = MyLib.MyRandom.Range(0.3f, 0.5f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x + MyLib.MyRandom.PlusMinus(3),
                        positionY = y + MyLib.MyRandom.PlusMinus(3),
                        lifeSpan = MyLib.MyRandom.Range(120, 180),
                        imageHandle = Image.particleStar2,
                        Vy = (float)Math.Sin(angle) * speed - MyLib.MyRandom.Range(0, 7f),
                        Vx = (float)Math.Cos(angle) * speed,
                        forceY = 0.15f,
                        damp = 0.99f,
                        startScale = scale,
                        endScale = scale,
                        red = MyLib.MyRandom.Range(100, 255),
                        green = MyLib.MyRandom.Range(100, 255),
                        blue = MyLib.MyRandom.Range(100, 255),
                        alpha = 255,
                        fadeOutTime = 1f,
                        angle = MyLib.MyRandom.PlusMinus(3.14f),
                        angularVelocity = MyLib.MyRandom.PlusMinus(0.08f),
                        angularDamp = 0.985f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void FlowLine(float x, float y)
        {
            particles.Add(
                new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(30),
                    positionY = y + MyLib.MyRandom.PlusMinus(30),
                    lifeSpan = MyLib.MyRandom.Range(20, 30),
                    imageHandle = Image.particleLine1,
                    Vy = -MyLib.MyRandom.Range(5f, 7f),
                    Vx = 0,
                    damp = 1f,
                    startScale = 0.4f,
                    endScale = 0.2f,
                    alpha = 255,
                    fadeOutTime = 1f,
                    angle = 3.14f / 180f * 90,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });
        }

        public void RadialLine(float x, float y)
        {
            for (int i = 0; i < 7; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed = MyLib.MyRandom.Range(2.5f, 7f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(8, 15),
                        imageHandle = Image.particleLine2,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        damp = 1f,
                        startScale = 0.4f,
                        endScale = 0.6f,
                        fadeOutTime = 1f,
                        angle = angle,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }

            particles.Add(
                new DxParticle()
                {
                    positionX = x,
                    positionY = y,
                    lifeSpan = 10,
                    imageHandle = Image.particleDot1,
                    startScale = 0.4f,
                    endScale = 2f,
                    fadeOutTime = 1f,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });

            for (int i = 0; i < 20; i++)
            {
                float angle3 = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed3 = MyLib.MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(14, 48),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle3) * speed3,
                        Vx = (float)Math.Cos(angle3) * speed3,
                        damp = 0.91f,
                        startScale = MyLib.MyRandom.Range(0.04f, 0.13f),
                        endScale = 0.03f,
                        fadeOutTime = 1f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void Slash(float x, float y, float angle)
        {
            float speed = 6f;
            float vy = (float)Math.Sin(angle) * speed;
            float vx = (float)Math.Cos(angle) * speed;

            particles.Add(
                new DxParticle()
                {
                    positionX = x - vx * 3f,
                    positionY = y - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    Vy = vy,
                    Vx = vx,
                    damp = 0.98f,
                    startScale = 0.7f,
                    endScale = 0.6f,
                    fadeOutTime = 1f,
                    angle = angle,
                    blendMode = DX.DX_BLENDMODE_ADD,
                });


            for (int i = 0; i < 7; i++)
            {
                float angle2 = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed2 = MyLib.MyRandom.Range(2f, 5f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(7, 12),
                        imageHandle = Image.particleLine2,
                        Vy = (float)Math.Sin(angle2) * speed2,
                        Vx = (float)Math.Cos(angle2) * speed2,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.4f,
                        fadeOutTime = 1f,
                        angle = angle2,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }

            for (int i = 0; i < 30; i++)
            {
                float angle3 = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed3 = MyLib.MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(10, 45),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle3) * speed3,
                        Vx = (float)Math.Cos(angle3) * speed3,
                        damp = 0.91f,
                        startScale = MyLib.MyRandom.Range(0.03f, 0.1f),
                        endScale = 0.03f,
                        fadeOutTime = 1f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void FireWork(float x, float y)
        {
            int[,] color = {
                {255, 170, 170},
                {170, 170, 255},
                {240, 240, 160},
                {255, 170, 170},
            };

            int colorPattern = MyLib.MyRandom.Range(0, 2);

            for (int i = 0; i < 400; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed = MyLib.MyRandom.Range(0f, 3f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(80, 140),
                        imageHandle = Image.particleDot3,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        damp = 0.95f,
                        forceX = MyLib.MyRandom.Range(0.002f, 0.006f),
                        forceY = MyLib.MyRandom.Range(0.016f, 0.027f),
                        startScale = MyLib.MyRandom.Range(0.2f, 0.26f),
                        endScale = 0f,
                        fadeOutTime = 1f,
                        red = color[colorPattern * 2, 0],
                        green = color[colorPattern * 2, 1],
                        blue = color[colorPattern * 2, 2],
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
            for (int i = 0; i < 700; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed = MyLib.MyRandom.Range(0f, 6f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(80, 120),
                        imageHandle = Image.particleDot3,
                        Vy = (float)Math.Sin(angle) * speed,
                        Vx = (float)Math.Cos(angle) * speed,
                        damp = 0.95f,
                        forceX = MyLib.MyRandom.Range(0.002f, 0.006f),
                        forceY = MyLib.MyRandom.Range(0.016f, 0.027f),
                        startScale = MyLib.MyRandom.Range(0.2f, 0.26f),
                        endScale = 0f,
                        fadeOutTime = 1f,
                        red = color[colorPattern * 2 + 1, 0],
                        green = color[colorPattern * 2 + 1, 1],
                        blue = color[colorPattern * 2 + 1, 2],
                        blendMode = DX.DX_BLENDMODE_ADD,
                    });
            }
        }

        public void Stone(float x, float y)
        {
            for (int i = 0; i < 7; i++)
            {
                float scale = MyLib.MyRandom.Range(0.02f, 0.08f);

                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(4),
                    positionY = y + MyLib.MyRandom.PlusMinus(3),
                    lifeSpan = MyLib.MyRandom.Range(30, 40),
                    Vy = -MyLib.MyRandom.Range(2f, 4.5f),
                    Vx = MyLib.MyRandom.PlusMinus(2.3f),
                    imageHandle = Image.particleStone1,
                    forceY = 0.13f,
                    angle = MyLib.MyRandom.PlusMinus(MathHelper.pi),
                    angularVelocity = MyLib.MyRandom.PlusMinus(0.3f),
                    angularDamp = 0.985f,
                    startScale = scale,
                    endScale = scale,
                    fadeOutTime = 1f,
                    red = 255,
                    green = 200,
                    blue = 180,
                });
            }

            for (int i = 0; i < 20; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(5),
                    positionY = y + MyLib.MyRandom.PlusMinus(2),
                    lifeSpan = MyLib.MyRandom.Range(15, 40),
                    imageHandle = Image.particleSmoke,
                    Vx = MyLib.MyRandom.PlusMinus(3f),
                    Vy = MyLib.MyRandom.PlusMinus(0.7f) + -0.5f,
                    damp = 0.93f,
                    forceY = 0.02f,
                    startScale = 0.15f,
                    endScale = 0.3f,
                    alpha = 170,
                    fadeOutTime = 1f,
                    angle = MyLib.MyRandom.PlusMinus(3.14f),
                    angularVelocity = MyLib.MyRandom.PlusMinus(0.05f),
                    angularDamp = 0.98f,
                    red = 255,
                    green = 200,
                    blue = 180,
                });
            }
        }

        public void Heal(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 25,
                imageHandle = Image.particleRing2,
                startScale = 0.1f,
                endScale = 0.4f,
                alpha = 180,
                fadeOutTime = 1f,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 255,
                blue = 170
            });
            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 12,
                imageHandle = Image.particleRing2,
                startScale = 0.2f,
                endScale = 0.6f,
                alpha = 150,
                fadeOutTime = 1f,
                blendMode = DX.DX_BLENDMODE_ADD,
                red = 170,
                green = 255,
                blue = 170
            });

            for (int i = 0; i < 20; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(20),
                    positionY = y + MyLib.MyRandom.PlusMinus(20),
                    Vx = MyLib.MyRandom.PlusMinus(0.8f),
                    Vy = MyLib.MyRandom.Range(-0.3f, 0.9f),
                    forceY = -0.06f,
                    damp = 0.98f,
                    lifeSpan = MyLib.MyRandom.Range(10, 45),
                    imageHandle = Image.particleGlitter1,
                    startScale = 0.05f,
                    endScale = MyLib.MyRandom.Range(0.4f, 0.8f),
                    alpha = 255,
                    fadeOutTime = 1f,
                    blendMode = DX.DX_BLENDMODE_ADD,
                    red = 170,
                    green = 255,
                    blue = 170
                });
            }

            for (int i = 0; i < 20; i++)
            {
                particles.Add(
                    new DxParticle()
                    {
                        positionX = x + MyLib.MyRandom.PlusMinus(30),
                        positionY = y + 30 + MyLib.MyRandom.PlusMinus(30),
                        lifeSpan = MyLib.MyRandom.Range(20, 40),
                        imageHandle = Image.particleLine1,
                        Vy = -MyLib.MyRandom.Range(1f, 5f),
                        Vx = 0,
                        forceY = -0.07f,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.15f,
                        alpha = 255,
                        fadeOutTime = 1f,
                        angle = MathHelper.toRadians(90.0f),
                        blendMode = DX.DX_BLENDMODE_ADD,
                        red = 170,
                        green = 255,
                        blue = 170
                    });
            }
        }

        public void Charge(float x, float y)
        {
            float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
            float distance = MyLib.MyRandom.Range(20f, 80f);
            float distanceX = (float)Math.Cos(angle) * distance;
            float distanceY = (float)Math.Sin(angle) * distance;
            int lifeSpan = MyLib.MyRandom.Range(15, 40);

            particles.Add(new DxParticle()
            {
                positionX = x + distanceX,
                positionY = y + distanceY,
                lifeSpan = lifeSpan,
                imageHandle = Image.particleDot2,
                Vx = -distanceX / lifeSpan,
                Vy = -distanceY / lifeSpan,
                startScale = MyLib.MyRandom.Range(0.13f, 0.25f),
                endScale = 0.0f,
                fadeInTime = 1f,
                angle = angle,
            });
        }

        public void Claw(float x, float y)
        {
            float angle = MathHelper.toRadians(70.0f);
            float speed = 6f;
            float vy = (float)Math.Sin(angle) * speed;
            float vx = (float)Math.Cos(angle) * speed;

            particles.Add(
                new DxParticle()
                {
                    positionX = x - vx * 3f,
                    positionY = y - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    Vy = vy,
                    Vx = vx,
                    damp = 0.98f,
                    startScale = 0.7f,
                    endScale = 0.6f,
                    alpha = 255,
                    fadeOutTime = 1f,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            particles.Add(
                new DxParticle()
                {
                    positionX = x + (float)Math.Cos(MathHelper.toRadians(angle + 90f)) * 15f - vx * 3f,
                    positionY = y + (float)Math.Sin(angle + 90f) * 15f - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    Vy = vy,
                    Vx = vx,
                    damp = 0.98f,
                    startScale = 0.55f,
                    endScale = 0.45f,
                    alpha = 255,
                    fadeOutTime = 1f,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            particles.Add(
                new DxParticle()
                {
                    positionX = x + (float)Math.Cos(MathHelper.toRadians(angle - 90f)) * 15f - vx * 3f,
                    positionY = y + (float)Math.Sin(MathHelper.toRadians(angle - 90f)) * 15f - vy * 3f,
                    lifeSpan = 13,
                    imageHandle = Image.particleSlash,
                    Vy = vy,
                    Vx = vx,
                    damp = 0.98f,
                    startScale = 0.55f,
                    endScale = 0.45f,
                    alpha = 255,
                    fadeOutTime = 1f,
                    angle = angle,
                    red = 255,
                    green = 0,
                    blue = 0,
                });

            for (int i = 0; i < 7; i++)
            {
                float angle2 = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed2 = MyLib.MyRandom.Range(2f, 6f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(7, 12),
                        imageHandle = Image.particleLine2,
                        Vy = (float)Math.Sin(angle2) * speed2,
                        Vx = (float)Math.Cos(angle2) * speed2,
                        damp = 1f,
                        startScale = 0.3f,
                        endScale = 0.45f,
                        alpha = 255,
                        fadeOutTime = 1f,
                        angle = angle2,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        red = 255,
                        green = 150,
                        blue = 40,
                    });
            }

            for (int i = 0; i < 45; i++)
            {
                float angle3 = MyLib.MyRandom.PlusMinus(3.1415f);
                float speed3 = MyLib.MyRandom.Range(0.3f, 4f);

                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(10, 45),
                        imageHandle = Image.particleDot1,
                        Vy = (float)Math.Sin(angle3) * speed3,
                        Vx = (float)Math.Cos(angle3) * speed3,
                        forceY = 0.03f,
                        damp = 0.91f,
                        startScale = MyLib.MyRandom.Range(0.04f, 0.2f),
                        endScale = 0.05f,
                        alpha = 255,
                        fadeOutTime = 1f,
                        red = 255,
                        green = 0,
                        blue = 0,
                    });
            }
        }


        public void SuperExplosion(float x, float y)
        {
            for (int i = 0; i < 30; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(30),
                    positionY = y + MyLib.MyRandom.PlusMinus(30),
                    lifeSpan = MyLib.MyRandom.Range(30, 80),
                    imageHandle = Image.particleSmoke,
                    Vx = MyLib.MyRandom.PlusMinus(3f),
                    Vy = MyLib.MyRandom.PlusMinus(2f),
                    damp = 0.95f,
                    forceY = -0.05f,
                    startScale = MyLib.MyRandom.Range(0.1f, 0.3f),
                    endScale = MyLib.MyRandom.Range(0.8f, 1.4f),
                    alpha = 170,
                    fadeOutTime = 1f,
                    angle = MyLib.MyRandom.PlusMinus(3.14f),
                    angularVelocity = MyLib.MyRandom.PlusMinus(0.05f),
                    angularDamp = 0.98f,
                    delay = 60 + MyLib.MyRandom.Range(0, 15),
                });
            }

            for (int j = 0; j < 11; j++)
            {
                float xx = x + MyLib.MyRandom.PlusMinus(40);
                float yy = y + MyLib.MyRandom.PlusMinus(40);

                for (int i = 0; i < 10; i++)
                {
                    float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
                    float speed = MyLib.MyRandom.Range(0f, 3f);
                    float startScale = MyLib.MyRandom.Range(0.2f, 0.8f);

                    particles.Add(
                        new DxParticle()
                        {
                            positionX = xx,
                            positionY = yy,
                            lifeSpan = MyLib.MyRandom.Range(10, 20),
                            imageHandle = Image.particleFire,
                            Vx = (float)Math.Cos(angle) * speed,
                            Vy = (float)Math.Sin(angle) * speed,
                            damp = 0.88f,
                            startScale = startScale,
                            endScale = startScale * 0.75f,
                            fadeOutTime = 1f,
                            blendMode = DX.DX_BLENDMODE_ADD,
                            angle = MyLib.MyRandom.PlusMinus(3.14f),
                            angularVelocity = MyLib.MyRandom.PlusMinus(0.15f),
                            angularDamp = 0.94f,
                            delay = j * 5,
                        });
                }
            }

            for (int i = 0; i < 70; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
                float speed = MyLib.MyRandom.Range(3f, 13f);
                float startScale = MyLib.MyRandom.Range(0.5f, 1.4f);


                particles.Add(
                    new DxParticle()
                    {
                        positionX = x,
                        positionY = y,
                        lifeSpan = MyLib.MyRandom.Range(15, 45),
                        imageHandle = Image.particleFire,
                        Vx = (float)Math.Cos(angle) * speed,
                        Vy = (float)Math.Sin(angle) * speed,
                        damp = 0.88f,
                        startScale = startScale,
                        endScale = startScale * 0.75f,
                        fadeOutTime = 1f,
                        blendMode = DX.DX_BLENDMODE_ADD,
                        angle = MyLib.MyRandom.PlusMinus(3.14f),
                        angularVelocity = MyLib.MyRandom.PlusMinus(0.15f),
                        angularDamp = 0.94f,
                        delay = 60,
                    });
            }

            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 12,
                imageHandle = Image.particleRing4,
                startScale = 0.15f,
                endScale = 1.7f,
                alpha = 200,
                fadeOutTime = 1f,
                angle = MathHelper.toRadians(90.0f),
                delay = 60,
            });

            for (int i = 0; i < 50; i++)
            {
                float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
                float distance = MyLib.MyRandom.Range(100f, 170f);
                float distanceX = (float)Math.Cos(angle) * distance;
                float distanceY = (float)Math.Sin(angle) * distance;
                int lifeSpan = MyLib.MyRandom.Range(20, 40);

                particles.Add(new DxParticle()
                {
                    positionX = x + distanceX,
                    positionY = y + distanceY,
                    lifeSpan = lifeSpan,
                    imageHandle = Image.particleDot2,
                    Vx = -distanceX / lifeSpan,
                    Vy = -distanceY / lifeSpan,
                    startScale = MyLib.MyRandom.Range(0.13f, 0.25f),
                    endScale = 0.0f,
                    fadeInTime = 1f,
                    fadeOutTime = 0f,
                    alpha = 255,
                    angle = angle,
                    delay = (int)(i / 1.7f),
                });
            }

            particles.Add(new DxParticle()
            {
                positionX = x,
                positionY = y,
                lifeSpan = 18,
                imageHandle = Image.particleDot1,
                startScale = 13f,
                endScale = 0.1f,
                alpha = 230,
                fadeInTime = 1f,
                delay = 42,
            });
        }

        public void Smoke2(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                positionX = x + MyLib.MyRandom.PlusMinus(3),
                positionY = y + MyLib.MyRandom.PlusMinus(3),
                lifeSpan = MyLib.MyRandom.Range(80, 120),
                imageHandle = Image.particleSmoke,
                Vx = MyLib.MyRandom.PlusMinus(0.2f),
                Vy = MyLib.MyRandom.Range(-3f, -3.5f),
                startScale = 0.08f,
                endScale = 0.5f,
                damp = 0.98f,
                forceX = MyLib.MyRandom.Range(0.01f, 0.02f),
                forceY = -0.01f,
                angle = MyLib.MyRandom.PlusMinus(MathHelper.pi),
                angularVelocity = MyLib.MyRandom.PlusMinus(0.04f),
                angularDamp = 0.99f,
                alpha = MyLib.MyRandom.Range(80, 140),
                fadeInTime = 0.3f,
                fadeOutTime = 0.5f,
            });
        }

        public void Smoke3(float x, float y)
        {
            for (int i = 0; i < 25; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(20),
                    positionY = y + MyLib.MyRandom.PlusMinus(7),
                    lifeSpan = MyLib.MyRandom.Range(70, 120),
                    imageHandle = Image.particleSmoke,
                    Vx = MyLib.MyRandom.PlusMinus(4f),
                    Vy = MyLib.MyRandom.PlusMinus(3f),
                    startScale = MyLib.MyRandom.Range(0.6f, 0.8f),
                    endScale = MyLib.MyRandom.Range(0.8f, 1.3f),
                    damp = 0.95f,
                    forceX = MyLib.MyRandom.Range(0.01f, 0.02f),
                    forceY = -0.01f,
                    angle = MyLib.MyRandom.PlusMinus(MathHelper.pi),
                    angularVelocity = MyLib.MyRandom.PlusMinus(0.04f),
                    angularDamp = 0.99f,
                    alpha = MyLib.MyRandom.Range(30, 120),
                    fadeInTime = 0.3f,
                    fadeOutTime = 0.5f,
                    delay = MyLib.MyRandom.Range(0, 6),
                });
            }
        }

        public void Glitter(float x, float y)
        {
            for (int i = 0; i < 12; i++)
            {
                particles.Add(new DxParticle()
                {
                    positionX = x + MyLib.MyRandom.PlusMinus(30),
                    positionY = y + MyLib.MyRandom.PlusMinus(30),
                    lifeSpan = MyLib.MyRandom.Range(30, 50),
                    imageHandle = Image.particleGlitter1,
                    Vx = MyLib.MyRandom.PlusMinus(0.5f),
                    Vy = MyLib.MyRandom.Range(0.4f, 1.3f),
                    startScale = MyLib.MyRandom.Range(0.2f, 0.4f),
                    endScale = MyLib.MyRandom.Range(0.3f, 0.6f),
                    damp = 0.96f,
                    forceY = -0.03f,
                    fadeInTime = 0.5f,
                    fadeOutTime = 0.5f,
                    delay = MyLib.MyRandom.Range(0, 50),
                    blendMode = DX.DX_BLENDMODE_ADD,
                    red = 255,
                    green = 255,
                    blue = 180,
                });
            }
        }

        public void FireWorks2(float x, float y)
        {
            particles.Add(new DxParticle()
            {
                // ヒュ～～と上に向かって飛ぶ1粒
                positionX = x,
                positionY = y,
                lifeSpan = 60,
                imageHandle = Image.particleDot1,
                Vy = -5.5f,
                forceY = 0.08f,
                startScale = 0.18f,
                endScale = 0.14f,
                blendMode = DX.DX_BLENDMODE_ADD,
                OnDeath = (p) =>
                {
                    // 周囲に飛び散る花火本体
                    for (int i = 0; i < 500; i++)
                    {
                        float angle = MyLib.MyRandom.PlusMinus(MathHelper.pi);
                        float speed = MyLib.MyRandom.Range(2f, 8f);

                        particles.Add(new DxParticle()
                        {
                            positionX = p.positionX,
                            positionY = p.positionY,
                            lifeSpan = MyLib.MyRandom.Range(40, 70),
                            imageHandle = Image.particleDot3,
                            Vx = (float)Math.Cos(angle) * speed,
                            Vy = (float)Math.Sin(angle) * speed,
                            forceY = 0.06f,
                            damp = 0.96f,
                            startScale = 0.2f,
                            endScale = 0.15f,
                            fadeOutTime = 0.7f,
                            red = 180,
                            green = 180,
                            blue = 255,
                            blendMode = DX.DX_BLENDMODE_ADD,
                            OnUpdate = (p2) =>
                            {
                                // 光の尾
                                particles.Add(new DxParticle()
                                {
                                    positionX = p2.positionX,
                                    positionY = p2.positionY,
                                    lifeSpan = 50,
                                    imageHandle = Image.particleDot3,
                                    forceY = 0.05f,
                                    damp = 0.94f,
                                    startScale = 0.16f,
                                    endScale = 0.11f,
                                    alpha = p2.currentAlpha,
                                    fadeOutTime = 1f,
                                    red = 180,
                                    green = 180,
                                    blue = 255,
                                    blendMode = DX.DX_BLENDMODE_ADD,
                                });
                            },
                        });
                    }
                }
            });
        }
    }
}
