using Godot;
using System;

public partial class Goblin : CharacterBody2D
{
	
	public int _movementSpeed = 200;
	public int _acceleration = 7;

	private NavigationAgent2D _navigationAgent;
	private CharacterBody2D _player;

	private Vector2 _movementTargetPosition;
	
	public bool Alive;

	public override void _Ready()
	{
		base._Ready();
		
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_player = GetNode<CharacterBody2D>("/root/Main/Player");
		_movementTargetPosition = _player.Position;

		Alive = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (Alive)
		{
			if (_navigationAgent.IsNavigationFinished())
			{
				return;
			}

			Vector2 Direction = _navigationAgent.GetNextPathPosition() - GlobalPosition;
			Direction = Direction.Normalized();
			Velocity = Velocity.Lerp(Direction * _movementSpeed, _acceleration * (float)delta);
			
			MoveAndSlide();
		}
	}

	private void _on_timer_timeout()
	{
		_navigationAgent.TargetPosition = _player.GlobalPosition;
	}
	
	public void Die() {
		Alive = false;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "dead";
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled" , true);
		GetNode<Timer>("QueueFreeTimer").Start();
	}

	private void _on_queue_free_timer_timeout()
	{
		QueueFree();
	}
}



