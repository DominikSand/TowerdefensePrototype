using Godot;
using System;


/// <summary>
/// Class that holds state of game and emits events in case of changed
/// </summary>
[GlobalClass]
public partial class Gamestate : Resource
{
    [Signal]
    public delegate void HitpointsChangedEventHandler(int newHitpoints);

    public int HitPoints
    {
        get { return _Hitpoints; }
        set
        {
            if (_Hitpoints != value)
            {
                _Hitpoints = value;
                EmitSignal(SignalName.HitpointsChanged, _Hitpoints);
            }
        }
    }


    [Signal]
    public delegate void MoneyChangedEventHandler(int newMoney);

    public int Money
    {
        get { return _Money; }
        set
        {
            if (_Money != value)
            {
                _Money = value;
                EmitSignal(SignalName.MoneyChanged, _Money);
            }
        }
    }


    [Signal]
    public delegate void ScoreChangedEventHandler(int newScore);

    public int Score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                EmitSignal(SignalName.ScoreChanged, _score);
            }
        }
    }

    [Signal]
    public delegate void SpawningChangedEventHandler(bool value);

    [Export]
    public bool IsSpawning
    {
        get => _isSpawning;
        set
        {
            if (_isSpawning != value)
            {
                _isSpawning = value;
                EmitSignal(SignalName.SpawningChanged, _isSpawning);
            }
        }
    }

    
    [Signal]
    public delegate void WaveChangedEventHandler(int value);
    
    [Export]
    public int CurrentWave
    {
        get => _currentWave;
        set
        {
            if (_currentWave != value)
            {
                _currentWave = value;
                EmitSignal(SignalName.WaveChanged, _currentWave);
            }
        }
    }
   
    private int _currentWave = 0;
    private int _Money = 0;
    private int _score = 0;
    private bool _isSpawning = false;
    private int _Hitpoints = 1000;
}