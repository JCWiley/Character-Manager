﻿#pragma checksum "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C894EC3DF2BA3DC7814923FBFFE1D783D7E7D8B1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CharacterManager.Views.DetailViews.CharacterTabViews;
using CharacterManager.Views.Helpers;
using Prism.Interactivity;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Services.Dialogs;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CharacterManager.Views.DetailViews.CharacterTabViews {
    
    
    /// <summary>
    /// CharacterTabView
    /// </summary>
    public partial class CharacterTabView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Description_Label;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Quirks_Label;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FullName_Label;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Alias_Label;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Occupation_Label;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Location_Label;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Birthplace_Label;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Race_Label;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox Character_Description_RTB;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox Quirks_RTB;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CharacterLocationComboBox;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OccupationTextBlock;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RaceTextBlock;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AliasTextBlock;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BirthPlaceTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CharacterManager;component/views/detailviews/charactertabviews/charactertabview." +
                    "xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Views\DetailViews\CharacterTabViews\CharacterTabView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Description_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Quirks_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.FullName_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Alias_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Occupation_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Location_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.Birthplace_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.Race_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Character_Description_RTB = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 10:
            this.Quirks_RTB = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 11:
            this.CharacterLocationComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 12:
            this.OccupationTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.RaceTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.NameTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.AliasTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.BirthPlaceTextBlock = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

