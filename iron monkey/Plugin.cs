using BepInEx;
using Photon.Pun;
namespace Imonkey
{
    [BepInPlugin("Shadow.IronMonkey", "Iron Monkey", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        bool on = true;
        public void FixedUpdate()
        {
            if (NetworkSystem.Instance.GameModeString.Contains("MODDED") && PhotonNetwork.InRoom)
            {
                if (ControllerInputPoller.instance.leftControllerGripFloat > 0.5f)
                {
                    GorillaTagger.Instance.rigidbody.AddForce(10 * -GorillaTagger.Instance.leftHandTransform.right, (UnityEngine.ForceMode)5);
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 25f * GorillaTagger.Instance.rigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
                }
                if (ControllerInputPoller.instance.rightControllerGripFloat > 0.5f)
                {
                    GorillaTagger.Instance.rigidbody.AddForce(10 * GorillaTagger.Instance.rightHandTransform.right, (UnityEngine.ForceMode)5);
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 25f * GorillaTagger.Instance.rigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
                }
            }
        }
        void OnEnable()
        {
            on = true;
        }
        void OnDisable()
        {
            on = false;
        }
    }
}