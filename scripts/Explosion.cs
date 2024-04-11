using Godot;
using System;

public partial class Explosion : CpuParticles2D
{
	public override void _Ready()
	{
		Emitting = true;
	}

	public override void _Process(double delta)
	{
		if (!Emitting)
			QueueFree();
	}
}
