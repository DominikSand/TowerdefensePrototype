using Godot;

public partial class EnemySpawner : Timer
{
	private PackedScene _enemyScene;
	private Node2D _main;

	public override void _Ready()
	{
		_enemyScene = GD.Load<PackedScene>("res://Scene/Gameobjects/Enemy.tscn");
		_main = GetNode<Node2D>("/root/Main");
		if(_main == null)
		{
			throw new System.Exception("Node not found");
		}
		// Main imitiert , und wir fangen
		_main.Connect(Main.SignalName.SpawnEnemies, new Callable(this, nameof(OnSpawnEnemy)));
	}

	public void OnSpawnEnemy()
	{
		var enemy = _enemyScene.Instantiate<PathFollow2D>();
		enemy.Progress = 0.5f; // start at beginning of the path
		var path = GetParent(); // this is your Path2D
		if (path is Path2D)
		{
			GD.Print("Valid node , spawning");
		}
		else 
		{
			GD.Print($"invalid node , type {path.GetType().Name}");
		}
		path.AddChild(enemy);
	}
}
