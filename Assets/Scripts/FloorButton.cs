using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
	[Space]
	[Header("Activity Collider")]
	public Collider ButtonZone;
	[Space]
	[Header("Materials")]
	public Material ActiveWireMaterial;
	public Material InactiveWireMaterial;
	[Space]
	[Header ("Objects")]
	public Transform GroupWire;
	public Transform ButtonForMaterial;
	[Space]
	[Header("Scripts to Run")]
	public ButtonLink[] Activities;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube")
		{
			for(int i=0;i<GroupWire.childCount;i++)
			{
				Renderer wire = GroupWire.GetChild(i).GetComponent<Renderer>();
				wire.material = ActiveWireMaterial;
			}

			for (int i = 0; i < Activities.Length; i++)
			{
				Activities[i].Active();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube")
		{
			for (int i = 0; i < GroupWire.childCount; i++)
			{
				Renderer wire = GroupWire.GetChild(i).GetComponent<Renderer>();
				wire.material = InactiveWireMaterial;
			}

			for (int i = 0; i < Activities.Length; i++)
			{
				Activities[i].Inactive();
			}
		}
	}
}
