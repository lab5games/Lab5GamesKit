using System.Collections.Generic;

namespace Lab5Games
{
    public class GizmosDrawer : Singleton<GizmosDrawer>
    {
        Stack<GizmosCommand> commands = new Stack<GizmosCommand>();

        public static void AddCommand(GizmosCommand cmd)
        {
            Instance.commands.Push(cmd);
        }

        private void OnDrawGizmos()
        {
            while(commands.Count > 0)
            {
                commands.Pop().Draw();
            }
        }
    }
}
