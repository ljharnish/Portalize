using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	public Animation doorAnimate;
	public bool OpenOrClose;
	private bool UsedAlready = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}
		if (UsedAlready) return;

		if (OpenOrClose) doorAnimate.Play("Open");
		if (!OpenOrClose) doorAnimate.Play("Close");
		if (!OpenOrClose) UsedAlready = true;
	}
}
