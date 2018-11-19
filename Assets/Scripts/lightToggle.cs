using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class lightToggle : MonoBehaviour
    {
        public SteamVR_Action_Boolean lightAction;

        public SteamVR_Action_Vibration vibrator;

        public Hand hand;

        public GameObject light;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (lightAction == null)
            {
                Debug.LogError("No plant action assigned");
                return;
            }

            lightAction.AddOnChangeListener(OnActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (lightAction != null)
                lightAction.RemoveOnChangeListener(OnActionChange, hand.handType);
        }


        private void OnActionChange(SteamVR_Action_In actionIn)
        {
            if (lightAction.GetStateDown(hand.handType))
            {
                light.SetActive(!light.activeSelf);
                vibrator.Execute(0.0f, 0.03f, 200f, 0.1f, SteamVR_Input_Sources.LeftHand);
            }
        }

    }
}