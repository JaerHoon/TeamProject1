using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void OnPlayerDamaged(int monDamage)
    {
        print("�÷��̾ ���� �޾ҽ��ϴ�!!" + monDamage +" : ������");
    }
    
    void Update()
    {
        
    }
}
