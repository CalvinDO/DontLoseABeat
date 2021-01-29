using Godot;
using System;

public class OrchestraPlayers : Node
{
    [Export]
    public Instruments instruments;
    [Export]
    public AudioStream audioStream;
    [Export]
    public float bpmTempo;
    [Export]
    public float pitch;


    public override void _Ready()
    {
        GD.Print("Started - OrchestraPlayers");
    }
}
