using UnityEngine;

namespace Lab5Games.StateMachine
{
    public abstract class StateMachineMonoBehavior : MonoBehaviour
    {
        protected StateMachine<StateMonoBehavior> FSM;

        public StateMonoBehavior CurrentState => FSM.CurrentState;
        public StateMonoBehavior PreviousState => FSM.PreviousState;

        public string[] GetStateNames() => FSM.GetStateNames();

        public StateMonoBehavior GetState(string stateName) => FSM.GetState(stateName);

        public void EnqueueTransition(string stateName) => FSM.EnqueueTransition(stateName);

        public void EnqueueTransition(StateMonoBehavior state) => FSM.EnqueueTransition(state);

        public void ForceState(string stateName) => FSM.ForceState(stateName);

        public void ForceState(StateMonoBehavior state) => FSM.ForceState(state);

        public void AddState(StateMonoBehavior state) => FSM.AddState(state);


        protected virtual void Start()
        {
            FSM = new StateMachine<StateMonoBehavior>();

            foreach(var state in GetComponentsInChildren<StateMonoBehavior>())
            {
                AddState(state);
            }
        }

        protected void Update()
        {
            FSM.UpdateStateMachine(Time.deltaTime);
        }
    }
}
