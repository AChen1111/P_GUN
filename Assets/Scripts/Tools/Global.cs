using System;
using QFramework.PG;
public class Global
{
    public static Player player;
    public static int HP = 3;
    public static Action OnHPChange;
    public static void Restart() {
        HP = 3;
    }
}