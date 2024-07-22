using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParth : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public GameObject player1;  // Player1�I�u�W�F�N�g���Q��
    public GameObject player2;  // Player2�I�u�W�F�N�g���Q��

    public Camera player1Camera; // Player1�̃J����
    public float activationDistance = 500.0f;  // �p�[�e�B�N�����Đ�����鋗����臒l

    private P_Controller_Platform player1Controller;

    void Start()
    {
        // �p�[�e�B�N���V�X�e�����ݒ肳��Ă��Ȃ��ꍇ�A�e�I�u�W�F�N�g����擾
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
        StopParticles();

        player1Controller = player1.GetComponent<P_Controller_Platform>();

        // Player1�̃J��������p�[�e�B�N�����\���ɂ��邽�߂̐ݒ�
        if (player1Camera != null)
        {
            // �p�[�e�B�N���V�X�e���p�̐V�������C���[���쐬
            int particleLayer = LayerMask.NameToLayer("ParticleLayer");
            if (particleLayer == -1)
            {
                // "ParticleLayer" ���C���[�����݂��Ȃ��ꍇ�͐V�K�ɍ쐬����
                Debug.LogError("Please add a layer named 'ParticleLayer' in the Layer settings.");
            }
            else
            {
                Debug.Log("Setting particle system and children to layer: " + particleLayer);

                // �p�[�e�B�N���V�X�e���Ƃ��̎q�I�u�W�F�N�g��V�������C���[�ɐݒ�
                SetLayerRecursively(particleSystem.gameObject, particleLayer);

                // Player1�̃J������Culling Mask����V�������C���[�����O
                Debug.Log("Before Culling Mask: " + player1Camera.cullingMask);
                player1Camera.cullingMask &= ~(1 << particleLayer);
                Debug.Log("After Culling Mask: " + player1Camera.cullingMask);
            }
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if (distance > activationDistance && !player1Controller.isCrouch && !player1Controller.Special)
        {
            PlayParticles();
        }
        else
        {
            StopParticles();
        }
    }

    // �p�[�e�B�N�����Đ����郁�\�b�h
    public void PlayParticles()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    // �p�[�e�B�N�����~���郁�\�b�h
    public void StopParticles()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    // �p�[�e�B�N�����ꎞ��~���郁�\�b�h
    public void PauseParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Pause();
        }
    }

    // �p�[�e�B�N�����N���A���郁�\�b�h
    public void ClearParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Clear();
        }
    }

    // �I�u�W�F�N�g�Ƃ��̂��ׂĂ̎q�I�u�W�F�N�g�Ƀ��C���[���ċA�I�ɐݒ肷�郁�\�b�h
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;
        Debug.Log("Setting layer for: " + obj.name);

        foreach (Transform child in obj.transform)
        {
            if (child != null)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}
