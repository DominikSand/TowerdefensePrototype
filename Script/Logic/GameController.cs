using Godot;
using System;

public partial class GameController : Node
{
    [Export] public Gamestate Gamestate;

    public override void _Ready()
    {

    }

    public void StartWave()
    {
        if (Gamestate.IsSpawning)
            return;

        Gamestate.CurrentWave += 1;
        Gamestate.IsSpawning = true;
    }

    public void OnSpawnerFinished()
    {
        Gamestate.IsSpawning = false;
    }

    public void LoseHitpoints(int amount)
    {
        Gamestate.HitPoints -= amount;
        if (Gamestate.HitPoints <= 0)
            GameOver();
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
}