using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonTrigger : MonoBehaviour
{
    public bool pressed = false;
	[Header("Sound")]
	[SerializeField] private AudioSource buttonSound;
    [Space]
    [Header("Animation")]
    [SerializeField] private Animation animator;
    [Space]
    [Header("Activation")]
    [SerializeField] private LayerMask LayersCanActivate;
	[SerializeField] private Renderer ButtonMats;
	[SerializeField] private Material PressedMaterial;
	[SerializeField] private Material DepressedMaterial;
	[Space]
    [SerializeField] private Material RedWire;
    [SerializeField] private Material GreenWire;
    [SerializeField] private Interactable Item;
    [SerializeField] private Renderer[] Wires;

	[SerializeField] private GameObject objectPressed;

	private void OnTriggerEnter(Collider other)
	{
		if (!pressed)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("PickupAllowed") || other.gameObject.tag == "Player")
			{
				objectPressed = other.gameObject;
				animator.Play("Pressed");
				buttonSound.Play();
				Material[] matArray = ButtonMats.materials;
				matArray[0] = ButtonMats.materials[0];
				matArray[1] = PressedMaterial;
				ButtonMats.materials = matArray;
				pressed = true;

				for (var i = 0; i < Wires.Length; i++)
				{
					Wires[i].material = GreenWire;
				}
				Item.On();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == objectPressed)
		{
			animator.Play("Depressed");
			Material[] matArray = ButtonMats.materials;
			matArray[0] = ButtonMats.materials[0];
			matArray[1] = DepressedMaterial;
			ButtonMats.materials = matArray;
			pressed = false;

			for (var i = 0; i < Wires.Length; i++)
			{
				Wires[i].material = RedWire;
			}
			Item.Off();

			objectPressed = null;
		}
	}
}
