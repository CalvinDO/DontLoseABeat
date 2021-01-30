using Godot;
using System;

public class PlayerSectionFake : Spatial
{
    private float timeSinceStart = 0;
    private float bpm = 130;

    private float currentAngle;
    private float angleLastFrame = 0;
    private float currentSpeed = 0;

    private bool mouseInsideRight = false;
    private bool mouseInsideLeft = false;

    private MusicianAudioStreamController playerSectionSoundNode;

    public float delta;

    [Export]
    public float angleAccelleration = 2;
    [Export]
    public Axis axis;

    public enum Axis
    {
        X, Y, Z
    }

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        this.delta = delta;
        this.timeSinceStart += delta;

        float bps = bpm / 60;

        this.currentAngle = (float)(Math.Sin(this.timeSinceStart * Math.PI * bps) * (Math.PI / 4));
        this.currentSpeed = this.currentAngle - this.angleLastFrame;
        this.angleLastFrame = this.currentAngle;

        this.Rotation = new Vector3();

        switch (this.axis)
        {
            case Axis.X:
                this.Rotation = new Vector3(currentAngle, 0, 0);
                break;
            case Axis.Y:
                this.Rotation = new Vector3(0, currentAngle, 0);
                break;
            case Axis.Z:
                this.Rotation = new Vector3(0, 0, currentAngle);
                break;
            default:
                break;
        }
    }

    public void AreaEntered(Area area, string name)
    {
        switch (name)
        {
            case "Left":
                GD.Print("Entered Right: " + timeSinceStart);
                break;
            case "Right":
                GD.Print("Entered Right: " + timeSinceStart);
                break;
            default:
                break;
        }
        this.IncrementBpm();
    }

    public void AreaExited(Area area, string name)
    {
        switch (name)
        {
            case "Left":
                GD.Print("Entered Right: " + timeSinceStart);
                break;
            case "Right":
                GD.Print("Entered Right: " + timeSinceStart);
                break;
            default:
                break;
        }
        this.IncrementBpm();
    }


    public void IncrementBpm()
    {
        GD.Print(this.currentSpeed);
        this.bpm += this.angleAccelleration * -1f * Math.Sign(this.currentSpeed) * delta;
    }
}