﻿using UnityEngine;
using System.Threading;

namespace Valve.VR.InteractionSystem.Sample
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;
        public float timeBetweenBullets = 0.15f;
        public float range = 100f;

        private Hand hand;


        float timer;
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        int shootableMask;
        ParticleSystem gunParticles;
        LineRenderer gunLine;
        AudioSource gunAudio;
        Light gunLight;
        float effectsDisplayTime = 0.2f;


        void Awake()
        {
            hand = GetComponent<Hand>();
            shootableMask = LayerMask.GetMask("Shootable");
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Shoot();
            }

            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                DisableEffects();
            }
        }


        public void DisableEffects()
        {
            gunLine.enabled = false;
            gunLight.enabled = false;
        }


        public void Shoot()
        {
            timer = 0f;

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position); 

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            Debug.Log("shoot() called");

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                /*
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
                if(enemyHealth != null)
                {
                    enemyHealth.TakeDamage (damagePerShot, shootHit.point);
                }*/
                shootHit.collider.GetComponent<healthManager>().disable();

                //Debug.Log("if is true");
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                Debug.Log("if is false");
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }

        }
    }
}
