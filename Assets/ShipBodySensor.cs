using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBodySensor : MonoBehaviour
{
	public Ship ship;
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("island"))
        {
            Island island = other.GetComponent<Island>();
            if (island.id == ship.id)
                player.inc(ship.id);
            else
                player.dec(island.id);

            Destroy(ship.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            Destroy(ship.gameObject);
            player.dec(ship.id);
        }
    }
}


