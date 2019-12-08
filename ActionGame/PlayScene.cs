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

        //エネミー1
        Enemy1 enemy1;

        public enum State
        {
            Active,
            PlayerDied
        }

        public Map map;
        //ハンドを設定
        public Hund hund;

        public List<ItemObject> itemObjects = new List<ItemObject>(); //アイテムオブジェクトの配列

        public State state = State.Active;
        int timeToGameOver = 120;
        bool isPausing = false;

        public PlayScene()
        {
            player = new Player(this, 100, 100);
            enemy1 = new Enemy1(this, 900, 400);
            map = new Map(this, "stage1");
        }
        public override void Update()
        {
            //手を消す処理
            if (Input.GetButtonDown(DX.PAD_INPUT_2))
            {
                hund = null;
                player.HundFrag = false;
            }

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
            
            //手の更新処理
            if (player.HundFrag)
            {
                hund.Update();
            }

            //エネミー1
            enemy1.Update();

            //アイテムオブジェクト----------------------------------------------------------------------------

            int itemObjectsCount = itemObjects.Count;//ループ前の個数を取得しておく

            for (int i = 0; i < itemObjectsCount; i++)
            {

                itemObjects[i].Update();
            }

            //オブジェクト同士の衝突を判定
            for (int i = 0; i < itemObjects.Count; i++)
            {
                ItemObject a = itemObjects[i];

                for (int j = i + 1; j < itemObjects.Count; j++)
                {
                    //オブジェクトAが死んでたらこのループは終了
                    if (a.isDead) break;

                    ItemObject b = itemObjects[j];

                    //オブジェクトBが死んでたらスキップ
                    if (b.isDead) continue;

                    //オブジェクトAとBが重なっているか？
                    if (MyMath.RectRectIntersect(a.GetLeft(), a.GetTop(), a.GetRight(), a.GetBottom(),
                        b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                    {
                        a.OnCollision(b);
                        b.OnCollision(a);
                    }
                }
            }
            //不要となったオブジェクトを除去する
            itemObjects.RemoveAll(go => go.isDead);

            //--------------------------------------------------------------------------------------------------

        }
        public override void Draw()
        {
            //プレイヤーの描画処理
            player.Draw();
            map.DrawTerrain(); 
            //線と手を描画しています
            if (player.HundFrag)
            {
                DX.DrawLine((int)player.PlayerPosition.x, (int)player.PlayerPosition.y, (int)hund.Position.x, (int)hund.Position.y, DX.GetColor(255, 255, 255));
                hund.Draw();
            }

            //エネミー1の描画
            enemy1.Draw();

            //アイテムの描画処理
            foreach (ItemObject go in itemObjects)
            {
                go.Draw();
            }
        }
    }
}
