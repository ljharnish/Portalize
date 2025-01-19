using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaterializer : ButtonLink
{
	public Material ActiveButtonMaterial;
	public Material InactiveButtonMaterial;
	public override void Active()
	{
		gameObject.GetComponentInParent<Animation>().Play("ButtonDown");
		Material[] mats = GetComponent<Renderer>().materials;
		mats[1] = ActiveButtonMaterial;
		GetComponent<Renderer>().materials = mats;
	}

	public override void Inactive()
	{
		gameObject.GetComponentInParent<Animation>().Play("ButtonUp");
		Material[] mats = GetComponent<Renderer>().materials;
		mats[1] = InactiveButtonMaterial;
		GetComponent<Renderer>().materials = mats;
	}
}
