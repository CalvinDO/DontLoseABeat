using Godot;
using System;

public class ThrownChair : RigidBody
{
    public float timeSinceThrow;
    public SimonsOrchestraManager OM;

    public AudioStreamPlayer player;

    public override void _Ready()
    {
        this.OM = GetNode<SimonsOrchestraManager>("/root/Root/OrchestraManager");
        this.player = (AudioStreamPlayer)GetChild(0);
        this.player.Autoplay = false;
        this.player.Play();
    }

    public override void _Process(float delta)
    {
        this.timeSinceThrow += delta;
        if (this.timeSinceThrow > this.OM.chairActiveTime)
        {
            this.QueueFree();
        }
    }
}
