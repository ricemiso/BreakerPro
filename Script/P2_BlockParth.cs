using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_BlockParth : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject player2;
    public float scaleReductionStep = 0.1f; // �p�[�e�B�N���X�P�[���̌�����
    public float minScale = 0.1f; // �p�[�e�B�N���̍ŏ��X�P�[��
    private P2_Controller_Platform player2Controller;
    private Vector3 originalScale; // �p�[�e�B�N���̌��̃X�P�[����ۑ����邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        player2Controller = player2.GetComponent<P2_Controller_Platform>();

        // �p�[�e�B�N���̌��̃X�P�[����ۑ�����
        originalScale = particleSystem.transform.localScale;

        StopParticles2();
    }

    // Update is called once per frame
    void Update()
    {
        if (player2Controller.isBlocking)
        {
            PlayParticles2();

            // �h�䒆�ɍU�����󂯂��ꍇ�̏���
            if (player2Controller.isweakdamage)
            {
                ReduceParticleScale();
                // �U�����󂯂���Ƀt���O�����Z�b�g
                player2Controller.isweakdamage = false;
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
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    public void StopParticles2()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
            particleSystem.Clear(); // �c��̃p�[�e�B�N���𑦎��N���A
        }
    }

    private void ReduceParticleScale()
    {
        Vector3 currentScale = particleSystem.transform.localScale;

        // �V�����X�P�[�����v�Z���A�ŏ��X�P�[���������Ȃ��悤�ɂ���
        float newScaleX = Mathf.Max(currentScale.x - scaleReductionStep, minScale);
        float newScaleY = Mathf.Max(currentScale.y - scaleReductionStep, minScale);
        float newScaleZ = Mathf.Max(currentScale.z - scaleReductionStep, minScale);

        // �X�P�[���̒l���X�V
        particleSystem.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

        // �ŏ��X�P�[������������ꍇ�̏���
        if (newScaleX <= minScale || newScaleY <= minScale || newScaleZ <= minScale)
        {
            player2Controller.isstrongdamage = true;
            StartCoroutine(player2Controller.DisableBlockInput());
        }

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Reduced particle scale: " + particleSystem.transform.localScale);
    }


    private void RestoreOriginalScale()
    {
        // ���̃X�P�[���Ƀp�[�e�B�N���̃X�P�[����߂�
        particleSystem.transform.localScale = originalScale;

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Restored original particle scale: " + particleSystem.transform.localScale);
    }
}
