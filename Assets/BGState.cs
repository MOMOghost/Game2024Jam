using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGState : MonoBehaviour
{
    public float maxLivetime;
    private BG_Pool parentPool;
    public Sprite BGSprite;
    public float rollSpeed,rollVector;
    public bool isMove;

    private bool onScreenOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        parentPool=transform.parent.GetComponent<BG_Pool>();
        gameObject.GetComponent<SpriteRenderer>().sprite = BGSprite;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        onScreenOnce = onScreen;
    }
    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen && onScreenOnce == false) { onScreenOnce = true; }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isMove) { Move(); }
    }
    void Move()
    {
        transform.Translate(0,rollSpeed*rollVector,0);
    }
    void FixUpdate()
    {
        
    }
}
