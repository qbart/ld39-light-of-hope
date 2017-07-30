using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseBeam : MonoBehaviour
{
    public LightHouseSteering lighthouse;

    void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("ship"))
        {
            Ship ship = other.gameObject.GetComponent<Ship>();
            if (other is CapsuleCollider)
                ship.lightIntensity = lighthouse.intensity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ship"))
        {
            Ship ship = other.gameObject.GetComponent<Ship>();
            if (other is CapsuleCollider)
                ship.lightIntensity = 0;
        }
    }

}
