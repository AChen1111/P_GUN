using System;
using QFramework.PG;

/// <summary>
/// 全局变量
/// </summary>



public class Global
{
    public static Player player;
    public static int HP = 3;
    public static int MaxHP = 3;
    public static Action OnHPChange;

    public static void Restart() {
        HP = MaxHP;
        OnHPChange?.Invoke();
    }

}