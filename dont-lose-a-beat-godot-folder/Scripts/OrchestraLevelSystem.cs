using Godot;
using System;
public class OrchestraLevelSystem : Node
{
    [Export]
    public OrchestraPlayers[] orchestraPlayers;
    [Export]
    public float difficulty;
    [Export]
    public float audioTempo;
    private bool isThresholdChecking;
    public override void _Ready()
    {
        GD.Print("Started - OrchestraLevelSystem");
    }
    public override void _Process(float delta)
    {
        if (!isThresholdChecking)
            checkThreshold();
    }
    private async void checkThreshold()
    {
        isThresholdChecking = true;
        GD.Print("Threshold checking...");
        await ToSignal(GetTree().CreateTimer(5.0f), "timeout");
        isThresholdChecking = false;
        GD.Print("Threshold checked!");
    }
    public void LoseMusicants()
    {
        GD.Print("Loose Musicants!");
    }

}
