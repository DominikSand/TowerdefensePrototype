using Godot;

public partial class DebugConsole : CanvasLayer
{
	[Export] public Gamestate Gamestate;

	private MarginContainer _outerMarginContainer;
	private Panel _background;
	private MarginContainer _innerMarginContainer;
	private VBoxContainer _LogContainer;
	private RichTextLabel _LogEntryTemplate;




	public override void _Ready()
	{
		_outerMarginContainer = GetNode<MarginContainer>("OuterMarginContainer");
		_background = GetNode<Panel>("OuterMarginContainer/Panel");
		_innerMarginContainer = GetNode<MarginContainer>("OuterMarginContainer/InnerMarginContainer");
		_LogContainer = GetNode<VBoxContainer>("OuterMarginContainer/InnerMarginContainer/LogContainer");
		_LogEntryTemplate = GetNode<RichTextLabel>("OuterMarginContainer/InnerMarginContainer/LogContainer/LogEntryTemplate");
		_LogEntryTemplate.Text = "";
		_outerMarginContainer.Hide();
		GameEvents.Instance.Connect(GameEvents.SignalName.EnemyReachedGoal, new Callable(this, nameof(OnEnemyReachedGoal)));
		GameEvents.Instance.Connect(GameEvents.SignalName.SpawnEnemies, new Callable(this, nameof(OnSpawnEnemies)));	
    }

	public void OnEnemyDied(int Money)
	{
		if (Money > 0)
		{
			Print($"");
		}
	}

	public void OnEnemyReachedGoal(int Health)
	{
		Print($"Enemy reached goal with {Health} Health left");
	}

	public void OnGameOver() 
	{
		Print("You lost the game");
	}

	private void OnSpawnEnemies()
    {
        Print("Spawning enemies");
    }


    public void Print(string message)
	{
		if (!_outerMarginContainer.Visible)
		{
			_outerMarginContainer.Show();
		}
		_LogEntryTemplate.AppendText($"{System.DateTime.Now.ToString("T")} {message} {System.Environment.NewLine}");
	}

	public void Clear()
	{
		foreach (var child in _LogContainer.GetChildren())
		{
			if (child != _LogEntryTemplate)
				((Node)child).QueueFree();
		}
	}
}
