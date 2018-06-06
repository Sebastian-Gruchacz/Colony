namespace Colony.Model.Reports
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PlayerRaidedReport : IReportWriter
    {
        /// <summary>
        /// Attacking player or NPC
        /// </summary>
        public Player Attacker { get; set; }

        /// <summary>
        /// Defending player (always, no way in-game to attack Aliens)
        /// </summary>
        public Player Defender { get; set; }

        /// <summary>
        /// Forces used by attacker to attack
        /// </summary>
        public IReadOnlyCollection<ForceInfo> Attackers { get; private set; }

        /// <summary>
        /// Forces available to defending player to defend
        /// </summary>
        public IReadOnlyCollection<ForceInfo> Deffenders { get; private set; }

        /// <summary>
        /// Lost units and machines of attacking player
        /// </summary>
        public IReadOnlyCollection<ForceDamage> AttackerLosses { get; set; }

        /// <summary>
        /// Lost units and machines of defending player
        /// </summary>
        public IReadOnlyCollection<ForceDamage> DefenderLosses { get; set; }


        /// <summary>
        /// Destroyed buildings
        /// </summary>
        public IReadOnlyCollection<StructureDamage> DefenderStructuralLosses { get; set; }


        /// <summary>
        /// Resources stolen by the Attacker
        /// </summary>
        public IReadOnlyCollection<ResourceDamage> ResourcesLost { get; set; }

        /// <summary>
        /// Amount of junk created form destroyed units and equipment - can be utilized into Resources with dedicated plants by defender.
        /// </summary>
        public uint JunkCreated { get; set; }

        public void WriteReportInto(TextWriter stream)
        {
            throw new NotImplementedException();
        }
    }
}
