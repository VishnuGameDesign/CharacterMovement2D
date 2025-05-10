// MIT License – Created by VishnuGameDesign, 2025

using UnityEngine;

namespace Character.Player
{
    /// <summary>
    /// ScriptableObject that stores tunable player parameters such as move speed and ground check settings.
    /// Drag this into the PlayerController in the inspector.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerDataAsset : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public bool EnableGroundCheck { get; private set; }
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
        [field: SerializeField] public float GroundCheckDistance { get; private set; }
        [field: SerializeField] public Vector3 GroundCheckOriginOffset { get; private set; }
    }
}
