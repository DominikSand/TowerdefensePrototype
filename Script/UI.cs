using Godot;
public partial class UI : CanvasLayer
{
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
		goldLabel.Text = $"Gold: {gold}";
		livesLabel.Text = $"Lives:  {lives}";
		startWaveButton.Pressed += OnStartWave;
	}

	private void OnStartWave()
	{
		EmitSignal(SignalName.StartWave);
	}

	// must be called via signal ? if enemy dies or gold is given
	public void onEnemyDied(int Reward)
	{
		gold += Reward;
		goldLabel.Text = $"Gold: {gold}";
	}

	public void onEnemyReachedGoal(int Health)
	{
		lives -= Health;
		livesLabel.Text = $"Lives: {lives}";
	}

	public void OnGameOver() 
	{
		startWaveButton.Disabled = true;
	}
}
