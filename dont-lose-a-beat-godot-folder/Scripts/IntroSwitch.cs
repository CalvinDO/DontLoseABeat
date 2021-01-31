using Godot;
using System;

public class IntroSwitch : Spatial
{
    public void LoadMainScene()
    {
        GetTree().ChangeScene("res://Scenes/MainScene.tscn");
    }
}
