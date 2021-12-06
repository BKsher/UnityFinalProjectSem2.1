using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public GameObject fireballPrephab;
    public GameObject target;
    public GameObject helthbar;
    public GameObject circle;

    public int type;
    public float cooldown, attackRadius, offsetY, damage;
    private float timeAttack;

    public float health;
    public float maxHealth;
    public float timeAlive;

    void attack()   // атака монстра фаерболом с кулдауном
    {
        if (type == 0) return;
        if (timeAttack >= cooldown)
        {
            spawnFireball();
            timeAttack = 0;
        }
        timeAttack += Time.deltaTime;
    }

    void spawnFireball()  // спавн фаербола
    {
        GameObject fireball = Instantiate(fireballPrephab, transform.position + Vector3.up*offsetY, Quaternion.identity);
        fireball.GetComponent<FireBallController>().target = target;
        fireball.GetComponent<FireBallController>().damage = damage;
    }

    void Start()
    {
        timeAttack = cooldown;
        maxHealth = health;
    }

    void Update()
    {
        helthbar.GetComponent<Health>().health = health / maxHealth;
        if (target != null && Vector3.Distance(target.transform.position, transform.position) > attackRadius) target = null;   // ищем таргет
        if (target != null) attack();   // атака таргета
        else timeAttack = cooldown;
        timeAlive += Time.deltaTime;
    }
}
