using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("플레이어 데이터가 준비되었습니다.");
    }

    void onEnable()
    {
        Debug.Log("플레이어가 로그인 하였습니다.");
    }

    void Start()
    {
        Debug.Log("사냥 장비를 챙겼습니다.");
    }

    void FixedUpdate()
    {
        Debug.Log("이동");
    }

    void Update()
    {
        Debug.Log("몬스터 사냥");
    }

    void LateUpdate()
    {
        Debug.Log("경험치 획득.");
    }

    void onDisable()
    {
        Debug.Log("플레이어가 로그아웃하였습니다.");
    }

    void onDestroy()
    {
        Debug.Log("플레이어 데이터를 해제하였습니다.");
    }
}
