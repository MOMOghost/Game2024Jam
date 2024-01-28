using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SupSystem;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{

    #region 角色音效
    [SerializeField] SoundController.AudioType volumeName;
        // Start is called before the first frame update
    [SerializeField]  SoundController soundController;
    #endregion
    [SerializeField] GameObject DeadScene;
    [SerializeField] GameObject Setting;
    float 收集梗圖查克拉 = 0;
    float 收集領域展開咒力 = 0;

    


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
    public float scorePoint = 0;
    public Text healthPointText;
    public Text ScoreText;
    public Text 梗圖查克拉Text;
   
    #endregion


    #region 場景互動
    public LayerMask platformLayer;
    public 梗圖觸發 梗圖觸發器;
    
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
        healthPointText.text = healthPoint.ToString();
        soundController = FindAnyObjectByType<SoundController>();
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

        //看他變成2.5條貓沒
        //Dead();受傷再測就好

        //暫停功能
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void MoveToLeft()
    {
        movement.x = -1;
        // Debug.Log("MoveToLeft" + movement.x);
    }

    public void MoveToRight()
    {
        movement.x = 1;
        // Debug.Log("MoveToRight" + movement.x);
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
        GetComponent<Animator>().SetBool("hit",true);
        //soundController.PlayAudio(soundController.Sound[HIT], SoundController.AudioType.Sound, false);
        hit = true;
        _rigidbody2D.AddForce(Vector2.down * jumpForce *1.5f);


        //-----------------這段打開會變很難
            // 收集梗圖查克拉--;
            // if(收集梗圖查克拉<=0){
            //     收集梗圖查克拉 = 0;
            // }
            // 梗圖查克拉Text.text = 收集梗圖查克拉.ToString();
        //-----------------這段打開會變很難

        GetComponent<Animator>().SetBool("hit", false);
        Dead();
    }

    private void Dead()
    {
        healthPointText.text = healthPoint.ToString();
        if (healthPoint <= 0)
        {
            GameObject game;
            if (!dead)
            {
                game = Instantiate(DeadScene);
                game.GetComponent<DeadScene>().text.text=scorePoint.ToString();
            }
            dead = true;
            soundController.StopPlay(soundController.BGM[0].name);
            soundController.PlayAudio("死亡_0128", SoundController.AudioType.Special);
        }
    }

    //傷害後回復閒置狀態
    private void RecoverIdle()
    {
        if (hit){
            hit = false;
        }
            
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
            // Debug.Log("一段跳一段跳一段跳一段跳一段跳");
            _jumpCount++;
            _rigidbody2D.AddForce(Vector2.up * jumpForce);
            jump = true;
            soundController.PlayAudio(soundController.Sound[0], SoundController.AudioType.Sound, false);
        }

        //二段跳
        if (_jumpKeyDown && !isGround)
        {
            // Debug.Log("二段跳二段跳二段跳二段跳二段跳");
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

    private void Score()
    {
        scorePoint += 10;
        ScoreText.text = scorePoint.ToString();

        if(scorePoint > 30){
            收集領域展開咒力++;
        }
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞到的物体是否有指定的组件
        BlockState blockStateComponent = collision.gameObject.GetComponent<BlockState>();

        if (blockStateComponent != null)
        {
            // 碰撞到了具有 BlockState 组件的物体
            // Debug.Log("碰撞到了具有 BlockState 组件的物体");

            if(  blockStateComponent.emoji == BlockEmoji.Happy){
                收集梗圖查克拉++;
                梗圖查克拉Text.text = 收集梗圖查克拉.ToString();

                if(收集梗圖查克拉 == 3){
                    Score();
                    收集梗圖查克拉 = 0;
                    梗圖查克拉Text.text = 收集梗圖查克拉.ToString();
                    梗圖觸發器.開啟梗圖();
                }
            }else{
                收集梗圖查克拉--;
                if(收集梗圖查克拉<=0){
                    收集梗圖查克拉 = 0;
                }

                梗圖查克拉Text.text = 收集梗圖查克拉.ToString();
            }
         
        }
    }


    bool GroundSound = false;

    /**
     * 判断是否落在平台上
     */
    private void IsGround()
    {
        isGround = _boxCollider2D.IsTouchingLayers(platformLayer);

        if(isGround && GroundSound && soundController != null){
            GroundSound = false;
            //soundController.PlayAudio(soundController.Sound[2], SoundController.AudioType.Sound, false);
        }

        if(!isGround){
              RunSoundCount = 0;
        }
    }


    private bool canTrigger = true;
    private IEnumerator PlayAudioWithDelay()
    {
        Debug.Log("1111111111111111111");
        // 触发音频播放
        soundController.PlayAudio(soundController.Sound[1], SoundController.AudioType.Sound, false);

        // 设置延迟时间为两秒
        float delayTime = 2f;

        // 等待两秒
        yield return new WaitForSeconds(delayTime);

        // 允许再次触发
        canTrigger = true;
    }


    int RunSoundCount = 0;

    /*
     * 角色動畫
     */
    private void ChangeAnimator()
    {
        fall = false;


        if (_rigidbody2D.velocity.y < 0){
            fall = true;
            GroundSound = true;

            //    if(doubleJump){
            //     // Debug.Log("fallfallfallfallfallfallfallfallfallfall");
            // }
        }
           

        jump = false;
        if (_rigidbody2D.velocity.y > 1.41){  //1.40275  // 0
             jump = true;
                // Debug.Log("jumpjumpjumpjumpjumpjumpjumpjumpjumpjumpjump");
        }
           
        run = false;
    
        
        // Debug.Log("_rigidbody2D.velocity.x : " + _rigidbody2D.velocity.x);

        if (Math.Abs(_rigidbody2D.velocity.x) > 0 && isGround){
            run = true;
            RunSoundCount++;

            if(RunSoundCount < 5){ 
                soundController.PlayAudio(soundController.Sound[1], SoundController.AudioType.Sound, false);
            }
    
        }



   
        if (_rigidbody2D.velocity.x < 0){
            transform.localScale = new Vector3(-0.3551f, transform.localScale.y, transform.localScale.z);

            //   if(doubleJump){
            //     Debug.Log("333333333333333333");
            // }
        }
       
        if (_rigidbody2D.velocity.x > 0){
            transform.localScale = new Vector3(0.3551f, transform.localScale.y, transform.localScale.z);

            //  if(doubleJump){
            //     Debug.Log("4444444444444");
            // }
        }
            
        _animator.SetBool(Hit, hit);
        _animator.SetBool(Fall, fall);
        _animator.SetBool(DoubleJump, doubleJump);
        _animator.SetBool(Jump, jump);
        _animator.SetBool(Run, run);
    }


    private bool isPaused = false;



    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // 暫停遊戲時間
                             // 在這裡可以添加其他暫停遊戲時的邏輯，例如顯示暫停菜單等
        if (!Setting.activeSelf)
            Setting.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // 恢復遊戲時間
                             // 在這裡可以添加其他恢復遊戲時的邏輯
        if (Setting.activeSelf)
            Setting.SetActive(false);
    }
    
}
