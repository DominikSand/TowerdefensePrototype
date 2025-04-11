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

    

    private Node2D _enemyPath;
    private DebugConsole _debugConsole;
    private UI _ui;

    public override void _Ready()
    {
        _enemyPath = GetNode<Node2D>("EnemyPath");
        _debugConsole = GetNode<DebugConsole>("DebugConsole");
        _ui = GetNode<UI>("UI");
        // Main emitiert singal, ui muss reagieren.
        Connect(SignalName.Gameover, new Callable(_ui, nameof(UI.OnGameOver)));
        Connect(SignalName.Gameover, new Callable(_debugConsole, nameof(DebugConsole.OnGameOver)));
        AddToGroup("game");
    }

    


    private void GameOver()
    {
        GetTree().Paused = true;
        GD.Print("Game Over");
    }
}