using Godot;
using System;

public class ChairThrower : Spatial
{

    private PackedScene thrownChairScene;
    private ThrownChair thrownChair;

    private bool IschairThrown = false;

    [Export]
    private float chairVelocity = 3f;

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
            this.thrownChair = (ThrownChair)this.thrownChairScene.Instance();
            Basis camBasis = this.camera.Transform.basis;

            this.thrownChair.Transform = this.camera.Transform;
            this.thrownChair.LinearVelocity = -camBasis.z * chairVelocity;
            GetNode("/root/Root").AddChild(this.thrownChair);

        }
    }
}
