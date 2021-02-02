using Godot;
using System;

public class Selector : RayCast {

    public override void _Input(InputEvent eInput) {

        if (eInput.IsActionPressed("InstrumentSelect") && IsColliding()) {
            if (GameState.selectedSection != null) GameState.selectedSection.ToggleRightLeftAreas(false);

            Area viewedArea = (Area)GetCollider();
            GD.Print(viewedArea);

            PlayerSection viewedSection;
            try {
                viewedSection = (PlayerSection)viewedArea.GetParent().GetParent();
                GameState.selectedSection = viewedSection;
                GameState.selectedSection.ToggleRightLeftAreas(true);
            } catch {
                GD.Print($"Cannot cast {viewedArea.GetParent().GetParent().GetType()} to type PlayerSection. Is a PlayerSection Scene setup wrongly?");
            }
        }
    }
}
