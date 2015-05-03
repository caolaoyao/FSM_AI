using UnityEngine;
public class AttackState : FSMState
{
    public AttackState(Transform[] wp)
    {
        waypoints = wp;
        stateId = FSMStateID.Attacking;
        curRotSpeed = 12.0f;
        curSpeed = 100.0f;
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
        float dist = Vector3.Distance(npc.position, player.position);
        if (dist >= attackDistance && dist < chaseDistance)
        {
            Debug.LogError("swtich to chase state");
            npc.GetComponent<AiController>().SetTransition(Transition.SawPlayer);
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
        npc.GetComponent<AiController>().ShootBullet();
    }
}