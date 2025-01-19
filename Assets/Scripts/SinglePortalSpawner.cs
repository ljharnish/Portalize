using UnityEngine;

public class SinglePortalSpawner : Interactable
{
	[SerializeField] private PlayerController Player;
	[Space]
	[SerializeField] private Vector3 PortalLocation;
	[SerializeField] private Quaternion PortalRotation;
	[SerializeField] private GameObject PortalWall;
	[Space]
	[SerializeField] private GameObject PortalPrefab;

	public override void On()
	{
		GameObject newPortal;
		newPortal = Instantiate(PortalPrefab);
		newPortal.transform.SetParent(transform);
		if(PortalPrefab.name == "Green Portal") newPortal.GetComponentInChildren<Portal>().OtherPortal = GameObject.Find("Red Portal(Clone)");
		if(PortalPrefab.name == "Red Portal") newPortal.GetComponentInChildren<Portal>().OtherPortal = GameObject.Find("Green Portal(Clone)");
		newPortal.GetComponentInChildren<Portal>().wallDisabler.wall = PortalWall;
		newPortal.transform.position = PortalLocation;
		newPortal.transform.rotation = PortalRotation;
		newPortal.GetComponentInChildren<Portal>().Player = Player.transform;
		Player.Portal2 = newPortal;
		
	}
	public override void Off()
	{
		Destroy(transform.GetChild(0).gameObject);
	}
}
