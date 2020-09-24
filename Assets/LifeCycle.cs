using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
   void Update()
   {
      if(Input.anyKeyDown)
         Debug.Log("플레이어가 아무 키를 눌렀습니다.");
      
      if(Input.anyKey)
         Debug.Log("플레이어가 아무 키를 누르고 있습니다.");
   }
}
