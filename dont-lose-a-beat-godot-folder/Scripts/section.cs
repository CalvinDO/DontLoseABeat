using Godot;
using System;

public class Section : Spatial
{

    #region  Pitcher

    //------------------------------------------------------------------------
    //  PITCHER
    //------------------------------------------------------------------------

    [Export]
    Instrument instrument;

    int BusID;
    string instrumentName;

    SimonsOrchestraManager OM;
    AudioStreamPlayer AP;
    AudioEffectPitchShift pitchEffect;
    Pitcher pitcher;


    [Export]
    public float tempoToSet = 1.0f, pitchToSet = 1.0f;


    #endregion

    //------------------------------------------------------------------------


    #region TempoController


    //------------------------------------------------------------------------
    //  TEMPOCONTROLLER
    //------------------------------------------------------------------------

    private float timeSinceStart = 0;
    public float bpm;

    private float currentAngle;
    private float angleLastFrame = 0;
    private float currentSpeed = 0;

    private bool mouseInsideRight = false;
    private bool mouseInsideLeft = false;

    public float delta;

    [Export]
    public float angleAccelleration = 35;
    [Export]
    public Axis axis;

    private TempoChangerScriptTransmitter tempoChanger;

    [Export]
    public float maxRotationAngle = 35f;

    public float t = 0;

    public enum Axis
    {
        X, Y, Z
    }
    #endregion

    //------------------------------------------------------------------------

    #region PitchController
    //------------------------------------------------------------------------
    //  PITCHCONTROLLER
    //------------------------------------------------------------------------

    private SectionChair chair;
    #endregion

    //------------------------------------------------------------------------

    #region MuteController
    //------------------------------------------------------------------------
    //  MUTECONTROLLER
    //------------------------------------------------------------------------

    public bool IsChairInCollider = false;

    //------------------------------------------------------------------------
    #endregion

    public override void _Ready()
    {
        AP = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        pitcher = GetNode<Pitcher>("Pitcher");

        BusID = (int)instrument + 1; //+1 because 0 is Master
        pitcher.Init(BusID);
        instrumentName = this.instrument.ToString().ToLower();

        OM = GetParent<SimonsOrchestraManager>();
        if (OM == null) GD.PrintErr(instrumentName + " Section must be Child of an OrchestraManager");

        this.tempoChanger = (TempoChangerScriptTransmitter)GetNodeOrNull<TempoChangerScriptTransmitter>("TempoChanger");


        if (GetNode<SectionChair>("chair_sections") != null)
        {
            this.chair = GetNode<SectionChair>("chair_sections");
        }


        this.bpm = this.OM.currentBPM;

        AP.Bus = AudioServer.GetBusName(BusID);

        pitchEffect = (AudioEffectPitchShift)AudioServer.GetBusEffect(BusID, 0);

        LoadLevel();
    }

    public void LoadLevel()
    {
        string filename = $"res://Audio/lvl{OM.currentLevel}/{instrumentName}.ogg";
        AP.Stream = GD.Load<AudioStream>(filename);
    }

    public void Play()
    {
        if (AP != null) AP.Play(this.timeSinceStart);
    }

    public void Stop()
    {

        if (AP != null) AP.Stop();
    }

    public override void _Process(float delta)
    {
        this.delta = delta;
        this.timeSinceStart += delta;


        if (this.AP.Playing)
        {
            CalculateRotation();
        }

        if (this.timeSinceStart > AP.Stream.GetLength())
        {
            this.timeSinceStart = 0;
        }
    }

    public void CalculateRotation()
    {

        if (Input.IsActionPressed("TempoControl"))
        {
            if (this.mouseInsideRight)
            {
                if (this.currentSpeed > 0)
                {
                    this.bpm -= this.angleAccelleration * delta;
                }
                else if (this.currentSpeed < 0)
                {
                    this.bpm += this.angleAccelleration * delta;
                }
            }
            else if (this.mouseInsideLeft)
            {

                if (this.currentSpeed > 0)
                {
                    this.bpm += this.angleAccelleration * delta;

                }
                else if (this.currentSpeed < 0)
                {
                    this.bpm -= this.angleAccelleration * delta;
                }
            }
        }
        if (this.bpm > this.OM.originalBPM * 2)
        {
            this.bpm = this.OM.originalBPM * 2;
        }

        if (this.bpm < this.OM.originalBPM / 2)
        {
            this.bpm = this.OM.originalBPM / 2;
        }

        //Mathf.Clamp(this.bpm, this.OM.originalBPM / 2, this.OM.originalBPM * 2);

        float bps = bpm / 60;

        this.t += delta * bps;

        this.currentAngle = (float)(Math.Sin(this.t * 1 / 2 * Math.PI) * (Mathf.Deg2Rad(maxRotationAngle)));
        this.currentSpeed = this.currentAngle - this.angleLastFrame;
        this.angleLastFrame = this.currentAngle;

        this.Rotation = new Vector3();

        if (this.tempoChanger != null)
        {
            switch (this.axis)
            {
                case Axis.X:
                    this.tempoChanger.Rotation = new Vector3(currentAngle, 0, 0);
                    break;
                case Axis.Y:
                    this.tempoChanger.Rotation = new Vector3(0, currentAngle, 0);
                    break;
                case Axis.Z:
                    this.tempoChanger.Rotation = new Vector3(0, 0, currentAngle);
                    break;
                default:
                    break;
            }
        }

        tempoToSet = this.bpm / this.OM.originalBPM;

        pitcher.SetPitchAndTempo(pitchToSet, tempoToSet);
        if (this.chair != null)
        {
            this.chair.SetPitchScale(pitchToSet);
        }
    }

    public void ForceSetPitch(float pitch)
    {
        this.pitchToSet = pitch;
    }

    public void ForceSetTempo(float tempo)
    {
        this.tempoToSet = tempo;

        this.bpm = this.OM.currentBPM * tempo;
    }

    public void AreaEntered(Area area, string name)
    {
        switch (name)
        {
            case "Left":
                this.mouseInsideLeft = true;
                break;
            case "Right":
                this.mouseInsideRight = true;
                break;
            default:
                break;
        }
    }

    public void AreaExited(Area area, string name)
    {
        switch (name)
        {
            case "Left":
                this.mouseInsideLeft = false;
                break;
            case "Right":
                this.mouseInsideRight = false;
                break;
            default:
                break;
        }
    }

    public void BodyEntered(Node body, string name)
    {
        GD.Print(name);
        switch (name)
        {
            case "InstrumentCollider":
                GD.Print("Body entered");
                if (this.AP.Playing)
                {
                    this.Stop();
                }
                else
                {
                    this.Play();
                }
                break;
            default:
                GD.Print("Other Body entered");
                break;
        }
    }
}
