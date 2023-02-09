using System.ComponentModel;

public enum EnemyState
{
    idle,
    frozen,
    walking,
    attacking
}

public enum EnemyMovementType
{
    idle,
    chasePlayer,
    awayFromPlayer
}

public enum EnemyType
{    
    [Description("Skeleton")] Skeleton = 1,
    [Description("Coffin")] Coffin = 2,
}
