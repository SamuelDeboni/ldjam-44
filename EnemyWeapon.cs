using Godot;
using System;

public class EnemyWeapon : Sprite
{
    public void shoot(Vector2 target)
    {
        LookAt(target);
        var bullet = GD.Load<PackedScene>("res://Bullet.tscn"); 
        var bulletInstance = bullet.Instance() as Area2D;    
        GetParent().GetNode("..").AddChild(bulletInstance);
        var bulletScript = bulletInstance as Bullet;
        bulletInstance.Position = GetNode<KinematicBody2D>("..").Position;
        Vector2 vel = GetGlobalTransform().x.Normalized();
        bulletScript.isFromEnemy = true;
        bulletScript.vel = vel;
		//GD.Print(GetGlobalTransform().x);
    }
}
