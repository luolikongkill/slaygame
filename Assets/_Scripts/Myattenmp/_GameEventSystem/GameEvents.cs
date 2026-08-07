/// <summary>
/// 所有游戏事件的定义
/// 每个事件只包含完成这个事件需要的最小数据
/// </summary>

// 房间事件完成：任何类型的房间完成后都触发这个事件
public struct RoomCompletedEvent
{
    public RoomType RoomType; // 完成的房间类型
    public RoomNode CompletedNode; // 完成的节点
}

// 战斗胜利事件
public struct BattleVictoryEvent
{
    public RoomType RoomType;
    public int EnemyCount;
}

// 战斗失败事件
public struct BattleDefeatEvent
{
}

// 休息完成事件
public struct RestCompletedEvent
{
    public int HealAmount;
}

// 商店购买完成事件
public struct ShopPurchaseCompletedEvent
{
    public Card PurchasedCard;
}

// 事件完成事件
public struct EventCompletedEvent
{
    public string EventId;
}