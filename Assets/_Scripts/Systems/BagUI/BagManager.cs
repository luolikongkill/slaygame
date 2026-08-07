using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : Singleton<BagManager>
{
    public GameObject BagPanel;
    public Button BagExitButton;

    public bool bagActivated;
    public Animator anim;
    public float animtime;

    [SerializeField]private BagCardView BCardPrefab;
    public Transform BagCardContent;

    private List<BagCardView> BCardViews = new ();
    public List<CardData> Deck = new();

    void Start()
    {
        anim = GetComponent<Animator>();
        BagExitButton.onClick.AddListener(ButtonBag);
        
    }
    public void Init(List<CardData> Deck)
    {
        Reset();
        this.Deck = Deck;
        foreach(var carddata in this.Deck)
        {
        BagCardView bagCardView = Instantiate(BCardPrefab,BagCardContent);
        Debug.Log("背包卡牌生成中");
        Card card1 = new (carddata);
        bagCardView.Setup(card1);   
        BCardViews.Add(bagCardView);
        }
        Debug.Log("卡牌生成完成");

    }
    public void Reset()
    {
        foreach(var view in BCardViews)
        {
            Destroy(view.gameObject);
        }
        BCardViews.Clear();
    }

    void Update()
        {
            if(Input.GetButtonDown("CardBag")&&!bagActivated)
            {
                // Time.timeScale=0;
                OpenBag();  
            }
            else if(Input.GetButtonDown("CardBag")&&bagActivated)
            {
                // Time.timeScale=1;
                CloseBag(); 
            }
        }
    public void BagAdd(CardData cdata)
    {
        BagCardView bagCardView = Instantiate(BCardPrefab,BagCardContent);
        Card card1 = new (cdata);
        bagCardView.Setup(card1);   
        BCardViews.Add(bagCardView);
    }
    public void BagSub(CardData cdata)
    {
        if(Deck.Contains(cdata))
        {
            Deck.Remove(cdata);
            Init(Deck);
        }
    }

    public void OpenBag()
    {
        anim.Play("OpenBag");
        BagPanel.SetActive(true);
        bagActivated = true; 
    }
    public void CloseBag()
    {
        anim.Play("CloseBag");
 
    }
    public void Closeanim()
    {
        BagPanel.SetActive(false);
        bagActivated = false;
    }
    public void ButtonBag()
    {
        if(!bagActivated)
            OpenBag();
        else CloseBag();
    }


}
