using UnityEngine;

public class PortalSpawner : Interactable
{
	[SerializeField] private PlayerController Player;
	[Space]
	[SerializeField] private Vector3 Portal1Location;
	[SerializeField] private Quaternion Portal1Rotation;
	[SerializeField] private GameObject Portal1Wall;
	[Space]
	[SerializeField] private Vector3 Portal2Location;
	[SerializeField] private Quaternion Portal2Rotation;
	[SerializeField] private GameObject Portal2Wall;
	[Space]
	[SerializeField] private GameObject Portal1Prefab;
	[SerializeField] private GameObject Portal2Prefab;
	[Space]
	[Header("Multiportal")]
	public bool multiPortal = false;
	private int mp_val = 0;
	[SerializeField] private Vector3[] Portal1Locations;
	[SerializeField] private Quaternion[] Portal1Rotations;
	[SerializeField] private GameObject[] Portal1Walls;
	[Space]
	[SerializeField] private Vector3[] Portal2Locations;
	[SerializeField] private Quaternion[] Portal2Rotations;
	[SerializeField] private GameObject[] Portal2Walls;

	private void Awake()
	{
		mp_val = 0;	
	}

	public override void On()
	{
		if (!multiPortal)
		{
			GameObject newPortal1;
			GameObject newPortal2;
			newPortal1 = Instantiate(Portal1Prefab);
			newPortal2 = Instantiate(Portal2Prefab);
			newPortal1.transform.SetParent(transform);
			newPortal2.transform.SetParent(transform);
			newPortal1.GetComponentInChildren<Portal>().OtherPortal = newPortal2;
			newPortal2.GetComponentInChildren<Portal>().OtherPortal = newPortal1;
			newPortal1.GetComponentInChildren<Portal>().wallDisabler.wall = Portal1Wall;
			newPortal2.GetComponentInChildren<Portal>().wallDisabler.wall = Portal2Wall;
			newPortal1.transform.position = Portal1Location;
			newPortal1.transform.rotation = Portal1Rotation;
			newPortal2.transform.position = Portal2Location;
			newPortal2.transform.rotation = Portal2Rotation;
			newPortal1.GetComponentInChildren<Portal>().Player = Player.transform;
			newPortal2.GetComponentInChildren<Portal>().Player = Player.transform;
			Player.Portal1 = newPortal1;
			Player.Portal2 = newPortal2;
		} else
		{
			if(Player.Portal1 != null && Player.Portal2 != null)
			{
				Destroy(transform.GetChild(0).gameObject);
				Destroy(transform.GetChild(1).gameObject);
			}

			GameObject newPortal1;
			GameObject newPortal2;
			newPortal1 = Instantiate(Portal1Prefab);
			newPortal2 = Instantiate(Portal2Prefab);
			newPortal1.transform.SetParent(transform);
			newPortal2.transform.SetParent(transform);
			newPortal1.GetComponentInChildren<Portal>().OtherPortal = newPortal2;
			newPortal2.GetComponentInChildren<Portal>().OtherPortal = newPortal1;
			newPortal1.transform.position = Portal1Locations[mp_val];
			newPortal1.transform.rotation = Portal1Rotations[mp_val];
			newPortal2.transform.position = Portal2Locations[mp_val];
			newPortal2.transform.rotation = Portal2Rotations[mp_val];
			newPortal1.GetComponentInChildren<Portal>().wallDisabler.wall = Portal1Walls[mp_val];
			newPortal2.GetComponentInChildren<Portal>().wallDisabler.wall = Portal2Walls[mp_val];
			Player.Portal1 = newPortal1;
			Player.Portal2 = newPortal2;

			mp_val++;
			mp_val %= Portal1Locations.Length;
		}
	}
	public override void Off()
	{
		if (!multiPortal)
		{
			Destroy(transform.GetChild(0).gameObject);
			Destroy(transform.GetChild(1).gameObject);
		}
	}
}
