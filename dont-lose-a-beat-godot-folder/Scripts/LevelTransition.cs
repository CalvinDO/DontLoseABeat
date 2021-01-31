using Godot;

public class LevelTransition : Spatial
{
    AnimationPlayer an;
    AudioStreamPlayer audio;
    //PackedScene mainScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //mainScene = GD.Load<PackedScene>("res://Scenes/MainScene.tscn");
        audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        an = GetNode<AnimationPlayer>("courtain/AnimationPlayer");
        PlayTrack();
    }

    public void PlayTrack() 
    {
        audio.Stream = GD.Load<AudioStream>($"res://Audio/full_tracks/lvl{GameState.currentLevel}.ogg");
        audio.Play();
    }

    public void OnNextLevel() 
    {
        an.PlayBackwards("curtain_open");
    }

    public void LoadNewScene() {
        GameState.currentLevel += 1;
        GD.Print("LOAD Level " + GameState.currentLevel);
        GetTree().ChangeSceneTo(GameState.mainScene);
    }

}
