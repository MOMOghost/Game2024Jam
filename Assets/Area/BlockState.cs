using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    public AreaLocal area;
    public BlockEmoji emoji;
    public Vector2 offset,size;
    public Sprite blockImage;
    public float moveSpeed;
    Rigidbody2D blockRigidBody;
    BoxCollider2D blockCollider;
    // Start is called before the first frame update
    void Start()
    {
        blockRigidBody=GetComponent<Rigidbody2D>();
        blockCollider = transform.AddComponent<BoxCollider2D>();
        transform.Find("Texture").GetComponent<SpriteRenderer>().sprite= blockImage;
        transform.position= offset;
        blockCollider.size=new Vector3(size.x,size.y,0);
        
    }
    public void Move(Vector2 moveVector)
    {
        blockRigidBody.velocity= moveVector * moveSpeed;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag=="Area") {
            Destroy(this.gameObject,0.5f);
        }
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