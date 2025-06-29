using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak.UI
{
    public sealed class TransitionScreen : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _maskAmountParamName;

        private Material _material;
        private IProjectSettings _projectSettings;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
        }

        public IEnumerator Show(float duration)
        {
            duration *= _projectSettings.ScreenAnimationDuration;
            TryCacheMaterial();
            float time = 0.0f;
            while (time < duration)
            {
                yield return null;
                time += Time.deltaTime;
                float maskAmount = time / duration;
                _image.material.SetFloat(_maskAmountParamName, maskAmount);
            }
        }

        public IEnumerator Hide(float duration)
        {
            duration *= _projectSettings.ScreenAnimationDuration;
            TryCacheMaterial();
            float time = duration;
            while (time > 0.0f)
            {
                yield return null;
                time -= Time.deltaTime;
                float maskAmount =  time / duration;
                _image.material.SetFloat(_maskAmountParamName, maskAmount);
            }
        }

        private void TryCacheMaterial()
        {
            if (_material == null)
            {
                _material = new Material(_image.material);
                _image.material = _material;
            }
        }
    }
}
