using Godot;
using System;

public class AudioTest : AudioStreamPlayer
{

    public AudioBusLayout audioBus;
    private AudioEffectPitchShift pianoPitch = (AudioEffectPitchShift)AudioServer.GetBusEffect(0, 0);

    [Export]
    public float currentSpeed = 1.0f;
    [Export]
    public float currentPitch = 1.0f;

    private void SetSpeed(float speed)
    {
        this.pianoPitch.PitchScale = this.currentSpeed / speed;
        this.PitchScale = speed;
        this.currentSpeed = speed;
    }

    private void SetPitch(float pitch)
    {
        /*
        this.pianoPitch.PitchScale = pitch / this.currentSpeed;
        this.PitchScale = pitch;
        this.currentSpeed = pitch;
        */
        this.pianoPitch.PitchScale = 0.5f;
    }

    public override void _Ready()
    {
        this.SetPitch(1.5f);
       // this.SetSpeed(0.8f);
    }


    public override void _Process(float delta)
    {

    }
}
