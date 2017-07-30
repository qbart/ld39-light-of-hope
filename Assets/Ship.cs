using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ship : MonoBehaviour
{
    public GameObject cloth;

    public Player player;

    Rigidbody rb;

    Vector3 targetPosition;

    [HideInInspector]
    public int id { get; set; }
    float speed = 5;
    public bool targetAcquired = false;

    //float navigationSkill = 0;
    float learningTime = 0;

    [HideInInspector]
    public float lightIntensity { get; set; }

    [HideInInspector]
    public float skill { get; set; }

    bool reactToObstacle = false;
    float reactionProgress = 0;
    Quaternion targetRotationOnReaction;

    public static void spawn(Player player, GameObject prefab, Transform spawningPoint, Transform destinationPoint, IslandID islandID)
    {
        Vector3 direction = destinationPoint.position - spawningPoint.position;
        direction.Normalize();
        GameObject gameObject = Instantiate(prefab, spawningPoint.position, Quaternion.FromToRotation(Vector3.forward, direction), null);
        Ship ship = gameObject.GetComponent<Ship>();
        ship.player = player;
        ShipBodySensor shipbody = gameObject.GetComponentInChildren<ShipBodySensor>();
        shipbody.player = player;
        Renderer renderer = ship.cloth.GetComponent<Renderer>();
        renderer.material = islandID.material;
        ship.setTarget(islandID);
    }

    public void onObstacleVisible(bool visible)
    {
        reactToObstacle = visible;
        reactionProgress = 0;

        targetRotationOnReaction = transform.rotation * Quaternion.Euler(0, -60 * skill, 0);
    }

    void setTarget(IslandID islandID)
    {
        targetPosition = islandID.targetPoint.position;
        id = islandID.id;
        targetAcquired = true;
        lightIntensity = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        learningTime = 0;
        //navigationSkill = (Random.Range(0, 10) + 1) * 0.5f;
    }

    void Update()
    {
        skill = calculateEffectiveSkill();
        learningTime += Time.deltaTime * skill * 0.05f;
        learningTime = Mathf.Clamp01(learningTime);

        if (reactToObstacle)
        {
            reactionProgress += Time.deltaTime;
            reactionProgress = Mathf.Clamp01(reactionProgress);
        }
    }

    void FixedUpdate()
    {
        if (!targetAcquired || !player.isAlive())
            return;

        if (reactToObstacle)
        {
            Quaternion applyRotation = Quaternion.Lerp(transform.rotation, targetRotationOnReaction, reactionProgress);
            rb.MoveRotation(applyRotation);
        }
        else if (skill > 0)
        {
            Vector3 direction = targetPosition - transform.position;
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
        return lightIntensity;
        //return navigationSkill + (1 - navigationSkill) * lightIntensity
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, "" + skill);
    }
#endif
}
