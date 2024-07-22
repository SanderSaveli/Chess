using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class FigureView : MonoBehaviour
    {
        [Header(H.Params)]
        [SerializeField] private float _yOffsetOnSelected;
        [SerializeField] private float _yOffsetOnDefeated;
        [SerializeField] private float _positionSmoothTime = 0.5f;

        public Vector3 TargetPosition { get; private set; }

        private Vector3 _positionVelocity;

        public void Defeat()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yOffsetOnDefeated;
            TargetPosition = newTargetPosition;
        }

        public void MoveTo(Vector3 position3)
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.x = position3.x;
            newTargetPosition.z = position3.z;
            TargetPosition = newTargetPosition;
        }

        public void Up()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y += _yOffsetOnSelected;
            TargetPosition = newTargetPosition;
        }

        public void Down()
        {
            Vector3 newTargetPosition = TargetPosition;
            newTargetPosition.y -= _yOffsetOnSelected;
            TargetPosition = newTargetPosition;
        }

        private void Awake() => TargetToAwakePosition();

        private void Update() => DampPosition();

        private void TargetToAwakePosition() => TargetPosition = transform.position;

        private void DampPosition() => transform.position = Vector3.SmoothDamp(
                transform.position,
                TargetPosition,
                ref _positionVelocity,
                _positionSmoothTime);
    }
}
