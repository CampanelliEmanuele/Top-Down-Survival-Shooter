namespace scripts.factories
{
    using scripts.enums;
    using Godot;
    using System;

    public class ItemFactory
    {
        
		private static readonly string _commonPath = ProjectSettings.GlobalizePath("res://assets/items/");

        private static readonly string _commonExtension = ".png";
        private static readonly string _commonSuffix = "_box" + _commonExtension;

        public static Tuple<ItemType, Texture2D> GetRandomItem() {
            ItemType randomType = getRandomItemType();
            
		    Texture2D texture = getItemTexture(randomType);

            return Tuple.Create(randomType, texture);
        }

        private static ItemType getRandomItemType()
        {
            // Get an array of enum values
            ItemType[] types = (ItemType[])Enum.GetValues(typeof(ItemType));
            
            int randomIndex = new Random().Next(0, types.Length);
            
            return types[randomIndex];
        }

        private static Texture2D getItemTexture(ItemType type)
        {
            // Convert the enum type to string
            string strType = type.ToString();
            
            // Set the first character to lowercase
            string lowerCaseStrType = Char.ToLower(strType[0]) + strType.Substring(1);
		    
            return GD.Load<Texture2D>(_commonPath + lowerCaseStrType + _commonSuffix);
        }

    }

}