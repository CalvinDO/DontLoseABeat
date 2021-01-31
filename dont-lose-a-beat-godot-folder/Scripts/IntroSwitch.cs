using Godot;
using System;

public class IntroSwitch : Spatial
{



    public override void _Ready() {
        GameState.mainScene = GD.Load<PackedScene>("res://Scenes/MainScene.tscn");
<<<<<<< HEAD

=======
        GameState.transitionScene = GD.Load<PackedScene>("res://Scenes/leveltransition.tscn");
>>>>>>> d76f01d5c650ac5b8dc93ec4eb5964e73c1b402b
    }
    public void LoadMainScene()
    {
        GetTree().ChangeSceneTo(GameState.mainScene);
    }
}
