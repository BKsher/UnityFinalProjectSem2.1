using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class FireBallController : MonoBehaviour
{
    public GameObject target;
    public float speed, offsetY, damage;
    private Rigidbody rb;
    private Vector3 movement, direction;

    void delete()
    {
        if (target == null || transform.position.y < 0) Destroy(gameObject);   // уничтожаем фаербол, если он вылетел за карту
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        delete();   // уничтажаем лишние фаерболы
        if (target == null) return;
        direction = (target.transform.position + Vector3.up * offsetY) - transform.position;
        direction.Normalize();
        movement = direction;   // находим направление
    }

    private void FixedUpdate()
    {
        Move(movement);   // перевщаем фаербол
    }

    void Move(Vector3 direction)  // перемещение фаербола по направлению
    {
        rb.MovePosition(transform.position + direction*speed*Time.deltaTime);
    }
}
