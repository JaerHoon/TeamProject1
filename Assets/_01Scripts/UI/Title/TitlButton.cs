using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitlButton : MonoBehaviour
{
    public float blinkSpeed = 1.0f; // �����̴� �ӵ� ������ ���� ����

    private TextMeshProUGUI textMesh;
    private bool isBlinking = true;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        // �ڷ�ƾ�� �̿��Ͽ� �����̴� ȿ���� �ֱ� ���� Start()���� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (GameManager.Instance.pressStartButton == false)
        {
            // ���� ���� �����Ͽ� �ؽ�Ʈ�� �����̰� �մϴ�.
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.PingPong(Time.time * blinkSpeed, 1.0f));
            yield return null;
        }
    }

    // �����̴� ���� ���߱� ���� �޼ҵ�
    public void StopBlinking()
    {
        isBlinking = false;
    }

}
