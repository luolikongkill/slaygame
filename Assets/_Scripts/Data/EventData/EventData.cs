using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EventData", menuName ="EventData/eventGroup")]
public class EventData : ScriptableObject
{
    public Sprite backgroundimage;
    public string eventName;
    public string eventID;

    public EventOptionGroup[] optionGroups;

    public bool hasnextevent;
    public EventData nextevent;

       [TextArea(3,5)] 
       public string eventtext;

}
