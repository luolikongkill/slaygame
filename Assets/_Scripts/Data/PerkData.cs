using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializeReferenceEditor;

[CreateAssetMenu(menuName = "Data/Perk")]
public class PerkData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeReference, SR] public PerkCondition Condition { get; private set; } 
    [field: SerializeField] public  List<AutoTargetEffect> AutoTargetEffects {get ; private set;} 
    [field: SerializeField] public bool UseAutoTarget { get; private set; } = true;
    [field: SerializeField] public bool UseActionCasterAsTarget { get; private set; } = false;
}
