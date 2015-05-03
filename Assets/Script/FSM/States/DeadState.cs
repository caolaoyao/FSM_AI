using UnityEngine;
public class DeadState : FSMState
{
    public DeadState()
    {
        stateId = FSMStateID.Dead;
    }

    public override void Reason(Transform player, Transform npc)
    {
    }

    public override void Act(UnityEngine.Transform player, UnityEngine.Transform npc)
    {
        Debug.LogError("目标死亡");
    }
}