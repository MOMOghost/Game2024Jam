using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BGState : MonoBehaviour
{
    public float maxLivetime;
    private BG_Pool parentPool;
    public BGTemplate template;
    public float rollSpeed,rollVector;
    public bool isMove;
    private float imageWidth,imageHeight;
    private SpriteRenderer spriterenderer;
    private float currentLiveTime;
    private bool onScreenOnce = false;
    // Start is called before the first frame update
   private bool CheckItemOnScreen()
    {   
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        float leftLimit = 0 - imageWidth/2;
        float rightLimit = 1+ imageWidth / 2;
        float downLimit=0-imageHeight/2;
        float upLimit=1+ imageHeight/2;
        
        bool onScreen =  screenPoint.x > leftLimit && screenPoint.x < rightLimit && screenPoint.y > downLimit && screenPoint.y < upLimit;
        return onScreen;
    }
    void Start()
    {
        currentLiveTime = maxLivetime;
        parentPool=transform.parent.GetComponent<BG_Pool>();
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        spriterenderer= GetComponent<SpriteRenderer>();
        //更新圖片尺寸
        UpdateImageSize();
        onScreenOnce =CheckItemOnScreen();
        bool onScreen =  screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        onScreenOnce = onScreen;
    }
    private void UpdateImageSize() {
        Vector3 min = spriterenderer.bounds.min;
        Vector3 max = spriterenderer.bounds.max;
        Vector3 screenMin = Camera.main.WorldToViewportPoint(min);
        Vector3 screenMax = Camera.main.WorldToViewportPoint(max);
        imageWidth = Mathf.Abs(screenMax.x - screenMin.x);
        imageHeight = Mathf.Abs(screenMax.y - screenMin.y);
    }
    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = CheckItemOnScreen();
        if (onScreen && onScreenOnce == false) { onScreenOnce = true; }
        if(onScreen==false && onScreenOnce == true) { isMove = false;}
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isMove) { Move(); }
        else { 

            parentPool.Pool.Get();

            parentPool.Pool.Release(this);
        }
    }
    
    private void OnEnable()
    {
        isMove = true;
        onScreenOnce = false;
        onScreenOnce = CheckItemOnScreen();

    }
    void Move()
    {
        currentLiveTime -= Time.deltaTime;
        transform.Translate(0,rollSpeed*rollVector*Time.deltaTime,0);
    }
}
