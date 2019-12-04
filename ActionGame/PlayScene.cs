using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;
using DxLibDLL;

namespace ActionGame
{
    public class PlayScene : Scene
    {
        //プレイヤーを設定
        Player player;

        public enum State
        {
            Active,
            PlayerDied
        }

        public Map map;

        public State state = State.Active;
        int timeToGameOver = 120;
        bool isPausing = false;

        public PlayScene()
        {
            player = new Player(this, 100, 100);
            map = new Map(this, "stage1");
        }
        public override void Update()
        {
            //プレイヤーの更新処理
            player.Update();
            if (isPausing)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_8))
                {
                    isPausing = false;
                }
                return;
            }
            if (state == State.PlayerDied)
            {
                timeToGameOver--;//カウントダウン

                if (timeToGameOver <= 0)
                {

                }
            }
        }
        public override void Draw()
        {
            //プレイヤーの描画処理
            player.Draw();
            map.DrawTerrain();
        }
    }
}
