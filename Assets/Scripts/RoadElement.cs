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
    Coin,
    Block1x1,
    Block2x1,
    Block1x2,
    Block2x2
}


