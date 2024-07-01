using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public class DontDestroy : MonoBehaviour
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
