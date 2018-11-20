using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{

    public float health = 100;
    public ParticleSystem DeathEffect;

    [SerializeField] private Text healthUI;
    public void UpdateHealth()
    {
        healthUI.text = health.ToString("0");
    }

    public void disable()
    {
        health = 0;
        Vector3 objectPosition = GetComponent<Transform>().position;
        Destroy(Instantiate(DeathEffect.gameObject, objectPosition, Quaternion.FromToRotation(Vector3.forward, objectPosition)) as GameObject, DeathEffect.main.startLifetime.constant);
        gameObject.SetActive(false);
        Debug.Log("cube hit");
    }


    public void enable()
    {
        health = 100;
        gameObject.SetActive(true);
    }

}
