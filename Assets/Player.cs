using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int red = 0;
    int blue = 0;
    int energy = 10;
    float time = 0;

    public Text labelRed;
    public Text labelBlue;
    public Text labelTime;
    public Text labelGameOver;
    public Slider sliderHealth;

    public void Update()
    {
        if (energy > 0)
        {
            time += Time.deltaTime;
            int seconds = (int)time % 60;
            int minutes = (int)time / 60;
            labelTime.text = string.Format("{0}:{1}", minutes, seconds.ToString("D2"));
        }
    }

    public void damage()
    {
        if (energy > 0)
        {
            --energy;
            sliderHealth.value = energy;

            if (energy == 0)
            {
                labelGameOver.gameObject.SetActive(true);
            }
        }
    }

    public void inc(int color)
    {
        switch (color)
        {
            case 1:
                red += 1;
                labelRed.text = red.ToString();
                break;

            case 2:
                blue += 1;
                labelBlue.text = blue.ToString();
                break;
        }
    }

    public void dec(int color)
    {
        switch (color)
        {
            case 1:
                red -= 1;
                labelRed.text = red.ToString();
                break;

            case 2:
                blue -= 1;
                labelBlue.text = blue.ToString();
                break;
        }
    }
}
