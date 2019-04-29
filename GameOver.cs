using Godot;
using System;

public class GameOver : Control
{
 
    public override void _Ready()
    {
        GetNode<Label>("KillCount").Text = "Enemies Killed " + GetTree().GetRoot().GetNode<autoLoad>("autoLoad").killCount;
    }

    public void _on_Button_button_up()
    {
        GetTree().ChangeScene("res://Menu.tscn");
    }
}
