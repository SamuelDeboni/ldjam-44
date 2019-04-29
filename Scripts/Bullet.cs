using Godot;
using System;

public class Bullet : Area2D
{

    public bool isFromEnemy;
    public Vector2 vel;
    public float bulletDamage = 5;

    [Export]
    float velMod = 10;

    public int piercingLV = 2;
    float t = 0;

    public override void _Process(float delta)
    {
        t += delta;
        
        if(t > 10) QueueFree();

        Position += vel*velMod*delta;
    }

    public void _on_Bullet_body_entered(PhysicsBody2D body)
    {
        if(body is Enemy)
        {
            var enemy = body as Enemy;
            enemy.damage(bulletDamage);
            piercingLV--;
        }
        if(piercingLV <= 0)
            QueueFree();
    }
}
