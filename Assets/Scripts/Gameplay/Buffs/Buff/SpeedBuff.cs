using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBuff", menuName = "PG/Buff/Speed Buff", order = 10)]
public class SpeedBuff : Buff
{
    [SerializeField] private float speedUp = 0f;

    private float originSpeed;

    public override void OnStart(BuffRuntimeInfo info)
    {
        if (info?.owner == null) return;

        originSpeed = info.owner.GetSpeed();
        info.owner.AddSpeedByValue(speedUp);
        Debug.Log("SpeedBuff OnStart");
    }

    protected override void OnTrigger(BuffRuntimeInfo info)
    {
    }

    public override void OnEnd(BuffRuntimeInfo info)
    {
        if (info?.owner == null) return;

        info.owner.SetSpeed(originSpeed);
        Debug.Log("SpeedBuff OnEnd");   
    }
}
