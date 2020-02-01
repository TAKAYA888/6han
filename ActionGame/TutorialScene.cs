using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;
using DxLibDLL;
using MyMath_KNMR;

namespace ActionGame
{
    //チュートリアルシーン
    public class TutorialScene : Scene
    {

        //プレイヤーを設定
        public Player player;
        //時計
        int timer;

        public enum State
        {
            Active,
            PlayerDied
        }

        public State state = State.Active;
        public Map map;
        MiniMap miniMap;

        public List<ItemObject> itemObjects = new List<ItemObject>();             //アイテムオブジェクトの配列
        public List<EnemyObject> enemyObjects = new List<EnemyObject>();          //Enemyの配列
        public List<playerObject> playerObjects = new List<playerObject>();       //Playerの配列
        public List<GimmickObject> gimmickObjects = new List<GimmickObject>();    //ギミックオブジェクトの配列
        public List<key> keys = new List<key>();

        int timeToGameOver = 120;
        bool isPausing = false;

        public TutorialScene()
        {
            DX.SetFontSize(64);
            //map = new Map(this, "tutorial1");//チュートリアルに変更する
            //miniMap = new MiniMap(this, "tutorial1");
            //DX.PlayMusic("BGM/Play_scene.mp3", DX.DX_PLAYTYPE_LOOP);
        }


        public override void Update()
        {
            timer++;

            if (player.isDead == true)
            {
                //Game.ChangeScene(new GameOverScene());
                //リスタート
            }

            //ポーズ画面の処理
            if (isPausing)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_8))
                {
                    isPausing = false;
                }
                return;
            }

            //---------------------------------------------------------------------------------------------------


            if (state == State.PlayerDied)
            {
                timeToGameOver--;//カウントダウン

                if (timeToGameOver <= 0)
                {

                }
            }


            //playerObjectとEnemyObjectの衝突処理
            for (int i = 0; i < enemyObjects.Count; i++)
            {
                for (int j = 0; j < playerObjects.Count; j++)
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

            //GimmickObjectとEnemyObjectの衝突処理
            for (int i = 0; i < enemyObjects.Count; i++)
            {
                for (int j = 0; j < gimmickObjects.Count; j++)
                {
                    GimmickObject gimmick = gimmickObjects[j];

                    if (gimmick.isDead) break;

                    EnemyObject enemy = enemyObjects[i];

                    if (enemy.isDead) break;

                    if (MyMath.RectRectIntersect(gimmick.GetLeft(), gimmick.GetTop(), gimmick.GetRight(), gimmick.GetBottom(),
                         enemy.GetLeft(), enemy.GetTop(), enemy.GetRight(), enemy.GetBottom()))
                    {
                        gimmick.OnCollisionE(enemy);
                        enemy.OncollisionG(gimmick);
                    }
                }
            }



            for (int i = 0; i < playerObjects.Count(); i++)
            {
                playerObject objectA = playerObjects[i];

                if (objectA.isDead == true) continue;

                for (int j = 0; j < playerObjects.Count(); j++)
                {
                    if (i == j)
                        break;

                    playerObject objectB = playerObjects[j];

                    if (objectB.isDead == true) break;

                    if (MyMath.RectRectIntersect(
                        objectA.GetLeft(), objectA.GetTop(), objectA.GetRight(), objectA.GetBottom(),
                        objectB.GetLeft(), objectB.GetTop(), objectB.GetRight(), objectB.GetBottom()))
                    {
                        objectA.OnCollisionP(objectB);
                        objectB.OnCollisionP(objectA);
                    }
                }
            }

            for (int i = 0; i < playerObjects.Count(); i++)
            {
                playerObject playerObject = playerObjects[i];

                if (playerObject.isDead) continue;

                for (int j = 0; j < gimmickObjects.Count(); j++)
                {
                    GimmickObject gimmickObject = gimmickObjects[j];

                    if (gimmickObject.isDead == true) break;

                    if (MyMath.RectRectIntersect(
                        playerObject.GetLeft(), playerObject.GetTop(), playerObject.GetRight(), playerObject.GetBottom(),
                        gimmickObject.GetLeft(), gimmickObject.GetTop(), gimmickObject.GetRight(), gimmickObject.GetBottom())
                        )
                    {
                        playerObject.OnCollisionG(gimmickObject);
                        gimmickObject.OnCollision(playerObject);
                    }
                }
            }



            //アイテムオブジェクト----------------------------------------------------------------------------

            int itemObjectsCount = itemObjects.Count;//ループ前の個数を取得しておく

            for (int i = 0; i < itemObjectsCount; i++)
            {
                itemObjects[i].Update();
            }

            //オブジェクト同士の衝突を判定
            for (int i = 0; i < itemObjects.Count; i++)
            {

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
            }
            //--------------------------------------------------------------------------------------------------     

            ObjectUpdate();


            //不要となったオブジェクトを除去する
            itemObjects.RemoveAll(go => go.isDead);
            //不要となったオブジェクトを除去する(player)
            playerObjects.RemoveAll(player => player.isDead);
            //死んでいるEnemyの処理？
            enemyObjects.RemoveAll(ene => ene.isDead);

            if (Input.GetButtonDown(DX.PAD_INPUT_8))
            {
                isPausing = true;
            }
        }
        public override void Draw()
        {
            Console.WriteLine(timer);
            Camera.DrawGraph(0, 0, Image.Stage01);
            //アイテムの描画処理
            foreach (ItemObject go in itemObjects)
            {
                go.Draw();
                //go.DrawHitBox();
            }

            foreach (playerObject playerObject in playerObjects)
            {
                playerObject.Draw();
                //playerObject.DrawHitBox();
            }

            map.DrawTerrain();

            foreach (EnemyObject enemy in enemyObjects)
            {
                enemy.Draw();
                //enemy.DrawHitBox();
            }


            foreach (GimmickObject gimmick in gimmickObjects)
            {
                gimmick.Draw();
                //gimmick.DrawHitBox();
            }

            // ポーズ中の半透明のスクリーンの描画
            if (isPausing)
            {
                // 半透明の指定。第2引数で0～255でアルファ値（不透明度）を指定する。
                // 不透明度を変えたら、明示的に元に戻すまでは継続されるので注意
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 80);
                // 画面全体を黒で塗りつぶす
                DX.DrawBox(0, 0, Screen.Width, Screen.Height, DX.GetColor(0, 0, 0), DX.TRUE);
                // 不透明度を元に戻す
                DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);

                miniMap.Draw();
            }

            DX.DrawGraph(0, 0, Image.IconIto);
            DX.DrawString(110, 50, "×" + player.haveWoolenYarn.ToString(), DX.GetColor(255, 255, 255));
            DX.DrawGraph(0, 130, Image.IconMap);
        }

        void ObjectUpdate()
        {
            //Player
            for (int i = 0; i < playerObjects.Count(); i++)
            {
                playerObjects[i].StorePostionAndHitBox();//1フレーム前の情報を記憶させる

                playerObjects[i].Update();
            }

            //EnemyObの更新処理
            foreach (EnemyObject enemyObject in enemyObjects)
            {
                enemyObject.Update(player);
            }


            //ギミックの更新処理
            foreach (GimmickObject gimmick in gimmickObjects)
            {
                gimmick.StorePostionAndHitBox();//1フレーム前の情報を記憶させる

                gimmick.Update();
            }
        }
    }
}

