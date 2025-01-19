using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    void Awake()
    {
        Update();
    }
    void Update()
    {
        float rY = Mathf.SmoothStep(-155, -115, Mathf.PingPong(Time.time * .05f, 1));
        transform.rotation = Quaternion.Euler(15, rY, 0);
    }
}
