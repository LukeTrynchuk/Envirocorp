﻿using UnityEngine;
using FireBullet.Enviro.Board;

namespace FireBullet.Enviro.Services
{
    /// <summary>
    /// The IInputService is a contract that
    /// all input services must implement.
    /// The Input Service is responsible for detecting
    /// when the player has hit a mouse button or key.
    /// </summary>
    public interface IInputService
    {
        event System.Action<HexCoordinate> OnHexPressed;
    }
}
