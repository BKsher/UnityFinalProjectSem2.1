using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfo : MonoBehaviour
{
    public GameObject entity;
    public GameObject picture;
    public GameObject healthBar;
    public GameObject text_health, text_damage, text_cooldown, text_spdattrad;
    public List<Sprite> towerSprites, monsterSprites;
    private Text health, damage, cooldown, spdattrad;
    private Image image;
    private bool isTower;

    void Start()
    {
        health = text_health.GetComponent<Text>();
        damage = text_damage.GetComponent<Text>();
        cooldown = text_cooldown.GetComponent<Text>();  // считываем компоненты
        spdattrad = text_spdattrad.GetComponent<Text>();
        image = picture.GetComponent<Image>();
    }


    void Update()
    {
        if (entity == null)    // если объект не выбран, то ничего не отображаем
        {
            gameObject.SetActive(false);    
            return;
        }
        isTower = (entity.tag == "tower");
        if (isTower)
        {
            health.text = "Health: " + entity.GetComponent<TowerController>().maxHealth.ToString();
            if (entity.GetComponent<TowerController>().type == 1)
            {   // обычный тавер
                damage.text = "Damage: " + entity.GetComponent<TowerController>().damage.ToString();
                cooldown.text = "Cooldown: " + entity.GetComponent<TowerController>().cooldown.ToString();
                spdattrad.text = "Attack radius: " + entity.GetComponent<TowerController>().attackRadius.ToString();
            }
            else
            {   // главный тавер
                damage.text = "";
                cooldown.text = "";
                spdattrad.text = "";
            }
            image.sprite = towerSprites[entity.GetComponent<TowerController>().type];
        }
        else
        {   // монстр
            health.text = "Health: " + entity.GetComponent<MonsterController>().maxHealth.ToString();
            damage.text = "Damage: " + entity.GetComponent<MonsterController>().damage.ToString();
            cooldown.text = "Cooldown: " + ((int)(entity.GetComponent<MonsterController>().cooldownAttack * 10)*1f/10f).ToString();
            spdattrad.text = "Speed: " + entity.GetComponent<MonsterController>().speed.ToString();
            image.sprite = monsterSprites[entity.GetComponent<MonsterController>().type];
        }

        if (isTower)
        {   // текущее хп тавера
            healthBar.GetComponent<Image>().fillAmount = entity.GetComponent<TowerController>().health / entity.GetComponent<TowerController>().maxHealth;
            healthBar.GetComponentInChildren<Text>().text = entity.GetComponentInChildren<TowerController>().health.ToString() +
                " / " + entity.GetComponentInChildren<TowerController>().maxHealth.ToString();
        }
        else
        {   // текущее хп монстра
            healthBar.GetComponent<Image>().fillAmount = entity.GetComponent<MonsterController>().health / entity.GetComponent<MonsterController>().maxHealth;
            healthBar.GetComponentInChildren<Text>().text = entity.GetComponentInChildren<MonsterController>().health.ToString() +
                " / " + entity.GetComponentInChildren<MonsterController>().maxHealth.ToString();
        }
    }
}
