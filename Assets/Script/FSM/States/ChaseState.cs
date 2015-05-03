using UnityEngine;
public class ChaseState : FSMState
{
    public ChaseState(Transform[] wp)
    {
        waypoints = wp;
        stateId = FSMStateID.Chasing;
        curRotSpeed = 6.0f;
        curSpeed = 160.0f;
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
        desPos = player.position;
        float dist = Vector3.Distance(npc.position, desPos);
        if (dist <= attackDistance)
        {
            Debug.LogError("swtich to attack state");
            npc.GetComponent<AiController>().SetTransition(Transition.ReachPlayer);
        }
        else if (dist >= chaseDistance)
        {
            Debug.LogError("swtich to patrol state");
            npc.GetComponent<AiController>().SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        desPos = player.position;
        Quaternion targetRotation = Quaternion.LookRotation(desPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        CharacterController controller = npc.GetComponent<CharacterController>();
        controller.SimpleMove(npc.transform.forward * Time.deltaTime * curSpeed);
    }
}