using Godot;
using System;

public partial class TowerManager : Node
{
    private PackedScene TowerPlacer;
    private PackedScene GhostTower;
    private PackedScene ActualTower;
    private TowerPlacer _towerPlacerInstance;


    public override void _Ready()
    {
        TowerPlacer = GD.Load<PackedScene>("res://Scene/UI/TowerPlacer.tscn");
        GhostTower = GD.Load<PackedScene>("res://Scene/Gameobjects/GhostTower.tscn");
        ActualTower = GD.Load<PackedScene>("res://Scene/Gameobjects/Tower.tscn");
    }

    public void PlaceTower(string TowerType)
    {
        GD.Print($"TowerManager -> PlaceTower: {TowerType}");
        _towerPlacerInstance = TowerPlacer.Instantiate<TowerPlacer>();
        // required to add the TowerPlacer to the scene tree , otherwise all changes called on the instance will be lost.
        AddChild(_towerPlacerInstance);
        _towerPlacerInstance.Setup(GhostTower, ActualTower);
        _towerPlacerInstance.StartPlacing();
    }

}
