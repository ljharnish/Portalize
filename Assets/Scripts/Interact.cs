using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private LayerMask InteractMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float dragSpeed = 12f;    
    [Space]
    [SerializeField] private float PickupRange;
    [SerializeField] private float InteractRange;
    [SerializeField] private Rigidbody CurrentObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
		{
            if(CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject.constraints = RigidbodyConstraints.None;
                CurrentObject = null;
                return;
            }

            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask))
			{
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
                CurrentObject.constraints = RigidbodyConstraints.FreezeRotation;
			}

            if (Physics.Raycast(CameraRay, out RaycastHit InteractInfo, InteractRange, InteractMask))
            {
                InteractInfo.collider.gameObject.GetComponent<Switch>().SwitchA();
            }
        }
    }

	void FixedUpdate()
	{
		if(CurrentObject)
		{
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            if(DistanceToPoint > 2f)
			{
                CurrentObject.useGravity = true;
                CurrentObject.constraints = RigidbodyConstraints.None;
                CurrentObject = null;
                return;
            }

            CurrentObject.velocity = DirectionToPoint * dragSpeed * DistanceToPoint;
		}
	}
}
