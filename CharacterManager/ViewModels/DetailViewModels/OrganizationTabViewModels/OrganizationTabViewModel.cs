﻿using CharacterManager.Events;
using CharacterManager.Model.Entities;
using CharacterManager.Model.Providers;
using CharacterManager.Model.RedundantTree;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;

namespace CharacterManager.ViewModels.DetailViewModels.OrganizationTabViewModels
{
    public class OrganizationTabViewModel : BindableBase
    {
        public OrganizationTabViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IDerivedDataProvider derivedDataProvider, IEntityProvider entityProvider)
        {
            EA = eventAggregator;
            RM = regionManager;
            EP = entityProvider;
            DDP = derivedDataProvider;

            EA.GetEvent<UIUpdateRequestEvent>().Subscribe( UIUpdateRequestExecute );
        }

        #region Variables
        private readonly IEventAggregator EA;
        private readonly IRegionManager RM;

        private IDerivedDataProvider ddp;
        private readonly IEntityProvider EP;
        public IDerivedDataProvider DDP
        {
            get
            {
                return ddp;
            }
            set
            {
                SetProperty( ref ddp, value );
            }
        }
        #endregion
        #region Binding Targets
        public Organization Org
        {
            get
            {
                if (EP.CurrentTargetAsOrganization != null)
                {
                    return (Organization)EP.CurrentTargetAsOrganization.Item;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<IRTreeMember<IEntity>> ImmidiateChildren
        {
            get
            {
                return EP.GetImmidiateChildren( EP.CurrentTargetAsOrganization );
            }
        }
        public bool IsEntityEnabled
        {
            get
            {
                if (Org is Organization)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Commands
        private DelegateCommand _commandlocationselectionchanged;

        public DelegateCommand CommandLocationSelectionChanged
        {
            get
            {
                return _commandlocationselectionchanged ??= new DelegateCommand( CommandLocationSelectionChangedExecute );
            }
        }

        private DelegateCommand _commandraceselectionchanged;

        public DelegateCommand CommandRaceSelectionChanged
        {
            get
            {
                return _commandraceselectionchanged ??= new DelegateCommand( CommandRaceSelectionChangedExecute );
            }
        }
        #endregion
        #region Command handlers
        private void CommandLocationSelectionChangedExecute()
        {
            DDP.UpdateLocationsList();
        }
        private void CommandRaceSelectionChangedExecute()
        {
            DDP.UpdateRacesList();
        }
        #endregion

        #region Event Handlers
        private void UIUpdateRequestExecute(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.SelectedCharacterChanged:
                    break;
                case ChangeType.SelectedOrganizationChanged:
                    RaisePropertyChanged( nameof( IsEntityEnabled ) );

                    RaisePropertyChanged( nameof( Org ) );
                    RaisePropertyChanged( nameof( ImmidiateChildren ) );
                    break;
                case ChangeType.JobEventListChanged:
                    break;
                case ChangeType.JobListChanged:
                    break;
                case ChangeType.DayAdvanced:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
