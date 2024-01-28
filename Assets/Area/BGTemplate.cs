using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBG", menuName = "BG/New BG", order = 1)]
public class BGTemplate : ScriptableObject
{
    public float rollSpeed;
    public BGState BGState;
    public Sprite BGImage;
    public void InitBG(SpriteRenderer renderer)
    {
       renderer.sprite = BGImage;
    }

}
