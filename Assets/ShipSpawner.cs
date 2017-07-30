using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipSpawner : MonoBehaviour
{
    public Transform[] spawningPoints;

    public Transform[] destinationPoints;

    public Transform[] islandPoints;

    public GameObject shipPrefab;

	void Start()
	{
        StartCoroutine(spawnShip());
    }
	
	void Update()
	{

	}

    IEnumerator spawnShip()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            Transform spawningPoint    = randomSpawningPoint();
            Transform destinationPoint = randomDestinationPoint();
            Transform islandPoint      = randomIslandPoint();

            Ship.spawn(shipPrefab, spawningPoint, destinationPoint, islandPoint);

            yield return new WaitForSeconds(3.0f);
        }
    }

    Transform randomSpawningPoint()
    {
        int index = Random.Range(0, spawningPoints.Length - 1);
        return spawningPoints[index];
    }

    Transform randomDestinationPoint()
    {
        int index = Random.Range(0, destinationPoints.Length - 1);
        return destinationPoints[index];
    }

    Transform randomIslandPoint()
    {
        int index = Random.Range(0, islandPoints.Length - 1);
        return islandPoints[index];
    }
}
