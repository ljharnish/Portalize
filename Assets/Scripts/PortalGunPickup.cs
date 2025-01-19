using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunPickup : MonoBehaviour
{
	[Space]
	public GameObject WholeGun;
	[Space]
	public GameObject EnabledItem;
	[Space]
	public GameObject PortalGun;
	[Space]
	public bool greenonly;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Contains("Player"))
		{
			Destroy(WholeGun);
			EnabledItem.SetActive(true);
			other.GetComponent<PlayerController>().PortalGun = PortalGun;
			other.GetComponent<PlayerController>().hasGun = true;
			if(greenonly) other.GetComponent<PlayerController>().GreenGunOnly = true;
			other.GetComponent<PlayerController>().PortalGun.GetComponent<Animation>().Play("idle");
		}
	}
}
