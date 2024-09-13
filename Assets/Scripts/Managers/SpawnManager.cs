using UnityEngine;
using Scripts.Piece;

namespace Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager m_Ins;
        public static SpawnManager Ins
        {
            get
            {
                if (m_Ins == null)
                    m_Ins = FindObjectOfType<SpawnManager>();
                return m_Ins;
            }
        }

        [SerializeField] private Sand m_SandPrefab;
        [SerializeField] private float m_ColorChangeSpeed = 1f;

        private Color m_CurrentColor;
        private float m_ColorChangeProgress;

        private void Start()
        {
            m_CurrentColor = Color.red;
            m_ColorChangeProgress = 0f;
        }

        private void Update()
        {
            m_ColorChangeProgress += m_ColorChangeSpeed * Time.deltaTime;
            if (m_ColorChangeProgress > 1f)
            {
                m_ColorChangeProgress -= 1f;
            }
        }

        public void SpawnSand(Piece.Grid grid)
        {
            var sand = Instantiate(m_SandPrefab, grid.transform.position, Quaternion.identity);

            m_CurrentColor = GetColorFromProgress(m_ColorChangeProgress);

            sand.SetColor(m_CurrentColor);

            sand.Row = grid.Row;
            sand.Column = grid.Column;
        }

        private Color GetColorFromProgress(float progress)
        {
            var gradient = new Gradient();
            gradient.colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(Color.red, 0f),
                new GradientColorKey(Color.yellow, 0.2f),
                new GradientColorKey(Color.green, 0.4f),
                new GradientColorKey(Color.cyan, 0.6f),
                new GradientColorKey(Color.blue, 0.8f),
                new GradientColorKey(Color.magenta, 1f)
            };
            gradient.alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            };

            return gradient.Evaluate(progress);
        }
    }
}
