using UnityEngine;
using Scripts.Managers;
using Scripts.Interfaces;

namespace Scripts.Piece
{
    [System.Serializable]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Grid : MonoBehaviour, IGrid, IPiece
    {
        [field: SerializeField] public int Row { get; set; }
        [field: SerializeField] public int Column { get; set; }
        [field: SerializeField] public bool IsEmpty {  get; set; }

        private void OnMouseDown()
        {
            SpawnManager.Ins.SpawnSand(this);
        }
    }
}