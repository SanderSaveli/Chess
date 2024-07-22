using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class SelectionView : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header(H.Params)]
        [SerializeField] private Color _colorNone;
        [SerializeField] private Color _colorCursor;
        [SerializeField] private Color _colorMovement;
        [SerializeField] private Color _colorAttack;
        [SerializeField] private Color _colorCanMove;

        public SelectionType SelectionType { get; private set; }

        public void ResetSelection() => SetSelection(SelectionType.None);

        public void SetSelection(SelectionType selectionType)
        {
            SelectionType = selectionType;
            _spriteRenderer.color = ChooseColor(SelectionType);
        }

        private Color ChooseColor(SelectionType selectionType)
        {
            return selectionType switch
            {
                SelectionType.None => _colorNone,
                SelectionType.Cursor => _colorCursor,
                SelectionType.Movement => _colorMovement,
                SelectionType.Attack => _colorAttack,
                SelectionType.CanMove => _colorCanMove,
                _ => throw new NotImplementedException()
            };
        }
    }
}
