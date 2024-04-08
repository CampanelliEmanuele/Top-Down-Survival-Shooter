using Godot;
using System;
using System.Collections.Generic;

public partial class enemy_spawner : Node2D
{
	[Signal]
	public delegate void DamagePlayerEventHandler();
	
	private Node2D EnemySpawner;
	
	// See more on the section "Loading scenes" at: https://docs.godotengine.org/en/stable/tutorials/scripting/resources.html#loading-resources-from-code
	private PackedScene GoblinScene = GD.Load<PackedScene>("res://scenes/goblin.tscn");
	
	private List<Marker2D> SpawnPoints = new List<Marker2D>();

	public override void _Ready()
	{
		// See more on the section "@onready annotation" at: https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html#onready-annotation
		EnemySpawner = GetNode<Node2D>("/root/Main/EnemySpawner");
		
		foreach (Node node in GetChildren())
			if (node is Marker2D)
				SpawnPoints.Add((Marker2D)node);
	}
	
	private void _on_timer_timeout()
	{
		// Getting the numbers of alive enemies
		var enemies = GetTree().GetNodesInGroup("enemies");
		
		Main parent = (Main)GetParent<Node>();
		
		if (enemies.Count < parent.MaxEnemies)
		{
			// Get a random spawn
			int RandomIndex = GD.RandRange(0, SpawnPoints.Count);
			var RandomSpawn = SpawnPoints[RandomIndex];
			
			// Instantiate a new goblin, set the position and add it to the EnemySpawner Node
			Goblin Goblin = (Goblin)GoblinScene.Instantiate();
			Goblin.Position = RandomSpawn.Position;
			EnemySpawner.AddChild(Goblin);
			Goblin.AddToGroup("enemies");
			
			// Connecting the Goblin's HitPlayer signal to the EnemySpawner's Hit signal
			//Goblin.Connect("hit_player", this, "OnGoblinHitPlayer");
			//Goblin.Connect("hit_player", OnGoblinHitPlayer);
			Goblin.HitPlayer += OnGoblinHitPlayer;
			
			// This solution sucks
			Goblin.Name = "Goblin";
		}
	}
	
	private void OnGoblinHitPlayer()
	{
		EmitSignal(SignalName.DamagePlayer);
	}

}
