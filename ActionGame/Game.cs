using System;
using DxLibDLL;
using MyLib;
using System.Windows.Forms;

namespace ActionGame
{
    public class Game
    {

        static Scene scene;
        public static ParticleManager particleManager;

        public void Init()
        {
            Input.Init();
            MyRandom.Init();
            Image.Load();
            SE.Load();
            scene = new TitleScene();

            particleManager = new ParticleManager();
        }

        public void Update()
        {
            Input.Update();

            if (Input.GetButtonDown(DX.PAD_INPUT_9)|| Input.GetButtonDown(DX.PAD_INPUT_7))
            {
                Application.GameEnd = true;                
            }
            scene.Update();

            particleManager.Update();
        }
        public void Draw()
        {
            scene.Draw();
            particleManager.Draw();
        }

        public static void ChangeScene(Scene newScene)
        {
            scene = newScene;
        }
    }
}
