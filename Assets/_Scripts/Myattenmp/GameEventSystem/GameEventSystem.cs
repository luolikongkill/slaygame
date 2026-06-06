using System;
using System.Collections.Generic;

/// <summary>
/// 全局事件系统：所有系统之间的通讯都通过这里
/// 完全解耦，发送方和接收方不需要知道对方的存在
/// </summary>
public static class GameEventSystem
{
    // 存储所有事件的字典
    private static readonly Dictionary<Type, Delegate> eventDictionary = new Dictionary<Type, Delegate>();

    /// <summary>
    /// 订阅一个事件
    /// </summary>
    /// <typeparam name="T">事件类型</typeparam>
    /// <param name="listener">事件处理方法</param>
    public static void Subscribe<T>(Action<T> listener)
    {
        Type type = typeof(T);
        if (!eventDictionary.ContainsKey(type))
        {
            eventDictionary.Add(type, null);
        }
        eventDictionary[type] = Delegate.Combine(eventDictionary[type], listener);
    }

    /// <summary>
    /// 取消订阅一个事件
    /// </summary>
    /// <typeparam name="T">事件类型</typeparam>
    /// <param name="listener">事件处理方法</param>
    public static void Unsubscribe<T>(Action<T> listener)
    {
        Type type = typeof(T);
        if (eventDictionary.TryGetValue(type, out Delegate del))
        {
            eventDictionary[type] = Delegate.Remove(del, listener);
        }
    }

    /// <summary>
    /// 触发一个事件
    /// </summary>
    /// <typeparam name="T">事件类型</typeparam>
    /// <param name="eventData">事件数据</param>
    public static void Trigger<T>(T eventData)
    {
        Type type = typeof(T);
        if (eventDictionary.TryGetValue(type, out Delegate del))
        {
            if (del is Action<T> callback)
            {
                callback.Invoke(eventData);
            }
        }
    }

    /// <summary>
    /// 清空所有事件（游戏重启时调用）
    /// </summary>
    public static void ClearAll()
    {
        eventDictionary.Clear();
    }
}