using Godot;
using System;

public class MusicianAudioStreamController : AudioStreamPlayer
{
    private AudioEffectPitchShift pitchShift;

    private float currentTempo = 1.0f;
    private float currentPitch = 1.0f;

    [Export]
    public float tempoToSet = 1.0f;
    [Export]
    public float pitchToSet = 1.0f;

    private float increment = 0.01f;

    public int index;

    public override void _Ready()
    {
        pitchShift = new AudioEffectPitchShift(); //Fix für NullReferenceException - ka.
    }


    public void Init(int index)
    {
        this.index = index;
        AudioServer.AddBus(this.index);
        AudioServer.AddBusEffect(this.index, pitchShift);
    }

    private void SetTempo(float tempo)
    {
        this.pitchShift.PitchScale = this.currentPitch / tempo;
        this.PitchScale = tempo;
        this.currentTempo = tempo;
    }

    private void SetPitch(float pitch)
    {        
        this.pitchShift.PitchScale = pitch / this.currentTempo;
        this.currentTempo = pitch;

    }

    public void SetPitchAndTempo(float pitch, float tempo)
    {
        pitchShift.PitchScale = pitch / tempo;
	    this.PitchScale = tempo;
	    currentTempo = tempo;
	    currentPitch = pitch;
    }


    public override void _Process(float delta)
    {
        /*
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
                */
        this.SetPitchAndTempo(this.pitchToSet, this.tempoToSet);
    }
}
