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

            destroyShip(ship.gameObject);
            player.damage();
        }
        else if (other.CompareTag("ship"))
        {
            playSound(sndCrash);

            destroyShip(ship.gameObject);
            destroyShip(other.gameObject);

            Ship otherShip = other.GetComponent<Ship>();
            player.dec(ship.id);
            player.dec(otherShip.id);
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

    void destroyShip(GameObject ship)
    {
        ship.SetActive(false);
        Destroy(ship, 2.0f);
    }

    void playSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1);
    }
}


