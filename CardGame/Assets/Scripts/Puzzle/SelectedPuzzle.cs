﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectedPuzzle
{
    private static int level;

    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
}
