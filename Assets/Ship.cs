using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider frontSensor;
    CapsuleCollider bodySensor;

    Vector3 destination;
    Vector3 island;

    float speed = 5;
    bool targetAcquired = false;

    float navigationSkill = 0;

    [HideInInspector]
    public float lightIntensity { get; set; }

    public static void spawn(GameObject prefab, Transform spawningPoint, Transform destinationPoint, Transform validDestinationPoint)
    {
        Vector3 direction = destinationPoint.position - spawningPoint.position;
        direction.Normalize();
        GameObject gameObject = Instantiate(prefab, spawningPoint.position, Quaternion.FromToRotation(Vector3.forward, direction), null);
        Ship ship = gameObject.GetComponent<Ship>();
        ship.setTarget(destinationPoint.position, validDestinationPoint.position);
    }

    void setTarget(Vector3 destination, Vector3 island)
    {
        this.destination = destination;
        this.island = island;
        targetAcquired = true;
        lightIntensity = 0;
    }

	void Start()
	{
        rb = GetComponent<Rigidbody>();
        frontSensor = GetComponent<BoxCollider>();
        bodySensor = GetComponent<CapsuleCollider>();
    }
	
	void FixedUpdate()
	{
        if (!targetAcquired)
            return;

        float skill = calculateEffectiveSkill();

        Vector3 direction = destination - transform.position;
        direction.Normalize();

        //Quaternion rotation = Quaternion.FromToRotation(transform.position.normalized, direction);
        //rb.MoveRotation(rotation);
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);

    }

    float calculateEffectiveSkill()
    {
        return navigationSkill + (1 - navigationSkill) * lightIntensity;
    }

}
