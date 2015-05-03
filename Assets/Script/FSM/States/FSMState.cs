using System.Collections.Generic;
using UnityEngine;
public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    protected FSMStateID stateId;
    public FSMStateID ID
    {
        get { return stateId; }
    }

    protected Vector3 desPos;
    protected Transform[] waypoints;
    protected float curRotSpeed;
    protected float curSpeed;
    protected float chaseDistance = 15.0f;
    protected float attackDistance = 8.0f;
    protected float arriveDistance = 3.0f;


    public void AddTransition(Transition transition, FSMStateID id)
    {
        if (map.ContainsKey(transition))
        {
            return;
        }
        map.Add(transition, id);
    }

    public void DeleteTransition(Transition transition)
    {
        if (map.ContainsKey(transition))
        {
            map.Remove(transition);
        }
    }

    public FSMStateID GetOutputState(Transition transition)
    {
        return map[transition];
    }

    public abstract void Reason(Transform player, Transform npc);
    public abstract void Act(Transform player, Transform npc);

    public void FindNextPoint()
    {
        int rndIndex = Random.Range(0, waypoints.Length);
        Vector3 rndPosition = Vector3.zero;
        desPos = waypoints[rndIndex].position + rndPosition;
    }
}
