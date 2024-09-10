using Scripts.Piece;
using UnityEngine;

namespace Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private int m_Rows = 5; 
        [SerializeField] private int m_Columns = 5; 
        [SerializeField] private Vector2 m_Offset = new Vector2(1.0f, 1.0f); 
        [SerializeField] private GameObject m_GridPrefab; 

        [Header("Parent Transform")]
        [SerializeField] private Transform m_GridParent;

        private Piece.Grid[,] m_Grids;

        private void OnValidate()
        {
            if (m_Grids != null)
            {
                UpdateGridPositions();
            }
        }

        [ContextMenu("Generate Grid")]
        public void GenerateGrid()
        {
            ClearGrid(); 
            CreateGrid();
        }

        private void CreateGrid()
        {
            m_Grids = new Piece.Grid[m_Columns, m_Rows];

            for (int x = 0; x < m_Columns; x++)
            {
                for (int y = 0; y < m_Rows; y++)
                {
                    GameObject gridObject = Instantiate(m_GridPrefab, m_GridParent);
                    gridObject.name = $"Grid_Column{x}_Row{y}";

                    gridObject.transform.localPosition = new Vector3(x * m_Offset.x, y * m_Offset.y, 0);

                    Piece.Grid grid = gridObject.GetComponent<Piece.Grid>();
                    grid.Row = y;
                    grid.Column = x;

                    m_Grids[x, y] = grid;
                }
            }
        }

        private void UpdateGridPositions()
        {
            for (int x = 0; x < m_Columns; x++)
            {
                for (int y = 0; y < m_Rows; y++)
                {
                    if (m_Grids[x, y] != null)
                    {
                        m_Grids[x, y].transform.localPosition = new Vector3(x * m_Offset.x, y * m_Offset.y, 0);
                    }
                }
            }
        }

        private void ClearGrid()
        {
            for (int i = m_GridParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(m_GridParent.GetChild(i).gameObject);
            }

            m_Grids = null;
        }
    }
}
