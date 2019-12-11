using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace ActionGame
{
    public class MapScene : Scene
    {
        int Selecct = 1;
        int timer = 0;

        public MapScene()
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
                Game.ChangeScene(new PlayScene());
            }
            else if (Input.GetButtonDown(DX.PAD_INPUT_10) && Selecct == 2)
            {
                Game.ChangeScene(new TitleScene());
            }
        }
        public override void Draw()
        {
            DX.DrawGraph(0, 0, Image.MapImage);
            if (Selecct == 1)
            {
                if (timer / 15 % 2 == 0)
                {
                    DX.DrawRotaGraph(Screen.Width / 2, Screen.Height / 2 + 450, 1, 0, Image.ItemContinue);
                }
                //
            }
            //
        }
    }
}
