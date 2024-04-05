using Godot;
using System;

public partial class goblin : CharacterBody2D
{
	
	public int _movementSpeed = 200;
	public int _acceleration = 7;

	private NavigationAgent2D _navigationAgent;
	private CharacterBody2D _player;

	private Vector2 _movementTargetPosition;

	public override void _Ready()
	{
		base._Ready();
		
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_player = GetNode<CharacterBody2D>("/root/Main/Player");
		_movementTargetPosition = _player.Position;

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (_navigationAgent.IsNavigationFinished())
		{
			return;
		}

		Vector2 Direction = _navigationAgent.GetNextPathPosition() - GlobalPosition;
		Direction = Direction.Normalized();
		Velocity = Velocity.Lerp(Direction * _movementSpeed, _acceleration * (float)delta);
		
		MoveAndSlide();
	}

	private void _on_timer_timeout()
	{
		_navigationAgent.TargetPosition = _player.GlobalPosition;
	}

}

