using System;
using UnityEngine;

public static class EventManager
{
    #region GameManagerEvents
    public static Action<Transform,Vector3> planetSelected;
    public static Action planetUnSelected;
    #endregion
}
