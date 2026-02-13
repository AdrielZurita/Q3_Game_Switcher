using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectA", menuName = "ObjectPlsHelp")]
public class ObjectPlsHelp : ScriptableObject
{
    public bool returning = false;
    public bool havedisc = true;
    public bool inGravBox = false;
    public bool isPositive = true;
}
