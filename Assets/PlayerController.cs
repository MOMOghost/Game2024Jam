using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    #region 移動操作
    public float speed = 1;
    public float jumpForce;
    public float maxJumpCount = 2;
    private float _jumpCount;
    private bool _jumpKeyDown;


    private Vector2 movement;

    #endregion





    #region 角色屬性欄
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    #endregion


    #region 場景互動
    public LayerMask platformLayer;
    #endregion


    public bool
        dead,
        isGround,
        hit,
        fall,
        doubleJump,
        jump,
        run;

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

        //水平移動
        Move();
        _jumpKeyDown = Input.GetKeyDown(KeyCode.Space);

        //判斷是否在平台
        IsGround();
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

    public void JumpButton()
    {
        _jumpKeyDown = true;
    }

    /**
     * 跳跃
     * 在平台上才能起跳
     * 跳跃次数不能超过maxJumpCount
     */
    private void JumpTrigger()
    {
        // Debug.Log("jump" + jump);
        //一段跳
        if (_jumpKeyDown && isGround)
        {
            _jumpCount++;
            _rigidbody2D.AddForce(Vector2.up * jumpForce);
            jump = true;
            //SoundManager.PlayJumpSound();
        }

        //二段跳
        if (_jumpKeyDown && !isGround)
        {
            jump = false;
            doubleJump = true;
            _jumpCount++;
            if (_jumpCount < maxJumpCount)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce);
                //SoundManager.PlayJumpSound();
            }
        }

        //触碰平台重置跳跃
        if (_boxCollider2D.IsTouchingLayers())
        {
            jump = doubleJump = false;
            _jumpCount = 0;
        }
    }

    /**
     * 判断是否落在平台上
     */
    private void IsGround()
    {
        isGround = _boxCollider2D.IsTouchingLayers(platformLayer);

        if(isGround){
                    Debug.Log("isGround : "+ isGround);
        }

    }
    
}
