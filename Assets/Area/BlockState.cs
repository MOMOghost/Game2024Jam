using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    public AreaLocal area;
    public Vector2 offset,size;
    public Sprite blockImage;
    public float moveSpeed;
    Rigidbody2D blockRigidBody;
    Collider2D blockCollider;
    // Start is called before the first frame update
    void Start()
    {
        blockRigidBody=GetComponent<Rigidbody2D>();
        blockCollider = GetComponent<Collider2D>();
        transform.Find("Texture").GetComponent<SpriteRenderer>().sprite= blockImage;
        transform.position= offset;
        blockCollider.bounds.size.Set(size.x,size.y,0);
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