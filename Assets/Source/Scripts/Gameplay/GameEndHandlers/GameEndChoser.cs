using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class GameEndChoser : MonoBehaviour
    {
        public List<GameEndHandler> endHandlers;


        [Inject]
        public void Construct(GameContextHolder gameContextHolder)
        {
            BaceGameEndContext ctx = gameContextHolder.LastGameContext;
        }
    }
}
