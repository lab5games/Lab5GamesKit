
namespace Lab5Games.StateMachine
{
    public interface IState 
    {
        public string StateName => GetType().Name;

        /// <summary>
        /// The state has entered the state machine
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fromState"></param>
        public abstract void EnterState(float dt, IState fromState);
        
        /// <summary>
        /// The state has exited the state machine
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="toState"></param>
        public abstract void ExitState(float dt, IState toState);
        
        /// <summary>
        /// update the state by frame
        /// </summary>
        /// <param name="dt"></param>
        public abstract void UpdateState(float dt);
        
        /// <summary>
        /// Check if the required conditions to enter this state are true
        /// </summary>
        /// <param name="toState"></param>
        /// <returns></returns>
        public abstract bool CheckTransition(IState toState);
    }
}
