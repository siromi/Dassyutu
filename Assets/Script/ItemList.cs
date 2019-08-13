using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemList
{

    string imageFolderName = "Image/"; //アイテムの画像のフォルダ名
    string selectImageName = "Select";
    string selectOpenName = "Image";
    string itemOpenName = "Open";
    string noItemImage = "no_item"; //何も入ってないときに表示する画像
    string noSelect = "None"; //何も入ってないときに表示する画像

    List<GameObject> itemButtonObj = new List<GameObject>(); //アイテムリスト（ボタン）のオブジェクト
    List<GameObject> selectImg = new List<GameObject>(); //選択枠
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

    public List<int> ItemSelectNum { get { return itemSelectNum; } }

    public ItemList()
    {
        //リストにボタンを入れる、画像を初期化
        int i = 0; //ボタンの番号
        while (GameObject.Find("Button (" + i.ToString() + ")") != null)
        {
            itemSelectNum.Add(0);

            itemButtonObj.Add(GameObject.Find("Button (" + i.ToString() + ")"));
            itemButtonObj[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;

            selectImg.Add(GameObject.Find("Image (" + i.ToString() + ")"));
            selectImg[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
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
            // imageName = itemImageName;
            itemSelectNum[buttonNum] = (int)Stat.selecting;
            selectImg[buttonNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectImageName, typeof(Sprite)) as Sprite;
        }
        //選択状態だったら開く
        else if (itemSelectNum[buttonNum] == (int)Stat.selecting)
        {
            //他にopenがあれば直す
            int openNow = itemSelectNum.FindIndex(x => x == (int)Stat.open);
            if (openNow != -1)
            {
                itemSelectNum[openNow] = (int)Stat.inItem;
                selectImg[openNow].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
            }

            itemSelectNum[buttonNum] = (int)Stat.open;
            selectImg[buttonNum].GetComponent<Image>().sprite = Resources.Load(imageFolderName + itemOpenName, typeof(Sprite)) as Sprite;
            itemDetailCanvas.GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectOpenName + itemImageName, typeof(Sprite)) as Sprite;
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
                itemSelectNum[i] = (int)Stat.inItem;
                selectImg[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
            }
        }
    }

    //アイテム詳細を閉じる 
    public int test()
    {
        return itemSelectNum.FindIndex(x => x == (int)Stat.open);
    }
    public void Close(int openNow)
    {
        itemSelectNum[openNow] = (int)Stat.inItem;
        selectImg[openNow].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
        itemDetailCanvas.SetActive(false);
    }

    //選択されているアイテムを削除
    public void DeletItem()
    {
        for (int i = 0; i < itemSelectNum.Count; i++)
        {
            if (itemSelectNum[i] == (int)Stat.selecting)
            {
                selectImg[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;

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



    //アイテム合成
    public void ItemComp(int clickDev)
    {
        //OPEN中のアイテムを探し、その番号のアイテム名を習得
        int openNow = itemSelectNum.FindIndex(x => x == (int)Stat.open);
        string openObj = itemButtonObj[openNow].GetComponent<Image>().sprite.name;

        //select中のアイテムを探し、その番号のアイテム名を習得
        int selectNow = ItemSelectNum.FindIndex(x => x == (int)Stat.selecting);
        string selectObj="";
       if (selectNow != -1)
        { selectObj = itemButtonObj[selectNow].GetComponent<Image>().sprite.name; }

        switch (openObj)
        {
            case "Red":
                if((clickDev==1|| clickDev == 2|| clickDev == 3) && selectObj=="Blue")
                {
                    Debug.Log("むらさき");
                    //open中のものを合成後に。詳細画像を合成後に。selectを削除
                    itemButtonObj[openNow].GetComponent<Image>().sprite = Resources.Load(imageFolderName + "Purple", typeof(Sprite)) as Sprite;

                    itemDetailCanvas.GetComponent<Image>().sprite = Resources.Load(imageFolderName + selectOpenName + "Purple", typeof(Sprite)) as Sprite;

                    itemButtonObj[selectNow].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;
                    itemSelectNum[selectNow] = (int)Stat.noImage;
                    selectImg[selectNow].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noSelect, typeof(Sprite)) as Sprite;
                }
                break;
        }
    }

}