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
        public Player player;
        //針
        NeedleObject needle;

        public enum State
        {
            Active,
            PlayerDied
        }

        public Map map;
        //ハンドを設定
        public Hund hund;

        public List<ItemObject> itemObjects = new List<ItemObject>(); //アイテムオブジェクトの配列
        public List<EnemyObject> enemyObjects = new List<EnemyObject>();
        public List<playerObject> playerObjects = new List<playerObject>();

        public State state = State.Active;
        int timeToGameOver = 120;
        bool isPausing = false;

        public PlayScene()
        {
            //enemy1 = new Enemy1(this, 700, 300);
            enemyObjects.Add(new Enemy1(this, 700, 300));
            //itemObjects.Add(new WoolenYarn(this, 300, 500));
            needle = new NeedleObject(this, 840, 1440);
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
            //---------------------------------------------------------------------------------------------------
            
            if (Input.GetButtonDown(DX.PAD_INPUT_10))
            {
               hund = new Hund(this, player, player.Position.x, player.Position.y);
            }

            foreach (playerObject playerObject in playerObjects)
            {
                playerObject.Update();
            }

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
            if (hund != null)
            {
                hund.Update();
            }

            ////エネミー1
            //enemy1.Update();

            foreach (EnemyObject enemyObject in enemyObjects)
            {
                enemyObject.Update();
            }
            

            for (int i = 0; i < enemyObjects.Count; i++)
            {
                for(int j =0;j<playerObjects.Count;j++)
                {
                    playerObject player = playerObjects[j];

                    if (player.isDead) break;

                    EnemyObject enemy = enemyObjects[i];

                    if (enemy.isDead) continue;

                    if (MyMath.RectRectIntersect(player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom(),
                             enemy.GetLeft(), enemy.GetTop(), enemy.GetRight(), enemy.GetBottom()))
                    {
                        player.OnCollision(enemy);
                        enemy.OnCollision(player);
                    }
                }
            }

            //オブジェクトAとBが重なっているか？
            if (hund != null)
            {
                for (int i = 0; i < enemyObjects.Count; ++i)
                {
                    EnemyObject enemyObject = enemyObjects[i];

                    if (enemyObject.isDead) continue;

                    if (MyMath.RectRectIntersect(hund.GetLeft(), hund.GetTop(), hund.GetRight(), hund.GetBottom(),
                         enemyObject.GetLeft(), enemyObject.GetTop(), enemyObject.GetRight(), enemyObject.GetBottom()))
                    {
                        hund.OnCollision(enemyObject);
                        enemyObject.OnCollisionH(hund);
                    }
                }
            }

            enemyObjects.RemoveAll(ene => ene.isDead);

            //針
            //オブジェクトAとBが重なっているか？
            //if (MyMath.RectRectIntersect(needle.GetLeft(), needle.GetTop(), needle.GetRight(), needle.GetBottom(),
            //    player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            //{
            //    player.OnCollisionG(needle);
            //    needle.OnCollision(player);
            //}

            //アイテムオブジェクト----------------------------------------------------------------------------

            int itemObjectsCount = itemObjects.Count;//ループ前の個数を取得しておく

            for (int i = 0; i < itemObjectsCount; i++)
            {
                itemObjects[i].Update();
            }

            //オブジェクト同士の衝突を判定
            for (int i = 0; i < itemObjects.Count; i++)
            {
                //***勝手に書き換えてすみません by 金森 *** 

                for (int j = 0; j < playerObjects.Count; j++)
                {
                    playerObject playerObject = playerObjects[j];

                    //オブジェクトAが死んでたらこのループは終了
                    if (playerObject.isDead) break;

                    ItemObject b = itemObjects[i];

                    //オブジェクトBが死んでたらスキップ
                    if (b.isDead) continue;

                    //オブジェクトAとBが重なっているか？
                    if (MyMath.RectRectIntersect(playerObject.GetLeft(), playerObject.GetTop(), playerObject.GetRight(), playerObject.GetBottom(),
                        b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                    {
                        playerObject.OnCollisionI(b);
                        b.OnCollision(playerObject);
                    }
                }

                //for (int j = i + 1; j < itemObjects.Count; j++)
                //{
                //    //オブジェクトAが死んでたらこのループは終了
                //    if (a.isDead) break;

                //    ItemObject b = itemObjects[j];

                //    //オブジェクトBが死んでたらスキップ
                //    if (b.isDead) continue;

                //    //オブジェクトAとBが重なっているか？
                //    if (MyMath.RectRectIntersect(a.GetLeft(), a.GetTop(), a.GetRight(), a.GetBottom(),
                //        b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                //    {
                //        a.OnCollision(b);
                //        b.OnCollision(a);
                //    }
                //}
            }
            //不要となったオブジェクトを除去する
            itemObjects.RemoveAll(go => go.isDead);
            //不要となったオブジェクトを除去する(player)
            playerObjects.RemoveAll(player => player.isDead);

            //--------------------------------------------------------------------------------------------------

            

        }
        public override void Draw()
        {
            Camera.DrawGraph(0, 0, Image.Stage01);
            //アイテムの描画処理
            foreach (ItemObject go in itemObjects)
            {
                go.Draw();
                go.DrawHitBox();
            }

            foreach(playerObject playerObject in playerObjects)
            {
                playerObject.Draw();
                playerObject.DrawHitBox();
            }

            map.DrawTerrain();
            //線と手を描画しています
            if (player != null && hund != null)
            {                
                Camera.DrawLine(player.Position, hund.Position);
            }

            if(hund != null)
            {
                hund.Draw();
            }            

            foreach( EnemyObject enemy in enemyObjects )
            {
                enemy.Draw();
                enemy.DrawHitBox();
            }

            //針の描画
            needle.Draw();
            needle.DrawHitBox();
        }
    }
}
