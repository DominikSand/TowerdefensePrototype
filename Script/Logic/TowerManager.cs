using Godot;
using System;

public partial class TowerManager : Node
{
    private TowerPlacer _towerPlacer;
    private PackedScene _ghostTower;
    private PackedScene _actualTower;


    public override void _Ready()
    {
        _towerPlacer = GetNode<TowerPlacer>("TowerPlacer");
        _ghostTower = GD.Load<PackedScene>("res://Scene/Gameobjects/GhostTower.tscn");
        _actualTower = GD.Load<PackedScene>("res://Scene/Gameobjects/Tower.tscn");
    }

    public void PlaceTower(string TowerType)
    {
        GD.Print($"TowerManager -> PlaceTower: {TowerType}");
        /*_towerPlacerInstance = TowerPlacer.Instantiate<TowerPlacer>();
        // required to add the TowerPlacer to the scene tree , otherwise all changes called on the instance will be lost.
        AddChild(_towerPlacerInstance);*/
        _towerPlacer.Setup(_ghostTower, _actualTower);
        _towerPlacer.StartPlacing();
    }

}
