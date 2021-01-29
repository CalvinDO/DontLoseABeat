using Godot;
using System;

public class OrchestraPlayers : Node
{
    [Export]
    public Instruments instruments;
    [Export]
    public PlayerSectionAudioPlayerFake audioStream;
    [Export]
    public float bpmTempo;
    [Export]
    public float pitch;


    public override void _Ready()
    {
        audioStream = GetNode<PlayerSectionAudioPlayerFake>("/root/testOrchestraScene/Root/AudioStreamPlayer");
    }
}
