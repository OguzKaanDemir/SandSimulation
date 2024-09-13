using UnityEngine;
using Scripts.Managers;
using Scripts.Interfaces;
using System.Threading.Tasks;

namespace Scripts.Piece
{
    [System.Serializable]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Grid : MonoBehaviour, IGrid, IPiece
    {
        [field: SerializeField] public int Row { get; set; }
        [field: SerializeField] public int Column { get; set; }
        [field: SerializeField] public bool IsEmpty { get; set; }

        private bool m_CanTriggerSpawn = true;

        private void OnMouseDrag()
        {
            DelayedSpawn();
        }

        private async void DelayedSpawn()
        {
            if (!m_CanTriggerSpawn) return;
            m_CanTriggerSpawn = false;

            await Task.Delay(5);

            SpawnManager.Ins.SpawnSand(this);
            m_CanTriggerSpawn = true;
        }
    }
}