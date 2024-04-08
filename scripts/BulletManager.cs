using Godot;
using System;

public partial class BulletManager : Node2D
{
	private PackedScene BulletScene = GD.Load<PackedScene>("res://scenes/bullet.tscn");

	private void _on_player_shoot(Vector2 StartPos, Vector2 Direction)
	{
		Bullet bullet = (Bullet)BulletScene.Instantiate();

		bullet.Position = StartPos;
		bullet.Direction = Direction.Normalized();
		bullet.AddToGroup("bullets");
		
		AddChild(bullet);
	}
}
