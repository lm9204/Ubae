using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gamaManager;
    public float maxSpeed = 4f;
    public float jumpPower = 15f;
    
    Rigidbody2D _rigid;
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    CapsuleCollider2D _collider;
    AudioSource _audioSource;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Jump") && !_anim.GetBool("isJumping"))
        {
            _rigid.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            _anim.SetBool("isJumping", true);
            PlaySound("JUMP");
        }
        
        if(Input.GetButtonUp("Horizontal"))
            _rigid.velocity = new Vector2(_rigid.velocity.normalized.x* 0.3f, _rigid.velocity.y);

        if (Input.GetButton("Horizontal"))
            _spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        
        if(Mathf.Abs(_rigid.velocity.x) < 0.3)
            _anim.SetBool("isWalking", false);
        else 
            _anim.SetBool("isWalking", true);
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        _rigid.AddForce(Vector2.right*h,ForceMode2D.Impulse);
        
        if(_rigid.velocity.x > maxSpeed )
            _rigid.velocity = new Vector2(maxSpeed, _rigid.velocity.y);
        else if(_rigid.velocity.x < maxSpeed*(-1))
            _rigid.velocity= new Vector2(maxSpeed*(-1), _rigid.velocity.y);

        Debug.DrawRay(_rigid.position, Vector3.down, new Color(0,1,0));
        if (_rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
                if (rayHit.distance < 0.5f)
                    _anim.SetBool("isJumping", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            if (_rigid.velocity.y < 0 && transform.position.y > other.transform.position.y)
            {
                onAttack(other.transform);
                PlaySound("ATTACK");
                gamaManager.stagePoint += 100;
            }
            else
            {
                onDamaged(other.transform.position);
                PlaySound("DAMAGED");
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            bool isBronze = other.gameObject.name.Contains("Bronze");
            bool isSilver = other.gameObject.name.Contains("Silver");
            bool isGold = other.gameObject.name.Contains("Gold");

            if (isBronze)
                gamaManager.stagePoint += 50;
            else if(isSilver)
                gamaManager.stagePoint += 100;
            else if(isGold)
                gamaManager.stagePoint += 300;
            
            other.gameObject.SetActive(false);
            PlaySound("ITEM");
        }else if (other.gameObject.tag == "Finish")
        {
            gamaManager.NextStage();
            PlaySound("FINISH");
        }
    }

    void onAttack(Transform e)
    {
        EnemyMove enemyMove = e.GetComponent<EnemyMove>();
        enemyMove.onDamaged();
    }
    void onDamaged(Vector2 targetPos)
    {
        gamaManager.HealthDown();
        
        gameObject.layer = 11;

        _spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int direction = transform.position.x - targetPos.x > 0 ? 1 : -1;
        _rigid.AddForce(new Vector2(direction,1) * 10, ForceMode2D.Impulse);

        _anim.SetTrigger("doDamaged");
        
        Invoke("offDamaged",2);
    }

    void offDamaged()
    {
        gameObject.layer = 10;
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                _audioSource.clip = audioJump;
                break;
            case "ATTACK":
                _audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                _audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                _audioSource.clip = audioItem;
                break;
            case "DIE":
                _audioSource.clip = audioDie;
                break;
            case "FINISH":
                _audioSource.clip = audioFinish;
                break;
        }

        _audioSource.Play();
    }

    public void onDie()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        _spriteRenderer.flipY = true;

       _collider.enabled = false;

        _rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        PlaySound("DIE");
    }

    public void VelocityZero()
    {
        _rigid.velocity = Vector2.zero;
    }
}
