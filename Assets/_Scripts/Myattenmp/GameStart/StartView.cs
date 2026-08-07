// StartView.cs
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private GameObject startUIPanel; // 拖你的StartUI面板
    [SerializeField] private GameObject mapUIPanel;
    
    
       // 拖你的MapUI面板
    [SerializeField] private MapView mapView;  
    [SerializeField] private SelectSystem selectsystem;       // 拖你的MapUI物体


    [SerializeField] private GameObject CharSelectPanel;

    private void Awake()
    {
        startButton.onClick.AddListener(ToSelectCharacter);
        continueButton.onClick.AddListener(OnContinueGame);
        settingsButton.onClick.AddListener(OnOpenSettings);
        
    }

    void Start()
    {
        // CharSelectPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetButtonDown("test"))
        {
            MatchSetupSystem.Instance.ReGame();
            EnemyPoolSystem.Instance.Setup(RoomType.Boss);
            UIChangeSet.Instance.UIChange(2);
        }
    }



    private void ToSelectCharacter()
    {
        CharSelectPanel.SetActive(true);
    }
    

    private void OnContinueGame()
    {
        if(MatchSetupSystem.Instance.isGameStarted)
        return;
        UIChangeSet.Instance.UIChange(1);
        // 后续实现存档加载
        Debug.Log("继续游戏（暂未实现）");
    }

    private void OnOpenSettings()
    {
        // 后续实现设置界面
        Debug.Log("打开设置（暂未实现）");
    }
}