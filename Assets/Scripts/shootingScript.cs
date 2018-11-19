using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class shootingScript : MonoBehaviour
    {
        public SteamVR_Action_Boolean shootAction;

        public SteamVR_Action_Vibration vibrator;

        public Hand hand;

        public GameObject GunTip;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (shootAction == null)
            {
                Debug.LogError("No plant action assigned");
                return;
            }

            shootAction.AddOnChangeListener(OnActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (shootAction != null)
                shootAction.RemoveOnChangeListener(OnActionChange, hand.handType);
        }


        private void OnActionChange(SteamVR_Action_In actionIn)
        {
            if (shootAction.GetStateDown(hand.handType))
            {
                Debug.Log("calling shoot()...");
                GunTip.GetComponent<PlayerShooting>().Shoot();
                vibrator.Execute(0.0f, 0.1f, 100f, 0.99f, SteamVR_Input_Sources.RightHand);
            }
        }

    }
}