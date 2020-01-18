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
    public class PlayScene : Scene
    {
        //プレイヤーを設定
        public Player player;
        //針
        NeedleObject needle;
        //動く床
        MoveFloorObject moveFloor;

        //時計
        int timer;

        public enum State
        {
            Active,
            PlayerDied
        }

        public Map map;
        MiniMap miniMap;
        //ハンドを設定
        public Hand hand;

        public List<ItemObject> itemObjects = new List<ItemObject>(); //アイテムオブジェクトの配列
        public List<EnemyObject> enemyObjects = new List<EnemyObject>();
        public List<EnemyBullet> enemyBullets;//敵弾のリスト
        public List<playerObject> playerObjects = new List<playerObject>();

        public State state = State.Active;
        int timeToGameOver = 120;
        bool isPausing = false;

        public PlayScene()
        {
            //enemy1 = new Enemy1(this, 700, 300);
            enemyObjects.Add(new Enemy1(this, 700, 300));
            enemyObjects.Add(new Enemy2(this, 1500, 1400));
            enemyObjects.Add(new Enemy3(this, 1700, 1400));
            enemyBullets = new List<EnemyBullet>();
            //itemObjects.Add(new WoolenYarn(this, 300, 500));
            needle = new NeedleObject(this, 840, 1440);
            moveFloor = new MoveFloorObject(this, 1500, 1400);
            map = new Map(this, "stage1");
            miniMap = new MiniMap(this, "stage1");
        }
        public override void Update()
        {
            timer++;

            if (player.isDead == true)
            {
                Game.ChangeScene(new GameOverScene());
            }

            if (isPausing)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_8))
                {
                    isPausing = false;
                }
                return;
            }
            
            //---------------------------------------------------------------------------------------------------

            //ての作成
            if (Input.GetButtonDown(DX.PAD_INPUT_10) || Input.GetButtonDown(DX.PAD_INPUT_1) && hand == null)
            {
                player.HandDestroyTimer = player.HandDestroyTime;                
                hand = new Hand(this, player, player.Position.x, player.Position.y);
            }

            //PlayerObjectの更新処理
            foreach (playerObject playerObject in playerObjects)
            {
                playerObject.Update();
            }

            if (state == State.PlayerDied)
            {
                timeToGameOver--;//カウントダウン

                if (timeToGameOver <= 0)
                {

                }
            }

            //手の更新処理
            if (hand != null)
            {
                hand.Update();
            }

            //EnemyObの更新処理
            foreach (EnemyObject enemyObject in enemyObjects)
            {
                enemyObject.Update(player);
            }

            // 敵弾の更新処理
            foreach (EnemyBullet b in enemyBullets)
            {
                b.Update();
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

            //手とEnemyobjectの衝突処理
            if (hand != null)
            {
                for (int i = 0; i < enemyObjects.Count; ++i)
                {
                    EnemyObject enemyObject = enemyObjects[i];

                    if (enemyObject.isDead) continue;

                    if (hand == null) { break; }//バグったので一時的に書き足しました

                    if (MyMath.RectRectIntersect(hand.GetLeft(), hand.GetTop(), hand.GetRight(), hand.GetBottom(),
                         enemyObject.GetLeft(), enemyObject.GetTop(), enemyObject.GetRight(), enemyObject.GetBottom()))
                    {
                        hand.OnCollision(enemyObject);
                        enemyObject.OnCollisionH(hand);
                    }
                }
            }

            for (int i = 0; i < playerObjects.Count(); i++)
            {
                if (hand == null)
                    break;

                playerObject player = playerObjects[i];

                if (MyMath.RectRectIntersect(hand.GetLeft(), hand.GetTop(), hand.GetRight(), hand.GetBottom(),
                         player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
                {
                    hand.OnCollisionP(player);
                    player.OnCollisionHand(hand);
                }


            }

            //死んでいるEnemyの処理？
            enemyObjects.RemoveAll(ene => ene.isDead);

            //針
            //オブジェクトAとBが重なっているか？
            //if (MyMath.RectRectIntersect(needle.GetLeft(), needle.GetTop(), needle.GetRight(), needle.GetBottom(),
            //    player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            //{
            //    player.OnCollisionG(needle);
            //    needle.OnCollision(player);
            //}

            //動く床
            moveFloor.Update();

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

            }
            //不要となったオブジェクトを除去する
            itemObjects.RemoveAll(go => go.isDead);
            //不要となったオブジェクトを除去する(player)
            playerObjects.RemoveAll(player => player.isDead);
            //不要となったオブジェクトを除去する(enemy_shot)
            enemyBullets.RemoveAll(eb => eb.isDead);

            //--------------------------------------------------------------------------------------------------     

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
                go.DrawHitBox();
            }

            foreach (playerObject playerObject in playerObjects)
            {
                playerObject.Draw();
                playerObject.DrawHitBox();
            }

            map.DrawTerrain();
            //線と手を描画しています
            if (player != null && hand != null)
            {
                Camera.DrawLine(player.Position, hand.Position);
            }

            if (hand != null)
            {
                hand.Draw();
            }

            foreach (EnemyObject enemy in enemyObjects)
            {
                enemy.Draw();
                enemy.DrawHitBox();
            }

            // 敵弾の描画
            foreach (EnemyBullet b in enemyBullets)
            {
                b.Draw();
                //b.DrawHitBox();
            }

            //針の描画
            needle.Draw();
            needle.DrawHitBox();

            //動く床の描画
            moveFloor.Draw();
            moveFloor.DrawHitBox();

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
        }
    }
}
