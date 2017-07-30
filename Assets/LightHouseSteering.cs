using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseSteering : MonoBehaviour
{
    public GameObject lightSource;

    public Light lightbeam;

    Player player;

    // how fast we can move
    float rotationSpeed = 90;

    // how bright light will be
    float lightMinIntensity = 0;
    float lightMaxIntentsity = 2;
    public float intensity { get; set; }

    float allowedIdleTimeForKeyStroke = 0.4f;
    float prevKeyStrokeTime;
    int expectedKey = 1;
    float[] keyStrokingSpeed; 

    void Start()
    {
        player = GetComponent<Player>();
        keyStrokingSpeed = new float[100];
        resetKeyStrokingSpeed();
	}
	
	void Update()
    {
        if (!player.isAlive())
        {
            lightbeam.intensity = 0;
            return;
        }

        // rotate light
        int direction = getMovementDirection();
        if (direction != 0)
            rotateLightBeam(direction);

        // keep light intensity
        intensity = calculateLightIntensity();
        intensity = 1;
        float lightIntensity = Mathf.Lerp(lightMinIntensity, lightMaxIntentsity, intensity);
        lightbeam.intensity = lightIntensity;

        //Debug.Log(string.Format("Intensity: {0}", lightIntensity));
    }

    void rotateLightBeam(int direction)
    {
        Quaternion rotation = lightSource.transform.rotation;
        float angle = rotationSpeed * direction * Time.deltaTime;
        lightSource.transform.Rotate(new Vector3(0, angle, 0));
    }

    float calculateLightIntensity()
    {
        int pressedKey = 0;
        if (Input.GetKeyUp(KeyCode.J))
            pressedKey = 1;
        else if (Input.GetKeyDown(KeyCode.K))
            pressedKey = 2;

        float currentTime = Time.timeSinceLevelLoad;
        float diffTime = currentTime - prevKeyStrokeTime;

        if (pressedKey == expectedKey)
        {
            addKeystrokeSpeed(diffTime);
            prevKeyStrokeTime = currentTime;

            toggleExpectedKey();
        }
        else if (diffTime > allowedIdleTimeForKeyStroke)
        {
            addKeystrokeSpeed(1);
            prevKeyStrokeTime = currentTime;
        }

        float avg = keyStrokingAverage();
        float intensity = Mathf.Clamp01(1 - avg);
        //Debug.Log(string.Format("Average: {0}, Intensity: {1}", avg, intensity));

        return intensity;
    }

    int getMovementDirection()
    {
        if (Input.GetKey(KeyCode.D))
            return -1;

        if (Input.GetKey(KeyCode.F))
            return 1;

        return 0;
    }

    void toggleExpectedKey()
    {
        if (expectedKey == 1)
            expectedKey = 2;
        else if (expectedKey == 2)
            expectedKey = 1;
    }

    void resetKeyStrokingSpeed()
    {
        prevKeyStrokeTime = Time.timeSinceLevelLoad;

        for (int i = 0; i < keyStrokingSpeed.Length; ++i)
            keyStrokingSpeed[i] = 1;
    }

    float keyStrokingAverage()
    {
        float sum = 0;

        for (int i = 0; i < keyStrokingSpeed.Length; ++i)
            sum += keyStrokingSpeed[i];

        return sum / keyStrokingSpeed.Length;
    }

    void addKeystrokeSpeed(float time)
    {
        for (int i = 1; i < keyStrokingSpeed.Length; ++i)
            keyStrokingSpeed[i] = keyStrokingSpeed[i - 1];

        keyStrokingSpeed[0] = time;
    }

}
