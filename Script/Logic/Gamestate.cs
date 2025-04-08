using Godot;
using System;

public partial class Gamestate : Node
{
	[Signal]
	public delegate void LivesChangedEventHandler(int newLives);

	[Signal]
	public delegate void MoneyChangedEventHandler(int newMoney);

	[Signal]
	public delegate void ScoreChangedEventHandler(int newScore);

	[Signal]
	public delegate void GameStateReadyEventHandler();

	public int Lives { get; private set; } = 10;
	public int Money { get; private set; } = 100;
	public int Score { get; private set; } = 0;

	public override void _Ready()
	{
		GD.Print("GameState ready.");
	}

	public void LoseLife(int amount = 1)
	{
		Lives -= amount;
		EmitSignal(SignalName.LivesChanged, Lives);

		GD.Print($"Life lost! Remaining lives: {Lives}");

		if (Lives <= 0)
			GameOver();
	}

	public void AddMoney(int amount)
	{
		Money += amount;
		EmitSignal(SignalName.MoneyChanged, Money);
	}

	public bool SpendMoney(int amount)
	{
		if (Money >= amount)
		{
			Money -= amount;
			EmitSignal(SignalName.MoneyChanged, Money);
			return true;
		}

		GD.Print("Not enough money!");
		return false;
	}

	public void AddScore(int amount)
	{
		Score += amount;
		EmitSignal(SignalName.ScoreChanged, Score);
	}

	private void GameOver()
	{
		GD.Print("Game Over!");
		
		// Handle game over logic here (pause, show menu, etc.)
	}
}
