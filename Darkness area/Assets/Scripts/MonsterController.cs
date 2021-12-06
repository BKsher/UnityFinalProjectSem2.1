using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animationMonster;
    public List<float> attackRadiuses;
    public float cooldownAttack, offsetTimeAttack, damage;
    public Transform target;
    private int typeTower;

    private float offsetAttack = 6.7f;
    private float timeAttack, distance;

    public int type;

    public GameObject healthbar;
    public float health;
    public float maxHealth;
    public float speed;

    void attack ()  // удары по таверу
    {
        timeAttack += Time.deltaTime;
        if (timeAttack >= cooldownAttack)
        {
            target.gameObject.GetComponent<TowerController>().health -= damage;
            timeAttack = 0;
        }
    }


    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animationMonster = GetComponent<Animator>();
        timeAttack = 0;
        maxHealth = health;
    }

    void Update()
    {
        healthbar.GetComponent<Health>().health = health / maxHealth;
        if (target == null)   // стоим афк, если нету тавера
        {
            animationMonster.Play("Idle");
            agent.speed = 0;
        }
        else
        {
            typeTower = target.gameObject.GetComponent<TowerController>().type;
            float attackRadius = attackRadiuses[typeTower];
            distance = Vector3.Distance(target.position, transform.position);   // растояние до таргета
            if (agent.enabled) agent.SetDestination(target.position);   // идем к таверу
            if (distance < attackRadius + offsetAttack)   // атакуем тавер, если в радиусе атаки
            {
                agent.speed = 0;
                animationMonster.Play("Attack(1)");
                attack();
            }
            else
            {   // идем к таверу
                timeAttack = offsetTimeAttack;
                agent.speed = speed;
                animationMonster.Play("Run");
            }
        }

    }

    private void OnTriggerEnter(Collider other)   // попададание фаерболом
    {
        if (other.gameObject.tag == "fireball")
        {
            health -= other.gameObject.GetComponent<FireBallController>().damage;   // отнимаем соответствующие хп
            Destroy(other.gameObject);
        }
    }
}
