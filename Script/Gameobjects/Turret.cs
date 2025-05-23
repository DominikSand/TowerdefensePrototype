using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Turret : Area2D
{
    [Export]
    public float FireRate = 1.0f;
    public int Price = 100;
    private float _cooldown = 0.01f;
    private List<Node2D> _enemies = new List<Node2D>();

    private PackedScene projectileScene;
    private const string ProjectilePath = "res://Scene/Gameobjects/Projectile.tscn";
    public override void _Ready()
    {
        projectileScene = GD.Load<PackedScene>(ProjectilePath);
        this.BodyEntered += OnBodyEntered;
        this.BodyExited += OnBodyExited;
    }

    public override void _Process(double delta)
    {
        _cooldown -= (float)delta;

        if (_cooldown <= 0)
        {
            if (_enemies.Count > 0)
            {
                var target = _enemies[0];
                if (target != null)
                {
                    FireAt(target);
                    _cooldown = FireRate;
                }
            }
        }

    }

    public void OnBodyEntered(Node2D body)
    {
        _enemies.Add(body);
    }

    public void OnBodyExited(Node2D body)
    {
        _enemies.Remove(body);
    }


    private void FireAt(Node2D enemy)
    {
        var projectile = projectileScene.Instantiate<Projectile>();

        if (projectile == null)
            return;

        // correct would be sprite 
        projectile.GlobalPosition = GlobalPosition;

        // Assuming your projectile script has a `public Node2D Target` property
        projectile.Set("Target", enemy);

        GetTree().CurrentScene.AddChild(projectile);
    }
}
