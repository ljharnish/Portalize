using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
	public bool isOpen = false;
	public Animation doorAnimate;
	[Space]
	public Renderer doorRenderer;
	public Material GreenLight;
	public Material RedLight;
	[Space]
	public bool MultipleItems = false;
	public FloorButtonTrigger[] buttons;

	public override void On()
	{
		if(!MultipleItems)
		{
			doorAnimate.Play("Open");
			Material[] mats = doorRenderer.materials;
			mats[1] = GreenLight;
			doorRenderer.materials = mats;
			isOpen = true;

		} else
		{
			bool allTriggered = true;
			for(int i = 0; i < buttons.Length; i++)
			{
				if (!buttons[i].pressed) allTriggered = false;
			}

			if(allTriggered)
			{
				doorAnimate.Play("Open");
				Material[] mats = doorRenderer.materials;
				mats[1] = GreenLight;
				doorRenderer.materials = mats;
				isOpen = true;
			}
		}
		
	}

	public override void Off()
	{
		if (!isOpen) return;
		doorAnimate.Play("Close");
		Material[] mats = doorRenderer.materials;
		mats[1] = RedLight;
		doorRenderer.materials = mats;
	}
}
