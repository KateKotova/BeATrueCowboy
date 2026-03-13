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

namespace BeATrueCowboy.Controls
{
	/// <summary>
	/// Interaction logic for UserProfileControl.xaml
	/// </summary>
	public partial class UserProfileControl : UserControl
	{
		public static DependencyProperty IconImageSourceProperty;
		public static DependencyProperty NameLabelContentProperty;
		public static DependencyProperty NicknameLabelContentProperty;
		public static DependencyProperty SelectedProfileGroupIndexProperty;
		public static DependencyProperty SelectedProfileIsCheckedProperty;
		public static DependencyProperty DeleteProfileIsCheckedProperty;
		public static DependencyProperty CanSelectProfileProperty;
		public static DependencyProperty CanDeleteProfileProperty;

		public static readonly RoutedEvent SelectProfileToggleCheckedEvent;
		public static readonly RoutedEvent DeleteProfileToggleCheckedEvent;

		static UserProfileControl( )
		{
			// Registration of routed event.
			SelectProfileToggleCheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( SelectProfileToggleChecked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( UserProfileControl )
			);
			DeleteProfileToggleCheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( DeleteProfileToggleChecked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( UserProfileControl )
			);

			// Registration of propeties dependencies.
			IconImageSourceProperty = DependencyProperty.Register(
				nameof( IconImageSource ),
				typeof( ImageSource ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnIconImageSourceChanged )
				)
			);
			NameLabelContentProperty = DependencyProperty.Register(
				nameof( NameLabelContent ),
				typeof( Object ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnNameLabelContentChanged )
				)
			);
			NicknameLabelContentProperty = DependencyProperty.Register(
				nameof( NicknameLabelContent ),
				typeof( Object ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnNicknameLabelContentChanged )
				)
			);
			SelectedProfileIsCheckedProperty = DependencyProperty.Register(
				nameof( SelectedProfileIsChecked ),
				typeof( Boolean ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnSelectedProfileIsCheckedChanged )
				)
			);
			SelectedProfileGroupIndexProperty = DependencyProperty.Register(
				nameof( SelectedProfileGroupIndex ),
				typeof( Int32 ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					SimpleToggleControl.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnSelectedProfileGroupIndexChanged )
				)
			);
			DeleteProfileIsCheckedProperty = DependencyProperty.Register(
				nameof( DeleteProfileIsChecked ),
				typeof( Boolean ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnDeleteProfileIsCheckedChanged )
				)
			);
			CanSelectProfileProperty = DependencyProperty.Register(
				nameof( CanSelectProfile ),
				typeof( Boolean ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					true,
					new PropertyChangedCallback( OnCanSelectProfileChanged )
				)
			);
			CanDeleteProfileProperty = DependencyProperty.Register(
				nameof( CanDeleteProfile ),
				typeof( Boolean ),
				typeof( UserProfileControl ),
				new FrameworkPropertyMetadata(
					true,
					new PropertyChangedCallback( OnCanDeleteProfileChanged )
				)
			);
		} // UserProfileControl

		public UserProfileControl( )
		{
			InitializeComponent( );
			this.SelectProfileToggle.IsCheckedChanged
				+= this.OnSelectProfileToggleIsChecked;
			this.DeleteProfileToggle.IsCheckedChanged
				+= this.OnDeleteProfileToggleIsChecked;
			this.SelectProfileToggle.GroupIndexChanged
				+= this.OnSelectProfileToggleGroupIndexChanged;
		} // UserProfileControl

		private static void OnIconImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newIconImageSource
				= ( ImageSource ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			control.IconImageSource = newIconImageSource;
			control.ImageBrushSource.ImageSource = newIconImageSource;
		} // OnIconImageSourceChanged

		private static void OnNameLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newNameLabelContent = ( Object ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			control.NameLabelContent = newNameLabelContent;
			control.NameLabel.Content = newNameLabelContent;
		} // OnNameLabelContentChanged

		private static void OnNicknameLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newNicknameLabelContent = ( Object ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			control.NicknameLabelContent = newNicknameLabelContent;
			control.NicknameLabel.Content = newNicknameLabelContent;
		} // OnNicknameLabelContentChanged

		private static void OnSelectedProfileIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newSelectedProfileIsChecked = ( Boolean ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			if ( control.SelectProfileToggle.IsChecked != newSelectedProfileIsChecked )
			{
				control.SelectProfileToggle.IsChecked = newSelectedProfileIsChecked;
				control.SelectedProfileIsChecked = control.SelectProfileToggle.IsChecked;
				if ( control.SelectedProfileIsChecked )
					control.RaiseSelectProfileToggleCheckedEvent( );
			} // if
		} // OnSelectedProfileIsCheckedChanged

		private static void OnSelectedProfileGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newSelectedProfileGroupIndex = ( Int32 ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			control.SelectProfileToggle.GroupIndex = newSelectedProfileGroupIndex;
			control.SelectedProfileGroupIndex = control.SelectProfileToggle.GroupIndex;
		} // OnSelectedProfileGroupIndexChanged

		private static void OnDeleteProfileIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newDeleteProfileIsChecked = ( Boolean ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			if ( control.DeleteProfileToggle.IsChecked != newDeleteProfileIsChecked )
			{
				control.DeleteProfileToggle.IsChecked = newDeleteProfileIsChecked;
				control.DeleteProfileIsChecked = control.DeleteProfileToggle.IsChecked;
				if ( control.DeleteProfileIsChecked )
					control.RaiseDeleteProfileToggleCheckedEvent( );
			} // if
		} // OnDeleteProfileIsCheckedChanged

		private static void OnCanSelectProfileChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newCanSelectProfile = ( Boolean ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			if ( control.CanSelectProfile != newCanSelectProfile )
			control.CanSelectProfile = newCanSelectProfile;
			control.SelectProfileToggleContainer.Visibility
				= newCanSelectProfile ? Visibility.Visible : Visibility.Hidden;
		} // OnCanSelectProfileChanged

		private static void OnCanDeleteProfileChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newCanDeleteProfile = ( Boolean ) parArgs.NewValue;
			UserProfileControl control = ( UserProfileControl ) parSender;
			control.CanDeleteProfile = newCanDeleteProfile;
			control.DeleteProfileToggleContainer.Visibility
				= newCanDeleteProfile ? Visibility.Visible : Visibility.Hidden;
		} // OnCanDeleteProfileChanged

		private void OnSelectProfileToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.SelectedProfileIsChecked = this.SelectProfileToggle.IsChecked;
		} // OnSelectProfileToggleIsChecked

		private void OnDeleteProfileToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.DeleteProfileIsChecked = this.DeleteProfileToggle.IsChecked;
		} // OnDeleteProfileToggleIsChecked

		private void OnSelectProfileToggleGroupIndexChanged(
			Object parSender, EventArgs parArgs )
		{
			this.SelectedProfileGroupIndex = this.SelectProfileToggle.GroupIndex;
		} // OnSelectProfileToggleGroupIndexChanged

		public event RoutedEventHandler SelectProfileToggleChecked
		{
			add { AddHandler( SelectProfileToggleCheckedEvent, value ); }
			remove { RemoveHandler( SelectProfileToggleCheckedEvent, value ); }
		} // SelectProfileToggleChecked

		public event RoutedEventHandler DeleteProfileToggleChecked
		{
			add { AddHandler( DeleteProfileToggleCheckedEvent, value ); }
			remove { RemoveHandler( DeleteProfileToggleCheckedEvent, value ); }
		} // DeleteProfileToggleChecked

		private void RaiseSelectProfileToggleCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( UserProfileControl.SelectProfileToggleCheckedEvent ) );
		} // RaiseSelectProfileToggleCheckedEvent

		private void RaiseDeleteProfileToggleCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( UserProfileControl.DeleteProfileToggleCheckedEvent ) );
		} // RaiseDeleteProfileToggleCheckedEvent

		private void OnSelectProfileToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseSelectProfileToggleCheckedEvent( );
		} // OnSelectProfileToggleChecked

		private void OnDeleteProfileToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseDeleteProfileToggleCheckedEvent( );
		} // OnDeleteProfileToggleChecked

		#region Properties

		public ImageSource IconImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( IconImageSourceProperty );
			}
			set
			{
				SetValue( IconImageSourceProperty, value );
			}
		} // IconImageSource

		public Object NameLabelContent
		{
			get
			{
				return ( Object ) GetValue( NameLabelContentProperty );
			}
			set
			{
				SetValue( NameLabelContentProperty, value );
			}
		} // NameLabelContent

		public Object NicknameLabelContent
		{
			get
			{
				return ( Object ) GetValue( NicknameLabelContentProperty );
			}
			set
			{
				SetValue( NicknameLabelContentProperty, value );
			}
		} // NicknameLabelContent

		public Boolean SelectedProfileIsChecked
		{
			get
			{
				return ( Boolean ) GetValue( SelectedProfileIsCheckedProperty );
			}
			set
			{
				SetValue( SelectedProfileIsCheckedProperty, value );
			}
		} // SelectedProfileIsChecked

		public Int32 SelectedProfileGroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( SelectedProfileGroupIndexProperty );
			}
			set
			{
				SetValue( SelectedProfileGroupIndexProperty, value );
			}
		} // SelectedProfileGroupIndex

		public Boolean DeleteProfileIsChecked
		{
			get
			{
				return ( Boolean ) GetValue( DeleteProfileIsCheckedProperty );
			}
			set
			{
				SetValue( DeleteProfileIsCheckedProperty, value );
			}
		} // DeleteProfileIsChecked

		public Boolean CanSelectProfile
		{
			get
			{
				return ( Boolean ) GetValue( CanSelectProfileProperty );
			}
			set
			{
				SetValue( CanSelectProfileProperty, value );
			}
		} // CanSelectProfile

		public Boolean CanDeleteProfile
		{
			get
			{
				return ( Boolean ) GetValue( CanDeleteProfileProperty );
			}
			set
			{
				SetValue( CanDeleteProfileProperty, value );
			}
		} // CanDeleteProfile

		#endregion Properties

	} // UserProfileControl
} // BeATrueCowboy.Controls
