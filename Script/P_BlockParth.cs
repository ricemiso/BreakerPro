using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_BlockParth : MonoBehaviour
{
    public ParticleSystem particleSystem2;
    public GameObject player1;
    public float scaleReductionStep = 0.1f; // �p�[�e�B�N���X�P�[���̌�����
    public float minScale = 0.1f; // �p�[�e�B�N���̍ŏ��X�P�[��
    private P_Controller_Platform player1Controller;
    private Vector3 originalScale; // �p�[�e�B�N���̌��̃X�P�[����ۑ����邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        player1Controller = player1.GetComponent<P_Controller_Platform>();

        // �p�[�e�B�N���̌��̃X�P�[����ۑ�����
        originalScale = particleSystem2.transform.localScale;

        StopParticles2();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Controller.isBlocking)
        {
            PlayParticles2();

            // �h�䒆�ɍU�����󂯂��ꍇ�̏���
            if (player1Controller.isweakdamage)
            {
                ReduceParticleScale();
                // �U�����󂯂���Ƀt���O�����Z�b�g
                player1Controller.isweakdamage = false;
            }
        }
        else
        {
            // �h�䂪�������ꂽ��p�[�e�B�N���̃X�P�[�������ɖ߂�
            RestoreOriginalScale();

            StopParticles2();
        }
    }

    public void PlayParticles2()
    {
        if (particleSystem2 != null && !particleSystem2.isPlaying)
        {
            particleSystem2.Play();
        }
    }

    public void StopParticles2()
    {
        if (particleSystem2 != null && particleSystem2.isPlaying)
        {
            particleSystem2.Stop();
            particleSystem2.Clear(); // �c��̃p�[�e�B�N���𑦎��N���A
        }
    }

    private void ReduceParticleScale()
    {
        Vector3 currentScale = particleSystem2.transform.localScale;

        // �V�����X�P�[�����v�Z���A�ŏ��X�P�[���������Ȃ��悤�ɂ���
        float newScaleX = Mathf.Max(currentScale.x - scaleReductionStep, minScale);
        float newScaleY = Mathf.Max(currentScale.y - scaleReductionStep, minScale);
        float newScaleZ = Mathf.Max(currentScale.z - scaleReductionStep, minScale);

        // �X�P�[���̒l���X�V
        particleSystem2.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

        // �ŏ��X�P�[������������ꍇ�̏���
        if (newScaleX <= minScale || newScaleY <= minScale || newScaleZ <= minScale)
        {
            player1Controller.isstrongdamage = true;
            StartCoroutine(player1Controller.DisableBlockInput());
        }

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Reduced particle scale: " + particleSystem2.transform.localScale);
    }



    private void RestoreOriginalScale()
    {
        // ���̃X�P�[���Ƀp�[�e�B�N���̃X�P�[����߂�
        particleSystem2.transform.localScale = originalScale;

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Restored original particle scale: " + particleSystem2.transform.localScale);
    }

}
