using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBodySensor : MonoBehaviour
{
	public Ship ship;
    public Player player;

    public AudioClip sndCrash;
    public AudioClip sndCorrect;
    public AudioClip sndWrong;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("island"))
        {
            Island island = other.GetComponent<Island>();
            if (island.id == ship.id)
            {
                playSound(sndCorrect);
                player.inc(ship.id);
            }
            else
            {
                playSound(sndWrong);
                player.dec(island.id);
            }

            destroyShip(ship.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            playSound(sndCrash);

            player.damage();
            destroyShip(ship.gameObject);
        }
        else if (other.CompareTag("ship"))
        {
            playSound(sndCrash);

            Ship otherShip = other.GetComponentInParent<Ship>();
            player.dec(ship.id);
            player.dec(otherShip.id);

            destroyShip(ship.gameObject);
            destroyShip(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            player.dec(ship.id);
            destroyShip(ship.gameObject);
        }
    }

    void destroyShip(GameObject ship)
    {
        Destroy(ship);
    }

    void playSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1);
    }
}


