using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    Timer timer;
    Timer waveDuration;
    
    [Export]
    PackedScene enemy;

    Label waveText;

    int waveCounter = 0;
    
    public override void _Ready()
    {
        timer = GetNode("Timer") as Timer;
        waveDuration = GetNode("WaveDuration") as Timer;
        timer.Start();
        GD.Print("Started");
        waveText = GetNode("WaveLabel") as Label;
        
    }

    public override void _Process(float delta)
    {      
        
        if(timer.TimeLeft == 0 && enemy != null && waveDuration.TimeLeft <= 30 && waveDuration.TimeLeft > 5)
        {
            waveText.Text = "Wave " + (waveCounter+1);
            waves();
            GD.Print(waveDuration.TimeLeft);
        }
        else if(timer.TimeLeft == 0 && waveDuration.TimeLeft > 30)
        {
            GD.Print(waveDuration.TimeLeft-30);
            waveText.Text = Mathf.Round(waveDuration.TimeLeft-30).ToString();
        }
        
        if(waveDuration.TimeLeft == 0)
        {
            GetNode<Roboto>("Roboto").hp = 100;
            waveCounter++;
            waveDuration.Start();
            //timer.WaitTime *= 0.5f;
        }
    }

    void waves()
    {

        switch(waveCounter)
        {
            case 0:
                    spawn(enemy);
            break;

            case 1:
                    spawn(enemy);
            break;

            case 2:
                    spawn(enemy);
            break;

            case 3:
                    spawn(enemy);
            break;
        }
    }

    public void spawn(PackedScene enemy)
    {
                    var enemyInstance = enemy.Instance() as KinematicBody2D;
            
            float rand = (float)GD.Randf();
            if(rand > 0.75)
                enemyInstance.Position = new Vector2(0,-448);
            else if(rand > 0.5)
                enemyInstance.Position = new Vector2(0,448);
            else if(rand > 0.25)
                enemyInstance.Position = new Vector2(448,0);
            else
                enemyInstance.Position = new Vector2(-448,0);
            
            AddChild(enemyInstance);
            timer.Start();
    }
}
