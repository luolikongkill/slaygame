using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectsUI : MonoBehaviour
{
   [SerializeField] private StatusEffectUI statusEffectUIPrefab;
   [SerializeField] private Sprite armorSprite, burnSprite;
   private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new Dictionary<StatusEffectType, StatusEffectUI>();
   public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
   {
    Debug.Log($"🔄 更新状态效果UI: {statusEffectType} 堆叠数: {stackCount}");
    if (!statusEffectUIs.ContainsKey(statusEffectType))
    {
    Debug.Log("===== statusEffectUIs 当前内容 =====");
    Debug.Log("Count = " + statusEffectUIs.Count);
    foreach (var kvp in statusEffectUIs)
    {
        Debug.Log("Key: " + kvp.Key + "   Value: " + kvp.Value);
    }
    Debug.Log("=====================================");
        Debug.LogWarning($"⚠️ StatusEffectsUI 中没有找到 {statusEffectType} 对应的图标，跳过UI更新");
    }
        else
        {
            Debug.Log("===== statusEffectUIs 当前内容 =====");
    Debug.Log("Count = " + statusEffectUIs.Count);
    foreach (var kvp in statusEffectUIs)
    {
        Debug.Log("Key: " + kvp.Key + "   Value: " + kvp.Value);
    }
    Debug.Log("=====================================");
        }
       if (stackCount == 0)
        {
            if (statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];
                statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
             }
                return; 
        }
       else
       {
            if(!statusEffectUIs.ContainsKey(statusEffectType))
                {
                    StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab, transform);
                    statusEffectUIs.Add(statusEffectType, statusEffectUI);
                 }
             Sprite sprite = GetSpriteByType(statusEffectType);
             statusEffectUIs[statusEffectType].Set(sprite, stackCount);
        }
    }

    private Sprite GetSpriteByType(StatusEffectType statusEffectType)
    {
        return statusEffectType switch

        {
           StatusEffectType.ARMOR => armorSprite,
           StatusEffectType.BURN => burnSprite,
           _ => null,
        };
    }


}