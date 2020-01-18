using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public static class Image
    {
        //プレイヤーのリソース画像
        public static int[] PlayerImage01 = new int[18]; //プレイヤーの正面画像
        public static int PlayerArraws;                  //playerの矢印
        public static int PlayerHand;                    //playerの手

        //ザコ敵のリソース画像
        public static int EnemyImage01;                  //ザコ敵01画像
        public static int EnemyImage02;                  //ザコ敵02画像
        public static int EnemyImage02_1;                //ザコ敵02画像
        public static int EnemyImage03;                  //ザコ敵03画像
        public static int EnemyImage03_1;                //ザコ敵03画像
        public static int Enemy_shot;                    //ザコ敵03の弾

        //アイテムのリソース画像
        public static int ItemStart;
        public static int ItemOver;
        public static int ItemRetry;
        public static int ItemTitle;
        public static int ItemContinue;

        //その他のリソース画像
        public static int[] mapchip = new int[12];     //
        public static int Block01; //ブロックの画像０１        
        public static int Floor01; //フロアの画像０１
        public static int GameClearImage; //ゲームクリア画像
        public static int GameOverImage; //ゲームオーバー画像
        public static int IconIto; //糸のアイコン画像
        public static int IconMap; //マップのアイコン画像
        public static int MapImage; //マップ画像
        public static int miniMapBackBround;//ミニマップの背景画像
        public static int Stage01; //ステージのイメージ画像０１
        public static int TitleImage;　//タイトル画面の画像
        public static int TogeWani01; //ワニのトゲ画像０１
        public static int TogeWani02;　//ワニのトゲ画像０２
        public static int TogeWani03;　//ワニのトゲ画像０２
        public static int MoveFloor; //動く床の画像

        public static int Ito; //糸の画像

        //Particle関係
        public static int particleDot1;
        public static int particleDot2;
        public static int particleDot3;
        public static int particleRing1;
        public static int particleRing2;
        public static int particleRing3;
        public static int particleRing4;
        public static int particleFire;
        public static int particleSteam;
        public static int particleSmoke;
        public static int particleGlitter1;
        public static int particleStar1;
        public static int particleStar2;
        public static int particleLine1;
        public static int particleLine2;
        public static int particleSlash;
        public static int particleStone1;



        public static void Load()
        {
            //プレイヤーのリソース画像
            DX.LoadDivGraph("Image/Player01.png", PlayerImage01.Length, 4, 5, 120, 180, PlayerImage01);
            PlayerArraws = DX.LoadGraph("Image/Arrows.png");
            PlayerHand = DX.LoadGraph("Image/PlayerHand01.png");

            //ザコ敵のリソース画像
            EnemyImage01 = DX.LoadGraph("Image/Enemy01.png");
            EnemyImage02 = DX.LoadGraph("Image/Enemy02.png");
            EnemyImage02_1 = DX.LoadGraph("Image/Enemy02_1.png");
            EnemyImage03 = DX.LoadGraph("Image/Enemy03.png");
            EnemyImage03_1 = DX.LoadGraph("Image/Enemy03_1.png");
            Enemy_shot = DX.LoadGraph("Image/Enemy_shot.png");

            //アイテムのリソース画像
            ItemTitle = DX.LoadGraph("Image/ItemTitle.png");
            ItemRetry = DX.LoadGraph("Image/Retry.png");
            ItemStart = DX.LoadGraph("Image/Start.png");
            ItemOver = DX.LoadGraph("Image/Over.png");
            ItemContinue = DX.LoadGraph("Image/Continue.png");

            //その他のリソース画像
            DX.LoadDivGraph("Image/mapchip.png", mapchip.Length, 6, 2, 60, 60, mapchip);
            Block01 = DX.LoadGraph("Image/Block01.png");
            Floor01 = DX.LoadGraph("Image/Floor01.png");
            GameClearImage = DX.LoadGraph("Image/GameClear.jpg");
            GameOverImage = DX.LoadGraph("Image/GameOver.jpg");
            IconIto = DX.LoadGraph("Image/IconIto.png");
            IconMap = DX.LoadGraph("Image/IconMap.png");
            MapImage = DX.LoadGraph("Image/Map.jpg");
            miniMapBackBround = DX.LoadGraph("Image/minimap.png");
            Stage01 = DX.LoadGraph("Image/Stage.png");
            TitleImage = DX.LoadGraph("Image/Title.png");
            TogeWani01 = DX.LoadGraph("Image/TogeWani01.png");
            TogeWani02 = DX.LoadGraph("Image/TogeWani02.png");
            TogeWani03 = DX.LoadGraph("Image/TogeWani03.png");
            MoveFloor = DX.LoadGraph("Image/MoveFloor.png");

            Ito = DX.LoadGraph("Image/Ito.png");

            //Particle関係
            particleDot1 = DX.LoadGraph("Image/Particle/particle_dot_1.png");
            particleDot2 = DX.LoadGraph("Image/Particle/particle_dot_2.png");
            particleDot3 = DX.LoadGraph("Image/Particle/particle_dot_3.png");
            particleRing1 = DX.LoadGraph("Image/Particle/particle_ring_1.png");
            particleRing2 = DX.LoadGraph("Image/Particle/particle_ring_2.png");
            particleRing3 = DX.LoadGraph("Image/Particle/particle_ring_3.png");
            particleRing4 = DX.LoadGraph("Image/Particle/particle_ring_4.png");
            particleFire = DX.LoadGraph("Image/Particle/particle_fire.png");
            particleSteam = DX.LoadGraph("Image/Particle/particle_steam.png");
            particleSmoke = DX.LoadGraph("Image/Particle/particle_smoke.png");
            particleGlitter1 = DX.LoadGraph("Image/Particle/particle_glitter_1.png");
            particleStar1 = DX.LoadGraph("Image/Particle/particle_star_1.png");
            particleStar2 = DX.LoadGraph("Image/Particle/particle_star_2.png");
            particleLine1 = DX.LoadGraph("Image/Particle/particle_line_1.png");
            particleLine2 = DX.LoadGraph("Image/Particle/particle_line_2.png");
            particleSlash = DX.LoadGraph("Image/Particle/particle_slash.png");
            particleStone1 = DX.LoadGraph("Image/Particle/particle_stone_1.png");
        }
    }
}
