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

        _fsm.AddBehaviour<ChaseState>(0,
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
        
        _fsm.AddBehaviour<PatrolState>(1,
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
    
    }
    
    void Update()
    {
        _fsm.Tick();
    }
}
