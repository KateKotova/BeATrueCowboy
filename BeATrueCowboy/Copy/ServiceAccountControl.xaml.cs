using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
	/// <summary>
	/// Interaction logic for ServiceAccountControl.xaml
	/// </summary>
	public partial class ServiceAccountControl : UserControl
	{
		public static DependencyProperty AccountIconImgSourceDP;
		public static DependencyProperty AccountNameLblContentDP;
		public static DependencyProperty AccountTeamLblContentDP;
		public static DependencyProperty FavoriteAccountGroupIndexDP;
		public static DependencyProperty FavoriteAccountIsCheckedDP;
		public static DependencyProperty AccountLocalSettingsAreCheckedProperty;
		public static DependencyProperty DeleteAccountIsCheckedDP;

		public static readonly RoutedEvent FavoriteAccountToggleCheckedEvent;
		public static readonly RoutedEvent AccountLocalSettingsToggleCheckedEvent;
		public static readonly RoutedEvent DeleteAccountToggleCheckedEvent;

		static ServiceAccountControl( )
		{
			// Registration of routed event.
			FavoriteAccountToggleCheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( FavoriteAccountToggleChecked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( ServiceAccountControl )
			);
			AccountLocalSettingsToggleCheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( AccountLocalSettingsToggleChecked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( ServiceAccountControl )
			);
			DeleteAccountToggleCheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( DeleteAccountToggleChecked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( ServiceAccountControl )
			);

			// Registration of propeties dependencies.
			AccountIconImgSourceDP = DependencyProperty.Register(
				nameof( AccountIconImageSource ),
				typeof( ImageSource ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAccountIconImgSourceChanged )
				)
			);
			AccountNameLblContentDP = DependencyProperty.Register(
				nameof( AccountNameLabelContent ),
				typeof( Object ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAccountNameLblContentChanged )
				)
			);
			AccountTeamLblContentDP = DependencyProperty.Register(
				nameof( AccountTeamLabelContent ),
				typeof( Object ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAccountTeamLblContentChanged )
				)
			);
			FavoriteAccountIsCheckedDP = DependencyProperty.Register(
				nameof( FavoriteAccountIsChecked ),
				typeof( Boolean ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnFavoriteAccountIsCheckedChanged )
				)
			);
			FavoriteAccountGroupIndexDP = DependencyProperty.Register(
				nameof( FavoriteAccountGroupIndex ),
				typeof( Int32 ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					SimpleToggle.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnFavoriteAccountGroupIndexChanged )
				)
			);
			AccountLocalSettingsAreCheckedProperty = DependencyProperty.Register(
				nameof( AccountLocalSettingsAreChecked ),
				typeof( Boolean ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnAccountLocalSettingsAreCheckedChanged )
				)
			);
			DeleteAccountIsCheckedDP = DependencyProperty.Register(
				nameof( DeleteAccountIsChecked ),
				typeof( Boolean ),
				typeof( ServiceAccountControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnDeleteAccountIsCheckedChanged )
				)
			);
	} // ServiceAccountControl

		public ServiceAccountControl( )
		{
			InitializeComponent( );
			this.FavoriteAccountToggle.IsCheckedChanged
				+= this.OnFavoriteAccountToggleIsChecked;
			this.AccountLocalSettingsToggle.IsCheckedChanged
				+= this.OnAccountLocalSettingsToggleIsChecked;
			this.DeleteAccountToggle.IsCheckedChanged
				+= this.OnDeleteAccountToggleIsChecked;
		} // ServiceAccountControl

		private static void OnAccountIconImgSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newAccountIconImageSource
				= ( ImageSource ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			control.AccountIconImageSource = newAccountIconImageSource;
			control.AccountImageBrushSource.ImageSource = newAccountIconImageSource;
		} // OnAccountIconImgSourceChanged

		private static void OnAccountNameLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newAccountNameLabelContent = ( Object ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			control.AccountNameLabelContent = newAccountNameLabelContent;
			control.AccountNameLabel.Content = newAccountNameLabelContent;
		} // OnAccountNameLblContentChanged

		private static void OnAccountTeamLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newAccountTeamLabelContent = ( Object ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			control.AccountTeamLabelContent = newAccountTeamLabelContent;
			control.AccountTeamLabel.Content = newAccountTeamLabelContent;
		} // OnAccountTeamLblContentChanged

		private static void OnFavoriteAccountIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newFavoriteAccountIsChecked = ( Boolean ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			if ( control.FavoriteAccountToggle.IsChecked != newFavoriteAccountIsChecked )
			{
				control.FavoriteAccountToggle.IsChecked = newFavoriteAccountIsChecked;
				control.FavoriteAccountIsChecked = control.FavoriteAccountToggle.IsChecked;
				if ( control.FavoriteAccountIsChecked )
					control.RaiseFavoriteAccountToggleCheckedEvent( );
			} // if
		} // OnFavoriteAccountIsCheckedChanged

		private static void OnFavoriteAccountGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newFavoriteAccountGroupIndex = ( Int32 ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			control.FavoriteAccountToggle.GroupIndex = newFavoriteAccountGroupIndex;
			control.FavoriteAccountGroupIndex = control.FavoriteAccountToggle.GroupIndex;
		} // OnFavoriteAccountGroupIndexChanged

		private static void OnAccountLocalSettingsAreCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAccountLocalSettingsAreChecked
				= ( Boolean ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			if ( control.AccountLocalSettingsToggle.IsChecked
				!= newAccountLocalSettingsAreChecked )
			{
				control.AccountLocalSettingsToggle.IsChecked
					= newAccountLocalSettingsAreChecked;
				control.AccountLocalSettingsAreChecked
					= control.AccountLocalSettingsToggle.IsChecked;
				if ( control.AccountLocalSettingsAreChecked )
					control.RaiseAccountLocalSettingsToggleCheckedEvent( );
			} // if
		} // OnAccountLocalSettingsAreCheckedChanged

		private static void OnDeleteAccountIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newDeleteAccountIsChecked = ( Boolean ) parArgs.NewValue;
			ServiceAccountControl control = ( ServiceAccountControl ) parSender;
			if ( control.DeleteAccountToggle.IsChecked != newDeleteAccountIsChecked )
			{
				control.DeleteAccountToggle.IsChecked = newDeleteAccountIsChecked;
				control.DeleteAccountIsChecked = control.DeleteAccountToggle.IsChecked;
				if ( control.DeleteAccountIsChecked )
					control.RaiseDeleteAccountToggleCheckedEvent( );
			} // if
		} // OnDeleteAccountIsCheckedChanged

		#region private void On...ToggleIsChecked

		private void OnFavoriteAccountToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.FavoriteAccountIsChecked = this.FavoriteAccountToggle.IsChecked;
		} // OnFavoriteAccountToggleIsChecked

		private void OnAccountLocalSettingsToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.AccountLocalSettingsAreChecked
				= this.AccountLocalSettingsToggle.IsChecked;
		} // OnAccountLocalSettingsToggleIsChecked

		private void OnDeleteAccountToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.DeleteAccountIsChecked = this.DeleteAccountToggle.IsChecked;
		} // OnDeleteAccountToggleIsChecked

		#endregion private void On...ToggleIsChecked

		#region RoutedEventHandler ...ToggleChecked

		public event RoutedEventHandler FavoriteAccountToggleChecked
		{
			add { AddHandler( FavoriteAccountToggleCheckedEvent, value ); }
			remove { RemoveHandler( FavoriteAccountToggleCheckedEvent, value ); }
		} // FavoriteAccountToggleChecked

		public event RoutedEventHandler AccountLocalSettingsToggleChecked
		{
			add { AddHandler( AccountLocalSettingsToggleCheckedEvent, value ); }
			remove { RemoveHandler( AccountLocalSettingsToggleCheckedEvent, value ); }
		} // AccountLocalSettingsToggleChecked

		public event RoutedEventHandler DeleteAccountToggleChecked
		{
			add { AddHandler( DeleteAccountToggleCheckedEvent, value ); }
			remove { RemoveHandler( DeleteAccountToggleCheckedEvent, value ); }
		} // DeleteAccountToggleChecked

		#endregion RoutedEventHandler ...ToggleChecked

		#region Raise...ToggleCheckedEvent

		private void RaiseFavoriteAccountToggleCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( ServiceAccountControl.FavoriteAccountToggleCheckedEvent ) );
		} // RaiseFavoriteAccountToggleCheckedEvent

		private void RaiseAccountLocalSettingsToggleCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( ServiceAccountControl.AccountLocalSettingsToggleCheckedEvent ) );
		} // RaiseAccountLocalSettingsToggleCheckedEvent

		private void RaiseDeleteAccountToggleCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( ServiceAccountControl.DeleteAccountToggleCheckedEvent ) );
		} // RaiseDeleteAccountToggleCheckedEvent

		#endregion Raise...ToggleCheckedEvent

		#region On...ToggleChecked

		private void OnFavoriteAccountToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseFavoriteAccountToggleCheckedEvent( );
		} // OnFavoriteAccountToggleChecked

		private void OnAccountLocalSettingsToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseAccountLocalSettingsToggleCheckedEvent( );
		} // OnAccountLocalSettingsToggleChecked

		private void OnDeleteAccountToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseDeleteAccountToggleCheckedEvent( );
		} // OnDeleteAccountToggleChecked

		#endregion On...ToggleChecked

		#region Properties
		public ImageSource AccountIconImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( AccountIconImgSourceDP );
			}
			set
			{
				SetValue( AccountIconImgSourceDP, value );
			}
		} // AccountIconImageSource

		public Object AccountNameLabelContent
		{
			get
			{
				return ( Object ) GetValue( AccountNameLblContentDP );
			}
			set
			{
				SetValue( AccountNameLblContentDP, value );
			}
		} // AccountNameLabelContent

		public Object AccountTeamLabelContent
		{
			get
			{
				return ( Object ) GetValue( AccountTeamLblContentDP );
			}
			set
			{
				SetValue( AccountTeamLblContentDP, value );
			}
		} // AccountTeamLabelContent

		public Boolean FavoriteAccountIsChecked
		{
			get
			{
				return ( Boolean ) GetValue( FavoriteAccountIsCheckedDP );
			}
			set
			{
				SetValue( FavoriteAccountIsCheckedDP, value );
			}
		} // FavoriteAccountIsChecked

		public Int32 FavoriteAccountGroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( FavoriteAccountGroupIndexDP );
			}
			set
			{
				SetValue( FavoriteAccountGroupIndexDP, value );
			}
		} // FavoriteAccountGroupIndex

		public Boolean AccountLocalSettingsAreChecked
		{
			get
			{
				return ( Boolean ) GetValue( AccountLocalSettingsAreCheckedProperty );
			}
			set
			{
				SetValue( AccountLocalSettingsAreCheckedProperty, value );
			}
		} // AccountLocalSettingsAreChecked

		public Boolean DeleteAccountIsChecked
		{
			get
			{
				return ( Boolean ) GetValue( DeleteAccountIsCheckedDP );
			}
			set
			{
				SetValue( DeleteAccountIsCheckedDP, value );
			}
		} // DeleteAccountIsChecked

		#endregion Properties

	} // ServiceAccountControl
} // Controls
