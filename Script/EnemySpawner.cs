using Godot;

public partial class EnemySpawner : Timer
{
	private PackedScene _enemyScene;
	private Node _gameController;

    [Export] public Gamestate Gamestate;

    public override void _Ready()
	{
		_enemyScene = GD.Load<PackedScene>("res://Scene/Gameobjects/Enemy.tscn");
		_gameController = GetNode<Node>("../../GameController");
		if(_gameController == null)
		{
			throw new System.Exception("Gamecontroller Node not found");
		}
		_gameController.Connect("TriggerSpawn", new Callable(this, nameof(OnSpawnEnemy)));
	}

	public void OnSpawnEnemy()
	{
		var enemy = _enemyScene.Instantiate<PathFollow2D>();
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
