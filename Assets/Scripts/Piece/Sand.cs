using UnityEngine;
using Scripts.Managers;
using Scripts.Interfaces;
using System.Collections;

namespace Scripts.Piece
{
    public class Sand : MonoBehaviour, IPiece
    {
        [field: SerializeField] public int Row { get; set; }
        [field: SerializeField] public int Column { get; set; }

        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private int m_AnimSpeed;

        private float m_MoveDistance;
        private const int DownDirection = 0;
        private const int RightDirection = 1;
        private const int LeftDirection = -1;

        private Grid m_CurrentGrid;
        private bool m_CanSetPosition = true;

        private int m_FirstRandomDir, m_SecondRandomDir;

        private void Start()
        {
            var sprite = m_SpriteRenderer.sprite;
            m_MoveDistance = (sprite.texture.width / (sprite.pixelsPerUnit / 100)) / 100 - 0.015f;
            SetSandPosition();
        }

        private void Update()
        {
            if (m_CurrentGrid == null)
            {
                SetSandPosition();
            }
        }

        public void SetColor(Color color)
        {
            m_SpriteRenderer.color = color;
        }

        private void SetSandPosition()
        {
            if (!m_CanSetPosition) return;

            m_CanSetPosition = false;
            var rnd = Random.Range(0, 2);
            m_FirstRandomDir = rnd == 0 ? RightDirection : LeftDirection;
            m_SecondRandomDir = rnd == 0 ? LeftDirection : RightDirection;

            if (TrySetGrid(DownDirection))
                return;
            if (TrySetGrid(m_FirstRandomDir))
                return;
            if (TrySetGrid(m_SecondRandomDir))
                return;

            m_CanSetPosition = true;
            Invoke(nameof(SetSandPosition), 0.75f);
        }

        private bool TrySetGrid(int direction)
        {
            if (GridManager.Ins.IsGridFree(Row - 1, Column + direction))
            {
                SetGrid(direction);
                StartCoroutine(ExecuteActions(direction));
                return true;
            }
            return false;
        }

        private IEnumerator ExecuteActions(int columnChangeValue)
        {
            Row -= 1;
            Column += columnChangeValue;
            SetPosition();
            yield return new WaitForSeconds(m_AnimSpeed / 1000f); 
            m_CanSetPosition = true;
            SetSandPosition();
        }

        private void SetGrid(int direction)
        {
            if (m_CurrentGrid != null)
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
