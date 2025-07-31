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
        float speedL = 10f;
        float speedR = 10f;
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
                    GorillaTagger.Instance.rigidbody.AddForce(speedL * -GorillaTagger.Instance.leftHandTransform.right, (ForceMode)5);
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 15f * GorillaTagger.Instance.rigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                    {
                        speedL = 25f;
                    }
                    else
                    {
                        speedL = 10f;
                    }
                }
                if (ControllerInputPoller.instance.rightControllerGripFloat > 0.5f && T)
                {
                    GorillaTagger.Instance.rigidbody.AddForce(speedR * GorillaTagger.Instance.rightHandTransform.right, (ForceMode)5);
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 15f * GorillaTagger.Instance.rigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
                    if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f)
                    {
                        speedR = 25f;
                    }
                    else
                    {
                        speedR = 10f;
                    }
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