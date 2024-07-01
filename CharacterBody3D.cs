using System;
using Godot;
public partial class CharacterBody3D : Godot.CharacterBody3D
{
	private Camera3D _playerCam;
	private Node3D _cameraNode;
	private const float Speed = 20f;
	private float _gravity = -9.8f;
	private bool _jumping;
	private int _jumpHeight = 9;
	private int _jumpForce = 3;
	private float _tempJumpPos; 
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized); 
		Engine.MaxFps = 120;
		_playerCam = GetNode<Camera3D>("CameraParent/PlayerCamera");
		_cameraNode = GetNode<Node3D>("CameraParent"); 
		
	}

	private void HandleKeys(float sens)
	{
		var velocity = Vector3.Zero; 
		if (Input.IsActionPressed("forward"))
		{
			velocity += _playerCam.Transform.Basis.Z;
		}
		if (Input.IsActionPressed("right"))
		{
			velocity -= _playerCam.Transform.Basis.X; 
		}

		if (Input.IsActionPressed("left"))
		{
			velocity += _playerCam.Transform.Basis.X; 
		}

		if (Input.IsActionPressed("backward"))
		{
			velocity -= _playerCam.Transform.Basis.Z;
		}
		
		// this single line of code is preventing the player from taking off when looking up
		velocity.Y = 0; 
		
		if (Input.IsActionPressed("up") && IsOnFloor())
		{
			_tempJumpPos = Position.Y; 
				_jumping = true;
		}
		
		if (Input.IsKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}

		if (_jumping && Position.Y < _jumpHeight + _tempJumpPos)
		{
			velocity -= new Vector3(0, _jumpForce, 0); 
		}

		if (Position.Y >= _jumpHeight)
		{
			_tempJumpPos = 0f; 
			_jumping = false; 
		}
		
		Velocity = velocity * -sens; 
		MoveAndSlide(); 
	}
	
	public override void _PhysicsProcess(double delta)
	{
		HandleKeys(Speed);
		
		// gravity
		if (!IsOnFloor())
		{
			var pos = Position;
			pos.Y += (float)delta * _gravity * 2;
			Position = pos; 
		}
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
