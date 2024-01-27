using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.SceneManagement; // 進行打包時需要關掉
using UnityEngine;

public class BlockState : MonoBehaviour
{
    public AreaLocal area;
    public BlockEmoji emoji;
    public Vector2 offset, size,moveVector;
    public Sprite blockImage;
    public float moveSpeed=1;
    public bool startMove=false;
    Rigidbody2D blockRigidBody;
    BoxCollider2D blockCollider;
    // Start is called before the first frame update
    void Start()
    {
        blockRigidBody= GetComponent<Rigidbody2D>();
        blockCollider = transform.AddComponent<BoxCollider2D>();
        transform.Find("Texture").GetComponent<SpriteRenderer>().sprite= blockImage;
        transform.position= offset;
        blockCollider.size=new Vector3(size.x,size.y,0);
        
    }
    private void Update()
    {
        if(startMove)
        {
            Move();
        }
    }
    public void Move()
    {
        blockRigidBody = GetComponent<Rigidbody2D>();
        Debug.Log(blockRigidBody);
        blockRigidBody.position+= moveVector * moveSpeed*0.01f;
    }
    
    
}
public enum AreaLocal
{
    Area1,Area2,Area3
}
public enum BlockEmoji
{
    Happy,
    Angry
}
public enum BlockType
{
    Nomal

}