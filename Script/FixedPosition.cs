using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    // �Œ肷����W���w��
    public Vector3 fixedPosition;

    void Start()
    {
        // �����ʒu��ݒ�
        transform.position = fixedPosition;
    }

    void Update()
    {
        // ���t���[���w�肵�����W�ɌŒ�
        transform.position = fixedPosition;
    }
}
