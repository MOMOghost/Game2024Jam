using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 梗圖觸發 : MonoBehaviour
{
    public List<Sprite> 梗圖清單;

    public GameObject 梗圖;//SpriteRenderer
   
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void 開啟梗圖(){
       // 梗圖.sprite.texture;
       梗圖.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
