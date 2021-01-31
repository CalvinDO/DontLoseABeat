using Godot;
using System;

public class ThrownChair : RigidBody
{
    public float timeSinceThrow;
    public SimonsOrchestraManager OM;
    public override void _Ready()
    {
        this.OM = GetParent<SimonsOrchestraManager>();
    }

    public override void _Process(float delta)
    {
        this.timeSinceThrow += delta;
        if (this.timeSinceThrow > this.OM.chairActiveTime)
        {

        }
    }
}
