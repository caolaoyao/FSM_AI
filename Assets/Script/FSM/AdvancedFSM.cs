using System.Collections.Generic;
using UnityEngine;
public enum Transition
{
    SawPlayer = 0,
    ReachPlayer,
    LostPlayer,
    NoHealth
}

public enum FSMStateID
{
    Patrolling = 0,
    Chasing,
    Attacking,
    Dead
}
public class AdvancedFSM : FSM
{
    private List<FSMState> fsmStates;
    private FSMStateID curStateID;
    public FSMStateID CurStateID
    {
        get { return curStateID; }
    }

    private FSMState curState;
    public FSMState CurState
    {
        get { return curState; }
    }

    public AdvancedFSM()
    {
        fsmStates = new List<FSMState>();
    }

    public void AddFSMState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("state is null");
            return;
        }

        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            curState = fsmState;
            curStateID = fsmState.ID;
            return;
        }

        foreach (var state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("state has exist");
                return;
            }
        }

        fsmStates.Add(fsmState);
    }

    public void DeleteState(FSMStateID id)
    {
        foreach (var state in fsmStates)
        {
            if (state.ID == id)
            {
                fsmStates.Remove(state);
                break;
            }
        }
    }

    public void PerformTransition(Transition transition)
    {
        FSMStateID id = curState.GetOutputState(transition);
        curStateID = id;

        foreach (var state in fsmStates)
        {
            if (state.ID == curStateID)
            {
                curState = state;
                break;
            }
        }
    }
}