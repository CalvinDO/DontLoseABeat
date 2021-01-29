using Godot;
using System;

public class OrchestraLevel : Node
{
    [Export]
    public float bpmTempo;

    public Musician[] playerSections;

    public override void _Ready()
    {
        this.playerSections = new Musician[this.GetChildCount()];

        for (int index = 0; index < this.GetChildCount() - 1; index++)
        {
            Musician currentMusician = playerSections[index];
            currentMusician = (Musician)this.GetChild(index);
            
            currentMusician.LoadAudio();
            currentMusician.Play();
        }
    }

    public override void _Process(float delta)
    {

    }
}
