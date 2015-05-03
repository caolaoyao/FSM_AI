using UnityEngine;
public class PatrolState : FSMState
{
    public PatrolState(Transform[] wp)
    {
        waypoints = wp;
        stateId = FSMStateID.Patrolling;
        curRotSpeed = 6.0f;
        curSpeed = 80.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, player.position) <= chaseDistance)
        {
            Debug.LogError("看到目标");
            npc.GetComponent<AiController>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, desPos) <= arriveDistance)
        {
            FindNextPoint();
        }

        Quaternion targetRotation = Quaternion.LookRotation(desPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        CharacterController controller = npc.GetComponent<CharacterController>();
        controller.SimpleMove(npc.transform.forward * Time.deltaTime * curSpeed);
    }
}