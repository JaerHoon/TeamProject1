using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{

    public float blinkSpeed = 1.0f; // �����̴� �ӵ� ������ ���� ����

    private TextMeshProUGUI textMesh;
    

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        // �ڷ�ƾ�� �̿��Ͽ� �����̴� ȿ���� �ֱ� ���� Start()���� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        int A = 0;
        while (true)
        {
            
            A++;
            print(A);
            if (A == 1000) StopAllCoroutines();
            // ���� ���� �����Ͽ� �ؽ�Ʈ�� �����̰� �մϴ�.
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.PingPong(Time.time * blinkSpeed, 1.0f));
            yield return null;
        }

       
    }



}
