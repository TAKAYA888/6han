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
        public static int[] PlayerImage01 = new int[18]; //プレイヤー(Normal)の画像
        public static int[] PlayerImage02 = new int[18]; //プレイヤー(Speed)の画像
        public static int[] PlayerImage03 = new int[18]; //プレイヤー(Damege)の画像
        public static int PlayerArraws;                  //playerの矢印
        public static int PlayerHand;                    //playerの手
        public static int PlayerHand2;                  

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
        public static int[] mapchip = new int[12];      //
        public static int Block01;                      //ブロックの画像０１        
        public static int Floor01;                      //フロアの画像０１
        public static int SpeedUpItem;
        public static int GameClearImage;               //ゲームクリア画像
        public static int GameOverImage;                //ゲームオーバー画像
        public static int GameOverImage01;                //ゲームオーバー画像
        public static int IconIto;                      //糸のアイコン画像
        public static int IconMap;                      //マップのアイコン画像
        public static int MapImage;                     //マップ画像
        public static int miniMapBackBround;            //ミニマップの背景画像
        public static int Tutorial01;                   //チュートリアル０１
        public static int Tutorial02;                   //チュートリアル０２
        public static int Tutorial03;                   //チュートリアル０３
        public static int Stage01;                      //ステージのイメージ画像０１
        public static int TitleImage;                 　//タイトル画面の画像
        public static int TogeWani01;                   //ワニのトゲ画像０１
        public static int TogeWani02;　                 //ワニのトゲ画像０２
        public static int TogeWani03;　                 //ワニのトゲ画像０２
        public static int MoveFloor;                    //動く床の画像
        public static int[] Goal = new int[12];       
        public static int[] KeyImage = new int[2];      //鍵の見た目 
        public static int[] PushAbuttom = new int[2];
        public static int[] PushBbuttom = new int[2];
        public static int T1hukidasi;
        public static int[] T1out = new int[13];
        public static int[] GetIto = new int[2];
        public static int[] GetSpeedUp = new int[2];
        public static int[] keysetumei = new int[2];
        public static int[] T2Out = new int[14];
        public static int T2hukidasi;
        public static int[] T3out = new int[15];
        public static int skip;

        public static int Ito;                          //糸の画像

        //スコア
        public static int ScoreS;                　　   //スコアS画像
        public static int ScoreA;               　　    //スコアA画像
        public static int ScoreB;               　　    //スコアB画像
        public static int ScoreC;              　　     //スコアC画像

        //ギミック
        public static int[] Gimmick1 = new int[3];                　　   //ギミックdoor←画像
        public static int[] Gimmick2 = new int[3];                　　   //ギミックdoor→画像
        public static int[] Gimmick3 = new int[3];                　　   //ギミックdoor↑画像
        public static int[] Gimmick4 = new int[3];                　　   //ギミックdoor↓画像

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
            DX.LoadDivGraph("Image/Player02.png", PlayerImage02.Length, 4, 5, 120, 180, PlayerImage02);
            DX.LoadDivGraph("Image/Player03.png", PlayerImage03.Length, 4, 5, 120, 180, PlayerImage03);
            PlayerArraws = DX.LoadGraph("Image/Arrows.png");
            PlayerHand = DX.LoadGraph("Image/PlayerHand01.png");
            PlayerHand2 = DX.LoadGraph("Image/PlayerHand02.png");

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
            SpeedUpItem = DX.LoadGraph("Image/ItemsSpeedUp.png");
            Block01 = DX.LoadGraph("Image/Block01.png");
            Floor01 = DX.LoadGraph("Image/Floor01.png");
            GameClearImage = DX.LoadGraph("Image/GameClear.jpg");
            GameOverImage = DX.LoadGraph("Image/GameOver02.jpg");
            //GameOverImage01=DX.LoadGraph("")
            IconIto = DX.LoadGraph("Image/IconIto.png");
            IconMap = DX.LoadGraph("Image/IconMap.png");
            MapImage = DX.LoadGraph("Image/Map.png");
            miniMapBackBround = DX.LoadGraph("Image/Map.png");
            Tutorial01 = DX.LoadGraph("Image/01.png");
            Tutorial02 = DX.LoadGraph("Image/02.png");
            Tutorial03 = DX.LoadGraph("Image/03.png");
            Stage01 = DX.LoadGraph("Image/Stage.png");
            TitleImage = DX.LoadGraph("Image/Title01.jpg");
            TogeWani01 = DX.LoadGraph("Image/TogeWani01.png");
            TogeWani02 = DX.LoadGraph("Image/TogeWani02.png");
            TogeWani03 = DX.LoadGraph("Image/TogeWani03.png");
            MoveFloor = DX.LoadGraph("Image/MoveFloor.png");
            DX.LoadDivGraph("Image/Friend01.png", Goal.Length, 4, 3, 120, 180,Goal);

            DX.LoadDivGraph("Image/Switch.png", KeyImage.Length, 2, 1, 40, 60, KeyImage);

            PushAbuttom[0] = DX.LoadGraph("Image/C/C03.png");
            PushAbuttom[1] = DX.LoadGraph("Image/C/C04.png");
            PushBbuttom[0] = DX.LoadGraph("Image/C/C04_1.png");
            PushBbuttom[1] = DX.LoadGraph("Image/C/C04_2.png");
            T1hukidasi = DX.LoadGraph("Image/C/C02.png");
            T1out[0] = DX.LoadGraph("Image/C/C01.png");
            T1out[1] = DX.LoadGraph("Image/C/C05.png");
            T1out[2] = DX.LoadGraph("Image/C/C06.png");
            T1out[3] = DX.LoadGraph("Image/C/C07.png");
            //T1out[4] = DX.LoadGraph("Image/C/C08.png");
            T1out[4] = DX.LoadGraph("Image/C/C09.png");
            T1out[5] = DX.LoadGraph("Image/C/C10.png");
            T1out[6] = DX.LoadGraph("Image/C/C11.png");
            T1out[7] = DX.LoadGraph("Image/C/C12.png");
            T1out[8] = DX.LoadGraph("Image/C/C13.png");
            T1out[9] = DX.LoadGraph("Image/C/C14.png");
            T1out[10] = DX.LoadGraph("Image/C/C15.png");
            T1out[11] = DX.LoadGraph("Image/C/C16.png");
            T1out[12] = DX.LoadGraph("Image/C/C17.png");
            GetIto[0] = DX.LoadGraph("Image/C/C3_03.png");
            GetIto[1] = DX.LoadGraph("Image/C/C3_03_1.png");
            GetSpeedUp[0] = DX.LoadGraph("Image/C/C3_02_1.png");
            GetSpeedUp[1] = DX.LoadGraph("Image/C/C3_02_2.png");
            keysetumei[0] = DX.LoadGraph("Image/C/C2_03.png");
            keysetumei[1] = DX.LoadGraph("Image/C/C2_03_1.png");
            T2Out[0] = DX.LoadGraph("Image/C/C2_04.png");
            T2Out[1] = DX.LoadGraph("Image/C/C2_05.png");
            T2Out[2] = DX.LoadGraph("Image/C/C2_06.png");
            T2Out[3] = DX.LoadGraph("Image/C/C2_07.png");
            T2Out[4] = DX.LoadGraph("Image/C/C2_08.png");
            T2Out[5] = DX.LoadGraph("Image/C/C2_09.png");
            T2Out[6] = DX.LoadGraph("Image/C/C2_10.png");
            T2Out[7] = DX.LoadGraph("Image/C/C2_11.png");
            T2Out[8] = DX.LoadGraph("Image/C/C2_12.png");
            T2Out[9] = DX.LoadGraph("Image/C/C2_13.png");
            T2Out[10] = DX.LoadGraph("Image/C/C2_14.png");
            T2Out[11] = DX.LoadGraph("Image/C/C2_15.png");
            T2Out[12] = DX.LoadGraph("Image/C/C2_16.png");
            T2hukidasi = DX.LoadGraph("Image/C/C3_02.png");
            T3out[0] = DX.LoadGraph("Image/C/C3_01.png");
            T3out[1] = DX.LoadGraph("Image/C/C3_04.png");
            T3out[2] = DX.LoadGraph("Image/C/C3_05.png");
            T3out[3] = DX.LoadGraph("Image/C/C3_06.png");
            T3out[4] = DX.LoadGraph("Image/C/C3_07.png");
            T3out[5] = DX.LoadGraph("Image/C/C3_08.png");
            T3out[6] = DX.LoadGraph("Image/C/C3_09.png");
            T3out[7] = DX.LoadGraph("Image/C/C3_10.png");
            T3out[8] = DX.LoadGraph("Image/C/C3_11.png");
            T3out[9] = DX.LoadGraph("Image/C/C3_12.png");
            T3out[10] = DX.LoadGraph("Image/C/C3_13.png");
            T3out[11] = DX.LoadGraph("Image/C/C3_14.png");
            T3out[12] = DX.LoadGraph("Image/C/C3_15.png");
            T3out[13] = DX.LoadGraph("Image/C/C3_16.png");
            skip = DX.LoadGraph("Image/C/Skip.png");

            //糸
            Ito = DX.LoadGraph("Image/Ito.png");

            //スコア
            ScoreS = DX.LoadGraph("Image/ScoreS.png");
            ScoreA = DX.LoadGraph("Image/ScoreA.png");
            ScoreB = DX.LoadGraph("Image/ScoreB.png");
            ScoreC = DX.LoadGraph("Image/ScoreC.png");

            //ギミック
            DX.LoadDivGraph("Image/GimmickLeft.png", Gimmick1.Length, 3, 1, 60, 60, Gimmick1);
            DX.LoadDivGraph("Image/GimmickRight.png", Gimmick2.Length, 3, 1, 60, 60, Gimmick2);
            DX.LoadDivGraph("Image/GimmickUp.png", Gimmick3.Length, 3, 1, 60, 120, Gimmick3);
            DX.LoadDivGraph("Image/GimmickDown.png", Gimmick4.Length, 3, 1, 60, 60, Gimmick4);


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
