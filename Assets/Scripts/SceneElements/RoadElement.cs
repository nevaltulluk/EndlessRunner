using System;
using UnityEngine;


public class RoadElement : MonoBehaviour
{
    public int width;
    public int height;
    public RoadElementType type;
} 

public enum RoadElementType
{
    Block1x1 = 0,
    Block1x2 = 1,
    Block2x1 = 2,
    Block2x2 = 3,
    Coin = 4
}


