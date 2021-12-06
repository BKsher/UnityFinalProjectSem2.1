using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> monsters;
    public float planeSize;
    public float cooldown;
    private float time;

    public int phase;
    private int totalSpawned;

    void Start()
    {
        time = cooldown;    // первый монстр спавнится на первой секунде
        totalSpawned = 0;
        phase = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > cooldown)         // таймер с кулдауном
        {
            spawnMonster();
            totalSpawned++;
            phase = totalSpawned / 5;
            if (totalSpawned % 5 == 0 && cooldown > 2) cooldown--;
            time -= cooldown;
        }
    }

    int getType()
    {
        if (phase < 1) 
            return 0;
        if (phase == 1)
            return Random.Range(0, 3) < 2 ? 0 : 1;
        if (phase == 2)
            return Random.Range(0, 2) < 1 ? 0 : 1;
        if (phase >= 4)
            return Random.Range(0, 3) < 1 ? 0 : 1;
        return 1;
    }

    void spawnMonster()   // спавн монстра
    {
        int type = getType();
        Vector3 place = new Vector3();
        place.y = 0;
        int dir = Random.Range(0, 4);
        if(dir < 2)
        {
            place.x = Random.Range(-planeSize / 2, planeSize / 2);
            place.z = planeSize / 2 * (dir == 0 ? 1 : -1);
        }                                                           // рандомит место спавна на краю платформы
        else
        {
            place.z = Random.Range(-planeSize / 2, planeSize / 2);
            place.x = planeSize / 2 * (dir == 2 ? 1 : -1);
        }
        GetComponent<gameController>().monsters.Add(Instantiate(monsters[type], place, Quaternion.identity));   // спавн монстра
    }
}
