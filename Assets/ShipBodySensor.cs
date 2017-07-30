﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBodySensor : MonoBehaviour
{
	public Ship ship;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("island"))
        {
            Destroy(ship.gameObject);
        }
    }
}