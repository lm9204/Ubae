using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    Rigidbody2D _rigid;
    Animator _anim;
    Vector3 dirVec;
    GameObject scanObject;

    
    float h;
    float v;

    bool isHorizonMove;
    
    
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = !manager.isAction && Input.GetButtonDown("Horizontal");
        bool vDown = !manager.isAction && Input.GetButtonDown("Vertical");
        bool hUp = !manager.isAction && Input.GetButtonUp("Horizontal");
        bool vUp = !manager.isAction && Input.GetButtonUp("Vertical");

        //check horizontal move
        if (hDown)
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;
            
        
        //#animation 
        if (_anim.GetInteger("hAxisRaw") != (int)h )
        {
            _anim.SetBool("isChange", true);
            _anim.SetInteger("hAxisRaw", (int)h);
        }else if(_anim.GetInteger("vAxisRaw") != (int)v)
        {
            _anim.SetBool("isChange", true);
            _anim.SetInteger("vAxisRaw", (int) v);
        }else
            _anim.SetBool("isChange", false);

        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == -1)
            dirVec = Vector3.left;
        else if(hDown && h == 1)
            dirVec = Vector3.right;
        
        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        _rigid.velocity = moveVec * Speed;
        
        //#ray
        Debug.DrawRay(_rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;


    }
}

