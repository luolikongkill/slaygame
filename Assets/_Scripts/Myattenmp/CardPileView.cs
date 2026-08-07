using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPileView : MonoBehaviour
{
    public GameObject panel;
    public Transform contain;
    public bool isopen;

    public Button drawbutton;
    public Button disbutton;
    public Button Exitbutton;

    public List<Card> drawcard = new();
    public List<Card> discard = new(); 

    private List<BagCardView> drawViews = new ();
    private List<BagCardView> disViews = new ();


    public Animator anim;
    public float animtime;

    public BagCardView BcardPrefab;


    void Start()
    {
        panel.SetActive(false);
        anim = GetComponent<Animator>();
        drawbutton.onClick.AddListener(opendrawview);
        disbutton.onClick.AddListener(opendisview);
        Exitbutton.onClick.AddListener(exitPanel);
    }

    public void Setup(List<Card> draw,List<Card> dis)
    {
        drawcard = draw;
        discard = dis;
    }

    private void opendrawview()
    {
        Clear();
        foreach(Card card in drawcard)
        {
            BagCardView view = Instantiate(BcardPrefab,contain);
            view.Setup(card);
            drawViews.Add(view);
        }
        panel.SetActive(true);
        isopen = true;
    }
    private void opendisview()
    {
        Clear();
        foreach(Card card in discard)
        {
            BagCardView view = Instantiate(BcardPrefab,contain);
            view.Setup(card);
            drawViews.Add(view);
        }
        panel.SetActive(true);
        isopen = true;
    }

    private void exitPanel()
    {
        panel.SetActive(false);
        isopen = false;
        Clear();
    }

    private void Clear()
    {
        foreach(BagCardView view in drawViews)
        {
            Destroy(view.gameObject);
        }
        foreach(BagCardView view in disViews)
        {
            Destroy(view.gameObject);
        }
        disViews.Clear();
        drawViews.Clear();
    }
}
