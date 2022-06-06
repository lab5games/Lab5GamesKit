using UnityEngine;

namespace Lab5Games.StateMachine
{
    public abstract class StateMachineMonoBehavior : MonoBehaviour
    {
        protected StateMachine<StateMonoBehavior> FSM;

        protected virtual void Start()
        {
            FSM = new StateMachine<StateMonoBehavior>();

            foreach(var state in GetComponentsInChildren<StateMonoBehavior>())
            {
                FSM.AddState(state);
            }
        }

        protected void Update()
        {
            FSM.UpdateStateMachine(Time.deltaTime);
        }
    }
}
