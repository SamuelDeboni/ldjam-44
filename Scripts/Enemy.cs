using Godot;
using System;

public class Enemy : KinematicBody2D
{
    public float hp = 10;
    public Roboto robotoScript;
    public KinematicBody2D roboto;

    public override void _Ready()
    {
        roboto = GetNode("../Roboto") as KinematicBody2D;
        robotoScript = roboto as Roboto;
    }

    public override void _Process(float delta)
    {
        Vector2 target = new Vector2();
        if(roboto != null)
            target = roboto.Position;
        else
            GD.Print("Roboto is null");
        
        Vector2 vel = (target - Position).Normalized() * delta * 100;
        
        var collision = MoveAndCollide(vel);
        if(collision != null)
        {
            if(collision.Collider is Roboto)
                robotoScript.damage(5);

        }

        
    }

    public void die()
    {
        QueueFree();
    }
    public void damage(float amount)
    {
        hp -= amount;
        if(hp <= 0) die();
    }
}
