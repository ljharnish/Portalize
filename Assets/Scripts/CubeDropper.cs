using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDropper : Interactable
{
	public GameObject CubePrefab;
	public Animation Animate;
	public Transform SpawnPoint;
	[Space]
	public GameObject CurrentCube;

	public override void On()
	{
		Animate.Play("Open");
		if (CurrentCube)
		{
			Destroy(CurrentCube);
			CurrentCube = null;
		}

		GameObject newCube = Instantiate(CubePrefab);
		newCube.transform.position = SpawnPoint.position;
		CurrentCube = newCube;
	}
	public override void Off()
	{
		Animate.Play("Close");
	}
}
