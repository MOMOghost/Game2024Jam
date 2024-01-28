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

    // Update is called once per frame
    void Update()
    {
        
    }
}
