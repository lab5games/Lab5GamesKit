using System.Collections.Generic;

namespace Lab5Games
{
    public class GizmosDrawer : Singleton<GizmosDrawer>
    {
        Stack<GizmosCommand> commands = new Stack<GizmosCommand>();

        public static void AddCommand(GizmosCommand cmd)
        {
            if (Instance == null)
                return;


            if(!Instance.commands.Contains(cmd))
                Instance.commands.Push(cmd);
        }

        private void OnDrawGizmos()
        {
            float t = UnityEngine.Time.time;

            while(commands.Count > 0)
            {
                var cmd = commands.Peek();
                
                cmd.Draw();

                if (UnityEngine.Application.isPlaying)
                {
                    if (t >= cmd.StartTime + cmd.Duration)
                        commands.Pop();
                }
            }
        }
    }
}
