﻿using System;
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
            enemy1 = new Enemy1(this, 500, 300);
            itemObjects.Add(new WoolenYarn(this, 300, 500));
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

            if (player == null)
            {
                return;
            }

            if (player.HP < 0)
            {
                player = null;
            }

            if (player == null)
            {
                return;
            }

            if (player!=null)
            {
                //プレイヤーの更新処理
                player.Update();
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
            if (player.HundFrag)
            {
                hund.Update();
            }

            //エネミー1
            enemy1.Update();

            //オブジェクトAとBが重なっているか？
            if (MyMath.RectRectIntersect(enemy1.GetLeft(), enemy1.GetTop(), enemy1.GetRight(), enemy1.GetBottom(),
                player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom()))
            {
                player.OnCollisionE(enemy1);
                enemy1.OnCollision(player);
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
                //オブジェクトAが死んでたらこのループは終了
                if (player.isDead) break;

                ItemObject b = itemObjects[i];

                //オブジェクトBが死んでたらスキップ
                if (b.isDead) continue;

                //オブジェクトAとBが重なっているか？
                if (MyMath.RectRectIntersect(player.GetLeft(), player.GetTop(), player.GetRight(), player.GetBottom(),
                    b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                {
                    player.OnCollisionI(b);
                    b.OnCollision(player);
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

            //--------------------------------------------------------------------------------------------------

        }
        public override void Draw()
        {            
            if (player != null)
            {
                //プレイヤーの描画処理
                player.Draw();
                player.DrawHitBox();
            }
                
            map.DrawTerrain(); 
            //線と手を描画しています
            if (player != null&&player.HundFrag)
            {
                DX.DrawLine((int)player.PlayerPosition.x, (int)player.PlayerPosition.y, (int)hund.Position.x, (int)hund.Position.y, DX.GetColor(255, 255, 255));
                hund.Draw();
            }

            //エネミー1の描画
            enemy1.Draw();

            enemy1.DrawHitBox();
            

            //アイテムの描画処理
            foreach (ItemObject go in itemObjects)
            {
                go.Draw();
                go.DrawHitBox();
            }
        }
    }
}
