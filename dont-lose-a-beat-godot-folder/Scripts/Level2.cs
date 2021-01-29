using Godot;
using System;

public class Level2 : OrchestraLevelSystem
{
    public OrchestraPlayers orchestraPlayers;
    public override void _Ready()
    {
        audioTempo = 65;
        GD.Print("Started - Level2");
    }
}