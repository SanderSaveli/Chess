using UnityEngine.UI;

namespace IUP.Toolkit
{
    public static class ExtensionsSlider
    {
        public static bool IsMinValue(this Slider slider) => slider.value == slider.minValue;

        public static bool IsNotMinValue(this Slider slider) => slider.value != slider.minValue;

        public static bool IsMaxValue(this Slider slider) => slider.value == slider.maxValue;

        public static bool IsNotMaxValue(this Slider slider) => slider.value != slider.maxValue;
    }
}
