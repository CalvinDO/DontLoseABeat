using Godot;
using System;

public class section : Spatial
{

    [Export]
    Instrument instrument;

    const float minSpeed = .5f, maxSpeed = 5,
    minPitch = .5f, maxPitch = 2;

    int BusID;
    string instrumentName;

    SimonsOrchestraManager OM;
    AudioStreamPlayer AP;
    AudioEffectPitchShift pitchEffect;
    Pitcher pitcher;
    float currentSpeed = 1;
    float currentPitch = 1;

    [Export]
    public float tempoToSet = 1.0f, pitchToSet = 1.0f;

    public override void _Ready()
    {
        AP = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        pitcher = GetNode<Pitcher>("Pitcher");

        BusID = (int)instrument + 1; //+1 because 0 is Master
        pitcher.Init(BusID);
        instrumentName = this.instrument.ToString().ToLower();

        OM = GetParent<SimonsOrchestraManager>();
        if (OM == null) GD.PrintErr(instrumentName + " Section must be Child of an OrchestraManager");

        AP.Bus = AudioServer.GetBusName(BusID);

        pitchEffect = (AudioEffectPitchShift)AudioServer.GetBusEffect(BusID, 0);

        LoadLevel();
    }

    public void LoadLevel() {
        string filename = $"res://Audio/lvl{OM.currentLevel}/{instrumentName}.ogg";
        AP.Stream = GD.Load<AudioStream>(filename);
    }

    public override void _Process (float delta) {
        //pitcher.SetPitchAndTempo(pitchToSet, tempoToSet);
        pitcher.SetPitchAndTempo(pitchToSet, tempoToSet);
    }

    public void Play()
    {
        if (AP != null) AP.Play();
    }

    public void Stop()
    {
        if (AP != null) AP.Stop();
    }
}
