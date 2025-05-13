using Godot;
using System;

public partial class Turret : Area2D
{
	[Export] public float FireRate = 1.0f;
	private float _cooldown = 0.0f;
    private PackedScene projectileScene;
    private const string ProjectilePath = "res://Scene/Gameobjects/Projectile.tscn";
    public override void _Ready()
    {
        projectileScene = GD.Load<PackedScene>(ProjectilePath);
    }

    public override void _Process(double delta)
	{
		_cooldown -= (float)delta;

		if (_cooldown <= 0)
		{
			var target = GetClosestEnemy();
			if (target != null)
			{
				FireAt(target);
				_cooldown = FireRate;
			}
		}
	}

	private Node2D GetClosestEnemy()
	{
		foreach (var body in GetOverlappingBodies())
		{
			if (body is Node2D node && node.IsInGroup("enemies"))
			{
				return node;
			}
		}
		return null;
	}

	private void FireAt(Node2D enemy)
	{
		var projectile = projectileScene.Instantiate<Node2D>();

		if (projectile == null)
			return;

		projectile.GlobalPosition = GlobalPosition;

		// Assuming your projectile script has a `public Node2D Target` property
		projectile.Set("target", enemy);

		GetTree().CurrentScene.AddChild(projectile);
	}
}
