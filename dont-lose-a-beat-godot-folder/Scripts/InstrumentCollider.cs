using Godot;
using System;

public class InstrumentCollider : RigidBody
{
    public Section section;
    public override void _Ready()
    {
        if (GetParent() != null)
        {
            this.section = GetParent<Section>();
        }

        Connect("body_entered", section, "BodyEntered", new Godot.Collections.Array() { "InstrumentCollider" });
    }
}
