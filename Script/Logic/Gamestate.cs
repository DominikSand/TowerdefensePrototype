using Godot;
using System;


/// <summary>
/// Class that holds state of game
/// </summary>
[GlobalClass]
public partial class Gamestate : Resource
{
    public int HitPoints
    {
        get { return _Hitpoints; }
        set
        {
            if (_Hitpoints != value)
            {
                _Hitpoints = value;
            }
        }
    }

    public int Score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
            }
        }
    }
    public int Money
    {
        get { return _Money; }
        set
        {
            if (_Money != value)
            {
                _Money = value;
                
            }
        }
    }
    public bool IsSpawning
    {
        get => _isSpawning;
        set
        {
            if (_isSpawning != value)
            {
                _isSpawning = value;
            }
        }
    }
    public int CurrentWave
    {
        get => _currentWave;
        set
        {
            if (_currentWave != value)
            {
                _currentWave = value;
                
            }
        }
    }
   
    private int _currentWave = 0;
    private int _Money = 500;
    private int _score = 0;
    private bool _isSpawning = false;
    private int _Hitpoints = 1000;
}