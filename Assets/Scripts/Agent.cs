using System;
using UnityEngine;
public enum AgentBehaviours { Chase,Patrol,Explode }
public enum Flags { OnTargetReach, OntargetNear , OnTargetLost }

public class Agent : MonoBehaviour
{
    [SerializeField] Transform ownerTransform;
    [SerializeField] Transform waypoint1;
    [SerializeField] Transform waypoint2;
    [SerializeField] private Transform chaseTarget;
    
    [SerializeField] float speed;
    [SerializeField] float chaseDistance;
    [SerializeField] float explodeDistance;
    [SerializeField] float lostDistance;
    
    private FSM<AgentBehaviours,Flags> _fsm = new FSM<AgentBehaviours,Flags>();
    
    void Start()
    {
        _fsm.Init();

        _fsm.AddBehaviour<ChaseState>(AgentBehaviours.Chase,
            onTickParameters: () => { return new object[]
            {
                transform,
                chaseTarget,
                speed,
                explodeDistance,
                lostDistance
            }; },
            onEnterParameters: () => { return new object[]
            {
                
            }; },
            onExitParameters: () => { return new object[]
            {
                
            }; });
        
        _fsm.AddBehaviour<PatrolState>(AgentBehaviours.Patrol,
            onTickParameters: () => { return new object[]
            {
                transform,
                waypoint1,
                waypoint2,
                chaseTarget,
                speed,
                chaseDistance
            }; },
            onEnterParameters: () => { return new object[]
            {
                
            }; },
            onExitParameters: () => { return new object[]
            {
                
            }; });
        
        _fsm.AddBehaviour<ExplodeState>(AgentBehaviours.Explode,
            onTickParameters: () => { return new object[]
            {
            }; },
            onEnterParameters: () => { return new object[]
            {
                
            }; },
            onExitParameters: () => { return new object[]
            {
                
            }; });
        
        _fsm.SetTransition(AgentBehaviours.Patrol,Flags.OntargetNear,AgentBehaviours.Chase , () => {Debug.Log("Te Vi!"); });
        _fsm.SetTransition(AgentBehaviours.Chase,Flags.OnTargetReach,AgentBehaviours.Explode);
        _fsm.SetTransition(AgentBehaviours.Chase,Flags.OnTargetLost,AgentBehaviours.Patrol);
        _fsm.SetTransition(AgentBehaviours.Explode,Flags.OnTargetLost,AgentBehaviours.Patrol);
        
        
        _fsm.ForceState(AgentBehaviours.Patrol);
    }
    
    void Update()
    {
        _fsm.Tick();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,lostDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,explodeDistance);
        }
    }
}
