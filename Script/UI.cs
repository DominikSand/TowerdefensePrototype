using Godot;

public partial class UI : CanvasLayer
{
    [Export]
    private Gamestate Gamestate;

    private Button startWaveButton;
    private Label goldLabel;
    private Label livesLabel;
    private int currentWave = 0;
    private int gold = 0;
    private int lives = 10;

    [Signal]
    public delegate void StartWaveEventHandler();


    public override void _Ready()
    {
        startWaveButton = GetNode<Button>("UIContainer/MarginContainer/VBoxContainer/StartWave");
        goldLabel = GetNode<Label>("UIContainer/MarginContainer/VBoxContainer/Gold");
        livesLabel = GetNode<Label>("UIContainer/MarginContainer/VBoxContainer/Lives");
        goldLabel.Text = $"Gold: {Gamestate.Money}";
        livesLabel.Text = $"Lives:  {Gamestate.HitPoints}";
        startWaveButton.Pressed += OnStartWave;
        Gamestate.Connect(Gamestate.SignalName.SpawningChanged, new Callable(this, nameof(onSpawningChanged)));
        Gamestate.Connect(Gamestate.SignalName.MoneyChanged, new Callable(this, nameof(onEarnedMoney)));
    }

    private void OnStartWave()
    {
        Gamestate.IsSpawning = true;
        startWaveButton.Disabled = true;
    }

    private void onSpawningChanged(bool isSpawning)
    {
        if (!isSpawning)
        {
            startWaveButton.Disabled = false;
        }
    }

    // must be called via signal ? if enemy dies or gold is given
    public void onEarnedMoney(int Reward)
    {
        goldLabel.Text = $"Gold: {Gamestate.Money}";
    }


    public void onEnemyReachedGoal(int Health)
    {
        Gamestate.HitPoints -= Health;
        livesLabel.Text = $"Hitpoints: {Gamestate.HitPoints}";
    }

    public void OnGameOver()
    {
        startWaveButton.Disabled = true;
    }
}