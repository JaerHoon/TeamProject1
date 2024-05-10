using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerDamageText : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; // �ؽ�Ʈ �̵��ӵ�
    [SerializeField]
    float alphaSpeed; // ���� ��ȯ�ӵ�
    [SerializeField]
    float destroyTime;
    

    public int Damage;

    TextMeshPro text;
    Color alpha;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = Damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
