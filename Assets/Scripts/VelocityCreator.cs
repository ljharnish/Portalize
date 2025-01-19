using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityCreator : MonoBehaviour
{
	public Vector3 VelocityStat;
	float forceAmount = 5f;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("m");
		StartCoroutine(FakeForce(other.gameObject.GetComponentInParent<Rigidbody>()));
	}

	IEnumerator FakeForce(Rigidbody c)
	{
		c.isKinematic = false;
		c.AddRelativeForce(VelocityStat, ForceMode.VelocityChange);
		float i = 0.01f;
		while (forceAmount > i)
		{
			//c.velocity = new Vector3(VelocityStat.x / i, VelocityStat.y / i, VelocityStat.z / i);
			i = i + Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		c.velocity = Vector3.zero;
		c.isKinematic = true;
		yield return null;
	}
}
