using UnityEngine;
using System.Collections;
using Scripts.Interfaces;
using System.Collections.Generic;
using Scripts.Managers;
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

        private void Start()
        {
            var sprite = m_SpriteRenderer.sprite;
            m_MoveDistance = (sprite.texture.width / (sprite.pixelsPerUnit / 100)) / 100 - 0.02f;
            SetSandPosition();
        }

        public async void SetSandPosition()
        {
            var rnd = Random.Range(0, 2);

            if (GridManager.Ins.IsGridFree(Row - 1, Column))
            {
                SetGrid(m_DownDirection);
                Row -= 1;
                transform.position = CalculatePosition(m_DownDirection);
                await Task.Delay(m_AnimSpeed);
                SetSandPosition();
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_RightDirection))
            {
                SetGrid(m_RightDirection);
                Row -= 1;
                Column += m_RightDirection;
                transform.position = CalculatePosition(m_RightDirection);
                await Task.Delay(m_AnimSpeed);
                SetSandPosition();
                return;
            }
            if (GridManager.Ins.IsGridFree(Row - 1, Column + m_LeftDirection))
            {
                SetGrid(m_LeftDirection);
                Row -= 1;
                Column += m_LeftDirection;
                transform.position = CalculatePosition(m_LeftDirection);
                await Task.Delay(m_AnimSpeed);
                SetSandPosition();
                return;
            }
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
            Debug.Log("DÝR*******" + direction);
            var position = transform.position;
            position.y -= m_MoveDistance;
            position.x += m_MoveDistance * direction;
            Debug.Log("POS********" + position);
            return position;
        }
    }
}