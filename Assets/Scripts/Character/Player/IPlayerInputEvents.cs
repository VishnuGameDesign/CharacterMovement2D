// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// Interface for handling player input through events, allowing decoupling between input sources and movement logic.
    /// </summary>
    
    public delegate void MoveEventDelegate(Vector2 moveInput);
    public delegate void InteractEventDelegate(bool isInteracting);
    
    public interface IPlayerInputEvents
    {
        event MoveEventDelegate OnMove;
        event InteractEventDelegate OnInteract;
    }
    
}