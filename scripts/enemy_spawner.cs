using Godot;
using System;
using System.Collections.Generic;

public partial class enemy_spawner : Node2D
{
	private Node Main;
	
	// See more on the section "Loading scenes" at: https://docs.godotengine.org/en/stable/tutorials/scripting/resources.html#loading-resources-from-code
	private PackedScene GoblinScene = GD.Load<PackedScene>("res://scenes/goblin.tscn");
	
	private List<Marker2D> SpawnPoints = new List<Marker2D>();

	public override void _Ready()
	{
		// See more on the section "@onready annotation" at: https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html#onready-annotation
		Main = GetNode<Node>("/root/Main");
		
		foreach (Node node in GetChildren())
			if (node is Marker2D)
				SpawnPoints.Add((Marker2D)node);
		
		//GD.Print("READY");
	}
	
	private void _on_timer_timeout()
	{
		// Get a random spawn
		int RandomIndex = GD.RandRange(0, SpawnPoints.Count);
		var RandomSpawn = SpawnPoints[RandomIndex];
		
		// Instantiate a new goblin, set the position and add it to the Main Node
		CharacterBody2D Goblin = (CharacterBody2D)GoblinScene.Instantiate();
		Goblin.Position = RandomSpawn.Position;
		Main.AddChild(Goblin);
		// This solution sucks
		Goblin.Name = "Goblin";
		//GD.Print(Goblin.Name);
		
		//GD.Print(Goblin.Position);
	}

}
