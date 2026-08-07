using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text overmana;
    public void UpdateManaText(int currentMana,int OverMana)    
    {
        mana.text = currentMana.ToString();
        overmana.text = OverMana.ToString();
        Debug.Log("Mana UI Updated: " + currentMana + "\n OverMana Updated" + OverMana);
    }



}
