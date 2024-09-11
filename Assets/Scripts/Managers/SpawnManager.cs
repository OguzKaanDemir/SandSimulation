using Scripts.Piece;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager m_Ins;
        public static SpawnManager Ins
        {
            get
            {
                if (!m_Ins)
                    m_Ins = FindObjectOfType<SpawnManager>();
                return m_Ins;
            }
        }

        [SerializeField] private Sand m_SandPrefab;

        public void SpawnSand(Piece.Grid grid)
        {
            var sand = Instantiate(m_SandPrefab, grid.transform.position, Quaternion.identity);
            sand.Row = grid.Row;
            sand.Column = grid.Column;
        }
    }
}
