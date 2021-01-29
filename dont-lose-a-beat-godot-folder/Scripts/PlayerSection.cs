using Godot;
using System;

public class PlayerSection : Spatial
{
    private float timeSinceStart;
    private float bpm = 130;


    [Export]
    private PlayerSectionAudioPlayer playerSectionSoundNode;

    public override void _Ready()
    {
        this.playerSectionSoundNode = GetNode<PlayerSectionAudioPlayer>("PlayerSectionSound");
        GD.Print(playerSectionSoundNode);
    }

    public override void _Process(float delta)
    {
        this.timeSinceStart += delta;

        float bps = bpm / 60;

        this.Rotation = new Vector3(0, 0, (float)Math.Cos(this.timeSinceStart * Math.PI * bps));

        //float tempo = playerSectionSoundNode.currentTempo;
    }
}
