using Godot;
using System;

public partial class TowerPlacer : Node
{
    [Export]
    public Gamestate Gamestate; // Reference to the game state
    private Node2D _ghostInstance; // The ghost instance of the tower being placed
    private PackedScene _actualTower; // The actual tower scene to be placed
    private bool _isPlacing = false;

    public override void _Ready()
    {
        base._Ready();
    }

    public void Setup(PackedScene ghostScene, PackedScene actualScene)
    {
        _ghostInstance?.QueueFree(); // Cleanup if needed
        _ghostInstance = ghostScene.Instantiate<Node2D>();
        AddChild(_ghostInstance);
        _actualTower = actualScene;
    }

    public void StartPlacing()
    {
        _isPlacing = true;
        _ghostInstance.Show();
        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }

    public override void _Process(double delta)
    {
        if (_isPlacing && _ghostInstance != null)
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            _ghostInstance.Position = SnapToGrid(mousePos);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (!_isPlacing || _ghostInstance == null)
            return;
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (PlaceTower())
                {
                    GD.Print("Tower Placed");
                }
                else
                {
                    GD.Print("Not enough money to place tower");
                }
            }
            else if (mouseEvent.ButtonIndex == MouseButton.Right)
            {
                GD.Print("Placing Canceled");
            }
            Cleanup();
        }
    }

    private Vector2 SnapToGrid(Vector2 pos, int gridSize = 32)
    {
        return new Vector2(
            Mathf.Floor(pos.X / gridSize) * gridSize + gridSize / 2,
            Mathf.Floor(pos.Y / gridSize) * gridSize + gridSize / 2
        );
    }


    private bool PlaceTower()
    {
        if (_ghostInstance == null || _actualTower == null)
            return false;

        var towerInstance = _actualTower.Instantiate<Turret>();
        if (Gamestate.Money < towerInstance.Price)
        {
            GameEvents.Instance.EmitSignal(GameEvents.SignalName.NotEnoughMoneyToBuildTower);
            towerInstance.Free();
            return false;
        }
        else
        {
            GameEvents.Instance.EmitSignal(GameEvents.SignalName.MoneyChanged, -towerInstance.Price);
            towerInstance.Position = _ghostInstance.Position;
            towerInstance.ZIndex = 1;
            // Add to tower layer, could be GetParent(), or better: a dedicated tower container
            var gameRoot = GetTree().Root.GetNode("Main");
            gameRoot.AddChild(towerInstance);
            return true;
        }
    }

    private void Cleanup()
    {
        _ghostInstance?.QueueFree();
        _ghostInstance = null;
        _actualTower = null;
        _isPlacing = false;
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
}
