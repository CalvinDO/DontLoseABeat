using Godot;
using System;

public class Level5 : OrchestraLevelSystem
{
    public OrchestraPlayers orchestraPlayers;
    public override void _Ready()
    {
        audioTempo = 70;
        GD.Print("Started - Level5");
    }
}
