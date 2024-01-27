using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName ="NewBlock",menuName ="Block/new Block")]
public class BlockTemple:ScriptableObject
{
    public Sprite blockImage;
    public void InitBlock(BlockState block)
    {
        block.blockImage = blockImage;
        
    }
}
