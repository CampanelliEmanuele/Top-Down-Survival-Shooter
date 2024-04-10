using Godot;

public partial class Goblin : CharacterBody2D
{
	public int _movementSpeed = 200;
	public int _acceleration = 7;

	private NavigationAgent2D _navigationAgent;
	private CharacterBody2D _player;
	private Vector2 _movementTargetPosition;
	
	public bool Alive;
	
	[Signal]
	public delegate void HitPlayerEventHandler();

	private PackedScene ItemScene = GD.Load<PackedScene>("res://scenes/Item.tscn");
	
	private Node _itemsNode;

	public override void _Ready()
	{
		base._Ready();
		
		_navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_player = GetNode<CharacterBody2D>("/root/Main/Player");
		_itemsNode = GetNode<Node>("/root/Main/ItemsNode");
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
	
	public void Die()
	{
		Alive = false;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "dead";
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled" , true);
		GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled" , true);
		
		GetNode<Timer>("QueueFreeTimer").Start();
		DropItem();
	}
	
	private void DropItem()
	{
		Area2D item = (Area2D)ItemScene.Instantiate();
		item.Position = this.Position;
		item.AddToGroup("items");
		// The following two lines are equivalent
		//_itemsNode.CallDeferred("add_child", item);
		_itemsNode.CallDeferred(Node.MethodName.AddChild, item);
	}

	private void _on_queue_free_timer_timeout()
	{
		QueueFree();
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body.Name == "Player")
			EmitSignal(SignalName.HitPlayer);
	}

}




