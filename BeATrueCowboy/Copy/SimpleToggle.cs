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
	/// Interaction logic for SimpleToggle.xaml
	/// </summary>
	public partial class SimpleToggle : UserControl
	{
		public static DependencyProperty ActiveStateUIElemDP;
		public static DependencyProperty InactiveStateUIElemDP;
		public static DependencyProperty AdditionalHitAreaUIElemDP;
		public static DependencyProperty IsCheckedDP;
		public static DependencyProperty IsAvailableDP;
		public static DependencyProperty AlwaysShowActiveImgIfIsAvailableDP;
		public static DependencyProperty AlwaysShowInactiveImgIfIsNotAvailableDP;
		public static DependencyProperty AssociatedUIElemDP;
		public static DependencyProperty GroupIndexDP;

		public event EventHandler ActiveStateUIElemChanged;
		public event EventHandler InActiveStateUIElemChanged;
		public event EventHandler AdditionalHitAreaUIElemChanged;
		public event EventHandler IsCheckedChanged;
		public event EventHandler IsAvailableChanged;
		public event EventHandler AlwaysShowActiveImgIfIsAvailableChanged;
		public event EventHandler AlwaysShowInactiveImgIfIsNotAvailableChanged;
		public event EventHandler AssociatedUIElemChanged;
		public event EventHandler GroupIndexChanged;

		public ImageSource ActiveImgSrc;
		public ImageSource InactiveImgSrc;

		public const Int32 UNDEFINED_GROUP_INDEX = -1;

		private static Dictionary<Int32, List<SimpleToggle>> _Groups
			= new Dictionary<Int32, List<SimpleToggle>>( );

		public static readonly RoutedEvent CheckedEvent;

		static SimpleToggle( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( SimpleToggle )
			);
			// Registration of propeties dependencies.
			ActiveStateUIElemDP = DependencyProperty.Register(
				nameof( ActiveStateUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnActiveStateUIElemChanged )
				)
			);
			InactiveStateUIElemDP = DependencyProperty.Register(
				nameof( InactiveStateUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnInActiveStateUIElemChanged )
				)
			);
			AdditionalHitAreaUIElemDP = DependencyProperty.Register(
				nameof( AdditionalHitAreaUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAdditionalHitAreaUIElemChanged )
				)
			);
			IsCheckedDP = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			IsAvailableDP = DependencyProperty.Register(
				nameof( IsAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsAvailableChanged )
				)
			);
			AlwaysShowActiveImgIfIsAvailableDP
					= DependencyProperty.Register(
				nameof( AlwaysShowActiveImageIfIsAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback
						( OnAlwaysShowActiveImgIfIsAvailableChanged )
				)
			);
			AlwaysShowInactiveImgIfIsNotAvailableDP
					= DependencyProperty.Register(
				nameof( AlwaysShowInactiveImageIfIsNotAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback
						( OnAlwaysShowInactiveImgIfIsNotAvailableChanged )
				)
			);
			AssociatedUIElemDP = DependencyProperty.Register(
					nameof( AssociatedUIElement ),
					typeof( UIElement ),
					typeof( SimpleToggle ),
					new FrameworkPropertyMetadata(
						null,
						new PropertyChangedCallback( OnAssociatedUIElemChanged )
					)
				);
			GroupIndexDP = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( SimpleToggle ),
				new FrameworkPropertyMetadata(
					UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
		} // static SimpleToggle

		public SimpleToggle( )
		{
			InitializeComponent( );
		} // SimpleToggle

		private void SetActiveImageSourceFromLabeledImageDataControl
			( LabeledImageDataControl parHavingActiveImageControl )
		{
			if ( parHavingActiveImageControl == null )
				return;

			this.ActiveImgSrc = parHavingActiveImageControl.ImageSource;

			if ( this.IsAvailable )
			{
				if ( this.AlwaysShowActiveImageIfIsAvailable )
				{
					LabeledImageDataControl havingInactiveImageControl
						= this.InactiveStateUIElement as LabeledImageDataControl;
					if ( havingInactiveImageControl != null )
						havingInactiveImageControl.ImageSource = this.ActiveImgSrc;
				}
			} // if ( this.IsAvailable )
			else
				if ( ( this.InactiveImgSrc != null )
						&& this.AlwaysShowInactiveImageIfIsNotAvailable )
				parHavingActiveImageControl.ImageSource = this.InactiveImgSrc;
		} // SetActiveImageSourceFromLabeledImageDataControl

		private void SetInactiveImageSourceFromLabeledImageDataControl
			( LabeledImageDataControl parHavingInactiveImageControl )
		{
			if ( parHavingInactiveImageControl == null )
				return;

			this.InactiveImgSrc = parHavingInactiveImageControl.ImageSource;

			if ( !this.IsAvailable )
			{
				if ( this.AlwaysShowInactiveImageIfIsNotAvailable )
				{
					LabeledImageDataControl havingActiveImageControl
						= this.ActiveStateUIElement as LabeledImageDataControl;
					if ( havingActiveImageControl != null )
						havingActiveImageControl.ImageSource = this.InactiveImgSrc;
				}
			} // if ( !this.IsAvailable )
			else
				if ( ( this.ActiveImgSrc != null )
						&& this.AlwaysShowActiveImageIfIsAvailable )
				parHavingInactiveImageControl.ImageSource = this.ActiveImgSrc;
		} // SetInactiveImageSourceFromLabeledImageDataControl

		private void OnActiveStateUIElementImgSourceChanged
			( Object parSender, EventArgs parArgs )
		{
			LabeledImageDataControl havingActiveImageControl
				= parSender as LabeledImageDataControl;
			if ( parSender != null )
				this.SetActiveImageSourceFromLabeledImageDataControl
					( havingActiveImageControl );
		} // OnActiveStateUIElementImgSourceChanged

		private void OnInactiveStateUIElementImgSourceChanged
			( Object parSender, EventArgs parArgs )
		{
			LabeledImageDataControl havingInactiveImageControl
				= parSender as LabeledImageDataControl;
			if ( parSender != null )
				this.SetInactiveImageSourceFromLabeledImageDataControl
					( havingInactiveImageControl );
		} // OnActiveStateUIElementImgSourceChanged


		private static void OnActiveStateUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newActiveStateUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			if ( control.ActiveStateUIElement != null )
			{
				control.ActiveStateUIElement.MouseDown -= control.OnMouseDown;
				LabeledImageDataControl oldHavingActiveImageControl
					= control.ActiveStateUIElement as LabeledImageDataControl;
				if ( oldHavingActiveImageControl != null )
					oldHavingActiveImageControl.ImgSourceChanged
						-= control.OnActiveStateUIElementImgSourceChanged;
			}
			control.ActiveStateUIElement = newActiveStateUIElement;
			control.ActiveStateUIElement.MouseDown += control.OnMouseDown;

			LabeledImageDataControl havingActiveImageControl
				= control.ActiveStateUIElement as LabeledImageDataControl;
			if ( havingActiveImageControl != null )
			{
				havingActiveImageControl.ImgSourceChanged
						+= control.OnActiveStateUIElementImgSourceChanged;
				control.SetActiveImageSourceFromLabeledImageDataControl
					( havingActiveImageControl );
			}
			else
				control.ActiveImgSrc = null;

			if ( control.IsChecked )
			{
				control.ActiveStateUIElement.Visibility = Visibility.Visible;
				if ( control.InactiveStateUIElement != null )
					control.InactiveStateUIElement.Visibility = Visibility.Collapsed;
			}
			else
			{
				control.ActiveStateUIElement.Visibility = Visibility.Collapsed;
				if ( control.InactiveStateUIElement != null )
					control.InactiveStateUIElement.Visibility = Visibility.Visible;
			}
			control.ActiveStateUIElemChanged?.Invoke( control, EventArgs.Empty );
		} // OnActiveStateUIElemChanged

		private static void OnInActiveStateUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newInactiveStateUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			if ( control.InactiveStateUIElement != null )
			{
				control.InactiveStateUIElement.MouseDown -= control.OnMouseDown;
				LabeledImageDataControl oldHavingInactiveImageControl
					= control.InactiveStateUIElement as LabeledImageDataControl;
				if ( oldHavingInactiveImageControl != null )
					oldHavingInactiveImageControl.ImgSourceChanged
						-= control.OnInactiveStateUIElementImgSourceChanged;
			}
			control.InactiveStateUIElement = newInactiveStateUIElement;
			control.InactiveStateUIElement.MouseDown += control.OnMouseDown;

			LabeledImageDataControl havingInactiveImageControl
				= control.InactiveStateUIElement as LabeledImageDataControl;
			if ( havingInactiveImageControl != null )
			{
				havingInactiveImageControl.ImgSourceChanged
					+= control.OnInactiveStateUIElementImgSourceChanged;
				control.SetInactiveImageSourceFromLabeledImageDataControl
					( havingInactiveImageControl );
			} // if ( havingInactiveImageControl != null )
			else
				control.InactiveImgSrc = null;

			if ( control.IsChecked )
			{
				control.InactiveStateUIElement.Visibility = Visibility.Collapsed;
				if ( control.ActiveStateUIElement != null )
					control.ActiveStateUIElement.Visibility = Visibility.Visible;
			}
			else
			{
				control.InactiveStateUIElement.Visibility = Visibility.Visible;
				if ( control.ActiveStateUIElement != null )
					control.ActiveStateUIElement.Visibility = Visibility.Collapsed;
			}
			control.InActiveStateUIElemChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnInActiveStateUIElemChanged

		private static void OnAdditionalHitAreaUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAdditionalHitAreaUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			if ( control.AdditionalHitAreaUIElement != null )
				control.AdditionalHitAreaUIElement.MouseDown -= control.OnMouseDown;
			control.AdditionalHitAreaUIElement = newAdditionalHitAreaUIElement;
			control.AdditionalHitAreaUIElement.MouseDown += control.OnMouseDown;
			control.AdditionalHitAreaUIElemChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAdditionalHitAreaUIElemChanged

		private void OnMouseDown( Object parSender, MouseEventArgs parArgs )
		{
			this.IsChecked = true;
		} // OnMouseDown

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			control.IsChecked = newIsChecked;

			if ( control.IsChecked )
			{
				if ( control.InactiveStateUIElement != null )
					control.InactiveStateUIElement.Visibility = Visibility.Collapsed;
				if ( control.ActiveStateUIElement != null )
					control.ActiveStateUIElement.Visibility = Visibility.Visible;
			}
			else
			{
				if ( control.InactiveStateUIElement != null )
					control.InactiveStateUIElement.Visibility = Visibility.Visible;
				if ( control.ActiveStateUIElement != null )
					control.ActiveStateUIElement.Visibility = Visibility.Collapsed;
			}

			control.ResetAssociatedUIElementVisibility( );

			if ( !control.GroupIndexIsUndefined( ) )
			{
				List<SimpleToggle> currentGroupControls
					= control.GetCurrentGroupControlsAndAddToItIfItIsEmpty( );

				UncheckGroupOtherControlsIfDefinedIsChecked
					( control, currentGroupControls );
			} // if
			control.IsCheckedChanged?.Invoke( control, EventArgs.Empty );
			if ( control.IsChecked )
				control.RaiseCheckedEvent( );
		} // OnIsCheckedChanged

		private static void OnIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsAvailable = ( Boolean ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			control.IsAvailable = newIsAvailable;

			ImageSource newActiveImageSource;
			ImageSource newInactiveImageSource;
			if ( control.IsAvailable )
			{
				newActiveImageSource = control.ActiveImgSrc;
				newInactiveImageSource = control.AlwaysShowActiveImageIfIsAvailable
					? control.ActiveImgSrc : control.InactiveImgSrc;
			}
			// if ( ! control.IsAvailable )
			else
			{
				newInactiveImageSource = control.InactiveImgSrc;
				newActiveImageSource = control.AlwaysShowInactiveImageIfIsNotAvailable
					? control.InactiveImgSrc : control.ActiveImgSrc;
			}

			if ( newActiveImageSource != null )
			{
				LabeledImageDataControl havingActiveImageControl
					= control.ActiveStateUIElement as LabeledImageDataControl;
				if ( havingActiveImageControl != null )
					havingActiveImageControl.ImageSource = newActiveImageSource;
			} // if ( newActiveImageSource != null )
			if ( newInactiveImageSource != null )
			{
				LabeledImageDataControl havingInactiveImageControl
					= control.InactiveStateUIElement as LabeledImageDataControl;
				if ( havingInactiveImageControl != null )
					havingInactiveImageControl.ImageSource = newInactiveImageSource;
			} // if ( newInactiveImageSource != null )

			control.IsAvailableChanged?.Invoke( control, EventArgs.Empty );
		} // OnIsAvailableChanged

		private static void OnAlwaysShowActiveImgIfIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowActiveImageIfIsAvailable
				= ( Boolean ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			control.AlwaysShowActiveImageIfIsAvailable
				= newAlwaysShowActiveImageIfIsAvailable;

			if ( control.IsAvailable )
			{
				ImageSource newInactiveImageSource
					= control.AlwaysShowActiveImageIfIsAvailable
					? control.ActiveImgSrc : control.InactiveImgSrc;
				if ( newInactiveImageSource != null )
				{
					LabeledImageDataControl havingInactiveImageControl
						= control.InactiveStateUIElement as LabeledImageDataControl;
					if ( havingInactiveImageControl != null )
						havingInactiveImageControl.ImageSource = newInactiveImageSource;
				} // if ( newInactiveImageSource != null )
			} // if ( control.IsAvailable )
			control.AlwaysShowActiveImgIfIsAvailableChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAlwaysShowActiveImgIfIsAvailableChanged

		private static void OnAlwaysShowInactiveImgIfIsNotAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowInactiveImageIfIsNotAvailable
				= ( Boolean ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			control.AlwaysShowInactiveImageIfIsNotAvailable
				= newAlwaysShowInactiveImageIfIsNotAvailable;

			if ( !control.IsAvailable )
			{
				ImageSource newActiveImageSource
					= control.AlwaysShowInactiveImageIfIsNotAvailable
					? control.InactiveImgSrc : control.ActiveImgSrc;
				if ( newActiveImageSource != null )
				{
					LabeledImageDataControl havingActiveImageControl
						= control.ActiveStateUIElement as LabeledImageDataControl;
					if ( havingActiveImageControl != null )
						havingActiveImageControl.ImageSource = newActiveImageSource;
				} // if ( newInactiveImageSource != null )
			} // if ( !control.IsAvailable )
			control.AlwaysShowInactiveImgIfIsNotAvailableChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAlwaysShowInactiveImgIfIsNotAvailableChanged

		private static void OnAssociatedUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggle control = ( SimpleToggle ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.ResetAssociatedUIElementVisibility( );
			control.AssociatedUIElemChanged?.Invoke( control, EventArgs.Empty );
		} // OnAssociatedUIElemChanged

		private void ResetAssociatedUIElementVisibility( )
		{
			if ( this.AssociatedUIElement != null )
				this.AssociatedUIElement.Visibility
					= this.IsChecked ? Visibility.Visible : Visibility.Collapsed;
		} // ResetAssociatedUIElementVisibility

		private static Boolean GroupIndexIsUndefined( Int32 parGroupIndex )
		{
			return ( parGroupIndex == UNDEFINED_GROUP_INDEX )
				|| ( parGroupIndex < 0 );
		} // GroupIndexIsUndefined

		private Boolean GroupIndexIsUndefined( )
		{
			if ( _Groups == null )
				_Groups = new Dictionary<Int32, List<SimpleToggle>>( );
			if ( GroupIndexIsUndefined( this.GroupIndex ) )
			{
				this.GroupIndex = UNDEFINED_GROUP_INDEX;
				return true;
			}
			return false;
		} // GroupIndexIsUndefined

		private void DeleteFromGroup( Int32 parGroupIndex )
		{
			if ( GroupIndexIsUndefined( parGroupIndex ) )
				return;

			KeyValuePair<Int32, List<SimpleToggle>>? group =
				_Groups.FirstOrDefault( ( KeyValuePair<Int32,
				List<SimpleToggle>> currentGroup )
				=> currentGroup.Key == parGroupIndex );
			if ( ( group == null ) || ( group.Value.Value == null ) )
				return;
			List<SimpleToggle> groupControls = _Groups[ parGroupIndex ];
			if ( groupControls == null )
				return;
			groupControls.Remove( this );
			/*groupControls.RemoveAt(
				groupControls.FindIndex( ( SimpleToggle currentControl )
					=> currentControl.Equals( this ) )
			);*/
		} // DeleteFromGroup

		public void DeleteFromCurrentGroup( )
		{
			this.DeleteFromGroup( this.GroupIndex );
			this.GroupIndex = UNDEFINED_GROUP_INDEX;
		} // DeleteFromCurrentGroup

		public static void DeleteAllGroups( )
		{
			_Groups.Clear( );
		} // DeleteFromCurrentGrop

		/// <summary>
		/// Should call it on the <see cref="parWindow"/> closing
		/// to delete all of it's controls from the <see cref="_Groups"/>.
		/// If the window closing and the controls are not deleted they are still
		/// in the <see cref="_Groups"/>. If the Window recreates the new controls
		/// are added to the _Groups, and if tham were not deleted
		/// there is the double complect of them in the <see cref="_Groups"/>.
		/// </summary>
		/// <param name="parWindow"></param>
		public static void DeleteWindowChildrenFromGroups( Window parWindow )
		{
			if ( ( _Groups == null ) || ( _Groups.Count( ) == 0 ) )
				return;

			foreach ( KeyValuePair<Int32, List<SimpleToggle>>
				group in _Groups )
			{
				List<SimpleToggle> controls = group.Value;
				if ( ( controls == null ) || ( controls.Count( ) == 0 ) )
					continue;

				for ( Int32 controlIndex = controls.Count( ) - 1;
						controlIndex >= 0; controlIndex-- )
				{
					Window parentWindow = Window.GetWindow( controls[ controlIndex ] );
					if ( ( parentWindow != null ) && ( parentWindow.Equals( parWindow ) ) )
						controls.Remove( controls[ controlIndex ] );
				} // for
			} // foreach
		} // DeleteWindowChildrenFromGroups

		private List<SimpleToggle>
			GetCurrentGroupControlsAndAddToItIfItIsEmpty( )
		{
			List<SimpleToggle> currentGroupControls = null;

			KeyValuePair<Int32, List<SimpleToggle>>? group =
				_Groups.FirstOrDefault( ( KeyValuePair<Int32,
				List<SimpleToggle>> currentGroup )
				=> currentGroup.Key == this.GroupIndex );
			if ( ( group == null ) || ( group.Value.Value == null ) )
			{
				currentGroupControls = new List<SimpleToggle> { this };
				_Groups.Add( this.GroupIndex, currentGroupControls );
			}
			else
			{
				currentGroupControls = _Groups[ this.GroupIndex ];
				if ( currentGroupControls == null )
				{
					currentGroupControls = new List<SimpleToggle> { this };
					_Groups[ this.GroupIndex ] = currentGroupControls;
				}
			}
			return currentGroupControls;
		} // GetCurrentGroupControlsAndAddToItIfItIsEmpty

		private static void UncheckGroupOtherControlsIfDefinedIsChecked
			( SimpleToggle parDefinedControl,
			List<SimpleToggle> parGroupControls )
		{
			if ( ( !parDefinedControl.IsChecked )
					|| ( parGroupControls == null )
					|| ( parGroupControls.Count( ) == 0 ) )
				return;

			foreach ( SimpleToggle control in parGroupControls )
				if ( !control.Equals( parDefinedControl ) )
					control.IsChecked = false;
		} // UncheckGroupOtherControlsIfDefinedIsChecked

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			SimpleToggle control
				= ( SimpleToggle ) parSender;
			control.DeleteFromGroup( ( Int32 ) parArgs.OldValue );

			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			control.GroupIndex = newGroupIndex;

			if ( !control.GroupIndexIsUndefined( ) )
			{

				List<SimpleToggle> currentGroupControls = control
					.GetCurrentGroupControlsAndAddToItIfItIsEmpty( );

				if ( !currentGroupControls.Contains( control ) )
					currentGroupControls.Add( control );

				UncheckGroupOtherControlsIfDefinedIsChecked
					( control, currentGroupControls );
			} // if
			control.GroupIndexChanged?.Invoke( control, EventArgs.Empty );
		} // OnGroupIndexChanged

		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs( SimpleToggle.CheckedEvent ) );
		} // RaiseCheckedEvent

		#region Properties

		public UIElement ActiveStateUIElement
		{
			get
			{
				return ( UIElement ) GetValue( ActiveStateUIElemDP );
			}
			set
			{
				SetValue( ActiveStateUIElemDP, value );
			}
		} // ActiveStateUIElement

		public UIElement InactiveStateUIElement
		{
			get
			{
				return ( UIElement ) GetValue( InactiveStateUIElemDP );
			}
			set
			{
				SetValue( InactiveStateUIElemDP, value );
			}
		} // ActiveStateUIElement

		public UIElement AdditionalHitAreaUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AdditionalHitAreaUIElemDP );
			}
			set
			{
				SetValue( AdditionalHitAreaUIElemDP, value );
			}
		} // ActiveStateUIElement

		public Boolean IsChecked
		{
			get
			{
				return ( Boolean ) GetValue( IsCheckedDP );
			}
			set
			{
				SetValue( IsCheckedDP, value );
			}
		} // IsChecked

		public Boolean IsAvailable
		{
			get
			{
				return ( Boolean ) GetValue( IsAvailableDP );
			}
			set
			{
				SetValue( IsAvailableDP, value );
			}
		} // IsAvailable

		public Boolean AlwaysShowActiveImageIfIsAvailable
		{
			get
			{
				return ( Boolean ) GetValue
					( AlwaysShowActiveImgIfIsAvailableDP );
			}
			set
			{
				SetValue( AlwaysShowActiveImgIfIsAvailableDP, value );
			}
		} // AlwaysShowActiveImageIfIsAvailable

		public Boolean AlwaysShowInactiveImageIfIsNotAvailable
		{
			get
			{
				return ( Boolean ) GetValue
					( AlwaysShowInactiveImgIfIsNotAvailableDP );
			}
			set
			{
				SetValue( AlwaysShowInactiveImgIfIsNotAvailableDP, value );
			}
		} // AlwaysShowInactiveImageIfIsNotAvailable

		public UIElement AssociatedUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AssociatedUIElemDP );
			}
			set
			{
				SetValue( AssociatedUIElemDP, value );
			}
		} // IsChAssociatedUIElementecked

		public Int32 GroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( GroupIndexDP );
			}
			set
			{
				SetValue( GroupIndexDP, value );
			}
		} // GroupIndex

		#endregion Properties
	} // SimpleToggle
} // Controls