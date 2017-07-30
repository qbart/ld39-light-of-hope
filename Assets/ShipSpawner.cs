using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IslandID
{
    public Transform targetPoint;

    public Material material;

    public int id;
}

public class ShipSpawner : MonoBehaviour
{
    public Player player;

    public Transform[] spawningPoints;

    public Transform[] destinationPoints;

    public IslandID[] islandPoints;

    public GameObject shipPrefab;

 	void Start()
	{
        StartCoroutine(spawnShip());
    }
	
    IEnumerator spawnShip()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            Transform spawningPoint    = randomSpawningPoint();
            Transform destinationPoint = randomDestinationPoint();
            IslandID islandPoint       = randomIslandPoint();

            Ship.spawn(player, shipPrefab, spawningPoint, destinationPoint, islandPoint);

            yield return new WaitForSeconds(3.0f);
        }
    }

    Transform randomSpawningPoint()
    {
        int index = Random.Range(0, spawningPoints.Length);
        return spawningPoints[index];
    }

    Transform randomDestinationPoint()
    {
        int index = Random.Range(0, destinationPoints.Length);
        return destinationPoints[index];
    }

    IslandID randomIslandPoint()
    {
        int index = Random.Range(0, islandPoints.Length);
        return islandPoints[index];
    }
}
