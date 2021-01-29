using Godot;
using System;
public class OrchestraLevelSystem : Node
{
    public OrchestraPlayers[] orchestraPlayers;
    public float difficulty;
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
