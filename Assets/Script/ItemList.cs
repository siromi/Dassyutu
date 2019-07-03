using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    static ItemList itemList = new ItemList();

    string imageFolderName = "Image/"; //アイテムの画像が入っているフォルダ名
    string noItemImage = "no_item"; //何も入ってないときに表示する画像

  public  List<GameObject> itemButtonList=new List<GameObject>(); //アイテムリスト（ボタン）の番号


    //何故かnew出来ないので、このメソッドでスプリクトを返す
    public static ItemList GetInstance()
    {
        return itemList;
    }

    //リストにボタンを入れる、画像を初期化
    public void  InitItemList()
    {
        int i=0; //ボタンの番号
        while (GameObject.Find("Button ("+i.ToString() + ")")!=null) 
        {
            itemButtonList.Add(GameObject.Find("Button (" + i.ToString() + ")"));

            itemButtonList[i].GetComponent<Image>().sprite = Resources.Load(imageFolderName + noItemImage, typeof(Sprite)) as Sprite;
            i++;
        }
    }


}
