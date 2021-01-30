using Godot;
using System.Collections.Generic;

public class SimonsOrchestraManager : Spatial
{

    [Export]
    bool loadDynamically = true;

    [Export]
    public int currentLevel;

    public float currentBPM = 130;

    Dictionary<string, PackedScene> sections;

    public override void _Ready()
    {
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

        foreach (Section cSection in this.GetChildren())
        {
            cSection.Play();
        }
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
