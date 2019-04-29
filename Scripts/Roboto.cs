
using Godot;
using System;

public class Roboto : KinematicBody2D
{

    
    [Export]
    float maxVel = 250;
    
    public float hp = 100;

    RobotoWeapon weapon;

    float damageTimer = 1f;

    public override void _Ready()
    {
        weapon = GetNode("RobotoWeapon") as RobotoWeapon;
    }

    public override void _Process(float delta)
    {
        AnimatedSprite robotoSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        // Movment Controls
        Vector2 vel;
        vel.x = Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left");
        vel.y = Input.GetActionStrength("move_down")-Input.GetActionStrength("move_up");
        MoveAndCollide(vel*maxVel*delta);

        // Rotates the weapon
        Vector2 mousePos = GetGlobalMousePosition();
        weapon.LookAt(mousePos);

        // Flips the sprite
        bool fliped = mousePos.x - Position.x < 0 ? true : false;
        robotoSprite.FlipH = !fliped;
        weapon.GetNode<AnimatedSprite>("Sprite").FlipV = fliped;
        weapon.Position = new Vector2(fliped ? 6:-6,0);

        if(Input.IsActionPressed("shoot") && weapon != null)
        {    
            weapon.shoot(Position,(mousePos-Position).Normalized());
            weapon.GetNode<AnimatedSprite>("Sprite").Animation = "Shoting";

        }
        else
        {
            weapon.GetNode<AnimatedSprite>("Sprite").Animation = "Default";
        }

        var hpBar = GetNode("../HUD/HPBar") as TextureProgress;
        if(hpBar != null) hpBar.Value = hp;

        if(damageTimer > 0)
        {
            damageTimer -= delta;
            robotoSprite.SetAnimation("Damage");
        }
        else if(vel != new Vector2(0,0))
        {
            robotoSprite.SetAnimation("Walking");
        }
        else
        {
            robotoSprite.SetAnimation("Default");
        }
        
        
    }

    public void die()
    {
        GD.Print("The player is dead");
    }

    public void damage(float amount)
    {
        if (damageTimer <= 0)
        {
            if (hp <= 0) die();
            else if(hp >= amount) hp -= amount;
            else hp = 0;

            damageTimer = 1f;
        }
    }
}
