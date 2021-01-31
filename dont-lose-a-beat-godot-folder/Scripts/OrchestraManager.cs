using Godot;
using System;
public class OrchestraManager : Node
{
    public Node[] Levels;
    public float difficulty;
    private bool isThresholdChecking;
    private int levelProgress;


    public override void _Ready()
    {
        levelProgress = 0;

        //load levels into Array
        Levels = new Node[this.GetChild(0).GetChildCount()];

        for (int i = 0; i < this.GetChild(0).GetChildCount() - 1; i++)
        {
            Node currentlevel = Levels[i];
            currentlevel = this.GetChild(0).GetChild(i);
        }

        difficulty = levelProgress;

        switch (levelProgress)
        {
            case (0):
                //orchestraPlayer.bpmTempo = 0;
                difficulty = 1;
                break;
            case (1):
                //orchestraPlayer.bpmTempo = 65;
                difficulty = 2;
                break;
            case (2):
                // orchestraPlayer.bpmTempo = 130;
                difficulty = 3;
                break;
            case (3):
                // orchestraPlayer.bpmTempo = 0;
                difficulty = 4;
                break;
            case (4):
                //orchestraPlayer.bpmTempo = 70;
                difficulty = 5;
                break;
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

