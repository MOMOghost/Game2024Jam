using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private Vector2 movement;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

         
    }

    void Update()
    {

        if(movement!=null)
        // 在Update中调用Move
        Move();
    }

    public void MoveToLeft()
    {
        movement.x = -1;
        Debug.Log("MoveToLeft" + movement.x);
    }

    public void MoveToRight()
    {
        movement.x = 1;
        Debug.Log("MoveToRight" + movement.x);
    }

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        // Debug.Log("Move");
        _rigidbody2D.velocity = new Vector2(movement.x * speed, _rigidbody2D.velocity.y);
    }
}
