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

        private void Start()
        {
            var sprite = m_SpriteRenderer.sprite;
            m_MoveDistance = (sprite.texture.width / (sprite.pixelsPerUnit / 100)) / 100 - 0.02f;
            SetSandPosition();
        }

        private void Update()
        {
            if (m_CurrentGrid == null)
                SetSandPosition();
        }

        public void SetSandPosition()
        {
            if (!m_CanSetPosition) return;

            m_CanSetPosition = false;
            var rnd = Random.Range(0, 2);

            if (GridManager.Ins.IsGridFree(Row - 1, Column))
            {
                SetGrid(m_DownDirection);
                SetActions(0);
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_RightDirection))
            {
                SetGrid(m_RightDirection);
                SetActions(m_RightDirection);
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_LeftDirection))
            {
                SetGrid(m_LeftDirection);
                SetActions(m_LeftDirection);
                return;
            }

            m_CanSetPosition = true;
        }

        private async void SetActions(int columnChangeValue)
        {
            Row -= 1;
            Column += columnChangeValue;
            transform.position = CalculatePosition(columnChangeValue);
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

        private Vector3 CalculatePosition(int direction)
        {
            var position = transform.position;
            position.y -= m_MoveDistance;
            position.x += m_MoveDistance * direction;
            return position;
        }
    }
}