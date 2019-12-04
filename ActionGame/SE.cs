using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ActionGame
{
    public static class SE
    {
        //プレイヤーのリソースSE
        public static int ArmShotSE;　//プレイヤーの腕の効果音
        public static int AlarmSE;　//プレイヤーの警告SE
        public static int PlayerDamageSE;　//プレイヤーのダメージSE

        //ザコ敵のリソースSE
        public static int EnemySE;　//敵のSE

        //アイテムのリソースSE
        public static int WoolemSE;　//毛糸のSE

        //その他のリソースSE

        public static void Load()
        {
            //プレイヤーのリソースSE
            ArmShotSE = DX.LoadSoundMem("SE/ArmShot.mp3");
            AlarmSE = DX.LoadSoundMem("SE/Alarm.mp3");
            PlayerDamageSE = DX.LoadSoundMem("SE/ PlayerDamage.mp3");

            //ザコ敵のリソースSE
            EnemySE = DX.LoadSoundMem("SE/ EnemySound.mp3");

            //アイテムのリソースSE
            WoolemSE = DX.LoadSoundMem("SE/ Woolem.mp3");

            //その他のリソースSE

        }
        public static void Play(int handle)
        {
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_BACK);
        }
    }
}
