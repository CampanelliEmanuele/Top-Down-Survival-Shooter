using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 200;

	[Signal]
	public delegate void ShootEventHandler(Vector2 StartPos, Vector2 Direction);
	
	public Vector2 ScreenSize;
	
	public bool CanShoot;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Position = ScreenSize / 2;
		CanShoot = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		
		// Limit the player movement to screen size
		Position = Position.Clamp(Vector2.Zero, ScreenSize);
		
		// Player rotation
		// More about this player rotation logic: https://kidscancode.org/godot_recipes/4.x/2d/8_direction/
		Vector2 mousePos = GetLocalMousePosition();
		
		// More about Angle(): https://docs.godotengine.org/en/stable/classes/class_vector2.html#class-vector2
		// More about Snappedf(): https://docs.godotengine.org/en/stable/classes/class_@globalscope.html#class-globalscope-method-snappedf
		var angle = Mathf.Snapped(mousePos.Angle(), Mathf.Pi / 4) / (Mathf.Pi / 4);
		// More about Wrapi(): https://docs.godotengine.org/en/stable/classes/class_@globalscope.html#method-descriptions
		angle = Mathf.Wrap(angle, 0, 8);
		
		//GD.Print("mousePos coordinater: " + mousePos + " angle: " + mousePos.Angle());
		//GD.Print(angle);
		
		// Player animation
		AnimatedSprite2D AS2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		AS2D.Animation = "walk" + angle;
		
		if (Velocity.Length() > 0)
		{
			AS2D.Play();
		}
		else
		{
			AS2D.Stop();
			AS2D.Frame = 1;
		}
	}
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		// Normalizing the vector gives the same speed even in the diagonal movement
		Velocity = inputDirection.Normalized() * Speed;
		
		if (CanShoot && Input.IsActionJustPressed("shoot"))
		{
			Vector2 Direction = GetGlobalMousePosition() - Position;
			EmitSignal(SignalName.Shoot, Position, Direction);
			CanShoot = false;
			GetNode<Timer>("ShotTimer").Start();
		}
		
	}
	
	private void _on_shot_timer_timeout()
	{
		CanShoot = true;
	}

}
