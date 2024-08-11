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
    
    private FSM _fsm = new FSM();
    
    void Start()
    {
        _fsm.Init(Enum.GetValues(typeof(AgentBehaviours)).Length,Enum.GetValues(typeof(Flags)).Length);

        _fsm.AddBehaviour<ChaseState>((int)AgentBehaviours.Chase,
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
        
        _fsm.AddBehaviour<PatrolState>((int)AgentBehaviours.Patrol,
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
        
        _fsm.AddBehaviour<ExplodeState>((int)AgentBehaviours.Explode,
            onTickParameters: () => { return new object[]
            {
            }; },
            onEnterParameters: () => { return new object[]
            {
                
            }; },
            onExitParameters: () => { return new object[]
            {
                
            }; });
        
        _fsm.SetTransition((int)AgentBehaviours.Patrol,(int)Flags.OntargetNear,(int)AgentBehaviours.Chase);
        _fsm.SetTransition((int)AgentBehaviours.Chase,(int)Flags.OnTargetReach,(int)AgentBehaviours.Explode);
        _fsm.SetTransition((int)AgentBehaviours.Chase,(int)Flags.OnTargetLost,(int)AgentBehaviours.Patrol);
        _fsm.SetTransition((int)AgentBehaviours.Explode,(int)Flags.OnTargetLost,(int)AgentBehaviours.Patrol);
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
