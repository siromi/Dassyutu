using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    EventSystem eventSystem;

    //Camera
    Camera mainCamera;

    //Instanse
    ItemList itemList;

    //Ray
    Ray ray;
    RaycastHit hit;
    public LayerMask mask;

    GameObject ClickObject; //UIをクリックしたか

    // Use this for initialization
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        itemList = new ItemList();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            ClickObject = eventSystem.currentSelectedGameObject;

            if (ClickObject == null) //UIではない→ゲーム画面をクリックしたとき
            {
                //openがあるなら閉じる
                int openNow = itemList.test();
                if (openNow != -1)
                {
                    Debug.Log(openNow);
                    itemList.Close(openNow);
                }

                //objの検出
                else
                {
                    GetObject();
                }
            }
            
            else if (ClickObject.tag == "ItemListButton") 
            {
                int buttonNum = itemList.ButtonNum(ClickObject);
                string itemName = itemList.ItemName(ClickObject);

                itemList.ItemSelect(buttonNum, itemName);
                itemList.CloseSelect(buttonNum);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            itemList.DeletItem();
            Debug.Log("1");
        }
    }

    //アイテムをクリックしたら、アイテムリストに入れる
    void GetObject()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000000, mask))
        {
            itemList.InItem(hit.collider.gameObject.name);

        }
    }
}
