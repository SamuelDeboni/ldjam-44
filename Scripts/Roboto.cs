using Godot;
using System;

public class Roboto : KinematicBody2D
{

    
    [Export]
    float maxVel = 250;
    
    float hp = 100;

    RobotoWeapon weapon;

    float damageTimer = 0.5f;

    public override void _Ready()
    {
        weapon = GetNode("RobotoWeapon") as RobotoWeapon;
    }

    public override void _Process(float delta)
    {
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
        GetNode<AnimatedSprite>("AnimatedSprite").FlipH = fliped;
        weapon.GetNode<Sprite>("Sprite").FlipV = fliped;
        weapon.Position = new Vector2(fliped ? 6:-6,0);

        if(Input.IsActionPressed("shoot") && weapon != null)
        {    
            weapon.shoot(Position,(mousePos-Position).Normalized());
        }

        var hpBar = GetNode("../HUD/HPBar") as TextureProgress;
        if(hpBar != null) hpBar.Value = hp;

        if(damageTimer > 0)
            damageTimer -= delta;
    }

    public void die()
    {

    }

    public void damage(float amount)
    {
        if (damageTimer <= 0)
        {
            if (hp <= 0) die();
            else if(hp >= amount) hp -= amount;
            else hp = 0;

            damageTimer = 0.5f;
        }
    }
}
