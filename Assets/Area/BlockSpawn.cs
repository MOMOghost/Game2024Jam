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
InvokeRepeating(nameof(SpawnBlock), 0,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnBlock()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(transform.position.x - (areaBound.size.x / 2), transform.position.x + (areaBound.size.x / 2)),
            Random.Range(transform.position.y - (areaBound.size.y / 2), transform.position.y + (areaBound.size.y / 2)));
        Transform newBlock=Instantiate(blockDefault.transform, spawnPoint,new Quaternion(), ownerArea.blockFolder.transform);
        BlockState state= newBlock.GetComponent<BlockState>();
        state.moveSpeed = ownerArea.rollSpeed;
        state.offset = spawnPoint;
        state.area=ownerArea.Area;
        ownerArea.ownerBlock[Mathf.Clamp(Random.Range(0, ownerArea.ownerBlock.Count),0, ownerArea.ownerBlock.Count)].InitBlock(state);
        state.emoji = (BlockEmoji)Mathf.Clamp(Random.Range(0, (int)BlockEmoji.Count), 0, (int)BlockEmoji.Count-1);
        newBlock.GetComponent<BlockState>().moveVector=ownerArea.rollVector;
        newBlock.GetComponent<BlockState>().startMove=true;
        ownerArea.nowBlocks.Add(newBlock.GetComponent<BlockState>());
    }
}
