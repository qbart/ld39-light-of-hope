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
    const float SPAWN_FIRST_AFTER = 1.0f;
    const float SPAWN_EVERY = 2;

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
        yield return new WaitForSeconds(SPAWN_FIRST_AFTER);

        while (true)
        {
            Transform spawningPoint    = randomSpawningPoint();
            Transform destinationPoint = randomDestinationPoint();
            IslandID islandPoint       = randomIslandPoint();

            Ship.spawn(player, shipPrefab, spawningPoint, destinationPoint, islandPoint);

            yield return new WaitForSeconds(SPAWN_EVERY);
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
