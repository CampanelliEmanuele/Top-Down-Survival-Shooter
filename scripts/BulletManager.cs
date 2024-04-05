using Godot;
using System;

public partial class BulletManager : Node2D
{
	private PackedScene BulletScene = GD.Load<PackedScene>("res://scenes/bullet.tscn");

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_player_shoot(Vector2 StartPos, Vector2 Direction)
	{
		//Area2D bulletInstance = (Area2D)BulletScene.Instantiate();
		//Bullet bullet = bulletInstance.GetNode<Bullet>(".");

		Bullet bullet = (Bullet)BulletScene.Instantiate();

		bullet.Position = StartPos;
		bullet.Direction = Direction.Normalized();
		bullet.AddToGroup("bullets");
		
		AddChild(bullet);
	}
}

