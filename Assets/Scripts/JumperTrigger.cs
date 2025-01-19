using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTrigger : MonoBehaviour
{
	public Transform target;
	public bool IsActive;
	private void OnTriggerEnter(Collider other)
	{
		/*if(other.gameObject.tag == "Player")
		{
			Rigidbody gameObjRB = other.gameObject.GetComponent<Rigidbody>();
			gameObjRB.AddForce(target.position - other.transform.position);
		}*/
		if(IsActive) StartCoroutine(movePlayer(other.transform));
	}

	private IEnumerator movePlayer(Transform player)
	{
		float current = 0;
		Vector3 center = (player.position + target.position) * 0.5F;
		center -= new Vector3(0, 3, 0);

		Vector3 playerPos = player.position - center;
		Vector3 targetPos = target.position - center;

		while (current < 1f)
		{
			player.position = Vector3.Slerp(playerPos, targetPos, current / 1f);
			player.position += center;
			current += Time.deltaTime;
			yield return null;
		}
		yield return null;
	}
}
