namespace Colony.Model.Core
{
    using System.Collections.Generic;

    public class GameState
    {
        public GameState()
        {
            this.Players = new List<Player>();    
        }

        public IList<Player> Players { get; private set; }

        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
        }
    }
}