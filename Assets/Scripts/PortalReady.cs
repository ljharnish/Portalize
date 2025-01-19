using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalReady : MonoBehaviour
{
	[SerializeField] private GameObject trigger;

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "PortalCollider" || other.gameObject.layer == 9)
		{
			trigger.SetActive(true);
		}
	}
}
