using Godot;
using System;

public class Level4 : OrchestraLevelSystem
{
    public OrchestraPlayers orchestraPlayers;
    public override void _Ready()
    {
        audioTempo = 0;
        GD.Print("Started - Level4");
    }
}
