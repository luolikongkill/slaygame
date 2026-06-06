// StartView.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject startUIPanel; // 拖你的StartUI面板
    [SerializeField] private GameObject mapUIPanel;   // 拖你的MapUI面板
    [SerializeField] private MapView mapView;         // 拖你的MapUI物体

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartGame);
        continueButton.onClick.AddListener(OnContinueGame);
        settingsButton.onClick.AddListener(OnOpenSettings);
    }

    private void OnStartGame()
    {
        // 1. 初始化玩家数据

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
            // startUIPanel.SetActive(false);
            // mapUIPanel.SetActive(true);
            //2026.6.05尝试使用UIChangeSet组件，展示屏蔽激活切换面板

            mapView.ShowMap();
        }

    }

    private void OnContinueGame()
    {
        // 后续实现存档加载
        Debug.Log("继续游戏（暂未实现）");
    }

    private void OnOpenSettings()
    {
        // 后续实现设置界面
        Debug.Log("打开设置（暂未实现）");
    }
}