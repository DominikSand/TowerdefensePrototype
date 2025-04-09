using Godot;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Main : Node2D
{
    //required for dynamic instancing
    [Export] private PackedScene EnemyScene;

    [Export] public Gamestate Gamestate;

    [Signal]
    public delegate void GameoverEventHandler();

    [Signal]
    public delegate void SpawnEnemiesEventHandler();

    private Node2D _enemyPath;
    private DebugConsole _debugConsole;
    private UI _ui;

    public override void _Ready()
    {
        _enemyPath = GetNode<Node2D>("EnemyPath");
        _debugConsole = GetNode<DebugConsole>("DebugConsole");
        _ui = GetNode<UI>("UI");
        // Gamestate emitiert signal , wir reagieren darauf mit onWaveStarted
        Gamestate.Connect(Gamestate.SignalName.SpawningChanged, new Callable(this, nameof(OnWaveStarted)));
        // Main emitiert singal, ui muss reagieren.
        Connect(SignalName.Gameover, new Callable(_ui, nameof(UI.OnGameOver)));
        Connect(SignalName.Gameover, new Callable(_debugConsole, nameof(DebugConsole.OnGameOver)));
        AddToGroup("game");
    }

    public async void OnWaveStarted(bool spawning)
    {
        if (spawning)
        {
            GD.Print("Main -> OnWaveStarted");
            _debugConsole.Print("WaveStarted");
            await SpawnWave(5);
            Gamestate.IsSpawning = false;
        }
    }

    private async Task SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await ToSignal(GetTree().CreateTimer(i * 0.5), SceneTreeTimer.SignalName.Timeout);
            SpawnEnemy(i);
        }
    }

    private void SpawnEnemy(int i)
    {
        EmitSignal(SignalName.SpawnEnemies);
        _debugConsole.Print($"Enemy {i} spawned");
    }


    private void GameOver()
    {
        GetTree().Paused = true;
        GD.Print("Game Over");
    }
}