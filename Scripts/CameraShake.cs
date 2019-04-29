using Godot;
using System;

public class CameraShake : Camera2D
{
    static CameraShake instance;

    float strength;
    float dropoff = 0.5f;
    float maxStrength = 100f;
    
    public override void _Ready()
    {
        instance = this;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (strength > 0.1f)
        {
            this.Offset = RandomInsideUnitCircle() * strength;
            strength *= dropoff;
        }
        else
        {
            strength = 0f;
            this.Offset = new Vector2(0, 0);
        }
    }
    
    private Vector2 RandomInsideUnitCircle()
    {
        float r = Mathf.Sqrt((float)GD.RandRange(0, 1));
        float theta = (float)GD.RandRange(0, 1) * 2 * Mathf.Pi;
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;
    }

    private void _Shake(float intensity)
    {
        strength += intensity;
        strength = Mathf.Clamp(strength, 0, maxStrength);
    }
    
    public static void Shake(float intensity)
    {
        if (instance != null)
            instance._Shake(intensity);
        else
            GD.Print("Trying to shake, but there's no camera");
    }
}
