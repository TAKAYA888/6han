using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using DxLibDLL;

namespace ActionGame
{
    public class Map
    {
        public const int None = -1;
        public const int Wall = 0;

        public const int Width = 64;
        public const int Height = 27;
        public const int CellSize = 60;

        PlayScene playScene;
        int[,] terrain;

        public Map(PlayScene playScene, string stageName)
        {
            this.playScene = playScene;

            LoadTerrain("Map/" + stageName + "_terrain.csv");
            LoadObjects("Map/" + stageName + "_object.csv");
        }
        void LoadTerrain(string filePath)
        {
            terrain = new int[Width, Height];
            string[] lines = File.ReadAllLines(filePath);

            Debug.Assert(lines.Length == Height, filePath + "の高さが不正です:" + lines.Length);

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
            string[] lines = File.ReadAllLines(filePath); // ファイルを行ごとに読み込む

            Debug.Assert(lines.Length == Height, filePath + "の高さが不正です：" + lines.Length);

            for (int y = 0; y < Height; y++)
            {
                string[] splitted = lines[y].Split(new char[] { ',' });

                Debug.Assert(splitted.Length == Width, filePath + "の" + y + "行目の列数が不正です:" + splitted.Length);

                for (int x = 0; x < Width; x++)
                {
                    int id = int.Parse(splitted[x]);

                    if (id == -1) continue;

                    SpawnObject(x, y, id);
                }
            }
        }

        void SpawnObject(int mapX, int mapY, int objectID)
        {
            float spawnX = mapX * CellSize;
            float spawnY = mapY * CellSize;
        }

        public void DrawTerrain()
        {
            int left = (int)(Camera.x / CellSize);
            int top = (int)(Camera.y / CellSize);
            int right = (int)((Camera.x + Screen.Width - 1) / CellSize);
            int bottom = (int)((Camera.y + Screen.Height - 1) / CellSize);

            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (right >= Width) right = Width - 1;
            if (bottom >= Height) bottom = Height - 1;

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    int id = terrain[x, y];

                    if (id == None) continue; // 描画しない 

                    Camera.DrawGraph(x * CellSize, y * CellSize, Image.Floor01);
                }
            }
        }

        public int GetTerrain(float worldX, float worldY)
        {
            // マップ座標系（二次元配列の行と列）に変換する 
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            // 二次元配列の範囲外は、何もないものとして扱う 
            if (mapX < 0 || mapX >= Width || mapY < 0 || mapY >= Height)
                return None;

            return terrain[mapX, mapY]; // 二次元配列から地形IDを取り出して返却する
        }

        public bool IsWall(float worldX, float worldY)
        {
            int terrainID = GetTerrain(worldX, worldY); // 指定された座標の地形のIDを取得

            return terrainID == Wall; // 地形が壁ならtrue、違うならfalseを返却する
        }
    }
}
