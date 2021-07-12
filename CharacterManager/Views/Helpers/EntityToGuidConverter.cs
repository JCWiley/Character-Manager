using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Ioc;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CharacterManager.Views.Helpers
{
    [ValueConversion( typeof( IRTreeMember<IEntity> ), typeof( Guid ) )]
    public class EntityToGuidConverter : IValueConverter
    {
        readonly IEntityProvider EP;
        public EntityToGuidConverter()
        {
            IContainerProvider CP = (IContainerProvider)Application.Current.Resources["IoC"];

            EP = CP.Resolve<IEntityProvider>();

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IRTreeMember<IEntity> E)
            {
                return E.Gid;
            }
            else
            {
                throw new ArgumentException( "EntityToGuidConverter Convert recived non IRTreeMember<IEntity> type" );
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid G)
            {
                return EP.GetTreeMemberForGuid( G );
            }
            else
            {
                throw new ArgumentException( "EntityToGuidConverter ConvertBack recived non Guid type" );
            }
        }
    }
}
