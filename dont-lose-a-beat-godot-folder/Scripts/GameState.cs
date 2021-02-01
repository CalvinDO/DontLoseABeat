using Godot;
using System;

public static class GameState
{
    public static PackedScene mainScene;
    public static PackedScene transitionScene;

    public static PlayerSection selectedSection;
    public static int currentLevel = 0;
}
