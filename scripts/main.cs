using Godot;
using System;

public partial class Main : Node
{
	public int PlayerLives;
	public int Wave;
	public int MaxEnemies  { get; set; }
	public int CurrEnemies { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerLives = 3;
		Wave = 1;
		MaxEnemies = 10;
	}
	
	public void _process(double delta)
	{
		GetNode<Label>("/root/Main/Hud/LifeLabel").Text = "X " + PlayerLives.ToString();
		GetNode<Label>("/root/Main/Hud/WaveLabel").Text = "WAVE: " + Wave.ToString();
		GetNode<Label>("/root/Main/Hud/EnemiesLabel").Text = "X: " + CurrEnemies.ToString();
	}

	private void _on_enemy_spawner_damage_player(int damage)
	{
		PlayerLives -= damage;
		GD.Print(PlayerLives);
	}

}


