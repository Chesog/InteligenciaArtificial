using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public Action<int> OnFlag;
    public abstract List<Action> GetOnEnterBehaviours(params object[] parameters);
    public abstract List<Action> GetOnExitBehaviours(params object[] parameters);
    public abstract List<Action> GetOnTickBehaviours(params object[] parameters);
}

public sealed class ChaseState : State
{
    public override List<Action> GetOnEnterBehaviours(params object[] parameters)
    {
        return new List<Action>();
    }

    public override List<Action> GetOnExitBehaviours(params object[] parameters)
    {
        return new List<Action>();
    }

    public override List<Action> GetOnTickBehaviours(params object[] parameters)
    {
        Transform OwnerTransform = parameters[0] as Transform;
        Transform TargetTransform = parameters[1] as Transform;
        
        float speed = Convert.ToSingle(parameters[2]);
        float explodeDistance = Convert.ToSingle(parameters[3]);
        float lostDistance = Convert.ToSingle(parameters[4]);
        
        List<Action> behaviours = new List<Action>();
        
        //behaviours.Add(() => { });  Exprecion Lambda o Metodo Anonimo 
        
        behaviours.Add(() =>
        {
            OwnerTransform.position += (TargetTransform.position - OwnerTransform.position).normalized * speed * Time.deltaTime;
        });

        behaviours.Add(() => { Debug.Log("Whistle"); });
        
        behaviours.Add(() =>
        {
            if (Vector3.Distance(TargetTransform.position,OwnerTransform.position) < explodeDistance)
                OnFlag?.Invoke((int)Flags.OnTargetReach);
            
            else if (Vector3.Distance(TargetTransform.position,OwnerTransform.position) > lostDistance)
                OnFlag?.Invoke((int)Flags.OnTargetLost);
        });
        
        return behaviours;
    }
}

public sealed class PatrolState : State
{
    private Transform actualTarget;
    public override List<Action> GetOnEnterBehaviours(params object[] parameters)
    {
        return new List<Action>();
    }

    public override List<Action> GetOnExitBehaviours(params object[] parameters)
    {
        return new List<Action>();
    }

    public override List<Action> GetOnTickBehaviours(params object[] parameters)
    {
        Transform ownerTransform = parameters[0] as Transform;
        Transform waypoint1 = parameters[1] as Transform;
        Transform waypoint2 = parameters[2] as Transform;
        Transform chaseTarget = parameters[3] as Transform;
        
        float speed = Convert.ToSingle(parameters[4]);
        float chaseDistance = Convert.ToSingle(parameters[5]);
        
        List<Action> behaviours = new List<Action>();
        
        behaviours.Add(() =>
        {
            if (actualTarget == null)
                actualTarget = waypoint1;

            if (Vector3.Distance(ownerTransform.position, actualTarget.position) < 0.2f)
            {
                if (actualTarget == waypoint1)
                    actualTarget = waypoint2;

                else
                    actualTarget = waypoint1;
            }
            
            ownerTransform.position += (actualTarget.position - ownerTransform.position).normalized * speed * Time.deltaTime;

        });
        
        behaviours.Add(() =>
        {
            if (Vector3.Distance(ownerTransform.position,chaseTarget.position) < chaseDistance)
                OnFlag?.Invoke((int)Flags.OntargetNear);
        });
        
        return behaviours;
    }
}

public sealed class ExplodeState : State
{
    public override List<Action> GetOnEnterBehaviours(params object[] parameters)
    {
        List<Action> behaviours = new List<Action>();
        
        behaviours.Add(() => {Debug.Log("Boom"); });
        
        return behaviours;
    }

    public override List<Action> GetOnExitBehaviours(params object[] parameters)
    {
        return new List<Action>();
    }

    public override List<Action> GetOnTickBehaviours(params object[] parameters)
    {
        List<Action> behaviours = new List<Action>();
        
        behaviours.Add(() => {Debug.Log("F"); });
        
        return behaviours;
    }
}
