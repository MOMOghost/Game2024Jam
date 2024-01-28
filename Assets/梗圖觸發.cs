using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SupSystem;
using System.Diagnostics;

public class 梗圖觸發 : MonoBehaviour
{
    public List<Sprite> 梗圖清單;

    public GameObject 梗圖;//SpriteRenderer

    public GameObject 無量空處;//SpriteRenderer
        public GameObject 無量空處1;//SpriteRenderer
            public GameObject 無量空處2;//SpriteRenderer
                public GameObject 無量空處3;//SpriteRenderer
                  public GameObject 無量空處4;//SpriteRenderer

        [SerializeField]  SoundController soundController;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void 開啟梗圖(){
       // 梗圖.sprite.texture;

    //    設置隨機梗圖();

    //    梗圖.SetActive(true);



       領域展開();
    }

    


    void 設置隨機梗圖(){

        System.Random random = new System.Random();
        int randomInt = random.Next(0, 16);

        梗圖.GetComponent<SpriteRenderer>().sprite = 梗圖清單[randomInt];


        switch(randomInt) {

            case 2:
            case 15:
                 soundController.PlayAudio(soundController.SE[0], SoundController.AudioType.SE, false);
                break;

            case 4:
             case 5:
                 case 7:
             case 10:
                case 13:
                 case 14:
             case 16:
                 soundController.PlayAudio(soundController.SE[2], SoundController.AudioType.SE, false);
                break;

            case 3:
                 soundController.PlayAudio(soundController.SE[1], SoundController.AudioType.SE, false);
                break;
                  case 6:
              soundController.PlayAudio(soundController.SE[3], SoundController.AudioType.SE, false);
                break;
                        case 8:
                 soundController.PlayAudio(soundController.SE[4], SoundController.AudioType.SE, false);
                break;

                              case 9:
                 soundController.PlayAudio(soundController.SE[5], SoundController.AudioType.SE, false);
                break;
                                   case 11:
                 soundController.PlayAudio(soundController.SE[6], SoundController.AudioType.SE, false);
                break;
                                   case 12:
                 soundController.PlayAudio(soundController.SE[7], SoundController.AudioType.SE, false);
                break;
                                   case 17:
                soundController.PlayAudio(soundController.SE[8], SoundController.AudioType.SE, false);
                break;
                                   case 18:
                soundController.PlayAudio(soundController.SE[9], SoundController.AudioType.SE, false);
                break;

            default:
                soundController.PlayAudio(soundController.SE[9], SoundController.AudioType.SE, false);
                break;
        }
        //soundController.PlayAudio(soundController.BGM[randomInt], SoundController.AudioType.BGM, true);

    }


    IEnumerator ActivateChildrenWithDelayCoroutine()
    {
        // 遍历父物体底下的所有子物体
        for (int i = 0; i < 無量空處.transform.childCount; i++)
        {
            // 获取第i个子物体
            GameObject child = 無量空處.transform.GetChild(i).gameObject;


            System.Random random = new System.Random();
            int randomInt = random.Next(0, 16);
            child.GetComponent<SpriteRenderer>().sprite = 梗圖清單[randomInt];

            // 激活子物体
            child.SetActive(true);

            // 等待0.2秒
            yield return new WaitForSeconds(0.2f);
        }
    }

    void 領域展開(){
        StartCoroutine(ActivateChildrenWithDelayCoroutine());
        soundController.StopPlay(soundController.BGM[0].name.ToString());
        soundController.PlayAudio(soundController.BGM[1], SoundController.AudioType.BGM, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
