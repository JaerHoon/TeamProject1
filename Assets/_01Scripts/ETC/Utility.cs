using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    //�ߺ����� ���� ���� ����
    public static int[] RandomCreate(int count, int RangeMin, int RangeMax)
    {
        int[] nums = new int[count];
        //1. �ŰԺ������� ���� count ������ ���� ������ ���� �迭�� ����ϴ�.

        List<int> rangenums = new List<int>();
        // 2. �׸���  �� �ٸ� ����Ʈ�� �ϳ� ����ϴ�. 

        for (int i = 0; i < RangeMax - RangeMin; i++)
        {
            rangenums.Add(i + RangeMin);// rangenums[0] = 0+RangeMin �̶�� ���ڰ� ���ϴ�.
        }
        //3. ������ ���� ����Ʈ�� RangeMin���� RangeMax������ ������ �߰��մϴ�.

        for (int i = 0; i < count; i++)
        {
            int RandomNum = Random.Range(0, rangenums.Count);
            // 4.RandomNum ������ ������ ���� ������ ������ ���� �ϳ� �����մϴ�.
            nums[i] = rangenums[RandomNum - RangeMin];
            // 5. �׸��� rangenums����Ʈ�� ����� ������ ������ �ּҰ��� �� ����°�� ���� ó�� ���� �迭 nums�� �߰��մϴ�.
            rangenums.RemoveAt(RandomNum - RangeMin);
            // 6. �׸��� �ѹ� �� ���� rangenums ����Ʈ���� ���� �˴ϴ�. �׷��Ƿ� �ѹ� ���� ���� ����Ǵ� ���� �������ϴ�.

        }
        // 7.�׷��� ���� ���� ���� ��ŭ nums[] �迭�� ����ݴϴ�.

        return nums; // 8.nums ��� �迭�� ��ȯ�մϴ�.  

    }

    public static Vector3 EaseInOutCubic(Vector3 start, Vector3 end, float value)//easing ������ ���� �Լ�
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    public static Vector3 EaseInQuint(Vector3 start, Vector3 end, float value)//easing ������ ���� �Լ�
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    public static float EaseInBounce(float start, float end, float value)//�ٿ ������
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }

    public static float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

}
