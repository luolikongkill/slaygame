using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardButton : MonoBehaviour
{
    public TMP_Text RewardType;
    public Sprite[] image;
    public Image curimage;
    
    public void init (string str)
    {
        if(str == "card")
        {
            RewardType.text = "RewardCard";
            curimage.sprite = image[0];
        }
        else if(str == "perk")
        {
            RewardType.text = "RewardPerk";
            curimage.sprite = image[1];
        }
        else
        {
            RewardType.text = "Gold";
            curimage.sprite = image[2];
        }
    }
    
}
