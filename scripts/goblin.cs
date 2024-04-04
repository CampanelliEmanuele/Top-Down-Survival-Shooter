using Godot;
using System;

public partial class goblin : CharacterBody2D
{
	
	public int _movementSpeed { get; set; } = 200;

	private NavigationAgent2D _navigationAgent;
	private CharacterBody2D _player;

	private Vector2 _movementTargetPosition;// = new Vector2(70.0f, 226.0f);

	public Vector2 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override void _Ready()
	{
		base._Ready();
		
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_player = GetNode<CharacterBody2D>("/root/Main/Player");
		_movementTargetPosition = _player.Position;

		// These values need to be adjusted for the actor's speed
		// and the navigation layout.
		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 4.0f;

		// Make sure to not await during _Ready.
		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (_navigationAgent.IsNavigationFinished())
		{
			return;
		}

		Vector2 currentAgentPosition = GlobalTransform.Origin;
		Vector2 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * _movementSpeed;
		MoveAndSlide();
	}

	private async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		// Now that the navigation map is no longer empty, set the movement target.
		MovementTarget = _movementTargetPosition;
	}
	
	private void _on_timer_timeout()
	{
		_navigationAgent.TargetPosition = _player.GlobalPosition;
	}

}

