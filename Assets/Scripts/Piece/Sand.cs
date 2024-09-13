using UnityEngine;
using Scripts.Managers;
using Scripts.Interfaces;
using System.Threading.Tasks;

namespace Scripts.Piece
{
    public class Sand : MonoBehaviour, IPiece
    {
        [field: SerializeField] public int Row { get; set; }
        [field: SerializeField] public int Column { get; set; }

        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private int m_AnimSpeed;

        private float m_MoveDistance;
        private int m_DownDirection = 0;
        private int m_RightDirection = 1;
        private int m_LeftDirection = -1;

        private Grid m_CurrentGrid;
        private bool m_CanSetPosition = true;

        private int m_1stRandomDir, m_2ndRandomDir;

        private void Start()
        {
            var sprite = m_SpriteRenderer.sprite;
            m_MoveDistance = (sprite.texture.width / (sprite.pixelsPerUnit / 100)) / 100 - 0.015f;
            SetSandPosition();
        }

        private void Update()
        {
            if (m_CurrentGrid == null)
                SetSandPosition();
        }

        public void SetColor(Color color)
        {
            m_SpriteRenderer.color = color;
        }

        public void SetSandPosition()
        {
            if (!m_CanSetPosition) return;

            m_CanSetPosition = false;
            var rnd = Random.Range(0, 2);

            m_1stRandomDir = rnd == 0 ? m_RightDirection : m_LeftDirection;
            m_2ndRandomDir = rnd == 0 ? m_LeftDirection : m_RightDirection;

            if (GridManager.Ins.IsGridFree(Row - 1, Column))
            {
                SetGrid(m_DownDirection);
                SetActions(0);
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_1stRandomDir))
            {
                SetGrid(m_1stRandomDir);
                SetActions(m_1stRandomDir);
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_2ndRandomDir))
            {
                SetGrid(m_2ndRandomDir);
                SetActions(m_2ndRandomDir);
                return;
            }

            m_CanSetPosition = true;
            Invoke(nameof(SetSandPosition), 0.75f);
        }

        private async void SetActions(int columnChangeValue)
        {
            Row -= 1;
            Column += columnChangeValue;
            SetPosition();
            await Task.Delay(m_AnimSpeed);
            m_CanSetPosition = true;
            SetSandPosition();
        }

        private void SetGrid(int direction)
        {
            if (m_CurrentGrid)
                m_CurrentGrid.IsEmpty = true;

            m_CurrentGrid = GridManager.Ins.GetGrid(Row - 1, Column + direction);
            m_CurrentGrid.IsEmpty = false;
        }

        private void SetPosition()
        {
            transform.position = m_CurrentGrid.transform.position;
        }
    }
}