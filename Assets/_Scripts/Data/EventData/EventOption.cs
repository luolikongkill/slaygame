using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "EventData", menuName ="EventData/eventOption")]
public class EventOption : ScriptableObject
{
    [TextArea(3,5)] public string text;
    public bool havenext;


}
