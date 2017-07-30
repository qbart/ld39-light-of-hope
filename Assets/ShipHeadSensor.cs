using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHeadSensor : MonoBehaviour
{
    public Ship ship;

    int obstacles = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ship") || other.CompareTag("Player"))
        {
            ++obstacles;

            if (obstacles == 1)
                ship.onObstacleVisible(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ship") || other.CompareTag("Player"))
        {
            --obstacles;

            if (obstacles == 0)
                ship.onObstacleVisible(false);
        }
    }
}
