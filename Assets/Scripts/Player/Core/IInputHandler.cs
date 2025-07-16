using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECO.Player
{
    public interface IInputHandler
    {
        bool IsPressed(string action);
        bool IsHeld(string action);
        bool IsReleased(string action);
        Vector2 GetAxis(string action);
    }
}