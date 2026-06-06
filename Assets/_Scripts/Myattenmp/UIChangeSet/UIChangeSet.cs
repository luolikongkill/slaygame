using UnityEngine;
using System.Collections.Generic;

// 封装每个界面的配置：界面本身 + 它的屏幕外位置
[System.Serializable]
public class UIConfig
{
    [Tooltip("要管理的UI面板")]
    public RectTransform panel;
    
    [Tooltip("这个面板不在显示时，放在屏幕外的哪个位置")]
    public Vector3 offScreenPos;
}

public class UIChangeSet : MonoBehaviour
{
    // 单例，方便全局调用（可选，但是非常方便）
    public static UIChangeSet Instance { get; private set; }

    [Header("所有要管理的UI界面")]
    [SerializeField] private List<UIConfig> allUIs = new List<UIConfig>();

    [Header("摄像机中心位置（显示位置）")]
    [SerializeField] private Vector3 centerPos = Vector3.zero;

    // 记录当前正在显示的界面
    private RectTransform currentShowUI;

    private void Awake()
    {
        // 单例初始化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化：游戏开始时，只显示第一个界面，其他都移到屏幕外
        InitAllUIPositions();
    }

    // 初始化所有界面的位置
    private void InitAllUIPositions()
    {
        if (allUIs.Count == 0)
        {
            Debug.LogWarning("没有配置任何UI界面！");
            return;
        }

        // 第一个界面放在中心显示
        currentShowUI = allUIs[0].panel;
        currentShowUI.localPosition = centerPos;

        // 其他界面都移到自己的屏幕外位置
        for (int i = 1; i < allUIs.Count; i++)
        {
            allUIs[i].panel.localPosition = allUIs[i].offScreenPos;
        }
    }

    /// <summary>
    /// 切换到指定的UI界面
    /// </summary>
    /// <param name="targetUI">要显示的UI面板</param>
    public void UIChange(RectTransform targetUI)
    {
        // 容错处理：如果要显示的就是当前界面，直接返回
        if (targetUI == currentShowUI) return;

        // 容错处理：如果传进来的UI不在配置列表里，报错
        UIConfig targetConfig = allUIs.Find(ui => ui.panel == targetUI);
        if (targetConfig == null)
        {
            Debug.LogError($"要切换的UI {targetUI.name} 不在配置列表里！");
            return;
        }

        // 1. 把当前显示的界面，移回它自己的屏幕外位置
        UIConfig currentConfig = allUIs.Find(ui => ui.panel == currentShowUI);
        if (currentConfig != null)
        {
            currentShowUI.localPosition = currentConfig.offScreenPos;
        }

        // 2. 把目标界面，移到摄像机中心
        targetUI.localPosition = centerPos;

        // 3. 更新当前显示的界面
        currentShowUI = targetUI;

        Debug.Log($"✅ 界面切换成功：{currentConfig?.panel.name} → {targetUI.name}");
    }

    /// <summary>
    /// 重载方法：通过索引切换界面（更方便按钮绑定）
    /// </summary>
    /// <param name="uiIndex">界面在列表里的索引（0是第一个）</param>
    public void UIChange(int uiIndex)
    {
        if (uiIndex < 0 || uiIndex >= allUIs.Count)
        {
            Debug.LogError($"UI索引 {uiIndex} 超出范围！");
            return;
        }

        UIChange(allUIs[uiIndex].panel);
    }
}