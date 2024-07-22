using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class UI_ScaleOnEnablingAnimation : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _scaledTransform;

        [Header(H.Styles)]
        [SerializeField] private Vector3 _unvisibleScale;
        [SerializeField] private Vector3 _visibleScale;
        [SerializeField] private float _time = 1.0f;

        private Vector3 _targetScale;

        public void ToVisible() => _targetScale = _visibleScale;

        public void ToUnvisible() => _targetScale = _unvisibleScale;

        private void Update() => UpdateView();

        private void UpdateView()
        {
            float time = _time * Time.deltaTime;
            _scaledTransform.localScale = Vector3.Lerp(
                _scaledTransform.localScale,
                _targetScale,
                time);
        }
    }
}
