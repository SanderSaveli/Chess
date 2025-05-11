using UnityEngine;

namespace OFG.ChessPeak
{
    public class RotateUI : MonoBehaviour
    {
        [Tooltip("Скорость вращения в градусах в секунду")]
        public float rotationSpeed = 100f;

        void Update()
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
