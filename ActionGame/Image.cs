﻿using System;
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
        public static int PlayerImage01; //プレイヤーの正面画像

        //ザコ敵のリソース画像
        public static int EnemyImage01; //ザコ敵01画像

        //アイテムのリソース画像

        //その他のリソース画像
        public static int[] mapchip = new int[128];
        public static int Block01; //ブロックの画像０１
        public static int Floor01; //フロアの画像０１
        public static int GameClearImage; //ゲームクリア画像
        public static int GameOverImage; //ゲームオーバー画像
        public static int IconIto; //糸のアイコン画像
        public static int IconMap; //マップのアイコン画像
        public static int Map; //マップ画像
        public static int Stage01; //ステージのイメージ画像０１
        public static int TitleImage;　//タイトル画面の画像
        public static int TogeWani01; //ワニのトゲ画像０１
        public static int TogeWani02;　//ワニのトゲ画像０２
        public static int TogeWani03;　//ワニのトゲ画像０２

        public static void Load()
        {
            //プレイヤーのリソース画像
            PlayerImage01 = DX.LoadGraph("Image/Player01.png");

            //ザコ敵のリソース画像
            EnemyImage01 = DX.LoadGraph("Image/Enemy01.png");

            //アイテムのリソース画像

            //その他のリソース画像
            DX.LoadDivGraph("Image/mapchip.png", mapchip.Length, 16, 8, 32, 32, mapchip);
            Block01 = DX.LoadGraph("Image/Block01.png");
            Floor01 = DX.LoadGraph("Image/Floor01.png");
            GameClearImage = DX.LoadGraph("Image/GameClear.jpg");
            GameOverImage = DX.LoadGraph("Image/GameOver.jpg");
            IconIto = DX.LoadGraph("Image/IconIto.png");
            IconMap = DX.LoadGraph("Image/IconMap.png");
            Map = DX.LoadGraph("Image/Map.png");
            Stage01 = DX.LoadGraph("Image/Stage01.jpg");
            TitleImage = DX.LoadGraph("Image/Title.png");
            TogeWani01 = DX.LoadGraph("Image/TogeWani01.png");
            TogeWani02 = DX.LoadGraph("Image/TogeWani02.png");
            TogeWani03 = DX.LoadGraph("Image/TogeWani03.png");
        }
    }
}
