// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// Runtime container for player configuration data loaded from a ScriptableObject.
    /// Used to keep behavior logic and editor data decoupled.
    /// </summary>
    public class PlayerData
    {
        public float moveSpeed;
        public bool enableGroundCheck;
        public LayerMask groundLayerMask;
        public float groundCheckDistance;
        public Vector3 groundCheckOriginOffset;

        public PlayerData(PlayerDataAsset dataAsset)
        {
            moveSpeed = dataAsset.MoveSpeed;
            enableGroundCheck = dataAsset.EnableGroundCheck;
            groundLayerMask = dataAsset.GroundLayerMask;
            groundCheckDistance = dataAsset.GroundCheckDistance;
            groundCheckOriginOffset = dataAsset.GroundCheckOriginOffset;
        }
    }
}