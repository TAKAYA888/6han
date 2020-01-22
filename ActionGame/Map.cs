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
        //readonlyは読み込み専用の変数の事です
        public readonly int None = -1;              //何もない場所
        public readonly int Wall = 0;               //刺さらない壁しかし現在は刺さる
        public readonly int ProtrusionBlock = 1;    //見た目は突起があるブロック、刺さる壁
        public readonly int NormalBlock = 2;        //見た目は突起がないブロック、刺さる壁

        public const int Width = 64;                //
        public const int Height = 27;               //
        public const int CellSize = 60;             //

        PlayScene playScene;
        int[,] terrain;

        public Map(PlayScene playScene, string stageName)
        {
            this.playScene = playScene;

            LoadTerrain("Map/" + stageName + "_terrain.csv");
            LoadObjects("Map/" + stageName + "_object.csv", "Map/" + stageName + "_number.csv");
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

        void LoadObjects(string filePath, string numberFailPath)
        {
            string[] ObjectLines = File.ReadAllLines(filePath); // ファイルを行ごとに読み込む(object)
            string[] number = File.ReadAllLines(numberFailPath);//ファイルを行ごとに読み込む(number)

            Debug.Assert(ObjectLines.Length == Height, filePath + "の高さが不正です：" + ObjectLines.Length);
            Debug.Assert(number.Length == Height, numberFailPath + "の高さが不正です:" + number.Length);

            for (int y = 0; y < Height; y++)
            {
                string[] splitted = ObjectLines[y].Split(new char[] { ',' });
                string[] objectNumber = number[y].Split(new char[] { ',' });

                Debug.Assert(splitted.Length == Width, filePath + "の" + y + "行目の列数が不正です:" + splitted.Length);

                for (int x = 0; x < Width; x++)
                {
                    int id = int.Parse(splitted[x]);
                    int OBJECTnumber = int.Parse(objectNumber[x]);

                    if (id == -1) continue;

                    SpawnObject(x, y, id, OBJECTnumber);
                }
            }
        }

        void SpawnObject(int mapX, int mapY, int objectID, int objectNumber)
        {
            float spawnX = mapX * CellSize;
            float spawnY = mapY * CellSize;

            if (objectID == 0)//Player
            {
                Player player = new Player(playScene, spawnX, spawnY);

                playScene.playerObjects.Add(player);

                playScene.player = player;
            }
            else if (objectID == 1) 　//ゴール描画
            {
                playScene.itemObjects.Add(new Goal(playScene, spawnX, spawnY));
            }
            else if (objectID == 2) 　//毛糸描画
            {
                playScene.itemObjects.Add(new WoolenYarn(playScene, spawnX, spawnY));
            }
            else if (objectID == 3) 　//SpeedUp描画
            {
                playScene.itemObjects.Add(new SpeedUp(playScene, spawnX, spawnY));
            }
            else if (objectID == 4)    //Enemy1
            {
                playScene.enemyObjects.Add(new Enemy1(playScene, spawnX, spawnY));
            }
            else if (objectID == 5)    //Enemy2
            {
                playScene.enemyObjects.Add(new Enemy2(playScene, spawnX, spawnY));
            }
            else if (objectID == 6)    //Enemy3
            {
                playScene.enemyObjects.Add(new Enemy3(playScene, spawnX, spawnY));
            }
            else if (objectID == 7)     //針
            {
                NeedleObject needleObject = new NeedleObject(playScene, spawnX, spawnY);

                needleObject.number = objectNumber;

                playScene.gimmickObjects.Add(needleObject);
            }
            else if (objectID == 8)     //動く床
            {
                playScene.gimmickObjects.Add(new MoveFloorObject(playScene, spawnX, spawnY));
            }
            else if (objectID == 9)     //鍵
            {
                key Key = new key(playScene, spawnX, spawnY);

                Key.KeyNunber = objectNumber;

                playScene.gimmickObjects.Add(Key);

                playScene.keys.Add(Key);
            }
            else if (objectID == 10)    //鍵のドア
            {
                KeyDoor keyDoor = new KeyDoor(playScene, spawnX, spawnY);

                keyDoor.DoorNunber = objectNumber;

                playScene.gimmickObjects.Add(keyDoor);
            }
            else if (objectID == 11)
            {
                playScene.itemObjects.Add(new Grounds(playScene, spawnX, spawnY));
            }
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

                    else if (id == Wall) Camera.DrawGraph(x * CellSize, y * CellSize, Image.mapchip[0]);

                    else if (id == ProtrusionBlock) Camera.DrawGraph(x * CellSize, y * CellSize, Image.mapchip[1]);

                    else if (id == NormalBlock) Camera.DrawGraph(x * CellSize, y * CellSize, Image.mapchip[2]);

                    else Camera.DrawGraph(x * CellSize, y * CellSize, Image.mapchip[0]);
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
            bool DoubtHitBlock = false;

            int terrainID = GetTerrain(worldX, worldY); // 指定された座標の地形のIDを取得

            // 地形が壁ならtrue、違うならfalseを返却する        
            if (terrainID == Wall || terrainID == ProtrusionBlock || terrainID == NormalBlock)
            {
                DoubtHitBlock = true;
            }

            return DoubtHitBlock;
        }
    }
}
