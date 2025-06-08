using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class addValute : MonoBehaviour
    {
        [SerializeField] private int add;

        private IValuteManager _valuteManager;

        [Inject]
        public void Construct(IValuteManager valuteManager)
        {
            _valuteManager = valuteManager;
        }

        public void Add()
        {
            Debug.Log("Add " + add);
            _valuteManager.AddValute(add);
        }
    }
}
