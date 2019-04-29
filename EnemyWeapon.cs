using Godot;
using System;

public class EnemyWeapon : Sprite
{
    public void shoot(Vector2 target)
    {
        LookAt(target);
        //GD.Print("atirou");
    }
}
