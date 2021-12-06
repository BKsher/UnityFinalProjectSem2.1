using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CharacterController controller;
    public float speed, rotationSpeedHorizontal, rotationSpeedVertical;

    private float smooth = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();  // компонент для премещения камеры
    }

    
    void Update()
    {
        float pos = 0, rot = 0, up = 0, uprot = 0;
        if (Input.GetKey(KeyCode.W)) pos = 1;
        if (Input.GetKey(KeyCode.S)) pos = -1;
        if (Input.GetKey(KeyCode.D)) rot = 1;
        if (Input.GetKey(KeyCode.A)) rot = -1;
        if (Input.GetKey(KeyCode.Space)) up = 1;
        if (Input.GetKey(KeyCode.LeftShift)) up = -1;
        if (Input.GetKey(KeyCode.E)) uprot = 1;
        if (Input.GetKey(KeyCode.Q)) uprot = -1;    // управление камерой с помощью кнопок
        Vector3 move = pos * transform.forward;
        move.y = up;
        move.Normalize();
        controller.Move(move * Time.deltaTime * speed);   // перемещения на вектор
        Vector3 rotation = new Vector3(uprot * rotationSpeedVertical * Time.deltaTime, rot * rotationSpeedHorizontal * Time.deltaTime, 0);  
        transform.eulerAngles = transform.eulerAngles + rotation;
    }
}
