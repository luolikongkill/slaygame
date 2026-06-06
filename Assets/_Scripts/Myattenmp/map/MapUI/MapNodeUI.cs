// MapNodeUI.cs
using UnityEngine;
using UnityEngine.UI;


public class MapNodeUI : MonoBehaviour
{
    [SerializeField] public Image nodeImage;//图片
    [SerializeField] public Image visitedindicator;//可访问遮罩遮罩

    [SerializeField] public Image accessibleindicator;//已选路线指示器
    [SerializeField] public Button Nodebutton;//进入按钮

    public RoomNode nodeData;// 当前节点数据
    private System.Action<MapNodeUI> onNodeClicked;// 点击回调

    // 不同房间类型的图标（在Inspector赋值）
    [SerializeField] public Sprite normalSprite;
    // [SerializeField] private Sprite eliteSprite;
    // [SerializeField] private Sprite bossSprite;
    // [SerializeField] private Sprite restSprite;
    // [SerializeField] private Sprite eventSprite;

    public void NodeInit(RoomNode data, System.Action<MapNodeUI> callback)
    {
        nodeData = data;
        onNodeClicked = callback;

        // 设置图标
        // nodeImage.sprite = data.roomType switch
        // {
        //     RoomType.Normal => normalSprite,
        //     // RoomType.Elite => eliteSprite,
        //     // RoomType.Boss => bossSprite,
        //     // RoomType.Rest => restSprite,
        //     // RoomType.Event => eventSprite,
        //     _ => normalSprite
        // };

        // 设置状态
        visitedindicator.enabled = data.isVisited;
        Nodebutton.interactable = data.isAccessible && !data.isVisited;
        accessibleindicator.enabled = false; // 初始时不显示已选路线指示器
        
        // 绑定点击
        Nodebutton.onClick.RemoveAllListeners();

        Nodebutton.onClick.AddListener(OnClick);
        //Debug.Log($"节点UI设置完成：{data.x}, {data.y}，房间类型：{data.roomType}");
    }

    private void OnClick()
    {
        onNodeClicked?.Invoke(this);
        Debug.Log($"节点UI被点击：{nodeData.x}, {nodeData.y}，房间类型：{nodeData.roomType}");
    }

}