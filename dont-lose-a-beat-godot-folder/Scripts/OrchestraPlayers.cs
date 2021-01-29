using Godot;
using System;

public class OrchestraPlayers : Node
{
    public AudioStream[] audioTracks;
    public override void _Ready()
    {
        GD.Print("Started - OrchestraPlayers");
    }
}
