using System.Collections.Generic;

namespace Lab5Games
{
    public class GizmosDrawer : Singleton<GizmosDrawer>
    {
        List<GizmosCommand> commands = new List<GizmosCommand>();

        public static void AddCommand(GizmosCommand cmd)
        {
            if (Instance == null)
                return;


            if(!Instance.commands.Contains(cmd))
                Instance.commands.Add(cmd);
        }

        private void OnDrawGizmos()
        {
            float t = UnityEngine.Time.time;

            for(int i=commands.Count-1; i>=0; i--)
            {
                var cmd = commands[i];

                cmd.Draw();

                if (t > cmd.StartTime + cmd.Duration)
                    commands.RemoveAt(i);
            }
        }
    }
}
