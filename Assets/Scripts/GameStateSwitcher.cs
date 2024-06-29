using System;
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
