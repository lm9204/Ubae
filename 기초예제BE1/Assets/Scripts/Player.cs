using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private bool isJump;
    
    AudioSource audio;

    public float jumpPower;
    public int score;
    public GameManagerLogic manager;
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rigid.AddForce(new Vector3(0,jumpPower,0 ), ForceMode.Impulse);
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h,0,v ), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
            isJump = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            score++;
            audio.Play();
            other.gameObject.SetActive(false);
            manager.getItem(score);
        }
        else if (other.CompareTag("Finish"))
        {
            if (score == manager.TotalItemCount)
            {
                SceneManager.LoadScene(manager.Stage+1);
            }
            else
            {
                SceneManager.LoadScene(manager.Stage);
            }
        }
    }
}
