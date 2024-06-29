using UDK;
using UDK.ViewElements;
using UnityEngine;

public class BarTester : MonoBehaviour
{
    public BarView<int> bar;

    public int curentValue;
    public int minValue;
    public int maxValue;

    ClampedValue<int> value;

    private void Start()
    {
        Change();
    }

    public void add()
    {
        value.value += 1;
    }
    public void Decrease()
    {
        value.value -= 1;
    }

    public void Change()
    {
        value = new ClampedValue<int>(curentValue, maxValue, minValue);
        bar.SetNewValueForObservation(value);
    }
}
