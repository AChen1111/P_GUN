namespace Game.Core
{
    public class GameEventId
    {
        public GameEventId(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class GameEventId<TPayload> : GameEventId
    {
        public GameEventId(string name)
            : base(name)
        {
        }
    }
}
