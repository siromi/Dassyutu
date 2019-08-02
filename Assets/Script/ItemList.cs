using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemList
{

    string imageFolderName = "Image/"; //アイテムの画像のフォルダ名
    string selectImageName = "Selected";
    string noItemImage = "no_item"; //何も入ってないときに表示する画像

    List<GameObject> itemButtonObj = new List<GameObject>(); //アイテムリスト（ボタン）のオブジェクト
    List<int> itemSelectNum = new List<int>(); //アイテムリスト（ボタン）が有効か　
    //0=空　1=入っている 2=入っている＆選択中 enum
    const int NO_IMAGE = 0;
    const int IN_ITEM = 1;
    const int SELECTING = 2;

    //リストにボタンを入れる、画像を初期化
    public ItemList()
    {
        int i = 0; //ボタンの番号
        while (GameObject.Find("Button (" + i.ToString() + ")") != null)
        {
            itemButtonObj.Add(GameObject.Find("Button (" + i.ToString() + ")"));
            itemSelectNum.Add(0);
            itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;
            i++;
        }
    }

    //アイテムリストの空いてる場所にアイテムを入れる
    public void InItem(string SetImageName)
    {

        int index = itemSelectNum.FindIndex(x => x == NO_IMAGE);
        itemSelectNum[index] = IN_ITEM;
        itemButtonObj[index].GetComponent<Image>().sprite = Resources.Load(imageFolderName + SetImageName, typeof(Sprite)) as Sprite;

    }

    //Button (0) ←ボタンの番号を取得
    public int ButtonNum(GameObject clickObject)
    {
        return int.Parse(clickObject.name.Substring("Button (".Length, clickObject.name.Length - "Button ()".Length)); //ログ確認
    }

    //入っているアイテム名を取得
    public string ItemName(GameObject clickObject)
    {
        return clickObject.GetComponent<Image>().sprite.name;
    }



    public void ItemSelect(int buttonNum, string itemImageName)
    {
        //アイテムを選択状態にする
        if (itemSelectNum[buttonNum] == IN_ITEM)
        {

            itemSelectNum[buttonNum] = SELECTING;
            itemButtonObj[buttonNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectImageName + itemImageName, typeof(Sprite)) as Sprite;
        }
    }

    //現在選択中のもの以外は、選択状態を解除
    public void CloseSelect(int buttonNum)
    {
        for (int i = 0; i < itemSelectNum.Count; i++)
        {
            if (itemSelectNum[i] == SELECTING && i != buttonNum)
            {
                string itemName = itemButtonObj[i].GetComponent<Image>().sprite.name.Substring(selectImageName.Length);

                itemSelectNum[i] = IN_ITEM;
                itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + itemName, typeof(Sprite)) as Sprite;
            }
        }
    }


    //選択されているアイテムを削除
    public void DeletItem()
    {
        for (int i = 0; i < itemSelectNum.Count; i++) 
        {
            if (itemSelectNum[i] == SELECTING)
            {
                //string itemName = itemButtonObj[i].GetComponent<Image>().sprite.name.Substring(selectImageName.Length);

                //itemSelectNum[i] = NO_IMAGE;
                //itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;

            for(int d = i; d < itemSelectNum.Count-1; d++)　//選択されていた番号から後ろ //最後のアイテムはno_itemになるので何もしない
                {
                    //リストの後ろのアイテムに
                    itemSelectNum[d] = itemSelectNum[d+1];
                    itemButtonObj[d].GetComponent<Image>().sprite = itemButtonObj[d+1].GetComponent<Image>().sprite;
                   // Debug.Log(++d); //なぜ二つずつ増える？

                }
            }
        }
    }



}