using System;
using DxLibDLL;
using MyLib;
using System.Windows.Forms;

namespace ActionGame
{
    public class Game
    {
        static Scene scene;
        public void Init()
        {
            Input.Init();
            MyRandom.Init();
            Image.Load();
            scene = new TitleScene();
        }

        public void Update()
        {
            Input.Update();
            if (Input.GetButtonDown(DX.PAD_INPUT_9))
            {
                Application.GameEnd = true;                
            }
            scene.Update();
        }
        public void Draw()
        {
            scene.Draw();
        }

        public static void ChangeScene(Scene newScene)
        {
            scene = newScene;
        }
    }
}
