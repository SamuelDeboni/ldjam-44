using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    Timer timer;
    
    [Export]
    PackedScene enemy;
    
    
    public override void _Ready()
    {
        timer = GetNode("Timer") as Timer;
        timer.Start();
    }

    public override void _Process(float delta)
    {
        if(timer.TimeLeft == 0 && enemy != null)
        {
            var enemyInstance = enemy.Instance() as KinematicBody2D;
            enemyInstance.Position = new Vector2(0,0);
            AddChild(enemyInstance);
            timer.Start();
        }
    }



}
