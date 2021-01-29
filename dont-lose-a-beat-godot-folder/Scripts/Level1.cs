using Godot;
using System;

public class Level1 : OrchestraLevelSystem
{
    public OrchestraPlayers orchestraPlayers;
    public override void _Ready()
    {
        audioTempo = 0;
        GD.Print("Started - Level1");
    }
}
