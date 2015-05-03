using UnityEngine;
public class AiController : AdvancedFSM
{
    private int health;

    protected override void Initialize()
    {
        health = 100;
        elapsedTime = 0.0f;
        shootRate = 2.0f;
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        if (playerTransform == null)
        {
            Debug.LogError("player doesn't exist");
        }
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        CurState.Reason(playerTransform, transform);
        CurState.Act(playerTransform, transform);
    }

    public void SetTransition(Transition transition)
    {
        PerformTransition(transition);
    }

    private void ConstructFSM()
    {
        pointList = GameObject.FindGameObjectsWithTag("PatrolPoint");
        Transform[] waypoints = new Transform[pointList.Length];
        int i = 0;
        foreach (var obj in pointList)
        {
            waypoints[i] = obj.transform;
            i++;
        }

        PatrolState patrol = new PatrolState(waypoints);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        patrol.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        ChaseState chase = new ChaseState(waypoints);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        chase.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AttackState attack = new AttackState(waypoints);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        DeadState dead = new DeadState();
        dead.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AddFSMState(patrol);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);
    }

    public void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            elapsedTime = 0.0f;
            Debug.LogError("--------------shoot-----------------");
            Debug.LogError("-----------------health减少10点--------------------");
            if (health <= 0)
            {
                Debug.LogError("----------------swithc to dead state----------------");
                SetTransition(Transition.NoHealth);
            }
        }
    }
}
