using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemList
{

    string imageFolderName = "Image/"; //アイテムの画像のフォルダ名
    string selectImageName = "Selected";
    string selectOpenName = "Image";
    string noItemImage = "no_item"; //何も入ってないときに表示する画像
    string noSelect = "None"; //何も入ってないときに表示する画像
    string imageName; //選択したオブジェクトの名前を保管する //画像別にすればこれ要らないんじゃあああああああ

    List<GameObject> itemButtonObj = new List<GameObject>(); //アイテムリスト（ボタン）のオブジェクト
    List<int> itemSelectNum = new List<int>(); //アイテムリスト（ボタン）が有効か　

    GameObject itemDetailCanvas; //画像の詳細表示をするobj

    //画像の状態管理
    enum Stat
    {
        noImage,
        inItem,
        selecting,
        open
    }


    public ItemList()
    {
    //リストにボタンを入れる、画像を初期化
        int i = 0; //ボタンの番号
        while (GameObject.Find("Button (" + i.ToString() + ")") != null)
        {
            itemButtonObj.Add(GameObject.Find("Button (" + i.ToString() + ")"));
            itemSelectNum.Add(0);
            itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;
            i++;
        }
        //詳細表示をoff
        itemDetailCanvas = GameObject.Find("ItemDetailCanvas");
        itemDetailCanvas.GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
        itemDetailCanvas.SetActive(false);
    }

    //アイテムリストの空いてる場所にアイテムを入れる
    public void InItem(string SetImageName)
    {
        int noImageNum = itemSelectNum.FindIndex(x => x == (int)Stat.noImage);
        if (noImageNum != -1) //-1 = 検索結果ゼロ
        {
            itemSelectNum[noImageNum] = (int)Stat.inItem;
            itemButtonObj[noImageNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName + SetImageName, typeof(Sprite)) as Sprite;
        }
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
        if (itemSelectNum[buttonNum] == (int)Stat.inItem)
        {
            imageName = itemImageName;
            itemSelectNum[buttonNum] = (int)Stat.selecting;
            itemButtonObj[buttonNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectImageName + itemImageName, typeof(Sprite)) as Sprite;
        }
        //選択状態だったら開く
      else  if (itemSelectNum[buttonNum] == (int)Stat.selecting)
        {
            //他にopenがあれば直す
            int openNow = itemSelectNum.FindIndex(x => x == (int)Stat.open);
            if (openNow != -1) 
            {
                 itemSelectNum[openNow] = (int)Stat.inItem;
            }

            itemSelectNum[buttonNum] = (int)Stat.open;
            itemButtonObj[buttonNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName +  imageName, typeof(Sprite)) as Sprite; //テスト
            itemDetailCanvas.GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectOpenName +imageName, typeof(Sprite)) as Sprite;           
            itemDetailCanvas.SetActive(true);
        }
    }
    //現在選択中のもの以外は、選択状態を解除
    public void CloseSelect(int buttonNum)
    {
        for (int i = 0; i < itemSelectNum.Count; i++)
        {
            if (itemSelectNum[i] == (int)Stat.selecting && i != buttonNum)
            {
                string itemName = itemButtonObj[i].GetComponent<Image>().sprite.name.Substring(selectImageName.Length);

                itemSelectNum[i] = (int)Stat.inItem;
                itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + itemName, typeof(Sprite)) as Sprite;
            }
        }
    }

    public void Open()
    {

    }

    //選択されているアイテムを削除
    public void DeletItem()
    {
        for (int i = 0; i < itemSelectNum.Count; i++)
        {
            if (itemSelectNum[i] == (int)Stat.selecting)
            {
                for (int d = i; d < itemSelectNum.Count - 1; d++)　//選択されていた番号から後ろ //最後のアイテムはno_itemになるので何もしない
                {
                    //リストの後ろのアイテムに
                    itemSelectNum[d] = itemSelectNum[d + 1];
                    itemButtonObj[d].GetComponent<Image>().sprite = itemButtonObj[d + 1].GetComponent<Image>().sprite;
                    // Debug.Log(++d); //なぜ二つずつ増える？

                }
            }
        }
    }



}