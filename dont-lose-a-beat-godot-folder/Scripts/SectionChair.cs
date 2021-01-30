using Godot;
using System;

public class SectionChair : Spatial
{

    Spatial stick;
    Spatial top;

    float currentScale = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stick = GetNode<Spatial>("chair_base//chair_stick");
        top = GetNode<Spatial>("chair_base/chair_top");
    }

    public void SetScale(float scale)
    {   //1.5 7.1
        currentScale = scale;
        float s = RemapRange(scale, 0, 1, 1, 7.1f);
        float t = scale * 1.5f;
        stick.Scale = new Vector3(1, s, 1);
        top.Translation = new Vector3(0, t,0);
    }

    //Maybe extract to util class
    public float RemapRange(float value, float InputA, float InputB, float OutputA, float OutputB) 
    {
        return(value - InputA) / (InputB - InputA) * (OutputB - OutputA) + OutputA;
    }
}
