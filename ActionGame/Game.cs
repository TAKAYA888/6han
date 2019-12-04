using System;
using DxLibDLL;
using MyLib;

namespace ActionGame
{
    public class Game
    {
        static Scene scene;
        public void Init()
        {
            Input.Init();
            MyRandom.Init();
            scene = new PlayScene();
        }

        public void Update()
        {
            if (Input.GetButtonDown(DX.PAD_INPUT_9))
            {
                Environment.Exit(0);
            }
            scene.Update();
        }

        public void Draw()
        {
            scene.Draw();
        }
    }
}
