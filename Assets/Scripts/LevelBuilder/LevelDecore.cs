using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelDecore : MonoBehaviour
    {
        private Transform _transform;
        private const float _scaleForOneCell = 0.125f;

        private void OnEnable()
        {
            _transform = GetComponent<Transform>();
        }

        public void ScaleDecoreForFieldSize(Vector2Int fieldSize)
        {
            Vector3 newScale = Vector3.one;
            newScale.x = fieldSize.x * _scaleForOneCell;
            newScale.y = fieldSize.y * _scaleForOneCell * 2;
            newScale.z = fieldSize.y * _scaleForOneCell;
            _transform.localScale = newScale;
        }
    }
}
