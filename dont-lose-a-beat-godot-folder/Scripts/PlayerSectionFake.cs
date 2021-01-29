using Godot;
using System;

public class PlayerSectionFake : Spatial
{
    private float timeSinceStart;
    private float bpm = 130;


    [Export]
    private MusicianController playerSectionSoundNode;

    public override void _Ready()
    {
        CollisionShape colsh = new CollisionShape();

        GD.Print("start ready erad");
        this.playerSectionSoundNode = GetNode<MusicianController>("PlayerSectionSound");
        GD.Print(playerSectionSoundNode);
    }

    public override void _Process(float delta)
    {
        this.timeSinceStart += delta;

        float bps = bpm / 60;

        this.Rotation = new Vector3(0, 0, (float)Math.Cos(this.timeSinceStart * Math.PI * bps));

        //float tempo = playerSectionSoundNode.currentTempo;
    }

    public void MouseEnteredRight()
    {

    }

    public void MouseEnteredLeft(Area area)
    {
        GD.Print(timeSinceStart);
    }
}