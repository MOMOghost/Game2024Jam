using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaState : MonoBehaviour
{
    public AreaLocal Area;
    [SerializeField] List<BlockTemple> ownerBlock;
    public List<BlockState> nowBlocks;
    public GameObject blockFolder;
    public float rollSpeed= 1;
    public Vector2 rollVector=Vector2.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Block")
        {
            nowBlocks.Remove(collision.GetComponent<BlockState>());
        }
        Destroy(collision.gameObject, 0.5f);
        
    }
}