using Godot;
using System;

public class InstrumentCollider : RigidBody
{
    public PlayerSection section;
    public override void _Ready()
    {
        if (GetParent() != null)
        {
            this.section = GetParent<PlayerSection>();
        }

        Connect("body_entered", section, "BodyEntered", new Godot.Collections.Array() { "InstrumentCollider" });
    }
}
