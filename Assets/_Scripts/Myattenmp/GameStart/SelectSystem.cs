using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSystem : MonoBehaviour
{
    private GameObject Mask;
    public Button[] charbuttons;
    public Button[] corebuttons;
    public Button EnterGamebutton;

    public Image max_;
    public Image regular_;
    public Image camp;
    public Sprite fIcon;
    public Sprite sIcon;
    public Sprite cIcon;
    public Sprite bIcon;
    public Sprite mIcon;
    public Sprite dIcon;


    public TMP_Text charName;

    [SerializeField]private Animator anim;

    [SerializeField]private Char_Dif_Data[] charactors;
    public Char_Dif_Data selectedchar;
    public CoreType selectedCore;
    private CoreType[] coretypes = new CoreType[]
    {
        CoreType.Balance,
        CoreType.Fight,
        CoreType.Shoting,
        CoreType.Motion,
        CoreType.Defense,
        CoreType.Coversing
    };

    [SerializeField] private MapView mapView;


    void Start()
    {
        EnterGamebutton.onClick.AddListener(OnStartGame);
        Setup();

        OnStartGame();

    } 


    public void Setup()
    {
        int index = 0;
        foreach(Char_Dif_Data data in charactors)
        {
            TMP_Text text = charbuttons[index].GetComponentInChildren<TMP_Text>();
            text.text = data.HeroName;
            charbuttons[index].onClick.AddListener(()=>OnSelected(data));

            index++;
        }
        for(;index < charbuttons.Length; index++)
            charbuttons[index].gameObject.SetActive(false);
        
        // for (int i = 0; i < corebuttons.Length; i++)
        // {
        //     // corebuttons[i].GetComponent<Image>().sprite = GetSpriteByType(coretypes[i]);
        //     corebuttons[i].onClick.AddListener(()=>SetCoreType(coretypes[i]));
        // }

    }

    private void OnStartGame()
    {
        // 1. 初始化玩家数据
        MatchSetupSystem.Instance.Setup(selectedchar.herodata);

        // 2. 生成地图
        MapManager.Instance.GenerateFullMap();
        if (MapManager.Instance != null)
        {
            Debug.Log("地图生成完成，准备进入地图场景");
        }
        else
        {
            Debug.LogError("错误：地图生成失败！");
        }
        // 场景一加载完，就自动显示地图
        if (mapView != null)
        {
            mapView.ShowMap();
        }

        UIChangeSet.Instance.UIChange(1);

        this.gameObject.SetActive(false);
        

    }

    private void OnSelected(Char_Dif_Data data)
    {
        max_.sprite = data.max_image;
        regular_.sprite = data.regular_image;
        camp.sprite = data.Camp;
        charName.text = data.HeroName;

        selectedchar = data;
    }

    private void SetCoreType(CoreType coreType)
    {
        selectedCore = coreType;
    }

    // private Sprite GetSpriteByType(CoreType coreType)
    // {
    //     return coreType switch

    //     {
    //        CoreType.Balance => bIcon,
    //        CoreType.Coversing => cIcon,
    //        CoreType.Defense => dIcon,
    //        CoreType.Fight => fIcon,
    //        CoreType.Motion => mIcon,
    //        CoreType.Shoting => sIcon, 
    //        _ => bIcon


    //     };
    // }

}
