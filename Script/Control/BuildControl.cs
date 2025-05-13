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
        _buildPopupMenu.AddItem("Option 1", 1);
        _buildPopupMenu.AddItem("Option 2", 2);
        _buildPopupMenu.AddItem("Option 3", 3);

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
                GD.Print("Option 1 selected");
                break;
            case 2:
                GD.Print("Option 2 selected");
                break;
            case 3:
                GD.Print("Option 3 selected");
                break;
        }
    }
}
