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

        //ザコ敵のリソース画像

        //アイテムのリソース画像

        //その他のリソース画像
        public static int[] mapchip = new int[128];

        public static void Load()
        {
            //プレイヤーのリソース画像

            //ザコ敵のリソース画像

            //アイテムのリソース画像

            //その他のリソース画像
            DX.LoadDivGraph("Image/mapchip.png", mapchip.Length, 16, 8, 32, 32, mapchip);
        }
    }
}
