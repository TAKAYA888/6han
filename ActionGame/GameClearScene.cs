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
            DX.PlayMusic("BGM/Game_clear.mp3", DX.DX_PLAYTYPE_LOOP);
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
                Game.ChangeScene(new TitleScene());
            }
            else if (Input.GetButtonDown(DX.PAD_INPUT_1) && Selecct == 2)
            {
                Game.ChangeScene(new PlayScene());
            }
            //Game.particleManager.FireWork(MyRandom.Range(0, 1920), 1080);
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
            DX.SetFontSize(60);
            DX.DrawString(1150, 385, "SCORE : "+Player.ScorePoint.ToString(), DX.GetColor(255, 0, 0));//スコア

            if (Player.ScorePoint < 1500)
            {
                //DX.SetFontSize(240);
                DX.DrawRotaGraph(1920/2, 1080/2,1,0, Image.ScoreC);
            }
            else if (Player.ScorePoint < 3000)
            {
                //DX.SetFontSize(240);
                DX.DrawRotaGraph(1920 / 2, 1080/2, 1, 0, Image.ScoreB);
            }
            else if (Player.ScorePoint < 4500)
            {
                //DX.SetFontSize(240);
                DX.DrawRotaGraph(1920 / 2, 1080/2, 1, 0, Image.ScoreA);
            }
            else 
            {
                //DX.SetFontSize(240);
                DX.DrawRotaGraph(1920 / 2, 1080/2, 1, 0, Image.ScoreS);
            }
        }
    }
}
