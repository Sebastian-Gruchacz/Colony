namespace Colony.Model
{
    using Colony.Model.Core;

    /// <summary>
    /// This also includes NPC players...
    /// </summary>
    public class Player
    {
        public Player(string name, PlayerType playerType)
        {
            this.Name = name;
            this.PlayerType = playerType;
        }

        public string Name { get; private set; }

        public PlayerType PlayerType { get; private set; }

        public UnitCollection BaseUnits { get; set; }
    }
}