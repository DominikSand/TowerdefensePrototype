using Godot;
using System;

public partial class BuildControl : Control
{
    [Export]
    private Button _buildButton;
    [Export]
    private PopupMenu _buildPopupMenu;

    public override void _Ready()
    {
        _buildPopupMenu.Visible = false;
        // Populate the popup menu
        _buildPopupMenu.AddItem("Cannon Turret", 1);
        _buildPopupMenu.AddItem("Antiair Turret", 2);
        _buildPopupMenu.AddItem("Chaingun Turret", 3);

        // Connect signals
        _buildButton.Pressed += OnButtonPressed;
        _buildPopupMenu.IdPressed += OnMenuItemSelected;
    }

    private void OnButtonPressed()
    {
        _buildPopupMenu.Popup();
    }

    private void OnMenuItemSelected(long id)
    {
        switch (id)
        {
            case 1:
                GameEvents.Instance.EmitSignal(GameEvents.SignalName.TowerToPlaceSelected, "Cannon Turret");
                break;
            case 2:
                GameEvents.Instance.EmitSignal(GameEvents.SignalName.TowerToPlaceSelected, "Antiair Turret");
                break;
            case 3:
                GameEvents.Instance.EmitSignal(GameEvents.SignalName.TowerToPlaceSelected, "Chaingun Turret");
                break;
        }
    }
}
