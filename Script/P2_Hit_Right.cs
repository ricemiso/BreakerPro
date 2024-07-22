using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Hit_Right : MonoBehaviour
{
	public ParticleSystem Parth;
	public GameObject player2;
	private P2_Controller_Platform player2Controller;

	private void Start()
	{
		Parth.Stop();
		player2Controller = player2.GetComponent<P2_Controller_Platform>();
	}

	//�I�u�W�F�N�g�ƐڐG�����u�ԂɌĂяo�����
	private void OnTriggerEnter(Collider other)
	{
		// �U���������肪Enemy�̏ꍇ
		if ((other.gameObject.layer == 12 || other.gameObject.layer == 16) && player2Controller.isAnim)
		{
			ParticleSystem particles = Instantiate(Parth, transform.position, Quaternion.identity);
			particles.Play();
			StartCoroutine(DestroyParticleAfterDelay(particles, 0.2f));
			Debug.Log("hit_HandP1  " + other.gameObject.tag);
		}

		// �U���������肪Player�̏ꍇ
		if (other.gameObject.layer == 9 && player2Controller.isAnim)
		{
			ParticleSystem particles2 = Instantiate(Parth, transform.position, Quaternion.identity);
			particles2.Play();
			StartCoroutine(DestroyParticleAfterDelay(particles2, 0.2f));
			Debug.Log("hit_HandP2  " + other.gameObject.tag);
		}
	}

	IEnumerator DestroyParticleAfterDelay(ParticleSystem particles, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(particles.gameObject);
	}
}