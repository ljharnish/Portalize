using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool switched = false;
	[Header("Sound")]
    [SerializeField] private AudioSource switchOn;
    [SerializeField] private AudioSource switchOff;
    [Space]
    [Header("Animation")]
    [SerializeField] private Animation animator;
    [Space]
    [Header("Activation")]
    [SerializeField] private Material RedWire;
    [SerializeField] private Material GreenWire;
    [SerializeField] private Interactable Item;
    [SerializeField] private Renderer[] Wires;
    [Space]
    [Header("Timing")]
    [SerializeField] private bool timed = false;
    [SerializeField] private float time;
    [SerializeField] private bool disableAfterTime = false;
    [Space]
	[Header("Multi-Activate")]
    [SerializeField] private bool multi = false;
    [SerializeField] private Transform[] WireGroups;
    [SerializeField] private Interactable[] Items;
    [SerializeField] private int ms_val = 0;

    void Start()
    {
        if (switched) animator.Blend("Flip UP");
        if (!switched) animator.Blend("Flip DOWN");

        if(multi && switched)
		{
            for (var i = 0; i < WireGroups.Length; i++)
            {
                for (var j = 0; j < WireGroups[i].childCount; j++)
                {
                    WireGroups[i].GetChild(j).GetComponent<Renderer>().material = RedWire;
                }
            }

            for (var k = 0; k < WireGroups[ms_val].childCount; k++)
            {
                WireGroups[ms_val].GetChild(k).GetComponent<Renderer>().material = GreenWire;
            }

            ms_val += 1;
            ms_val %= WireGroups.Length;
        }
    }

    public void SwitchA()
	{
		if (!multi)
		{
            switched = !switched;

            if (switched)
            {
                switchOn.Play();
                animator.Blend("Flip UP");
                for(var i=0;i<Wires.Length;i++)
			    {
                    Wires[i].material = GreenWire;
			    }
                Item.On();
            }
            else if (!switched)
            {
                switchOff.Play();
                animator.Blend("Flip DOWN");
                for (var i = 0; i < Wires.Length; i++)
                {
                    Wires[i].material = RedWire;
                }
                Item.Off();
            }
        } else
        {
            switchOn.Play();
            animator.Blend("Flip UP");
			Items[ms_val].On();
            for (var i=0;i<WireGroups.Length;i++)
			{
                for (var j=0;j<WireGroups[i].childCount;j++)
                {
                    WireGroups[i].GetChild(j).GetComponent<Renderer>().material = RedWire;
                }
            }

            for (var k = 0; k < WireGroups[ms_val].childCount; k++)
            {
                WireGroups[ms_val].GetChild(k).GetComponent<Renderer>().material = GreenWire;
            }

            ms_val += 1;
            ms_val %= WireGroups.Length;
        }

        if(timed)
		{
            StartCoroutine(timedSwitch(time));
		}
    }

    IEnumerator timedSwitch(float time)
	{
        yield return new WaitForSeconds(time);
        switchOff.Play();
        animator.Blend("Flip DOWN");

        if(disableAfterTime)
		{
            if(multi)
			{
                for (var k = 0; k < WireGroups[ms_val].childCount; k++)
                {
                    WireGroups[ms_val].GetChild(k).GetComponent<Renderer>().material = RedWire;
                }
                Items[ms_val].Off();
            } else
			{
                for (var i = 0; i < Wires.Length; i++)
                {
                    Wires[i].material = RedWire;
                }
                Item.Off();
            }
		}
    }
}
