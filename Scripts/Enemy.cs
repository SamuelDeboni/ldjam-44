using Godot;
using System;

public class Enemy : KinematicBody2D
{
    float shootT = 0.5f;
    [Export]
    bool isRanged = false;
    public float hp = 20;
    public Roboto robotoScript;
    public KinematicBody2D roboto;

    public override void _Ready()
    {
        roboto = GetNode("../Roboto") as KinematicBody2D;
        robotoScript = roboto as Roboto;
    }

    public override void _Process(float delta)
    {
        shootT -= delta;
        Vector2 target = new Vector2();
        if(roboto != null)
             target = roboto.Position;
        else
            GD.Print("Roboto is null");
       
        Vector2 vel = (target - Position).Normalized() * delta * 150;
        if(!isRanged)
        {
            var collision = MoveAndCollide(vel);
            if(collision != null && collision.Collider is Roboto)
                 robotoScript.damage(10);
        }
        else
        {
            if(Position.DistanceTo(target) > 256)
                MoveAndCollide(vel);
            else if(shootT <= 0)
            {
                GetNode<EnemyWeapon>("EnemyWeapon").shoot(target);
                shootT = 0.5f;
            }
        }
    }

    public void die()
    {
        Main main = GetTree().GetRoot().GetNode("Main") as Main;
        main.enemyKilled++;
        
        if (roboto != null)
        {
            float dist = Position.DistanceTo(roboto.Position);
            float strength = Mathf.Clamp(40 - dist/10, 5, 20);
            CameraShake.Shake(strength);
        }
        

        QueueFree();
    }
    public void damage(float amount)
    {
        hp -= amount;
        if(hp <= 0) die();
    }
}
