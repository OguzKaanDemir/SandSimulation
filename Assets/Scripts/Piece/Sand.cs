using UnityEngine;
using System.Collections;

namespace Scripts.Piece
{
    public class Sand : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        private float m_MoveDistance;

        private IEnumerator Start()
        {
            var sprite = m_SpriteRenderer.sprite;
            m_MoveDistance = (sprite.texture.width / (sprite.pixelsPerUnit / 100)) / 100 - 0.02f;

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(2f);
                transform.position = CalculateNextPosition();
            }
        }

        private Vector3 CalculateNextPosition()
        {
            var position = transform.position;
            position.y -= m_MoveDistance;
            return position;
        }
    }
}