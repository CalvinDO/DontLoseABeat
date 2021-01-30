using Godot;
using System;

public class CollisionConnector : CollisionShape
{
    [Export]
    public Side side;

    public enum Side
    {
        Right, Left
    }

    public void SetSignals(Section section)
    {
        Connect("area_entered", section, "AreaEntered", new Godot.Collections.Array() { this.side.ToString() });
        Connect("area_exited", section, "AreaExited", new Godot.Collections.Array() { this.side.ToString() });
    }
}
