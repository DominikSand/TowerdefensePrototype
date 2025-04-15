using Godot;
using System;

public partial class Hpbar : Control
{
    [Export] private Gamestate Gamestate;
    private int _maxHp;
    private int _currentHp;

    public Sprite2D CurrentHpBar { get; private set; }

    public override void _Ready()
    {
        _maxHp = Gamestate.HitPoints;
        _currentHp = Gamestate.HitPoints;
        CurrentHpBar = GetNode<Sprite2D>("CurrentHp");
        GameEvents.Instance.Connect(GameEvents.SignalName.HitpointsChanged, new Callable(this, nameof(TakeDamage)));
    }

    /// <summary>
    /// Decreases the current hp by the amount given.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        if (amount < 0)
            return;
        _currentHp = Mathf.Max(_currentHp - amount, 0);
        UpdateBar();
    }

    private void UpdateBar()
    {
        float ratio = (float)_currentHp / _maxHp;
        var fullSize = CurrentHpBar.Texture.GetSize();
        var newWidth = fullSize.X * ratio;

        // Cut Hp Bar from Right to Left 
        // Coordninates need to be this way because inner hp bar starts at 0,12  in the Texture file.
        // The texture covers about 12 additional pixel therefore offset is 24
        CurrentHpBar.RegionRect = new Rect2(0, 12, newWidth, 24);
    }

    public void ResetHp()
    {
        _currentHp = _maxHp;
        UpdateBar();
    }
}
