using Godot;
using System;

/// <summary>
/// Proectile class. Spawns with a target and moves towards it.
/// </summary>
public partial class Projectile : Area2D
{
    [Export] 
    public float Speed;
    [Export]
    public int Damage;
    private Node2D Target;

    public override void _Process(double delta)
    {
        if (Target == null || !IsInstanceValid(Target))
        {
            QueueFree();
            return;
        }

        Vector2 direction = (Target.GlobalPosition - GlobalPosition).Normalized();
        GlobalPosition += direction * Speed * (float)delta;

        // Projectile Connects with target
        if (GlobalPosition.DistanceTo(Target.GlobalPosition) < 10f)
        {
            if (Target.GetParent() is Enemy enemy)
            {
                enemy.TakeDamage(Damage);
            }
            QueueFree();
        }
    }
}
