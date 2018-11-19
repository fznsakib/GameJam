//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Planting : MonoBehaviour
    {
        public SteamVR_Action_Boolean shootAction;

        public SteamVR_Action_Boolean lightAction;

        public SteamVR_Action_Vibration vibrator;

        public Hand hand;

        public GameObject GunTip;

        public GameObject light;

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
                GunTip.GetComponent<PlayerShooting>().Shoot();
                vibrator.Execute(0.0f, 0.1f, 100f, 0.99f, SteamVR_Input_Sources.RightHand);
            }
            if (lightAction.GetStateDown(hand.handType))
            {
                //turn off the flashlight
                if (light.activeSelf == true) light.SetActive(false);
                vibrator.Execute(0.0f, 0.05f, 100f, 0.5f, SteamVR_Input_Sources.LeftHand);
            }
        }

    }
}