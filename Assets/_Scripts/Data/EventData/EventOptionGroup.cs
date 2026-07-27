using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName ="EventData/eventOptionGroup")]
public class EventOptionGroup : ScriptableObject
{
    public EventOption[] eventOptions;


    public bool isHaveReward;
    public OptionReward optionReward;
}

[System.Serializable]
public class OptionReward
{
    public int optionindex;
    public string rewardtype;
}


