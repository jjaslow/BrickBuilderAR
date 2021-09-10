using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public Size size;
    public Color color;
    public int leftPos;
    public int rightPos;
}


