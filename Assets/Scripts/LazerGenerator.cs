using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGenerator : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        Physics.Raycast(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hitInfo.point);

        if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("LazerCatcher"))
		{
            Debug.Log("Lazer Caught! - LazerGenerator");
		}
    }
}
