using System.Collections.ObjectModel;

namespace CharacterManager.Model.Providers
{
    public interface IDerivedDataProvider
    {

        public ObservableCollection<string> Races
        {
            get;
        }
        public void UpdateRacesList();


        public ObservableCollection<string> Locations
        {
            get;
        }
        public void UpdateLocationsList();


    }
}
