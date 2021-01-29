using Godot;
using System;

public class Musician : Node
{
    [Export]
    public Instrument instrument;

    public AudioStreamPlayer audioStream;
    public MusicianAudioStreamController audioStreamController;
    [Export]
    public float bpmTempo;
    [Export]
    public float pitch;

    public int index;

    public override void _Ready()
    {

    }

    public void LoadAudio(int index)
    {
        this.index = index;
        if (this.GetChildCount() > 0)
        {
            this.audioStream = (AudioStreamPlayer)GetChild(0);
            this.audioStreamController = (MusicianAudioStreamController)GetChild(0);
            this.audioStreamController.Init(index);

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
