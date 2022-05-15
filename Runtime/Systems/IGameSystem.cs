
namespace Lab5Games
{ 
    public interface IGameSystem
    {
        public GameSystemStatus Status { get; }

        public string Message { get; }
    }
}
