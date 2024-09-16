using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class BuilderNotificationManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] TMP_Text _text;

        [Header(H.Params)]
        [SerializeField][Min(0)] float _notificationDuration = 3f;
        [SerializeField][Min(0)] float _hideDuration = 0.5f;

        [SerializeField] Color _positiveNotification;
        [SerializeField] Color _neutralNotification;
        [SerializeField] Color _negativeNotification;

        private Coroutine _coroutine;

        private const string _noBlackKingNotification = "There must be one black king on the board.";
        private const string _moreThenOneBlackKingNotification = "There cannot be more than one black king on the board.";
        private const string _blackKingUnderAtackNotification = "The black king should not be under attack.";
        private const string _noWhiteFigureNotification = "There are no white pieces on the board.";
        private const string _noCardsInHandNotification = "There must be at least one card in your hand.";
        private const string _noLevelNameNotification = "Incorrect level name.";

        private readonly Dictionary<IncorrectPositionReason, string> positionNotifications = new() 
        {
            {IncorrectPositionReason.NoBlackKing, _noBlackKingNotification},
            {IncorrectPositionReason.MoreThenOneBlackKing, _moreThenOneBlackKingNotification},
            {IncorrectPositionReason.BlackKingUnderAttack, _blackKingUnderAtackNotification},
            {IncorrectPositionReason.NoWhiteFigures, _noWhiteFigureNotification},
            {IncorrectPositionReason.HandEmpty, _noCardsInHandNotification},
            {IncorrectPositionReason.NoLevelName, _noLevelNameNotification},
        };

        public void SwowNotification(IncorrectPositionReason reson, ButtonType type)
        {
            ShowNotification(positionNotifications[reson], type);
        }

        public void ShowNotification(string message, ButtonType type)
        {
            _text.color = GetColorOfType(type);
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(SendNotification(message));
        }

        private IEnumerator SendNotification(string message)
        {
            _text.text = message;
            yield return new WaitForSeconds(_notificationDuration);

            float timer = 0f;
            while (timer < _hideDuration) 
            { 
                _text.color = SetColorAlfa(_text.color, 1 - (timer/_hideDuration));
                timer += Time.deltaTime;
            }
            _text.color = SetColorAlfa(_text.color, 0);
            _coroutine = null;
        }

        private Color GetColorOfType(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.positive:
                    return _positiveNotification;
                case ButtonType.neutral:
                    return _neutralNotification;
                case ButtonType.negative:
                    return _negativeNotification;
                default:
                    return _neutralNotification;
            }
        }

        private Color SetColorAlfa(Color color, float alfa)
        {
            color.a = alfa;
            return color;
        }
    }
}
