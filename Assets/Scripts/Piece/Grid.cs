using UnityEngine;
using Scripts.Interfaces;

namespace Scripts.Piece
{
    [System.Serializable]
    public class Grid : MonoBehaviour, IGrid
    {
        [field: SerializeField] public int Row { get; set; }
        [field: SerializeField] public int Column { get; set; }
        [field: SerializeField] public bool IsEmpty {  get; set; }
    }
}