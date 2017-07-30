using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int red = 0;
    int blue = 0;
    int energy = 10;

    public Text labelRed;
    public Text labelBlue;

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
