using Godot;
using System;

public class RobotoWeapon : Node2D
{
    
    public void shoot(Vector2 pos, Vector2 vel)
    {
        var timer = GetNode("Timer") as Timer;
        
        if(timer.TimeLeft == 0)
        {
            var bullet = GD.Load<PackedScene>("res://Bullet.tscn"); 
            var bulletInstance = bullet.Instance() as Area2D;    
            GetParent().GetNode("..").AddChild(bulletInstance);
            var bulletScript = bulletInstance as Bullet;
            bulletScript.vel = vel;
            bulletInstance.Position = pos;
            timer.Start();
        }
        
    }
}
