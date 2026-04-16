using System;
using QFramework.PG;

/// <summary>
/// 全局变量
/// </summary>



public class Global
{
    public static Player player;
    public static int HP = 100;
    public static int MaxHP = 100;
    public static Action OnHPChange;

    public static void Restart() {
        HP = MaxHP;
        OnHPChange?.Invoke();
    }

}