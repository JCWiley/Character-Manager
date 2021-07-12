namespace CharacterManager.Model.Items
{
    public class Item : IItem
    {
        public Item()
        {
            aquired = false;
            name = "";
            location = "";
            description = "";
            weight = 0;
        }
        #region Data_Members
        private bool aquired;
        public bool Aquired
        {
            get
            {
                return aquired;
            }
            set
            {
                if (aquired != value)
                {
                    aquired = value;
                }
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                }
            }
        }

        private float weight;
        public float Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight != value)
                {
                    weight = value;
                }
            }
        }
        #endregion
    }
}
