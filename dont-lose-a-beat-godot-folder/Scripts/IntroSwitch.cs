using Godot;
using System;

public class IntroSwitch : Spatial
{



    public override void _Ready() {
        GameState.mainScene = GD.Load<PackedScene>("res://Scenes/MainScene.tscn");

    }
    public void LoadMainScene()
    {
        GetTree().ChangeSceneTo(GameState.mainScene);
    }
}
