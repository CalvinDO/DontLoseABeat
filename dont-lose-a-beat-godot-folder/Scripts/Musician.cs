using Godot;
using System;

public class Musician : Node
{
    [Export]
    public Instrument instrument;
    [Export]
    public AudioStreamPlayer audioStream;
    [Export]
    public float bpmTempo;
    [Export]
    public float pitch;

    public override void _Ready()
    {

    }

    public void LoadAudio()
    {
        if (this.GetChildCount() > 0)
        {
            audioStream = (AudioStreamPlayer)GetChild(0);

            string level = this.GetParent().Name.ToLower();
            string instrumentForPath = this.instrument.ToString().ToLower();


            audioStream.Stream = (AudioStream)GD.Load($"res://Audio/{level}/{level}_{instrumentForPath}.ogg");
            GD.Print(instrumentForPath);
        }
    }

    public void Play()
    {
        if (this.audioStream != null)
        {
            this.audioStream.Play();
        }
    }

    public void Stop()
    {
        if (this.audioStream != null)
        {
            this.audioStream.Stop();
        }
    }
}
