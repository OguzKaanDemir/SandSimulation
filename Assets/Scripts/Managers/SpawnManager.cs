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
        [SerializeField] private float colorChangeSpeed = 1f;

        private Color m_CurrentColor;
        private int colorStage; // Renk de�i�im a�amas�n� kontrol eden de�i�ken (0: k�rm�z�, 1: ye�il, 2: mavi)

        private void Start()
        {
            m_CurrentColor = new Color(1, 0, 0); // Ba�lang�� rengini parlak k�rm�z� olarak ayarl�yoruz.
            colorStage = 0; // Renk d�ng�s�n�n ba�lang�� a�amas�
        }

        public void SpawnSand(Piece.Grid grid)
        {
            var sand = Instantiate(m_SandPrefab, grid.transform.position, Quaternion.identity);

            // Renk ge�i�ini y�net
            m_CurrentColor = UpdateColor(m_CurrentColor, ref colorStage, colorChangeSpeed * Time.deltaTime);

            sand.SetColor(m_CurrentColor);

            sand.Row = grid.Row;
            sand.Column = grid.Column;
        }

        // Renk ge�i�lerini a�amal� bir �ekilde y�neten fonksiyon
        private Color UpdateColor(Color color, ref int stage, float speed)
        {
            switch (stage)
            {
                case 0: // K�rm�z�dan Ye�ile ge�i�
                    color.g += speed; // Ye�ili artt�r
                    if (color.g >= 1) // E�er ye�il 1'e (255'e) ula�t�ysa
                    {
                        color.g = 1;
                        stage = 1; // Sonraki a�amaya ge�
                    }
                    break;

                case 1: // Ye�ilden Maviye ge�i�
                    color.r -= speed; // K�rm�z�y� azalt
                    if (color.r <= 0) // E�er k�rm�z� 0'a (0'a) ula�t�ysa
                    {
                        color.r = 0;
                        stage = 2; // Sonraki a�amaya ge�
                    }
                    break;

                case 2: // Maviden K�rm�z�ya ge�i�
                    color.b += speed; // Maviyi artt�r
                    if (color.b >= 1) // E�er mavi 1'e (255'e) ula�t�ysa
                    {
                        color.b = 1;
                        stage = 3; // Sonraki a�amaya ge�
                    }
                    break;

                case 3: // Maviden K�rm�z�ya ge�i� (di�er bile�enler azal�rken)
                    color.g -= speed; // Ye�ili azalt
                    if (color.g <= 0) // E�er ye�il 0'a (0'a) ula�t�ysa
                    {
                        color.g = 0;
                        stage = 4; // Sonraki a�amaya ge�
                    }
                    break;

                case 4: // K�rm�z�ya ge�i� i�in haz�rl�k
                    color.r += speed; // K�rm�z�y� artt�r
                    if (color.r >= 1) // E�er k�rm�z� 1'e (255'e) ula�t�ysa
                    {
                        color.r = 1;
                        stage = 5; // Sonraki a�amaya ge�
                    }
                    break;

                case 5: // Maviyi azalt
                    color.b -= speed; // Maviyi azalt
                    if (color.b <= 0) // E�er mavi 0'a (0'a) ula�t�ysa
                    {
                        color.b = 0;
                        stage = 0; // D�ng�y� ba�a al
                    }
                    break;
            }

            return color;
        }
    }
}
