using Godot;

public partial class UI : CanvasLayer
{
    [Export]
    private Gamestate Gamestate;
  
    [Export]
    private Button startWaveButton;

    [Export]
    private Label goldLabel;

    private int currentWave = 0;


    [Signal]
    public delegate void StartWaveEventHandler();




    public override void _Ready()
    {
        goldLabel.Text = $"Gold: {Gamestate.Money}";      
        startWaveButton.Pressed += OnStartWave;
        GameEvents.Instance.Connect(GameEvents.SignalName.SpawningChanged, new Callable(this, nameof(onSpawningChanged)));
        GameEvents.Instance.Connect(GameEvents.SignalName.MoneyChanged, new Callable(this, nameof(onEarnedMoney)));
        GameEvents.Instance.Connect(GameEvents.SignalName.EnemyReachedGoal, new Callable(this, nameof(onEnemyReachedGoal)));
        GameEvents.Instance.Connect(GameEvents.SignalName.GameOver, new Callable(this, nameof(onGameOver)));
        GameEvents.Instance.Connect(GameEvents.SignalName.NotEnoughMoneyToBuildTower, new Callable(this, nameof(onNotEnoughMoneyToBuildTower)));
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
        GD.Print("UI -> Hitpoints Changed");
    }

    public void onGameOver()
    {
        startWaveButton.Disabled = true;
        GD.Print("UI -> Gameover Received");
    }

    public void onNotEnoughMoneyToBuildTower()
    {
        GD.Print("UI -> Not enough money to build tower");
        // Show a message or some UI feedback
    }
}