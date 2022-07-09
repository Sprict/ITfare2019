using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameState
{
    public enum statusList{
        PreStart,
        Started,
        Finished
    };

    public static statusList state = statusList.PreStart;

}
