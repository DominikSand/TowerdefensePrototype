using Godot;
using System;
using System.Threading.Tasks;

public partial class GameController : Node
{
    [Export] public Gamestate Gamestate;

    [Signal]
    public delegate void TriggerSpawnEventHandler();

    public override void _Ready()
    {
        GameEvents.Instance.Connect(GameEvents.SignalName.StartWave, new Callable(this, nameof(StartWave)));
        GameEvents.Instance.Connect(GameEvents.SignalName.EnemyReachedGoal, new Callable(this, nameof(LoseHitpoints)));
    }

    public async void StartWave()
    {
        if (Gamestate.IsSpawning)
            return;
        Gamestate.CurrentWave += 1;
        Gamestate.IsSpawning = true;
        GD.Print("Gamecontroller -> OnWaveStarted");
        await SpawnWave(5);
        Gamestate.IsSpawning = false;
        GameEvents.Instance.EmitSignal(GameEvents.SignalName.SpawningChanged, false);
    }

    public void LoseHitpoints(int amount)
    {
        Gamestate.HitPoints -= amount;
        if (Gamestate.HitPoints <= 0)
            GameOver();
        GameEvents.Instance.EmitSignal(GameEvents.SignalName.HitpointsChanged, amount);
    }

    public void AddMoney(int amount)
    {
        Gamestate.Money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (Gamestate.Money >= amount)
        {
            Gamestate.Money -= amount;
            return true;
        }

        GD.Print("Not enough money!");
        return false;
    }

    public void GameOver()
    {
        GD.Print("Game Over from Gamecontroller!");
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
        EmitSignal(SignalName.TriggerSpawn);
    }

}