using Godot;

public partial class UI : CanvasLayer
{
    [Export]
    private Gamestate Gamestate;

    private Button startWaveButton;
    private Label goldLabel;
    private int currentWave = 0;
    private int gold = 0;
    private int lives = 10;


    [Signal]
    public delegate void StartWaveEventHandler();




    public override void _Ready()
    {
        startWaveButton = GetNode<Button>("UIContainer/MarginContainer/VBoxContainer/Start/StartWave");
        goldLabel = GetNode<Label>("UIContainer/MarginContainer/VBoxContainer/Gold/GoldLabel");
        goldLabel.Text = $"Gold: {Gamestate.Money}";      
        startWaveButton.Pressed += OnStartWave;
        GameEvents.Instance.Connect(GameEvents.SignalName.SpawningChanged, new Callable(this, nameof(onSpawningChanged)));
        GameEvents.Instance.Connect(GameEvents.SignalName.MoneyChanged, new Callable(this, nameof(onEarnedMoney)));
        GameEvents.Instance.Connect(GameEvents.SignalName.EnemyReachedGoal, new Callable(this, nameof(onEnemyReachedGoal)));
    }

    private void OnStartWave()
    {
        GameEvents.Instance.EmitSignal(GameEvents.SignalName.StartWave);
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
    }

    public void OnGameOver()
    {
        startWaveButton.Disabled = true;
    }
}