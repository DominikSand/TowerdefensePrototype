using Godot;
using static Godot.PathFollow3D;
using static Godot.XRServer;

public partial class Enemy : PathFollow2D
{
	[Export] public float Speed = 100f; // pixels per second
	[Export] public int Health = 10;
	[Export] public int Reward = 15;

    private AnimatedSprite2D animatedSprite;


    public override void _Ready()
	{
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play("Drive");      
    }

	/// <summary>
	/// If enemy reaches goal damage is taken corresponding to the amount of Health
	/// </summary>
	/// <param name="delta"></param>
	public override void _Process(double delta)
	{
		
		var _currentPosition = Progress;
		Progress = _currentPosition + (float)(Speed * delta);

        if (ProgressRatio >= 1.0f)
		{
			GameEvents.Instance.EmitSignal(GameEvents.SignalName.EnemyReachedGoal, Health);
			// Todo: Death animation ?
			QueueFree();
		}
	}

	public void TakeDamage(int amount)
	{
		Health -= amount;
		if (Health <= 0)
		{
			GameEvents.Instance.EmitSignal(GameEvents.SignalName.EnemyDied, Reward);
			// Todo: Death Animation ?
			QueueFree();
		}
	}
}
