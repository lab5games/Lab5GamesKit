using UnityEngine;

namespace Lab5Games.StateMachine
{
    public abstract class StateMonoBehavior : MonoBehaviour, IState
    {
        public abstract void EnterState(float dt, IState fromState);

        public abstract void ExitState(float dt, IState toState);

        public abstract void UpdateState(float dt);

        public abstract bool CheckTransition(IState toState);
    }
}
