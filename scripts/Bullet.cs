using Godot;
using System;

public partial class Bullet : Area2D
{
	
	public Vector2 Direction { get; set; }
	public int Speed { get; set; } = 500;
	
	public override void _Process(double delta)
	{
		// Calculate the movement vector by multiplying each component of Direction by Speed
		//Vector2 movement = new Vector2(Direction.X * Speed * (float)delta, Direction.Y * Speed * (float)delta);
		
		// Update the position using the calculated movement vector
		//Position += movement;
		
		Position += Direction * Speed * (float)delta;
	}
	

}
