using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SupSystem;

public class 梗圖觸發 : MonoBehaviour
{
    public List<Sprite> 梗圖清單;

    public GameObject 梗圖;//SpriteRenderer

        [SerializeField]  SoundController soundController;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void 開啟梗圖(){
       // 梗圖.sprite.texture;

       設置隨機梗圖();

       梗圖.SetActive(true);
    }


    void 設置隨機梗圖(){

        System.Random random = new System.Random();
        int randomInt = random.Next(0, 16);

        梗圖.GetComponent<SpriteRenderer>().sprite = 梗圖清單[randomInt];
        //soundController.PlayAudio(soundController.BGM[randomInt], SoundController.AudioType.BGM, true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
