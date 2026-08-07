using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusEffectUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text stackCountText;
    private StatusEffectType type;
    [SerializeField]private TooltipTrigger tooltipTrigger;
    public void Set(Sprite sprite, StatusEffectType effecttype,int stackCount)
    {
        image.sprite = sprite;
        stackCountText.text = stackCount.ToString();
        type = effecttype;

        tooltipTrigger.Setup(type.GetDescription(),type.ToString()+stackCount.ToString());
    }

}
