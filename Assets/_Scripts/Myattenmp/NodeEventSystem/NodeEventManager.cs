using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeEventManager : MonoBehaviour
{
    public EventData curdata;
    public int optionindex;

    public TMP_Text eventtext;
    public Image backgroundimage;
    public Image backgroundimage_;
    public string eventName;



    public Button[] Options;
    public CanvasGroup canvasGroup;

    public void Start()
    {
        InitEvent(curdata);
        CloseUI();
    }
    public void Setup (EventData data)
    {
        optionindex = 0;
        this.curdata = data;
        InitEvent(data);
        
    }

    private void OpenUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    private void CloseUI()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void InitEvent(EventData eventData)
    {
        backgroundimage.sprite = eventData.backgroundimage;
        backgroundimage_.sprite = eventData.backgroundimage;
        eventName = eventData.eventName;
        eventtext.text = eventData.eventtext;

        curdata = eventData;
        optionindex = 0;

        for(int i = 0 ; i< Options.Length;i++)
        {
            Options[i].gameObject.SetActive(false);
        }
        
        SetEventOption();
        OpenUI();
    }


    private void SetEventOption()
    {
        for (int i = 0; i < curdata.optionGroups.Length; i++)
        {
            Debug.Log("EventLength"+curdata.optionGroups.Length);
            SetupOption(i,curdata.optionGroups[i]);
        }
    }

    private void SetupOption(int index,EventOptionGroup group)
    {
        if(index == 0)
        for(int i =0 ; i< Options.Length;i++)
            {
                Options[i].gameObject.SetActive(false);
            }
        Options[index].onClick.RemoveAllListeners();
        Options[index].GetComponentInChildren<TMP_Text>().text = group.eventOptions[optionindex].text;
        Options[index].gameObject.SetActive(true);

        Options[index].onClick.AddListener(() =>OptionAdvance(group));
    }

    private void OptionAdvance(EventOptionGroup curgroup)
    {
        if(curgroup.eventOptions.Length-1 == optionindex)
        {
            if(curdata.hasnextevent!=false)
                InitEvent(curdata.nextevent);
            else    
            EndOptionSet();
        }    
        else
        {
            optionindex++;
            SetupOption(0,curgroup);
        }
    }
    

    private void EndOptionSet()
    {
        for(int i = 1 ; i< Options.Length;i++)
        {
            Options[i].gameObject.SetActive(false);
        }
        Options[0].GetComponentInChildren<TMP_Text>().text = "END ";
        Debug.Log("ENDSet"+optionindex);
        Options[0].onClick.AddListener(()=>
        {
            MapView.Instance.UpdateAccessibleNodes(MapView.Instance.currentnodeUI);
            UIChangeSet.Instance.UIChange(1);
            CloseUI();
        } );
    }



}
