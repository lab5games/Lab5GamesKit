using System;
using System.Collections.Generic;

namespace Lab5Games.StateMachine
{
    public delegate void StateChangeHandler(IState fromState, IState toState);

    public class StateMachine
    {
        float _dt;

        Dictionary<string, IState> states = new Dictionary<string, IState>();
        Queue<IState> transitions = new Queue<IState>();

        public IState CurrentState { get; protected set; }
        public IState PreviousState { get; protected set; }

        
        public event StateChangeHandler onStateChanged;

        public IState GetState(string stateName)
        {
            IState output = null;

            states.TryGetValue(stateName, out output);
            return output;
        }

        public void EnqueueTransition(string stateName)
        {
            EnqueueTransition(GetState(stateName));
        }

        public void EnqueueTransition(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            transitions.Enqueue(state);
        }

        public void ForceState(string stateName)
        {
            ForceState(GetState(stateName));
        }

        public void ForceState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            PreviousState = CurrentState;
            CurrentState = state;

            PreviousState?.ExitState(_dt, CurrentState);
            CurrentState?.EnterState(_dt, PreviousState);
        }

        public virtual void AddState(IState state)
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

            IState nextTransition = CheckTransitions();

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

        IState CheckTransitions()
        {
            IState next = null;

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
