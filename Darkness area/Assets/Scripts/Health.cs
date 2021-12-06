using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject healthBar;
    private Image bar;
    public float health;

    void Start()
    {
        bar = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (healthBar != null)   // поворот хелсбара на камеру
        {
            healthBar.transform.LookAt(Camera.main.transform);
            healthBar.transform.Rotate(0, 180, 0);
        }
        bar.fillAmount = health;   // полоса хп юнита
    }
}
