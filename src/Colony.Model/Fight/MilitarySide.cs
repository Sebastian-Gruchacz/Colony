namespace Colony.Model.Fight
{
    using System;

    [Flags]
    public enum MilitarySide
    {
        Ofense = 1,

        Defense = 2,

        Both = Ofense | Defense
    }
}