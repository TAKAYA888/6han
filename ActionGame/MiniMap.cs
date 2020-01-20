using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Diagnostics;
using System.IO;

namespace ActionGame
{
    public class MiniMap
    {
        public const int Width = 64;
        public const int Height = 27;
        public const int CellSizeX = 27;
        const int CellSizeY = 27;
        public const int None = -1;
        public const int Wall = 0;
        PlayScene playScene;
        int[,] terrain;
        int[,] _object;

        public MiniMap(PlayScene playScene, string stageNeme)
        {
            terrain = new int[Width, Height];
            _object = new int[Width, Height];
            LoadTerrain("Map/" + stageNeme + "_terrain.csv");
            LoadObjects("Map/" + stageNeme + "_object.csv");
        }

        void LoadTerrain(string filePath)
        {
            terrain = new int[Width, Height];
            string[] lines = File.ReadAllLines(filePath);

            //Debug.Assert(lines.Length == Height, filePath + "の高さが不正です:" + lines.Length);

            for (int y = 0; y < Height; y++)
            {
                string[] splitted = lines[y].Split(new char[] { ',' });

                Debug.Assert(splitted.Length == Width, filePath + "の" + y + "行目の列数が不正です:" + splitted.Length);

                for (int x = 0; x < Width; x++)
                {
                    terrain[x, y] = int.Parse(splitted[x]);
                }
            }
        }

        void LoadObjects(string filePath)
        {
            _object = new int[Width, Height];
            string[] lines = File.ReadAllLines(filePath);

            //Debug.Assert(lines.Length == Height, filePath + "の高さが不正です:" + lines.Length);

            for (int y = 0; y < Height; y++)
            {
                string[] splitted = lines[y].Split(new char[] { ',' });

                Debug.Assert(splitted.Length == Width, filePath + "の" + y + "行目の列数が不正です:" + splitted.Length);

                for (int x = 0; x < Width; x++)
                {
                    _object[x, y] = int.Parse(splitted[x]);
                }
            }
        }
        public void Draw()
        {

            DX.DrawGraph(0, 0, Image.miniMapBackBround);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int id = terrain[x, y];

                    if (id == None) continue; // 描画しない 

                    DX.DrawRotaGraph(x * CellSizeX + 100, y * CellSizeY + 200, 0.5, 0, Image.Floor01);
                }
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int id = _object[x, y];

                    if (id == None) continue; // 描画しない 

                    else if (id == 0)
                    {
                        DX.DrawRotaGraph(x * CellSizeX + 100, y * CellSizeY + 200, 0.5, 0, Image.PlayerImage01[1]);
                    }
                    else if (id == 1)
                    {
                        DX.DrawRotaGraph(x * CellSizeX + 100, y * CellSizeY + 200, 0.5, 0, Image.PlayerImage01[1]);
                    }
                    else if (id == 2)
                    {
                        DX.DrawRotaGraph(x * CellSizeX + 100, y * CellSizeY + 200, 0.5, 0, Image.Ito);
                    }
                    else if (id == 3)
                    {
                        DX.DrawRotaGraph(x * CellSizeX + 100, y * CellSizeY + 200, 0.5, 0, Image.IconIto);
                    }
                }
            }

        }
    }
}
