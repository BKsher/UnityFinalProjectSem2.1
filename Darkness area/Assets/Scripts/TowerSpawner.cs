using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{

    public List<GameObject> towerPreviews, towers;
    public bool previewMode;
    public LayerMask lm;
    public List<int> price;

    private GameObject towerpreview;
    private Rigidbody rb;
    private bool canBePlaced;
    private int curType;

    void Start()
    {
        
    }

    void spawnTower(Vector3 position, int type)
    {
        GetComponent<gameController>().towers.Add(Instantiate(towers[type], position, Quaternion.identity));   // спавним тавер
        GetComponent<Player>().money -= price[type];    // вычитаем цену за тавер
    }

    void enter_previewMode(Vector3 position, int type)
    {
        if (GetComponent<Player>().money < price[type]) return; // нехватает монет
        towerpreview = Instantiate(towerPreviews[type], position, Quaternion.identity);   // отображам превью тавера
        rb = towerpreview.GetComponent<Rigidbody>();
        previewMode = true;
        curType = type;
    }

    void exit_previewMode() // выходим из превью режима
    {
        Destroy(towerpreview);
        previewMode = false;

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;                                         // пускаем луч до пересечения с землей
        Physics.Raycast(ray, out hit, Mathf.Infinity, lm);          
        if (towerpreview != null) canBePlaced = towerpreview.GetComponent<PlaceChecker>().canBePlaced;    // проверяем может ли здесь быть установле тавер
        if (Input.GetKeyDown(KeyCode.Alpha1)) // переходим в превью режим
        {
            exit_previewMode();
            enter_previewMode(hit.point, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            exit_previewMode();
            enter_previewMode(hit.point, 2);
        }

        if (previewMode)
            rb.MovePosition(hit.point);     // премещам превью тавера за курсором

        if (Input.GetMouseButtonDown(0) && previewMode && canBePlaced)   // установка тавера
        {
            spawnTower(hit.point, curType);
            exit_previewMode();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && previewMode)
            exit_previewMode();
    }

}
