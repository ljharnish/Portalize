using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAnimation : MonoBehaviour
{
	private Animation a;

	private void Start()
	{
		a = GetComponent<Animation>();
	}

	public void StartIdle()
	{
		a.Play("idle");
	}
}
