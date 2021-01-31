using Godot;
using System;

public class ChairThrower : Spatial
{

    private PackedScene thrownChairScene;
    private ThrownChair thrownChair;

    private bool IschairThrown = false;

    [Export]
    private float chairVelocity = 3f;

    RandomNumberGenerator rng;

    private Camera camera;
    public override void _Ready()
    {
        this.thrownChairScene = ResourceLoader.Load<PackedScene>("res://prefabs/ThrownChair.tscn");
        this.camera = GetParent<Camera>();
        rng = new RandomNumberGenerator();
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
            this.thrownChair.Translate(new Vector3(0,-0.3f,0));

            Vector3 randomRotation = new Vector3(1,1,1);
            randomRotation.x = rng.Randf();
            rng.Randomize();
            randomRotation.y = rng.Randf();
            rng.Randomize();
            randomRotation.z = rng.Randf();
            rng.Randomize();

            randomRotation = randomRotation.Normalized();
            rng.Randomize();

            this.thrownChair.Rotate(randomRotation, rng.RandfRange(0,360f));
            GetNode("/root/Root").AddChild(this.thrownChair);

        }
    }
}
