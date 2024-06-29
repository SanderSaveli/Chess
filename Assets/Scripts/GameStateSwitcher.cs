using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Pause,
    CardDrawing,
    WhiteFigureMove,
    CellAction,
    BlackMove
}

public class GameStateSwitcher : MonoBehaviour
{
    public Action<GameState> OnGameStateChange;


}
