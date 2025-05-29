using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class GameTutorialShower : MonoBehaviour
    {
        [SerializeField] private TutorialView _tutorialView;

        private ITutorialManager _tutorialManager;
        private SignalBus _signalBus;
        private IReadOnlyList<TutorialPopupData> _currTutorial;
        private int _currTutorialIndex;
        private int _levelNumber;

        [Inject]
        public void Construct(SignalBus signalBus, ITutorialManager tutorialManager)
        {
            _tutorialManager = tutorialManager;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SignalLoadSystemLevel>(HandleLoadSystemLevel);
            _tutorialView.OnButtonClecked += ShowNextPopup;
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SignalLoadSystemLevel>(HandleLoadSystemLevel);
            _tutorialView.OnButtonClecked -= ShowNextPopup;
        }

        private void HandleLoadSystemLevel(SignalLoadSystemLevel ctx)
        {
            _levelNumber = ctx.LevelNumber;
            if(_tutorialManager.IsNeedToShowTutorial(_levelNumber))
            {
                _currTutorial = _tutorialManager.GetTutorial(_levelNumber).TutorialPopups;
                _currTutorialIndex = 0;
                _tutorialView.Show();
                ShowNextPopup();
            }
        }

        private void ShowNextPopup()
        {
            if(_currTutorialIndex >= _currTutorial.Count)
            {
                CompleteTutoeial();
                return;
            }
            Debug.Log("Show tutorial");
            _tutorialView.ShowTutorial(_currTutorial[_currTutorialIndex]);
            _currTutorialIndex++;
        }

        private void CompleteTutoeial()
        {
            _tutorialView.Hide();
            _currTutorial = null;
            _currTutorialIndex = 0;
            _tutorialManager.CompleteTutorial(_levelNumber);
        }
    }
}
