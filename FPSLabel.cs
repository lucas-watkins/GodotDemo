using Godot;
using System;

public partial class FPSLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var dimensions = GetWindow().Size; 
		Text = "  fps";
		Position = new Vector2(0, 0);
		Size = new Vector2(dimensions.X / 1f, dimensions.Y / 10f); 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $"{(int) Engine.GetFramesPerSecond()} fps";
		if (Input.IsKeyPressed(Key.F))
		{
			Visible = !Visible;
		}
	}
}
