using System.Collections;
using UnityEngine;

namespace UDK.ViewElements
{
    public class IntBarView : BarView<int>
    {
        public float fillPercent =>(float)value / (value.max - value.min);
        protected override IEnumerator DescendingAnimation()
        {
            float targetValue = fillPercent;
            if (depletionIndicator.fillAmount - tolerance < targetValue)
            {
                depletionIndicator.fillAmount = targetValue;
            }
            yield return new WaitForSeconds(animationDelay);

            while (depletionIndicator.fillAmount - tolerance > targetValue)
            {
                depletionIndicator.fillAmount = Mathf.Lerp(depletionIndicator.fillAmount, targetValue, animationSpeed * Time.deltaTime);
                Debug.Log(targetValue);
                yield return null;
            }
        }

        protected override void SetUpInitialValue()
        {
            valueBar.fillAmount = fillPercent;
            if (isAnimated)
            {
                depletionIndicator.fillAmount = valueBar.fillAmount;
            }
        }

        protected override void UpdateValueView(int newValue)
        {
            valueBar.fillAmount = fillPercent;
            if (isAnimated)
            {
                if(fillPercent > depletionIndicator.fillAmount)
                {
                    depletionIndicator.fillAmount = fillPercent;
                    return;
                }
                StartCoroutine(DescendingAnimation());
            }
        }
    }

}
