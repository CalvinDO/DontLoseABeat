using Godot;
using System;

public class ChairThrower : Spatial
{

    private PackedScene thrownChairScene;
    private ThrownChair thrownChair;

    private bool IschairThrown = false;

    private Camera camera;
    public override void _Ready()
    {
        this.thrownChairScene = ResourceLoader.Load<PackedScene>("res://prefabs/ThrownChair.tscn");
        this.camera = GetParent<Camera>();
    }

    public override void _Process(float delta)
    {
        this.CheckKeyboardInput();

    }

    public void CheckKeyboardInput()
    {

        if (Input.IsActionJustPressed("ThrowChair"))
        {
            GD.Print("Hello");
            this.thrownChair = (ThrownChair)this.thrownChairScene.Instance();
            AddChild(this.thrownChair);
            this.IschairThrown = true;
       
        }
    }
}
