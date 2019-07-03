using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    ItemList itemList; //スプリクトを入れる


	// Use this for initialization
	void Start () {
        itemList = ItemList.GetInstance();
        itemList.InitItemList();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
