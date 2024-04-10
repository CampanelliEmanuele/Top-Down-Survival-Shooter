using Godot;

public partial class Main : Node
{
	public int Wave;
	public int MaxEnemies  { get; set; }
	public int CurrEnemies { get; set; }

	private Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Wave = 1;
		MaxEnemies = 10;
		_player = (Player)GetNode<CharacterBody2D>("Player");
	}
	
	public void _process(double delta)
	{
		GetNode<Label>("/root/Main/Hud/LifeLabel").Text = "X " + _player._playerLives;
		GetNode<Label>("/root/Main/Hud/WaveLabel").Text = "WAVE: " + Wave.ToString();
		GetNode<Label>("/root/Main/Hud/EnemiesLabel").Text = "X: " + CurrEnemies.ToString();
	}

}
