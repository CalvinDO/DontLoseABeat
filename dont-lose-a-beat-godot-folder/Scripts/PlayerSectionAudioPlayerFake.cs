using Godot;
using System;

public class PlayerSectionAudioPlayer : AudioStreamPlayer
{
    private AudioEffectPitchShift pianoPitch = (AudioEffectPitchShift)AudioServer.GetBusEffect(0, 0);

    [Export]
    public float currentTempo = 1.0f;
    [Export]
    public float currentPitch = 1.0f;

    private float tempoToSet = 1.0f;
    private float pitchToSet = 1.0f;

    private float increment = 0.01f;

    private void SetTempo(float tempo)
    {
        this.pianoPitch.PitchScale = this.currentTempo / tempo;
        this.PitchScale = tempo;
        this.currentTempo = tempo;
    }

    private void SetPitch(float pitch)
    {

        this.pianoPitch.PitchScale = pitch / this.currentTempo;
        this.PitchScale = pitch;
        this.currentTempo = pitch;
    }

    public void SetPitchAndTempo(float pitch, float tempo)
    {
        this.SetPitch(pitch);
        this.SetTempo(tempo);
    }
    public override void _Ready()
    {
        this.SetPitchAndTempo(1.5f, 0.8f);
    }


    public override void _Process(float delta)
    {

        if (Input.IsActionPressed("IncreasePitch"))
        {
            this.pitchToSet += 0.02f;
        }
        if (Input.IsActionPressed("DecreasePitch"))
        {
            this.pitchToSet -= 0.02f;
        }
        if (Input.IsActionPressed("IncreaseSpeed"))
        {
            this.tempoToSet += 0.02f;
        }
        if (Input.IsActionPressed("DecreaseSpeed"))
        {
            this.tempoToSet -= 0.02f;
        }
        this.SetPitchAndTempo(this.pitchToSet, this.tempoToSet);
    }
}
