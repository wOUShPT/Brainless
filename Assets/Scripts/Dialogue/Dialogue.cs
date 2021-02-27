using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To appear in the inspector
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
}
