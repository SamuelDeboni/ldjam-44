using Godot;
using System;

public class RobotoWeapon : Node2D
{
    public Timer timer;
    bool isOverloaded = false;
    float overloadPercent;
    Timer overloadTimer;

    public float damage = 5;
    public float cooling = 40;  
    public float overload = 1;
    public int piercing = 1;

    public override void _Ready()
    {
        overloadTimer = GetNode("OverloadTimer") as Timer;
        timer = GetNode("Timer") as Timer;
    }

    public override void _Process(float delta)
    {
        if(overloadPercent > 0) overloadPercent -= delta*cooling;

        if(overloadPercent >= 100 && !isOverloaded)
            isOverloaded = true;
        else if(overloadPercent <= 0)
            isOverloaded = false;
        
        GetNode<ProgressBar>("../../HUD/OverloadBar").Value = overloadPercent;

        if(isOverloaded)
            GetNode<ProgressBar>("../../HUD/OverloadBar").Modulate = new Color(1,0,0);
        else
            GetNode<ProgressBar>("../../HUD/OverloadBar").Modulate = new Color(1,1,1);
    }
    public void shoot(Vector2 pos, Vector2 vel)
    {
        
        
        if(timer.TimeLeft == 0 && !isOverloaded)
        {
            var bullet = GD.Load<PackedScene>("res://Bullet.tscn"); 
            var bulletInstance = bullet.Instance() as Area2D;    
            GetParent().GetNode("..").AddChild(bulletInstance);
            var bulletScript = bulletInstance as Bullet;
            bulletScript.vel = vel;
            bulletScript.bulletDamage = damage;
            bulletScript.piercingLV = piercing;
            bulletInstance.Position = pos+vel*10+Position;
            timer.Start();

            overloadPercent += overload*20;
            
        }
        
    }
}
