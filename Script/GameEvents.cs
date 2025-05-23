using Godot;
using System;

/// <summary>
/// Basically Eventbus for the game.
/// </summary>
public partial class GameEvents : Node
{
    public override void _Ready()
    {
        Instance = this;
    }

    public static GameEvents Instance { get; private set; }

    #region UiEvents

    [Signal]
    public delegate void StartWaveEventHandler();
    [Signal]
    public delegate void SpawnEnemiesEventHandler();
    [Signal]
    public delegate void TowerToPlaceSelectedEventHandler(string towerType);

    #endregion


    #region GameStateEvents

    [Signal]
    public delegate void HitpointsChangedEventHandler(int newHitpoints);
    [Signal]
    public delegate void MoneyChangedEventHandler(int newMoney);
    [Signal]
    public delegate void ScoreChangedEventHandler(int newScore);
    [Signal]
    public delegate void SpawningChangedEventHandler(bool value);
    [Signal]
    public delegate void WaveChangedEventHandler(int value);
    [Signal]
    public delegate void GameOverEventHandler();

    #endregion

    #region GameControllerEvents

    [Signal]
    public delegate void NotEnoughMoneyToBuildTowerEventHandler();
    #endregion


    #region EnemyEvents

    [Signal]
    public delegate void EnemyDiedEventHandler(int reward);

    [Signal]
    public delegate void EnemyReachedGoalEventHandler(int damageTaken);

    #endregion

}
