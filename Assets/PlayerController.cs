using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SupSystem;

public class PlayerController : MonoBehaviour
{

    #region 角色音效
    [SerializeField] SoundController.AudioType volumeName;
        // Start is called before the first frame update
    [SerializeField]  SoundController soundController;
    #endregion



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
    public float healthPoint = 100;
    #endregion


    #region 場景互動
    public LayerMask platformLayer;
    
    //天地壩傷害
    public float trapDamage = 10;
    #endregion

    #region 角色動畫
        public bool
            dead,
            isGround,
            hit,
            fall,
            doubleJump,
            jump,
            run;

        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Fall = Animator.StringToHash("fall");
        private static readonly int DoubleJump = Animator.StringToHash("doubleJump");
        private static readonly int Jump = Animator.StringToHash("jump");
        private static readonly int Run = Animator.StringToHash("run");
    #endregion

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();


        soundController.PlayAudio(soundController.BGM[0], SoundController.AudioType.BGM, true);
    }

    void Update()
    {

        if(movement!=null)
        // 在Update中调用Move

        //水平移動
        Move();

        //垂直移動 - 跳躍
        _jumpKeyDown = Input.GetKeyDown(KeyCode.Space);

        ChangeAnimator();

        //判斷是否在平台
        IsGround();

        //跳躍狀態判斷
        JumpTrigger();
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

    public void SpikeTrigger()
    {
        //傷害不重複
        if (!hit){
            healthPoint -= trapDamage;
        }
            
        //soundController.PlayAudio(soundController.Sound[HIT], SoundController.AudioType.Sound, false);
        hit = true;
        _rigidbody2D.AddForce(Vector2.down * jumpForce);
    }

    //傷害後回復閒置狀態
    private void RecoverIdle()
    {
        if (hit)
            hit = false;
    }



    public void JumpButton()
    {
        _jumpKeyDown = true;
    }

    /**
     * 跳跃
     * 在平台上才能起跳
     * 跳跃次数不能超过maxJumpCount   等等就給他可以
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
            soundController.PlayAudio(soundController.Sound[0], SoundController.AudioType.Sound, false);
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
                soundController.PlayAudio(soundController.Sound[0], SoundController.AudioType.Sound, false);
            }
        }

        //触碰平台重置跳跃
        if (_boxCollider2D.IsTouchingLayers())
        {
            jump = doubleJump = false;
            _jumpCount = 0;
        }
    }


    bool GroundSound = false;

    /**
     * 判断是否落在平台上
     */
    private void IsGround()
    {
        isGround = _boxCollider2D.IsTouchingLayers(platformLayer);

        if(isGround && GroundSound){
            GroundSound = false;
            soundController.PlayAudio(soundController.Sound[2], SoundController.AudioType.Sound, false);
        }
    }



    /*
     * 角色動畫
     */
    private void ChangeAnimator()
    {
        fall = false;
        if (_rigidbody2D.velocity.y < 0){
            fall = true;
            GroundSound = true;
        }
           

        jump = false;
        if (_rigidbody2D.velocity.y > 1.41){  //1.40275  // 0
             jump = true;
        }
           
        run = false;
        // Debug.Log("_rigidbody2D.velocity.x : " + _rigidbody2D.velocity.x);

        if (Math.Abs(_rigidbody2D.velocity.x) > 0 && isGround){
            run = true;
            soundController.PlayAudio(soundController.Sound[1], SoundController.AudioType.Sound, false);
        }
            
        if (_rigidbody2D.velocity.x < 0){
            transform.localScale = new Vector3(-0.3551f, transform.localScale.y, transform.localScale.z);
        }
       
        if (_rigidbody2D.velocity.x > 0){
            transform.localScale = new Vector3(0.3551f, transform.localScale.y, transform.localScale.z);
        }
            
        _animator.SetBool(Hit, hit);
        _animator.SetBool(Fall, fall);
        _animator.SetBool(DoubleJump, doubleJump);
        _animator.SetBool(Jump, jump);
        _animator.SetBool(Run, run);
    }
    
}
