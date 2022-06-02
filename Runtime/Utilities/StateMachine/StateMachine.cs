using System;
using System.Collections.Generic;

namespace Lab5Games.StateMachine
{
    public delegate void StateChangeHandler(IState fromState, IState toState);

    public class StateMachine<TState> where TState : class, IState
    {
        float _dt;

        Dictionary<string, TState> states = new Dictionary<string, TState>();
        Queue<TState> transitions = new Queue<TState>();

        public TState CurrentState { get; protected set; }
        public TState PreviousState { get; protected set; }

        
        public event StateChangeHandler onStateChanged;

        public TState GetState(string stateName)
        {
            TState output = null;

            states.TryGetValue(stateName, out output);
            return output;
        }

        public void EnqueueTransition(string stateName)
        {
            EnqueueTransition(GetState(stateName));
        }

        public void EnqueueTransition(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            transitions.Enqueue(state);
        }

        public void ForceState(string stateName)
        {
            ForceState(GetState(stateName));
        }

        public void ForceState(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            PreviousState = CurrentState;
            CurrentState = state;

            PreviousState?.ExitState(_dt, CurrentState);
            CurrentState?.EnterState(_dt, PreviousState);
        }

        public virtual void AddState(TState state)
        {
            if(GetState(state.StateName) != null)
            {
                GLogger.LogAsType($"The state({state.StateName}) is already existing, failed to add it", GLogType.Error);
            }

            states.Add(state.StateName, state);
        }

        public virtual void UpdateStateMachine(float dt)
        {
            _dt = dt;

            if (CurrentState == null)
                return;

            TState nextTransition = CheckTransitions();

            transitions.Clear();

            if(nextTransition != null)
            {
                PreviousState = CurrentState;
                CurrentState = nextTransition;

                PreviousState.ExitState(_dt, CurrentState);
                CurrentState.EnterState(_dt, PreviousState);

                onStateChanged?.Invoke(PreviousState, CurrentState);
            }

            CurrentState.UpdateState(_dt);
        }

        TState CheckTransitions()
        {
            TState next = null;

            while(transitions.Count > 0)
            {
                next = transitions.Dequeue();

                if (next == null)
                    continue;

                if(CurrentState.CheckEnterTransition(next))
                {
                    return next;
                }
            }

            return null;
        }
    }
}
