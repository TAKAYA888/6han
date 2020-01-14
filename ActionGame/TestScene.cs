using DxLibDLL;
using MyLib;

namespace ActionGame
{
    public class TestScene : Scene
    {
        public TestScene()
        {

        }

        public override void Update()
        {
            if (Input.GetButton(DX.PAD_INPUT_1))
            {
                Game.particleManager.Fountain(320, 240);
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_2))
            {
                Game.particleManager.ShockWave(520, 240, MyMath_KNMR.MathHelper.toRadians(90.0f));
            }

            if (Input.GetButtonDown(DX.PAD_INPUT_3))
            {
                Game.particleManager.Spark(720, 240, MyRandom.Range(0.0f,360.0f));
            }

            if (Input.GetButton(DX.PAD_INPUT_4))
            {
                Game.particleManager.Steam(920, 240);
            }

            if (Input.GetButton(DX.PAD_INPUT_5))
            {
                Game.particleManager.Fire(320, 240);
            }

        }

        public override void Draw()
        {

        }
    }
}
