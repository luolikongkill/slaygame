using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

// ==================== 1. 特性定义 ====================
/// <summary>
/// 用于为枚举值提供描述文本的特性
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class DescriptionAttribute : Attribute
{
    public string Text { get; }
    public DescriptionAttribute(string text) => Text = text;
}
public enum StatusEffectType 
{
  [Description("Counteract the physical damage received")]
  ARMOR,
  [Description("Take fire damage based on the number of stacked layers, with the fire ignoring shields and causing a -1 deduction each round")]
  BURN,
  [Description("The damage received is increased by 50%")]
  VULNERABLE,
  [Description("The AttackPower is discrease by 10%")]
  WEAKNESS
}

// ==================== 3. 扩展方法（获取描述） ====================
public static class EnumExtensions
{
    // 可选：缓存反射结果，提升多次调用的性能
    // private static readonly Dictionary<Enum, string> _cache = new Dictionary<Enum, string>();

    /// <summary>
    /// 获取枚举值的描述文本，若没有特性则返回枚举名本身
    /// </summary>
    public static string GetDescription(this Enum enumValue)
    {
        // // 先查缓存
        // if (_cache.TryGetValue(enumValue, out string cached))
        //     return cached;

        // 反射获取字段上的 DescriptionAttribute
        var field = enumValue.GetType().GetField(enumValue.ToString());
        var attr = field?.GetCustomAttribute<DescriptionAttribute>();
        string result = attr?.Text ?? enumValue.ToString();

        // // 存入缓存（注意：枚举值作为键，确保唯一性）
        // _cache[enumValue] = result;
        return result;
    }
}

public class VulnerableChane : DamageChane
{
  public float multipliter = .5f;

  public int DamageChange(int damage)
  {
      return (int)(damage*(1f + multipliter));
  }
}

public class WeaknessChane : DamageChane
{
  public float multipliter = .1f;

  public int DamageChange(int damage)
  {
      return (int)(damage*(1f - multipliter));
  }
}
