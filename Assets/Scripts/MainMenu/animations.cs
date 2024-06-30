using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.Chess
{
    public class animations : MonoBehaviour
    {
        public Animator anim;

        public void PlayAnim()
        {
            anim.SetTrigger("Play");
        }
    }
}
