using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FSM<EnumState,EnumFlag> 
    where EnumState : Enum 
    where EnumFlag : Enum 
{
    private const int UNNASSIGNED_TRANSITION = -1;

    public int currentState = 0;
    private (int destinationState, Action onTransition)[,] transitions;
    
    ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 32 };
    
    private Dictionary<int, State> behaviours;
    private Dictionary<int, Func<object[]>> behaviourTickParamaters;
    private Dictionary<int, Func<object[]>> behaviourOnEnterParameters;
    private Dictionary<int, Func<object[]>> behaviourOnExitParameters;

  
    
    private BehaviourActions GetCurrentOnEnterBehaviour => behaviours[currentState].GetOnEnterBehaviours(behaviourOnEnterParameters[currentState]?.Invoke());
    private BehaviourActions GetCurrentOnExitBehaviour => behaviours[currentState].GetOnExitBehaviours(behaviourOnExitParameters[currentState]?.Invoke());
    private BehaviourActions GetCurrentTickBehaviour => behaviours[currentState].GetOnTickBehaviours(behaviourTickParamaters[currentState]?.Invoke());
    
    public void Init()
    {
        int states = Enum.GetValues(typeof(EnumState)).Length;
        int flags = Enum.GetValues(typeof(EnumFlag)).Length;
        behaviours = new Dictionary<int, State>();
        transitions = new (int,Action)[states, flags];

        for (int i = 0; i < states; i++)
        {
            for (int j = 0; j < flags; j++)
            {
                transitions[i, j] = (UNNASSIGNED_TRANSITION,null);
            }
        }

        behaviourTickParamaters = new Dictionary<int, Func<object[]>>();
        behaviourOnEnterParameters = new Dictionary<int, Func<object[]>>();
        behaviourOnExitParameters = new Dictionary<int, Func<object[]>>();
    }

    public void ForceState(EnumState state)
    {
        int _state = Convert.ToInt16(state);
        currentState = _state;
    }

    public void SetTransition(EnumState originState,EnumFlag flag,EnumState destinationState,Action onTransition = null)
    {
        transitions[Convert.ToInt32(originState),Convert.ToInt32(flag)] = (Convert.ToInt32(destinationState), onTransition);
    }

    private void Transition(Enum flag)
    {
        if (transitions[currentState,Convert.ToInt32(flag)].destinationState != UNNASSIGNED_TRANSITION)
        {
            ExecuteBehaviour(GetCurrentOnExitBehaviour);
            
            transitions[currentState,Convert.ToInt32(flag)].onTransition?.Invoke();
 
            currentState = transitions[currentState,Convert.ToInt32(flag)].destinationState;
            
            ExecuteBehaviour(GetCurrentOnEnterBehaviour);
  
        }
    }

    public void AddBehaviour<T>(EnumState state,Func<object[]> onTickParameters = null,Func<object[]> onEnterParameters = null,Func<object[]> onExitParameters = null) where T : State, new()
    {
        int stateIndex = Convert.ToInt32(state);
        if (!behaviours.ContainsKey(stateIndex))
        {
            State newBehaviour = new T();
            newBehaviour.OnFlag += Transition;
            
            behaviours.Add(stateIndex,newBehaviour);
            
            behaviourTickParamaters.Add(stateIndex,onTickParameters);
            behaviourOnEnterParameters.Add(stateIndex,onEnterParameters);
            behaviourOnExitParameters.Add(stateIndex,onExitParameters);
            
        }
    }

    public void Tick()
    {
        if (behaviours.ContainsKey(currentState))
        {
            ExecuteBehaviour(GetCurrentTickBehaviour);
        }

        
    }
    private void ExecuteBehaviour(BehaviourActions behaviourAction)
    {
        if (behaviourAction.Equals(default(BehaviourActions)))
            return;

        int executionOrder = 0;
        
        /*
         * Una cpu es un array de nucleos y un nucleo es un array de hilos o (Threads)
         * El main thread es el hilo que te proporciona el propio windows (Tu sistema operativo) alocado en la parte de CODE de la Ram
         */

        while ((behaviourAction.MainThreadBehaviours != null && behaviourAction.MainThreadBehaviours.Count > 0) ||
               (behaviourAction.MultithreadBehaviours != null && behaviourAction.MultithreadBehaviours.Count > 0))
        {
            // Un task es una definicion de una accion a ejecutarse en un thread
            Task multithreadBehaviour = new Task(() =>
            {
                if (behaviourAction.MultithreadBehaviours != null)
                {
                    if (behaviourAction.MultithreadBehaviours.ContainsKey(executionOrder))
                    {
                        Parallel.ForEach(behaviourAction.MultithreadBehaviours[executionOrder],parallelOptions, (behaviours) =>
                        {
                            behaviours?.Invoke();
                        });
                        behaviourAction.MultithreadBehaviours.TryRemove(executionOrder,out _);
                    }
                }
            });
            
            multithreadBehaviour.Start();

            if (behaviourAction.MainThreadBehaviours != null)
            {
                if (behaviourAction.MainThreadBehaviours.ContainsKey(executionOrder))
                {
                    foreach (Action behaviour in behaviourAction.MainThreadBehaviours[executionOrder])
                    {
                        behaviour?.Invoke();
                    } ;
                    behaviourAction.MainThreadBehaviours.Remove(executionOrder);
                }
            }
            multithreadBehaviour.Wait();

            executionOrder++;
        }

        behaviourAction.TransitionBehaviour?.Invoke();
    }
}
