using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private Interactable item;

	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "PortalCollider") item.On();
	}
}
