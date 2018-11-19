using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{

    public float health = 100;

    [SerializeField] private Text healthUI;
    public void UpdateHealth()
    {
        healthUI.text = health.ToString("0");
    }

    public void disable()
    {
        health = 0;
        gameObject.SetActive(false);
        Debug.Log("cube hit");
    }
}
