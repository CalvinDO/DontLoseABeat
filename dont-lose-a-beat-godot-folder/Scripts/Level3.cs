using Godot;
using System;

public class Level3 : OrchestraLevelSystem
{
    public OrchestraPlayers orchestraPlayers;
    public override void _Ready()
    {
        audioTempo = 130;
        GD.Print("Started - Level3");
    }

}
