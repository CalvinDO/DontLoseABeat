using Godot;
using System;
public class OrchestraLevelSystem : Node
{
    [Export]
    public Node[] Levels;
    [Export]
    public OrchestraPlayers orchestraPlayers;
    [Export]
    public float difficulty;
    private bool isThresholdChecking;
    public override void _Ready()
    {
        //load levels into Array
        Levels = new Node[this.GetChild(0).GetChildCount()];
        for (int i = 0; i < this.GetChild(0).GetChildCount() - 1; i++)
        {
            Levels[i] = this.GetChild(0).GetChild(i);
            switch (i)
            {
                case (0):
                    orchestraPlayers.bpmTempo = 0;
                    difficulty = 1;
                    break;
                case (1):
                    orchestraPlayers.bpmTempo = 65;
                    difficulty = 2;
                    break;
                case (2):
                    orchestraPlayers.bpmTempo = 130;
                    difficulty = 3;
                    break;
                case (3):
                    orchestraPlayers.bpmTempo = 0;
                    difficulty = 4;
                    break;
                case (4):
                    orchestraPlayers.bpmTempo = 70;
                    difficulty = 5;
                    break;
            }
        }
    }
    public override void _Process(float delta)
    {
        if (!isThresholdChecking)
            CheckThreshold();
    }
    private async void CheckThreshold()
    {
        //need to check bpmtempo-threshold
        isThresholdChecking = true;
        GD.Print("Threshold checking...");
        await ToSignal(GetTree().CreateTimer(10f), "timeout");
        isThresholdChecking = false;
        GD.Print("Threshold checked!");
    }
    public void LoseMusicants()
    {
        GD.Print("Loose Musicants!");
    }

}
