using Godot;
using System;

public partial class main : Node
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
		
		GetNode<Label>("LifeLabel").Text = "X " + PlayerLives.ToString();
		GetNode<Label>("WaveLabel").Text = "WAVE: " + Wave.ToString();
		GetNode<Label>("EnemiesLabel").Text = "X: " + CurrEnemies.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_enemy_spawner_damage_player()
	{
		// Replace with function body.
	}

}

