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
        private int colorStage; // Renk deðiþim aþamasýný kontrol eden deðiþken (0: kýrmýzý, 1: yeþil, 2: mavi)

        private void Start()
        {
            m_CurrentColor = new Color(1, 0, 0); // Baþlangýç rengini parlak kýrmýzý olarak ayarlýyoruz.
            colorStage = 0; // Renk döngüsünün baþlangýç aþamasý
        }

        public void SpawnSand(Piece.Grid grid)
        {
            var sand = Instantiate(m_SandPrefab, grid.transform.position, Quaternion.identity);

            // Renk geçiþini yönet
            m_CurrentColor = UpdateColor(m_CurrentColor, ref colorStage, colorChangeSpeed * Time.deltaTime);

            sand.SetColor(m_CurrentColor);

            sand.Row = grid.Row;
            sand.Column = grid.Column;
        }

        // Renk geçiþlerini aþamalý bir þekilde yöneten fonksiyon
        private Color UpdateColor(Color color, ref int stage, float speed)
        {
            switch (stage)
            {
                case 0: // Kýrmýzýdan Yeþile geçiþ
                    color.g += speed; // Yeþili arttýr
                    if (color.g >= 1) // Eðer yeþil 1'e (255'e) ulaþtýysa
                    {
                        color.g = 1;
                        stage = 1; // Sonraki aþamaya geç
                    }
                    break;

                case 1: // Yeþilden Maviye geçiþ
                    color.r -= speed; // Kýrmýzýyý azalt
                    if (color.r <= 0) // Eðer kýrmýzý 0'a (0'a) ulaþtýysa
                    {
                        color.r = 0;
                        stage = 2; // Sonraki aþamaya geç
                    }
                    break;

                case 2: // Maviden Kýrmýzýya geçiþ
                    color.b += speed; // Maviyi arttýr
                    if (color.b >= 1) // Eðer mavi 1'e (255'e) ulaþtýysa
                    {
                        color.b = 1;
                        stage = 3; // Sonraki aþamaya geç
                    }
                    break;

                case 3: // Maviden Kýrmýzýya geçiþ (diðer bileþenler azalýrken)
                    color.g -= speed; // Yeþili azalt
                    if (color.g <= 0) // Eðer yeþil 0'a (0'a) ulaþtýysa
                    {
                        color.g = 0;
                        stage = 4; // Sonraki aþamaya geç
                    }
                    break;

                case 4: // Kýrmýzýya geçiþ için hazýrlýk
                    color.r += speed; // Kýrmýzýyý arttýr
                    if (color.r >= 1) // Eðer kýrmýzý 1'e (255'e) ulaþtýysa
                    {
                        color.r = 1;
                        stage = 5; // Sonraki aþamaya geç
                    }
                    break;

                case 5: // Maviyi azalt
                    color.b -= speed; // Maviyi azalt
                    if (color.b <= 0) // Eðer mavi 0'a (0'a) ulaþtýysa
                    {
                        color.b = 0;
                        stage = 0; // Döngüyü baþa al
                    }
                    break;
            }

            return color;
        }
    }
}
