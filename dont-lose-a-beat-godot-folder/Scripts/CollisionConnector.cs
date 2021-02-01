using Godot;
using System;

public class CollisionConnector : Area
{
    public enum Side {Right, Left }
    
    [Export]
    public Side side;


    public void SetSignals(PlayerSection section)
    {
        Connect("area_entered", section, "AreaEntered", new Godot.Collections.Array() { this.side.ToString() });
        Connect("area_exited", section, "AreaExited", new Godot.Collections.Array() { this.side.ToString() });
    }
}
