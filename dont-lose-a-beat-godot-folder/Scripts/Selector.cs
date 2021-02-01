using Godot;
using System;

public class Selector : RayCast {

    public override void _Input(InputEvent eInput) {

        if (eInput.IsActionPressed("InstrumentSelect") && IsColliding()) {
            try {

                //todo add exceptionhandling
                if (GameState.selectedSection != null) GameState.selectedSection.ToggleRightLeftAreas(false);

                Area viewedArea = (Area)GetCollider();
                PlayerSection viewedSection = (PlayerSection)viewedArea.GetParent().GetParent();
                GameState.selectedSection = viewedSection;
                GameState.selectedSection.ToggleRightLeftAreas(true);
            } catch {
                GD.Print($"input {eInput}");
            }
        }
    }
}
