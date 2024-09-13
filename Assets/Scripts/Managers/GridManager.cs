using UnityEngine;

namespace Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        private static GridManager m_Ins;
        public static GridManager Ins
        {
            get
            {
                if (!m_Ins)
                    m_Ins = FindObjectOfType<GridManager>();
                return m_Ins;
            }
        }

        [Header("Grid Settings")]
        [SerializeField] private int m_Rows = 5;
        [SerializeField] private int m_Columns = 5;
        [SerializeField] private Vector2 m_Offset = new Vector2(1.0f, 1.0f);
        [SerializeField] private GameObject m_GridPrefab;

        [Header("Parent Transform")]
        [SerializeField] private Transform m_GridParent;

        [SerializeField] private Piece.Grid[,] m_Grids;

        private void Start()
        {
            GenerateGrid();
        }

        public bool IsGridFree(int row, int column)
        {
            try
            {
                if (m_Grids[column, row] != null && m_Grids[column, row].IsEmpty == true)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public Piece.Grid GetGrid(int row, int column)
        {
            return m_Grids[column, row];
        }

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

        private void ClearGrid()
        {
            for (int i = m_GridParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(m_GridParent.GetChild(i).gameObject);
            }
        }
    }
}
