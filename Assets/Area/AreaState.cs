using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaState : MonoBehaviour
{

    public AreaLocal Area;
    public List<BlockTemple> ownerBlock;
    public List<BGTemplate> ownerBG;
    public EmojiControll emojis;
    public List<BlockState> nowBlocks;
    public GameObject blockFolder;
    public float rollSpeed= 1;
    public float spawnRollTime= 2;
    public Vector2 rollVector=Vector2.up;
    // Start is called before the first frame update
    void Start()
    {
        if (ownerBlock.Count == 0)
        {
            Debug.LogError("Can't run when no block temple!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Block")
        {
            nowBlocks.Remove(collision.GetComponent<BlockState>());
            Destroy(collision.gameObject);
        }

    }
}
