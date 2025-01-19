using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPatch : MonoBehaviour
{
    public Vector3 VelocityVector;
	public float forceAmount = 0f;

	public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
		{
			var c = other.gameObject.GetComponent<Rigidbody>();
			c.isKinematic = false;
			c.AddRelativeForce(0, 60, 0, ForceMode.VelocityChange);
		}
    }

	IEnumerator FakeForce(Rigidbody c)
	{
		c.isKinematic = false;
		c.AddRelativeForce(VelocityVector, ForceMode.VelocityChange);
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
