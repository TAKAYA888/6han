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
        public static int EnemyDamageSE;//敵のダメージSE

        //アイテムのリソースSE
        public static int WoolemSE;　//毛糸のSE

        //その他のリソースSE
        public static int keySE;//鍵のSE

        public static void Load()
        {
            //プレイヤーのリソースSE
            ArmShotSE = DX.LoadSoundMem("SE/ArmShot.wav");
            AlarmSE = DX.LoadSoundMem("SE/Alarm.wav");
            PlayerDamageSE = DX.LoadSoundMem("SE/PlayerDamage.wav");

            //ザコ敵のリソースSE
            EnemySE = DX.LoadSoundMem("SE/EnemySound.wav");
            EnemyDamageSE = DX.LoadSoundMem("SE/Enemy_damage.wav");

            //アイテムのリソースSE
            WoolemSE = DX.LoadSoundMem("SE/Woolem.wav");

            //その他のリソースSE
            keySE = DX.LoadSoundMem("SE/Gimmick.wav");

        }
        public static void Play(int handle)
        {
            DX.PlaySoundMem(handle, DX.DX_PLAYTYPE_BACK);
        }
    }
}
