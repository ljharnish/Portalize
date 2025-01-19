using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
	[Header("Animated Materials")]
	[SerializeField] private Material ResetZone;
	private float ResetZoneTime;


	private void Update()
	{
		ResetZoneUpdate();
		
	}

	private void ResetZoneUpdate()
	{
		ResetZoneTime += 0.1f * Time.deltaTime;
		float maximum = 1f;
		float minimum = 0f;
		if (ResetZoneTime > 1.0f)
		{
			float temp = maximum;
			maximum = minimum;
			minimum = temp;
			ResetZoneTime = 0.0f;
		}
		ResetZone.mainTextureOffset = new Vector2(ResetZone.mainTextureOffset.x, ResetZoneTime);
	}
}
