using Godot;
using System;
using System.Collections.Generic;

public class Main : Node2D
{
    public int enemyKilled;
    Timer timer;
    public Timer waveDuration;
    
    [Export]
    PackedScene enemy;
    
    [Export]
    PackedScene enemy2;

    Label waveText;
    public bool stoped = false;

    int waveCounter = 0;

    public bool lastWave = false;
    
    public override void _Ready()
    {
        timer = GetNode("Timer") as Timer;
        waveDuration = GetNode("WaveDuration") as Timer;
        timer.Start();
        GD.Print("Started");
        waveText = GetNode("HUD/WaveLabel") as Label;
        GetTree().GetRoot().GetNode<autoLoad>("autoLoad").killCount = enemyKilled;
    }

    public bool hasEnemy()
    {
        foreach(var e in GetChildren())
        {
            if(e is Enemy)
                return true;
            //GD.Print(e);
        }
        return false;
    }
    public override void _Process(float delta)
    {      
        GetNode<Label>("HUD/KillCount").Text = "Enemies Killed " + enemyKilled.ToString();
        GetTree().GetRoot().GetNode<autoLoad>("autoLoad").killCount = enemyKilled;

        if(timer.TimeLeft == 0 && enemy != null && waveDuration.TimeLeft <= 40 && waveDuration.TimeLeft > 5)
        {
            waveText.Text = "Wave " + (waveCounter+1);
            waves();
            //GD.Print(waveDuration.TimeLeft);
        }
        else if(timer.TimeLeft == 0 && waveDuration.TimeLeft > 40)
        {
            //GD.Print(waveDuration.TimeLeft-30);
            waveText.Text = Mathf.Round(waveDuration.TimeLeft-40).ToString();
        }
        
        if(waveDuration.TimeLeft == 0 && !stoped && !hasEnemy())
        {
            stoped = true;
            GetNode<Roboto>("Roboto").hp = 100;
            waveCounter++;
            //waveDuration.Start();
            timer.WaitTime *= 0.8f;
            waveText.Text = "Wave complete\nGo to the center";
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
                    if(GD.Randf() > 0.2f)
                        spawn(enemy);
                    else
                        spawn(enemy2);
            break;

            case 2:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);
            break;

            case 3:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 4:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 5:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 6:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 7:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 8:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 9:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 10:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 12:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);;
            break;

            case 13:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);
            break;

            case 14:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);
            break;

            case 15:
                    if(GD.Randf() > 0.25f)
                        spawn(enemy);
                    else
                        spawn(enemy2);

                    lastWave = true;
                    waveDuration.Start();
                     waveText.Text = "∞";
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
