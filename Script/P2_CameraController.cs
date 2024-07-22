using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_CameraController : MonoBehaviour
{
	private GameObject target; // �Ǐ]����^�[�Q�b�g�I�u�W�F�N�g
	private GameObject target2;
	private P2_Controller_Platform playerController; // �v���C���[�R���g���[���[�ւ̎Q��
	public float followSpeed = 5f; // �Ǐ]����X�s�[�h
	public float cameraHeight = 5f; // �J�����̒ʏ�̍���
	public float jumpCameraHeight = 10f; // �W�����v���̃J�����̍���
	public float downInputCameraHeight = 15f; // isDownInput��true�̏ꍇ�̃J�����̍���
	public float overheadCameraHeight = 20f; // �ォ��΂ߌ��̃J�����̍���
	public float cameraDistance = 10f; // �J�����̃v���C���[����̋���

	void Start()
	{
		// �^�[�Q�b�g���擾
		target = GameObject.Find("Player2_CameraCube"); // �J�����̒Ǐ]�^�[�Q�b�g
		target2 = GameObject.Find("Player2_Fighter");

		// �^�[�Q�b�g��������Ȃ������ꍇ�̃G���[�`�F�b�N
		if (target == null)
		{
			Debug.LogError("�^�[�Q�b�g�I�u�W�F�N�g��������܂���B�I�u�W�F�N�g�������������m�F���Ă��������B");
			return;
		}

		// �v���C���[�R���g���[���[���擾
		playerController = target2.GetComponent<P2_Controller_Platform>();

		// �v���C���[�R���g���[���[��������Ȃ������ꍇ�̃G���[�`�F�b�N
		if (playerController == null)
		{
			Debug.LogError("�v���C���[�R���g���[���[��������܂���BP_Controller_Platform�X�N���v�g���������A�^�b�`����Ă��邩�m�F���Ă��������B");
		}
	}

	void LateUpdate()
	{
		if (target != null)
		{
			// �v���C���[�̌����ɍ��킹�ăJ��������]������
			Vector3 lookDirection = -target2.transform.forward; // �v���C���[�̌�����������

			// �J�����̍��������肷��
			float currentCameraHeight = cameraHeight;
			if (playerController.isJump)
			{
				// �W�����v���̓W�����v�p�̍����ɐݒ�
				currentCameraHeight = jumpCameraHeight;

				// �J�����̈ʒu���v���C���[�̌�납�猩���ʒu�ɐݒ�
				Vector3 targetPosition = target.transform.position - target2.transform.forward * cameraDistance + Vector3.up * currentCameraHeight;

				// �J�������^�[�Q�b�g�ɒǏ]
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
			}
			else if (playerController.isDownInput)
			{
				// isDownInput��true�̏ꍇ�͎΂ߌ��ɂ���
				Vector3 offset = new Vector3(-1, 1, -1); // �΂ߌ��̕����ւ̃I�t�Z�b�g
				Vector3 targetPosition = target.transform.position + offset.normalized * cameraDistance + Vector3.up * downInputCameraHeight;

				// �J�����̈ʒu���v���C���[�̎΂ߌ��ɔz�u���A�X���[�Y�ɒǏ]������
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
			}
			else
			{
				// �ʏ�̒Ǐ]�̏ꍇ
				Vector3 targetPosition = target.transform.position + lookDirection * cameraDistance + Vector3.up * currentCameraHeight;

				// �J�����̈ʒu���v���C���[�̔w�ʂɔz�u���A�X���[�Y�ɒǏ]������
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
			}

			// �^�[�Q�b�g������
			transform.LookAt(target.transform.position);
		}
	}
}
