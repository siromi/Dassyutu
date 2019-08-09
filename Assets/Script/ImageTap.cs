using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全体画面サイズ　16:9                                 1600*900
//ゲーム画面      (16*0.85):9           = 13.6:9       1360*900
//詳細画面        (16*0.85*0.8):(9*0.8) = 10.88:7.2    1088*720


public class ImageTap
{

    Vector2 gameScreenSize; //ゲーム画面のサイズ
    Vector2 detailScreenSize; //詳細画面のサイズ

    int divX; //分割する数
    int divY;

    public ImageTap()
    {
        gameScreenSize = new Vector2(Screen.width * 0.85f, Screen.height);
        detailScreenSize = gameScreenSize * 0.8f;

        divX = 3;
        divY = 2;
    }


    public void ClickDetail()
    {

        Vector2 panelSize = new Vector2(detailScreenSize.x / divX, detailScreenSize.y / divY); //1分割のサイズ
        Vector2 clickDetailPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - gameScreenSize * 0.1f; //クリックした座標から空白分だけずらす
        Debug.Log((int)(clickDetailPos.y / panelSize.y )+ ":" + (int)(clickDetailPos.x / panelSize.x));
    }



}
