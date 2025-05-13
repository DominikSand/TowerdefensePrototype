using Godot;
using System;

public partial class TowerPlacer : Node2D
{
    private Node2D _ghostInstance; // The ghost instance of the tower being placed
    private PackedScene _actualTower; // The actual tower scene to be placed
    private bool _isPlacing = false;

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
                PlaceTower();
                GD.Print("Tower Placed");
            }
            else if (mouseEvent.ButtonIndex == MouseButton.Right)
            {
                //CancelPlacement();
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


    private void PlaceTower()
    {
        if (_ghostInstance == null || _actualTower == null)
            return;

        var towerInstance = _actualTower.Instantiate<Node2D>();
        
        towerInstance.Position = _ghostInstance.Position;
        towerInstance.ZIndex = 1;

        // Add to tower layer, could be GetParent(), or better: a dedicated tower container
        var gameRoot = GetTree().Root.GetNode("Main"); // or whatever your main game scene is called
        gameRoot.AddChild(towerInstance);
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
