using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RoomExits
{
    None,
    T,
    R,
    B,
    L,
    TR,
    TB,
    TL,
    RB,
    RL,
    BL,
    TRB,
    TBL,
    TRL,
    RBL,
    TRBL
}

public static class RoomExitsExtension
{
    private static readonly RoomExits[] tops =
         {
            RoomExits.T,
            RoomExits.TR,
            RoomExits.TB,
            RoomExits.TL,
            RoomExits.TRB,
            RoomExits.TBL,
            RoomExits.TRL,
            RoomExits.TRBL
        };

    private static readonly RoomExits[] rights =
         {
            RoomExits.R,
            RoomExits.TR,
            RoomExits.RB,
            RoomExits.RL,
            RoomExits.TRB,
            RoomExits.TRL,
            RoomExits.RBL,
            RoomExits.TRBL
        };

    private static readonly RoomExits[] bottoms =
         {
            RoomExits.B,
            RoomExits.TB,
            RoomExits.RB,
            RoomExits.BL,
            RoomExits.TRB,
            RoomExits.TBL,
            RoomExits.RBL,
            RoomExits.TRBL
        };

    private static readonly RoomExits[] lefts =
         {
            RoomExits.L,
            RoomExits.TL,
            RoomExits.RL,
            RoomExits.BL,
            RoomExits.TBL,
            RoomExits.TRL,
            RoomExits.RBL,
            RoomExits.TRBL
        };

    public static bool HasTopExit(this RoomExits roomType)
    {
        foreach (var item in tops)
        {
            if (item == roomType)
                return true;
        }

        return false;
    }

    public static bool HasRightExit(this RoomExits roomType)
    {
        foreach (var item in rights)
        {
            if (item == roomType)
                return true;
        }

        return false;
    }

    public static bool HasBottomExit(this RoomExits roomType)
    {
        foreach (var item in bottoms)
        {
            if (item == roomType)
                return true;
        }

        return false;
    }

    public static bool HasLeftExit(this RoomExits roomType)
    {
        foreach (var item in lefts)
        {
            if (item == roomType)
                return true;
        }

        return false;
    }

    public static List<RoomExits> GetPossibleExits(bool needsTopExit, 
        bool needsRightExit, 
        bool needsBottomExit, 
        bool needsLeftExit,
        bool noDeadEnd = true)
    {
        List<RoomExits> exits = new List<RoomExits>();
        exits.AddRange(tops);
        exits.AddRange(rights);
        exits.AddRange(bottoms);
        exits.AddRange(lefts);

        if (needsTopExit)
            exits.RemoveAll(e => !e.HasTopExit());
        if (needsRightExit)
            exits.RemoveAll(e => !e.HasRightExit());
        if (needsBottomExit)
            exits.RemoveAll(e => !e.HasBottomExit());
        if (needsLeftExit)
            exits.RemoveAll(e => !e.HasLeftExit());

        if(noDeadEnd)
            exits.RemoveAll(e => e == RoomExits.T || e == RoomExits.R || e == RoomExits.B || e == RoomExits.L);

        return exits;
    }
}