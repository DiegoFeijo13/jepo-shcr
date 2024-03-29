﻿using System.ComponentModel;

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
    awayFromPlayer,
    random
}

public enum EnemyType
{    
    [Description("Skeleton")] Skeleton = 1,
    [Description("Coffin")] Coffin = 2,
    [Description("Float Eye")] FloatEye = 3,
}
