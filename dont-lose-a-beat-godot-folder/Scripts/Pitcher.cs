using Godot;
using System;

public class Pitcher : Node
{
    [Export]
    int audioBusIndex = 1;


    public static float startTempo = 1, startPitch = 1,
    minTempo = .5f, maxTempo = 5,
    minPitch = .5f, maxPitch = 2;

    float currentSpeed = 1;
    float currentPitch = 1;

    AudioEffectPitchShift pitchEffect;
    AudioStreamPlayer AP;

    //Equivalent zu Awake() in Unity
    public void Init(int BusID) {
        pitchEffect = (AudioEffectPitchShift)AudioServer.GetBusEffect(BusID, 0);
        GD.Print(pitchEffect);
        AP = GetNode<AudioStreamPlayer>("../AudioStreamPlayer");
    }

    public void SetSpeed(float speed) {
        speed = Mathf.Clamp(speed, minTempo, maxTempo);
        pitchEffect.PitchScale = currentPitch / speed;
        AP.PitchScale = speed;
        currentSpeed = speed;
    }

    public void SetPitch(float pitch) {
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        pitchEffect.PitchScale = pitch / currentSpeed;
        currentPitch = pitch;
    }

    public void SetPitchAndTempo(float pitch, float speed)
    {
        pitchEffect.PitchScale = pitch / speed;
	    AP.PitchScale = speed;
	    currentSpeed = speed;
	    currentPitch = pitch;
    }
}
