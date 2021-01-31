using Godot;
using System;
using System.Collections.Generic;

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

    private bool mouseHoverBody = false;

    private bool selected = false;

    public float delta;

    [Export]
    public float angleAccelleration = 35;
    [Export]
    public Axis axis;

    private TempoChangerScriptTransmitter tempoChanger;

    [Export]
    public float maxRotationAngle = 35f;

    public float t = 0;

    public Area areaLeft;
    public Area areaRight;


    public float pitchIncrement = 0.01f;

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

    private Godot.Collections.Array chairs;

    public bool IsWheelUp = false;
    public bool IsWheelDown = false;

    #endregion

    //------------------------------------------------------------------------

    #region MuteController
    //------------------------------------------------------------------------
    //  MUTECONTROLLER
    //------------------------------------------------------------------------

    public AudioStreamPlayer ChairLandPlayer;

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

        this.areaLeft = (Area)GetNodeOrNull<Area>("TempoChanger/AreaLeft");
        this.areaRight = (Area)GetNodeOrNull<Area>("TempoChanger/AreaRight");
        this.ToggleRightLeftAreas(false);

        ChairLandPlayer = new AudioStreamPlayer();
        ChairLandPlayer.Autoplay = false;
        ChairLandPlayer.Stream = (AudioStream)GD.Load("res://Audio/effects/chair_land.tres");
        this.AddChild(ChairLandPlayer);


        if (GetNode<Spatial>("chairs") != null)
        {
            this.chairs = GetNode<Spatial>("chairs").GetChildren();
        }


        this.bpm = this.OM.currentBPM;

        AP.Bus = AudioServer.GetBusName(BusID);

        pitchEffect = (AudioEffectPitchShift)AudioServer.GetBusEffect(BusID, 0);

        LoadLevel();
    }

    public void LoadLevel()
    {
        string filename = $"res://Audio/lvl{GameState.currentLevel}/{instrumentName}.ogg";
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




        if (Input.IsActionPressed("InstrumentSelect"))
        {
            this.ManageSelection();
        }
        else
        {
            this.selected = false;
            GameState.selectedSection = null;
        }

        if (this.selected)
        {
            this.ToggleRightLeftAreas(true);
        }
        else
        {
            if (!this.mouseHoverBody)
            {
                this.ToggleRightLeftAreas(false);
            }
        }

        if (this.AP.Playing)
        {
            CalculateRotation();
        }
        if (AP.Stream != null)
        {

            if (this.timeSinceStart > AP.Stream.GetLength())
            {
                this.timeSinceStart = 0;
            }
        }
    }

    public void ManagePitchControl()
    {
        if (this.IsWheelDown)
        {

        }
        if (this.IsWheelUp)
        {


        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton)
        {
            InputEventMouseButton emb = (InputEventMouseButton)@event;
            if (this.mouseHoverBody)
            {
                if (emb.IsPressed())
                {
                    // this.IsWheelDown = true;
                    this.pitchToSet -= this.pitchIncrement;

                    if (this.pitchToSet < 0.5f)
                    {
                        this.pitchToSet = 0.5f;
                    }

                }
                if (emb.ButtonIndex == (int)ButtonList.WheelDown)
                {
                    this.pitchToSet += this.pitchIncrement;

                    if (this.pitchToSet > 2f)
                    {
                        this.pitchToSet = 2;
                    }
                }
            }
        }
    }

    public void ManageSelection()
    {
        if (this.mouseHoverBody)
        {
            if (this.OM.selectedSection == null)
            {
                this.selected = true;
                GameState.selectedSection = this;
            }
            else
            {
                if (this.OM.selectedSection != this)
                {
                    this.ToggleRightLeftAreas(false);
                }
            }
        }
        GD.Print(this.OM.selectedSection);
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
                    this.tempoChanger.Rotation = new Vector3(currentAngle, this.tempoChanger.Rotation.y, this.tempoChanger.Rotation.z);
                    break;
                case Axis.Y:
                    this.tempoChanger.Rotation = new Vector3(this.tempoChanger.Rotation.x, currentAngle, this.tempoChanger.Rotation.z);
                    break;
                case Axis.Z:
                    this.tempoChanger.Rotation = new Vector3(this.tempoChanger.Rotation.x, this.tempoChanger.Rotation.y, currentAngle);
                    break;
                default:
                    break;
            }
        }

        tempoToSet = this.bpm / this.OM.originalBPM;

        pitcher.SetPitchAndTempo(pitchToSet, tempoToSet);
        if (this.chairs != null)
        {
            this.SetChairsPitchScales(pitchToSet);
        }
    }

    public void SetChairsPitchScales(float pitchToSet)
    {
        foreach (SectionChair chair in this.chairs)
        {
            chair.SetPitchScale(pitchToSet);
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
            case "Body":
                this.mouseHoverBody = true;
                this.ToggleRightLeftAreas(true);
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
            case "Body":
                this.mouseHoverBody = false;
                this.ToggleRightLeftAreas(false);
                break;
            default:
                break;
        }
    }

    public void BodyEntered(Node body, string name)
    {
        switch (name)
        {
            case "InstrumentCollider":
                if (this.AP.Playing)
                {
                    this.Stop();
                    this.ChairLandPlayer.Play();
                }
                else
                {
                    this.Play();
                    this.ChairLandPlayer.Play();
                }
                break;
            default:
                break;
        }
    }

    public void ToggleRightLeftAreas(bool setBool)
    {
        this.areaLeft.SetProcess(setBool);
        this.areaLeft.Visible = (setBool);
        this.areaRight.SetProcess(setBool);
        this.areaRight.Visible = (setBool);
    }
}
