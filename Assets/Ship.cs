using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject cloth;
    
    Rigidbody rb;

    Vector3 destination;
    Vector3 island;

    float speed = 5;
    bool targetAcquired = false;

    float navigationSkill = 0;
    float learningTime = 0;

    [HideInInspector]
    public float lightIntensity { get; set; }

    [HideInInspector]
    public float skill { get; set; }

    public static void spawn(GameObject prefab, Transform spawningPoint, Transform destinationPoint, IslandID islandID)
    {
        Vector3 direction = destinationPoint.position - spawningPoint.position;
        direction.Normalize();
        GameObject gameObject = Instantiate(prefab, spawningPoint.position, Quaternion.FromToRotation(Vector3.forward, direction), null);
        Ship ship = gameObject.GetComponent<Ship>();
        Renderer renderer = ship.cloth.GetComponent<Renderer>();
        renderer.material = islandID.material;
        ship.setTarget(destinationPoint.position, islandID.targetPoint.position);
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
        learningTime = 0;
    }

    void Update()
    {
        skill = calculateEffectiveSkill();
        learningTime += Time.deltaTime * skill * 0.05f;
        learningTime = Mathf.Clamp01(learningTime);
    }

    void FixedUpdate()
	{
        if (!targetAcquired)
            return;

        if (skill > 0)
        {
            Vector3 direction = island - transform.position;
            direction.Normalize();

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, direction);
            Quaternion applyRotation = Quaternion.Lerp(transform.rotation, targetRotation, learningTime);
            rb.MoveRotation(applyRotation);
        }
        else
        {
            learningTime = 0;
        }

        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    float calculateEffectiveSkill()
    {
        return navigationSkill + (1 - navigationSkill) * lightIntensity;
    }

}
