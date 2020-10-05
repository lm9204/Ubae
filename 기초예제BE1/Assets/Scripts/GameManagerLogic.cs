using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int TotalItemCount;
    public int Stage;
    public Text StageCount;
    public Text PlayerCount;

    void Awake()
    {
        StageCount.text = "  / " + TotalItemCount;
    }

    public void getItem(int count)
    {
        PlayerCount.text = count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            SceneManager.LoadScene(Stage);
        }
    }
}
