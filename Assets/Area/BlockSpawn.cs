using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    BoxCollider2D areaBound;
    AreaState ownerArea;
    [SerializeField] GameObject blockDefault;
    
    // Start is called before the first frame update
    void Start()
    {
        areaBound=GetComponent<BoxCollider2D>();
        ownerArea=transform.parent.GetComponent<AreaState>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnBlock()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(areaBound.offset.x - (areaBound.size.x / 2), areaBound.offset.x + (areaBound.size.x / 2)),
            Random.Range(areaBound.offset.y - (areaBound.size.y / 2), areaBound.offset.y + (areaBound.size.y / 2)));
        Transform newBlock=Instantiate(blockDefault.transform, spawnPoint,new Quaternion(), ownerArea.blockFolder.transform);
        newBlock.GetComponent<BlockState>().area=ownerArea.Area;

        newBlock.GetComponent<BlockState>().Move(ownerArea.rollVector);
        ownerArea.nowBlocks.Add(newBlock.GetComponent<BlockState>());
    }
}
