using BepInEx;
using Photon.Pun;
using UnityEngine;
namespace Iron_Monkey
{
    [BepInPlugin("Shadow.IronMonkey", "Iron Monkey", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        bool on = true;
        bool T = false;
        public float cooldownTime = 0.3f;
        private float nextAllowedTime = 0f;
        public void FixedUpdate()
        {
            if (NetworkSystem.Instance.GameModeString.Contains("MODDED") && PhotonNetwork.InRoom && on)
            {
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && Time.time >= nextAllowedTime)
                {
                    T = !T;
                    nextAllowedTime = Time.time + cooldownTime;
                }
                if (ControllerInputPoller.instance.leftControllerGripFloat > 0.5f && T)
                {
                    GorillaTagger.Instance.rigidbody.AddForce(10 * -GorillaTagger.Instance.leftHandTransform.right, (ForceMode)5);
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 25f * GorillaTagger.Instance.rigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
                }
                if (ControllerInputPoller.instance.rightControllerGripFloat > 0.5f && T)
                {
                    GorillaTagger.Instance.rigidbody.AddForce(10 * GorillaTagger.Instance.rightHandTransform.right, (ForceMode)5);
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
            T = false;
        }
    }
}