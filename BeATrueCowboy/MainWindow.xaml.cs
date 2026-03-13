using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BeATrueCowboy.Controls;
using System.Windows.Threading;
using BeATrueCowboy.Abillities;

namespace BeATrueCowboy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public const Int32 TOGGLE_CHECK_DELAY = 150;
		public const String USER_PROFIE_GROUP_INDEX_RESOURCE_NAME
			= "UserProfieGroupIndex";
		public const String POSITIVE_BUYING_ABILITY_VALUE_RESOURCE_NAME
			= "BuyingAbilityPositiveValueText";
		public const String NEGATIVE_BUYING_ABILITY_VALUE_RESOURCE_NAME
			= "BuyingAbilityNegativeValueText";

		private List<Ability> _Abilities;

		private void CreateAbilities( )
		{
			this._Abilities = new List<Ability>
			{
				new Ability
				{
					Type = AbilityType.DomesticAnimalsBuying,
					Toggle = this.HolsteinFriesianToggle,
					ValueLabel = this.HolsteinFriesianBuyingAbilityValue
				},
				new Ability
				{
					Type = AbilityType.DomesticAnimalsBuying,
					Toggle = this.TexasLonghornToggle,
					ValueLabel = this.TexasLonghornBuyingAbilityValue
				},
				new Ability
				{
					Type = AbilityType.LeatherBootsBuying,
					Toggle = this.FarmerBootsToggle,
					ValueLabel = this.FarmerBootBuyingAbilityValue
				},
				new Ability
				{
					Type = AbilityType.DomesticAnimalsBuying,
					Toggle = this.RaceHorse,
					ValueLabel = this.RaceHorseBuyingAbilityValue
				},
				new Ability
				{
					Type = AbilityType.NoMatter,
					Toggle = this.CowboyHat,
					ValueLabel = null
				},
				new Ability
				{
					Type = AbilityType.NoMatter,
					Toggle = this.MrArgusEyedEagleToggle,
					ValueLabel = null
				},
				new Ability
				{
					Type = AbilityType.NoMatter,
					Toggle = this.MrPathfinder,
					ValueLabel = null
				},
				new Ability
				{
					Type = AbilityType.DomesticAnimalsBuying,
					Toggle = this.Trotter,
					ValueLabel = this.TrotterBuyingAbilityValue
				},
				new Ability
				{
					Type = AbilityType.LeatherBootsBuying,
					Toggle = this.BootsWithSpurs,
					ValueLabel = this.BootsWithSpursBuyingAbilityValue
				}
			}; // _Abilities
		} // CreateAbilities

		public MainWindow( )
		{
			InitializeComponent( );
			this.CreateAbilities( );
			this.SetInitialAbilities( );
		} // MainWindow

		#region General window functions

		public static void SetToggleCheckDelay( Action parNextAction )
		{
			DispatcherTimer timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds( TOGGLE_CHECK_DELAY )
			};
			timer.Start( );
			timer.Tick += ( Object timerSender, EventArgs timerArgs ) =>
			{
				parNextAction( );
				timer.Stop( );
			};
		} // SetToggleCheckDelay

		private void OnCloseToggleChecked(
			Object parSender, RoutedEventArgs parArgs )
		{
			SetToggleCheckDelay( ( ) => this.Close( ) );
		} // OnCloseToggleChecked

		private void OnTitleBarMouseLeftButtonDown( Object sender,
			MouseButtonEventArgs e )
		{
			this.DragMove( );
		} // OnTitleBarMouseLeftButtonDown

		#endregion General window functions

		#region Abilities functions

		private Boolean BootsAreAvailableAndDomesticAnimalsAreNot
			=> this.BootsToggle.IsChecked;
		private Boolean DomesticAnimalsAreAvailableAndBootsAreNot
			=> this.DomesticAnimalsToggle.IsChecked;
		private Boolean AlwaysShowLightIconsForAvailableAbilities
			=> this.LightIconsForAvailableAbilitiesToggle.IsChecked;
		private Boolean AlwaysShowDarkIconsForNotAvailableAbilities
			=> this.DarkIconsForNotAvailableAbilitiesToggle.IsChecked;
		private String PositiveBuyingAbilityValue
			=> ( String )
			this.Resources[ POSITIVE_BUYING_ABILITY_VALUE_RESOURCE_NAME ];
		private String NegativeBuyingAbilityValue
			=> ( String )
			this.Resources[ NEGATIVE_BUYING_ABILITY_VALUE_RESOURCE_NAME ];

		private void SetBootsAreAvailableAndDomesticAnimalsAreNot( )
		{
			if ( this._Abilities == null )
				return;
			foreach ( Ability ability in this._Abilities )
				switch ( ability.Type )
				{
					case AbilityType.LeatherBootsBuying:
						ability.Toggle.IsAvailable = true;
						ability.ValueLabel.Content = this.PositiveBuyingAbilityValue;
						break;
					case AbilityType.DomesticAnimalsBuying:
						ability.Toggle.IsAvailable = false;
						ability.ValueLabel.Content = this.NegativeBuyingAbilityValue;
						break;
					default:
						ability.Toggle.IsAvailable = true;
						break;
				} // switch
		} // SetBootsAreAvailableAndDomesticAnimalsAreNot

		private void SetDomesticAnimalsAreAvailableAndBootsAreNot( )
		{
			if ( this._Abilities == null )
				return;
			foreach ( Ability ability in this._Abilities )
				switch ( ability.Type )
				{
					case AbilityType.LeatherBootsBuying:
						ability.Toggle.IsAvailable = false;
						ability.ValueLabel.Content = this.NegativeBuyingAbilityValue;
						break;
					case AbilityType.DomesticAnimalsBuying:
						ability.Toggle.IsAvailable = true;
						ability.ValueLabel.Content = this.PositiveBuyingAbilityValue;
						break;
					default:
						ability.Toggle.IsAvailable = true;
						break;
				} // switch
		} // SetDomesticAnimalsAreAvailableAndBootsAreNot

		private void SetAlwaysShowLightIconsForAvailableAbilities
			( Boolean parValue )
		{
			if ( this._Abilities == null )
				return;
			foreach ( Ability ability in this._Abilities )
				ability.Toggle.AlwaysShowActiveImageIfIsAvailable = parValue;
		} // SetAlwaysShowLightIconsForAvailableAbilities

		private void SetAlwaysShowDarkIconsForNotAvailableAbilities
			( Boolean parValue )
		{
			if ( this._Abilities == null )
				return;
			foreach ( Ability ability in this._Abilities )
				ability.Toggle.AlwaysShowInactiveImageIfIsNotAvailable = parValue;
		} // SetAlwaysShowDarkIconsForNotAvailableAbilities

		private void SetInitialAbilities( )
		{
			if ( this.BootsAreAvailableAndDomesticAnimalsAreNot )
				this.SetBootsAreAvailableAndDomesticAnimalsAreNot( );
			else
				this.SetDomesticAnimalsAreAvailableAndBootsAreNot( );
			this.SetAlwaysShowLightIconsForAvailableAbilities
				( this.AlwaysShowLightIconsForAvailableAbilities );
			this.SetAlwaysShowDarkIconsForNotAvailableAbilities
				( this.AlwaysShowDarkIconsForNotAvailableAbilities );
		} // SetInitialAbilities

		#region Abilities On..ToggleChecked

		private void OnBootsToggleChecked(
				Object parSender, RoutedEventArgs parArgs )
		{
			this.SetBootsAreAvailableAndDomesticAnimalsAreNot( );
		} // OnBootsToggleChecked

		private void OnDomesticAnimalsToggleChecked(
		Object parSender, RoutedEventArgs parArgs )
		{
			this.SetDomesticAnimalsAreAvailableAndBootsAreNot( );
		} // OnDomesticAnimalsToggleChecked

		private void OnLightIconsForAvailableAbilitiesToggleChecked(
			Object parSender, RoutedEventArgs parArgs )
		{
			this.SetAlwaysShowLightIconsForAvailableAbilities( true );
		} // OnLightIconsForAvailableAbilitiesToggleChecked

		private void OnChangingIconsForAvailableAbilitiesToggleChecked(
			Object parSender, RoutedEventArgs parArgs )
		{
			this.SetAlwaysShowLightIconsForAvailableAbilities( false );
		} // OnChangingIconsForAvailableAbilitiesToggleChecked

		private void OnDarkIconsForNotAvailableAbilitiesToggleChecked(
			Object parSender, RoutedEventArgs parArgs )
		{
			this.SetAlwaysShowDarkIconsForNotAvailableAbilities( true );
		} // OnDarkIconsForNotAvailableAbilitiesToggleChecked

		private void OnChangIconsForNotAvailableAbilitiesToggleChecked(
			Object parSender, RoutedEventArgs parArgs )
		{
			this.SetAlwaysShowDarkIconsForNotAvailableAbilities( false );
		} // OnChangIconsForNotAvailableAbilitiesToggleChecked

		#endregion Abilities On..ToggleChecked

		#endregion Abilities functions

		#region User profilies functions

		private Int32 UserProfieGroupIndex
			=> ( Int32 ) this.Resources[ USER_PROFIE_GROUP_INDEX_RESOURCE_NAME ];
		private Boolean IndianProfileIsAdded
			=> this.IndianProfile.Visibility == Visibility.Visible;
		private Boolean CoyotProfileIsAdded
			=> this.CoyoteProfile.Visibility == Visibility.Visible;
		private Boolean IndianAndCoyotProfiliesAreIsAdded
			=> this.IndianProfileIsAdded && this.CoyotProfileIsAdded;

		private void EnableAddingUsersProfiles( )
			=> this.AddMemberToggle.Visibility = Visibility.Visible;
		private void DisableAddingUsersProfiles( )
			=> this.AddMemberToggle.Visibility = Visibility.Hidden;

		private void AddIndianProfile( )
		{
			this.IndianProfile.Visibility = Visibility.Visible;
			this.IndianProfile.SelectedProfileGroupIndex
				= this.UserProfieGroupIndex;
		} // AddIndianProfile
		private void AddCoyoteProfile( )
		{
			this.CoyoteProfile.Visibility = Visibility.Visible;
			this.CoyoteProfile.SelectedProfileGroupIndex
				= this.UserProfieGroupIndex;
		} // AddCoyoteProfile
		private void RemoveIndianProfile( )
		{
			this.IndianProfile.Visibility = Visibility.Collapsed;
			this.IndianProfile.SelectedProfileGroupIndex
				= SimpleToggleControl.UNDEFINED_GROUP_INDEX;
		} // RemoveIndianProfile
		private void RemoveCoyoteProfile( )
		{
			this.CoyoteProfile.Visibility = Visibility.Collapsed;
			this.CoyoteProfile.SelectedProfileGroupIndex
				= SimpleToggleControl.UNDEFINED_GROUP_INDEX;
		} // RemoveCoyoteProfile

		#region User profilies On..ToggleChecked

		private void OnAddMemberToggleChecked(
				Object parSender, RoutedEventArgs parArgs )
		{
			SetToggleCheckDelay( ( ) =>
				{
					if ( !this.IndianProfileIsAdded )
						this.AddIndianProfile( );
					else
						if ( !this.CoyotProfileIsAdded )
							this.AddCoyoteProfile( );
					if ( this.IndianAndCoyotProfiliesAreIsAdded )
						this.DisableAddingUsersProfiles( );

					this.AddMemberToggle.IsChecked = false;
				}
			);
		} // OnAddMemberToggleChecked

		private void OnDeleteIndianProfileToggleChecked(
				Object parSender, RoutedEventArgs parArgs )
		{
			SetToggleCheckDelay( ( ) =>
				{
					this.RemoveIndianProfile( );
					if ( this.CoyotProfileIsAdded )
						this.EnableAddingUsersProfiles( );
					this.IndianProfile.DeleteProfileIsChecked = false;
				}
			);
		} // OnDeleteIndianProfileToggleChecked

		private void OnCoyoteIndianProfileToggleChecked(
				Object parSender, RoutedEventArgs parArgs )
		{
			SetToggleCheckDelay( ( ) =>
				{
					this.RemoveCoyoteProfile( );
					if ( this.IndianProfileIsAdded )
						this.EnableAddingUsersProfiles( );
					this.CoyoteProfile.DeleteProfileIsChecked = false;
				}
			);
		} // OnCoyoteIndianProfileToggleChecked

		#endregion User profilies On..ToggleChecked

		#endregion User profilies functions

	} // MainWindow
} // BeATrueCowboy