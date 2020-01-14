using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace ActionGame
{
    public class TitleScene : Scene
    {
        int Selecct = 1;
        int timer = 0;

        public TitleScene()
        {
            //DX.PlayMusic("", DX.DX_PLAYTYPE_LOOP);
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
            if (Input.GetButtonDown(DX.PAD_INPUT_1) && Selecct == 1)
            {
                Game.ChangeScene(new PlayScene());
            }
            else if (Input.GetButtonDown(DX.PAD_INPUT_1) && Selecct == 2)
            {
                Application.GameEnd = true;
            }

            if(Input.GetButton(DX.PAD_INPUT_1)&&Input.GetButton(DX.PAD_INPUT_2)&&Input.GetButton(DX.PAD_INPUT_3))
            {
                Game.ChangeScene(new TestScene());
            }
        }
        public override void Draw()
        {
            DX.DrawGraph(0, 0, Image.TitleImage);
            if (Selecct == 1)
            {
                if (timer / 15 % 2 == 0)
                {
                    DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 180, 1, 0, Image.ItemStart);
                }
                DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 300, 1, 0, Image.ItemOver);
            }
            else if (Selecct == 2)
            {
                if (timer / 15 % 2 == 0)
                {
                    DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 300, 1, 0, Image.ItemOver);
                }
                DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 180, 1, 0, Image.ItemStart);
            }
        }
    }
}
