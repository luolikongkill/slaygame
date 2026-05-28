using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text mana;
    public void UpdateManaText(int currentMana)    
    {
        mana.text = currentMana.ToString();
        Debug.Log("Mana UI Updated: " + currentMana);
    }
    void Awake()
    {
        UpdateManaText(3);
        Debug.Log("Mana UI Awake called");
    }


}
