using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Data/HeroAndCore")]    
public class Char_Dif_Data : ScriptableObject
{
    public string HeroName;
   [field: SerializeField] public HeroData herodata;

   public Sprite max_image;
   public Sprite regular_image;
   public Sprite Camp;

    public Sprite core;
    [field: SerializeField]public CoreType coreType;

}
