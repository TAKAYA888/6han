using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace ActionGame
{
    public class GameClearScene : Scene
    {
        int Selecct = 1;
        int timer = 0;
       

        public GameClearScene()
        {
            //DX.PlayMusic(" ", DX.DX_PLAYTYPE_LOOP);
        }

        public override void Update()
        {
            timer++;
            if (Input.GetButtonDown(DX.PAD_INPUT_UP))
            {
                Selecct = 1;
            }
            else if (Input.GetButtonDown(DX.PAD_INPUT_DOWN))
            {
                Selecct = 2;
            }
            if (Input.GetButtonDown(DX.PAD_INPUT_10) && Selecct == 1)
            {
                Game.ChangeScene(new TitleScene());
            }
            else if (Input.GetButtonDown(DX.PAD_INPUT_10) && Selecct == 2)
            {
                Game.ChangeScene(new PlayScene());
            }
        }
        public override void Draw()
        {
            DX.DrawGraph(0, 0, Image.GameClearImage);
            if (Selecct == 1)
            {
                if (timer / 15 % 2 == 0)
                {
                    DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 300, 1, 0, Image.ItemTitle);
                }
                DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 380, 1, 0, Image.ItemRetry);
            }
            else if (Selecct == 2)
            {
                if (timer / 15 % 2 == 0)
                {
                    DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 380, 1, 0, Image.ItemRetry);
                }
                DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 300, 1, 0, Image.ItemTitle);
            }
        }
    }
}
