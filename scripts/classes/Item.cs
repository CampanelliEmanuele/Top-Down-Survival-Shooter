namespace scripts.classes
{
	using Godot;
	using scripts.enums;
	using scripts.factories;
	using System;

	public partial class Item : Area2D
	{
		private ItemType _type;

		private Texture2D _texture;

		public override void _Ready() {
			Tuple<ItemType, Texture2D> itemDatas = ItemFactory.GetRandomItem();

			_type = itemDatas.Item1;
			_texture = itemDatas.Item2;

			GetNode<Sprite2D>("Sprite2D").Texture = _texture;
		}

		private void _on_body_entered(Player player)
		{
			switch (_type)
			{
				case ItemType.Coffee:
					player.BoostSpeed();
					break;
				
				case ItemType.Gun:
					break;
				
				case ItemType.Health:
					player.increaseLife(1);
					break;
			}

			QueueFree();
		}

	}
}
