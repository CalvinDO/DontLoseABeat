using Godot;
using System;

public class InstrumentCollider : RigidBody
{
    public Section section;
    public override void _Ready()
    {
        if (GetParent().GetParent() != null)
        {
            this.section = GetParent().GetParent<Section>();
        }
        else
        {
            this.section = this.GetParent<Section>();
        }

        //Connect("body_entered", section, "BodyEntered", new Godot.Collections.Array() { "InstrumentCollider" });
        //Connect("body_exited", section, "BodyExited", new Godot.Collections.Array() { "InstrumentCollider" });
    }
}
