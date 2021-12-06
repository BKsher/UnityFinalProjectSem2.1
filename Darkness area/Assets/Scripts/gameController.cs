using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{


    public  List<GameObject> towers = new List<GameObject>();
    public List<GameObject> monsters = new List<GameObject>();
    public GameObject infoPanel, entityInfo;
    public int reward;

    private GameObject lastActiveEntity;

    void aliveTowers()  // уничтожает тавер с хп меньше 0
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i].GetComponent<TowerController>().health <= 0)
            {
                Destroy(towers[i]);
                towers.RemoveAt(i);

            }
        }

    }

    void aliveMonsters()  // уничтожает монстров с хп меньше 0
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].GetComponent<MonsterController>().health <= 0)
            {
                Destroy(monsters[i]);
                monsters.RemoveAt(i);
                GetComponent<Player>().money += reward;
            }
        }

    }

    void findTargetMonster()  // каждому монстру находим таргет(тавер, который он будет атаковать)
    {
        if (towers.Count == 0) return;
        for (int i = 0; i < monsters.Count; i++)
        {
            GameObject target = towers[0];
            for(int j = 1; j < towers.Count; j++)
            {
                if (Vector3.Distance(monsters[i].transform.position, towers[j].transform.position) <
                   Vector3.Distance(monsters[i].transform.position, target.transform.position))   // находим ближайший тавер к монстру
                    target = towers[j];
            }
            monsters[i].GetComponent<MonsterController>().target = target.transform;
        }
    }

    void findTargetTower()  //каждому таверу находим таргет(монстр, которого он будет атаковать)
    {
        if (monsters.Count == 0) return;
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i].GetComponent<TowerController>().target != null) continue;   // тавер не может поменять таргет
            GameObject target = monsters[0];
            for (int j = 1; j < monsters.Count; j++)
            {
                if (Vector3.Distance(towers[i].transform.position, monsters[j].transform.position) <
                   Vector3.Distance(towers[i].transform.position, target.transform.position))  // находим ближайшего монстра к таверу
                    target = monsters[j];
            }
            towers[i].GetComponent<TowerController>().target = target;
        }
    }

    void updateInfoPlane()   // обновление панели с информацией
    {
        infoPanel.GetComponentInChildren<Text>().text = "coins: " + GetComponent<Player>().money.ToString();   // обновление информации о монетах
    }

    bool clicked()  // проверка на клик
    {
        return Input.GetMouseButtonDown(0) && !GetComponent<TowerSpawner>().previewMode;   // проверка нажатия ЛКМ и нахождения в нормальном моде
    }

    bool isTowerShooting(GameObject tower)  // проверка на башню, которая стреляет
    {
        if (tower == null || tower.tag != "tower") return false; 
        int type = tower.GetComponent<TowerController>().type;
        return type == 1;
    }

    bool isTower(GameObject tower)  // проверка на башню
    {
        return (tower != null && tower.tag == "tower");
    }

    void entityInformation()   // оттображает информацию про конретный юнит
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;                                           // пускаем луч из камеры
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        if (isTowerShooting(lastActiveEntity)) lastActiveEntity.GetComponent<TowerController>().circle.SetActive(false); //убираем окружность у последней выбраной башни
        if (hit.transform.gameObject.tag == "tower" || hit.transform.gameObject.tag == "monster")
        {
            if (isTower(hit.transform.gameObject)) if (hit.transform.gameObject.GetComponent<TowerController>().timeAlive <= 0.1) return; // для случая активациции панели сразу при спавне
            entityInfo.GetComponent<EntityInfo>().entity = hit.transform.gameObject;    // устанавливаем на панель соответсвующий юнит
            if(isTowerShooting(hit.transform.gameObject)) hit.transform.gameObject.GetComponent<TowerController>().circle.SetActive(true);   // отображаем окружность вокруг тавера
            entityInfo.SetActive(true);   // активируем панель
        }
        else
        {
            entityInfo.SetActive(false);    // прячем панель
        }
        lastActiveEntity = hit.transform.gameObject;    // последний активный объект
    }

    void Start()
    {
        entityInfo.SetActive(false);   // изначально не отображаем панель юнита
    }

    
    void Update()
    {
        aliveTowers();
        aliveMonsters();
        findTargetMonster();
        findTargetTower();
        updateInfoPlane();
        if (clicked()) entityInformation();
    }
}
