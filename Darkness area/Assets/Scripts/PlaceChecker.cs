using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceChecker : MonoBehaviour
{
    public bool canBePlaced;
    public int type;

    private Color green, red;
    private Renderer rendererTower;

    void Start()
    {
        green.a = 0.5f; green.r = 0; green.g = 1f; green.b = 0;   // создаем зеленый цвет
        red.a = 0.5f; red.r = 1f; red.g = 0; red.b = 0;     // создаем красный цвет
        if(type == 1)
            rendererTower = gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>();   // добываем рендерер
        if(type == 2)
            rendererTower = gameObject.GetComponent<Renderer>();
        rendererTower.sharedMaterial.color = green;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "ground")
        {
            rendererTower.sharedMaterial.color = red;   //если с чем-то пересекаемся, то красим в красный и не можем установить тавер здесь
            canBePlaced = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rendererTower.sharedMaterial.color = green;    // красим в зеленый и можем установить тавер здесь
        canBePlaced = true;
    }
}
