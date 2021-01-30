using Godot;
using System.Collections.Generic;
using System;

public class SimonsOrchestraManager : Spatial
{

    [Export]
    bool loadDynamically = true;

    [Export]
    public int currentLevel;

    public float currentBPM = 130;

    Dictionary<string, PackedScene> sections;


    private float delta;

    //Valentins
    //------
    [Export]
    public float maxPitchOrTempo = 1.1f;
    [Export]
    public float minPitchOrTempo = 0.9f;
    bool isInThreshold;
    bool checkNow;
    [Export]
    public float timeBeforeStartingChecking = 20;
    [Export]
    public float checkingDuration = 30;
    float thresholdTime;
    RandomNumberGenerator randomFloatNumber = new RandomNumberGenerator();
    private float timeIntervallForUpSetSections;
    public Section[] cleanSections;
    private bool isUpSetting;

    [Export]
    public int minLoseIntervall = 5;
    [Export]
    public int maxLoseIntervall = 20;

    ///------

    public override void _Ready()
    {
        timeIntervallForUpSetSections = randomFloatNumber.RandfRange(5f, 15f);
        LoadPrefabs();
        LoadLevel();
    }
    public void LoadLevel()
    {

        //Get Intruments
        List<string> files = new List<string>();
        Directory dir = new Directory();
        dir.Open($"res://Audio/lvl{currentLevel}");
        dir.ListDirBegin();

        while (true)
        {
            string fileName = dir.GetNext();
            if (fileName == "") break;
            else if (fileName.EndsWith(".ogg") && loadDynamically)
            {
                files.Add(fileName);
                fileName = fileName.Remove(fileName.Length - 4);
                Section cSection = (Section)sections[fileName].Instance();

                AddChild(cSection);
            }
            else if (!fileName.EndsWith(".ogg.import"))
            {
                GD.PrintErr($"Folder res://Audio/lvl{currentLevel} contains wrongly named file {fileName}. SHAME!");
            }
        }
        dir.ListDirEnd();


        int index = 0;
        this.cleanSections = new Section[this.GetChildCount()];
        foreach (Section cSection in this.GetChildren())
        {
            cSection.Play();

            this.cleanSections[index] = cSection;
            index++;
        }

        this.thresholdTime = this.checkingDuration;
    }

    public override void _Process(float delta)
    {
        this.delta = delta;
        timeIntervallForUpSetSections -= delta;
        if (timeIntervallForUpSetSections <= 0f)
            if (!isUpSetting)
                this.RandomIncrementSections();
        this.CheckThreshholdAndPitch();
    }
    public void RandomIncrementSections()
    {
        randomFloatNumber.Randomize();
        timeIntervallForUpSetSections = randomFloatNumber.RandfRange(this.minLoseIntervall, this.maxLoseIntervall);
        isUpSetting = true;
        randomFloatNumber.Randomize();
        int randomSection = (int)randomFloatNumber.Randfn(0, cleanSections.Length - 1);
        randomFloatNumber.Randomize();
        float upSetFactor = randomFloatNumber.RandfRange(0.5f, 2f);
        randomFloatNumber.Randomize();
        float pitcherOrTempo = randomFloatNumber.RandfRange(0f, 1f);
        Pitcher pitcher = cleanSections[randomSection].GetNode<Pitcher>("Pitcher");

        if (pitcherOrTempo < 0.5f)
        {
            GD.Print(cleanSections[randomSection].Name + "--pitchchange: " + upSetFactor);
            pitcher.SetPitchAndTempo(upSetFactor, pitcher.currentPitch);
            cleanSections[randomSection].currentPitch = upSetFactor;
        }
        else
        {
            GD.Print(cleanSections[randomSection].Name + "--tempochange: " + upSetFactor);
            pitcher.SetPitchAndTempo(pitcher.currentTempo, upSetFactor);
            cleanSections[randomSection].currentTempo = upSetFactor;
        }


        isUpSetting = false;
    }
    public void CheckThreshholdAndPitch()
    {
        if (timeBeforeStartingChecking >= 0f)
            timeBeforeStartingChecking -= this.delta;
        if (timeBeforeStartingChecking <= 0f)
        {
            if (!isInThreshold)
            {
                if (this.IsInThreshold())
                {
                    GD.Print("Start checking duration of threshhold keeping!");
                    isInThreshold = true;
                }
            }
            if (isInThreshold)
            {
                if (this.IsInThreshold())
                {
                    thresholdTime -= this.delta;
                }
                else
                {
                    isInThreshold = false;
                    thresholdTime = this.checkingDuration;
                }
            }

            if (thresholdTime <= 0f)
                GD.Print("-----------WIN-Placeholder-----------");
        }
    }

    public bool IsInThreshold()
    {
        foreach (Section cSection in this.cleanSections)
        {
            Pitcher pitcher = cSection.GetNode<Pitcher>("Pitcher");
            if (!(pitcher.currentTempo >= minPitchOrTempo && pitcher.currentPitch >= minPitchOrTempo && pitcher.currentTempo <= maxPitchOrTempo && pitcher.currentPitch <= maxPitchOrTempo))
            {
                return false;
            }
        }
        return true;
    }

    void LoadPrefabs()
    {
        sections = new Dictionary<string, PackedScene>();
        sections.Add("strings", ResourceLoader.Load<PackedScene>("res://prefabs/sections/strings.tscn"));
        sections.Add("deepstrings", ResourceLoader.Load<PackedScene>("res://prefabs/sections/deepstrings.tscn"));
        sections.Add("horn", ResourceLoader.Load<PackedScene>("res://prefabs/sections/horn.tscn"));
        sections.Add("trombone", ResourceLoader.Load<PackedScene>("res://prefabs/sections/trombone.tscn"));
        sections.Add("trumpet", ResourceLoader.Load<PackedScene>("res://prefabs/sections/trumpet.tscn"));
        sections.Add("tuba", ResourceLoader.Load<PackedScene>("res://prefabs/sections/tuba.tscn"));
        sections.Add("oboe", ResourceLoader.Load<PackedScene>("res://prefabs/sections/oboe.tscn"));
        sections.Add("clarinet", ResourceLoader.Load<PackedScene>("res://prefabs/sections/clarinet.tscn"));
        sections.Add("percussion", ResourceLoader.Load<PackedScene>("res://prefabs/sections/percussion.tscn"));
        sections.Add("timpani", ResourceLoader.Load<PackedScene>("res://prefabs/sections/timpani.tscn"));
        sections.Add("piano", ResourceLoader.Load<PackedScene>("res://prefabs/sections/piano.tscn"));
        sections.Add("harp", ResourceLoader.Load<PackedScene>("res://prefabs/sections/harp.tscn"));
        sections.Add("flute", ResourceLoader.Load<PackedScene>("res://prefabs/sections/flute.tscn"));
    }


}
