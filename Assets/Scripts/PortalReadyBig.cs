using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalReadyBig : MonoBehaviour
{
	[SerializeField] private GameObject trigger;
	public GameObject wall;

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "PortalCollider" || other.gameObject.layer == 9)
		{
			trigger.SetActive(true);
			wall.GetComponent<Collider>().enabled = true;
		}
	}
}
