using Godot;
using System;
using System.ComponentModel;
using Environment = Godot.Environment;

public partial class CharacterBody3D : Godot.CharacterBody3D
{
	private Camera3D _playerCam;
	private Node3D _cameraNode; 
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		Engine.MaxFps = 120;
		_playerCam = GetNode<Camera3D>("CameraParent/PlayerCamera");
		_cameraNode = GetNode<Node3D>("CameraParent"); 
	}

	private const double Speed = 5.0;

	private void HandleKeys(int movement)
	{
		var velocity = Vector3.Zero; 
		if (Input.IsActionPressed("forward"))
		{
			velocity -= _playerCam.Transform.Basis.Z;
		}
		if (Input.IsActionPressed("right"))
		{
			velocity += _playerCam.Transform.Basis.X; 
		}

		if (Input.IsKeyPressed(Key.A))
		{
			velocity -= _playerCam.Transform.Basis.X; 
		}

		if (Input.IsKeyPressed(Key.S))
		{
			velocity += _playerCam.Transform.Basis.Z;
		}
		
		if (Input.IsKeyPressed(Key.Space))
		{
			velocity += new Vector3(0, movement, 0);
		}

		if (Input.IsKeyPressed(Key.Shift))
		{
			velocity += new Vector3(0, -movement, 0); 
		}

		if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}

		MoveAndCollide(velocity); 
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleKeys((int) Math.Round(delta * Speed * 10));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion motion)
		{
			var temp = _playerCam.RotationDegrees;
			temp.X += motion.Relative.Y * 0.05f;
			temp.Y += motion.Relative.X * 0.05f;

			temp.X = Mathf.Clamp(temp.X, -30.0f, 90.0f);
			_playerCam.RotationDegrees = temp; 
		}
	}
}
