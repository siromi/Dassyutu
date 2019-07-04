using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList
{

    string imageFolderName = "Image/"; //アイテムの画像のフォルダ名
    string noItemImage = "no_item"; //何も入ってないときに表示する画像

    List<GameObject> itemButtonObj = new List<GameObject>(); //アイテムリスト（ボタン）のオブジェクト
    //public List<GameObject> ItemButtonObj => itemButtonObj;
    List<int> itemButtonNum = new List<int>(); //アイテムリスト（ボタン）が有効か　0=空　1=入っている 2=入っている＆選択中


    //リストにボタンを入れる、画像を初期化
    public ItemList()
    {
        int i = 0; //ボタンの番号
        while (GameObject.Find("Button (" + i.ToString() + ")") != null)
        {
            itemButtonObj.Add(GameObject.Find("Button (" + i.ToString() + ")"));
            itemButtonNum.Add(0);
            itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;
            i++;
        }
    }

    //空のアイテムリストに画像を入れる
    public void InItem()
    {
        //for(int i = 0; i < itemButtonNum.Count; i++)
        //{

        //}


    }


}