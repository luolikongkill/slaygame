using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private GameObject RewardView;
    public  void OnClick()
    {

        RewardView.SetActive(false);
    }
    
}
