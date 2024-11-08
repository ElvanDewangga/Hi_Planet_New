using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SRWSO : ScriptableObject
{
    public int iteration = 10, walkLength = 10;
    public bool StartRandomEachIteration = true;
}
