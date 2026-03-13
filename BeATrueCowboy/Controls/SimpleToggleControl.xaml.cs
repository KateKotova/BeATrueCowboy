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
	/// Interaction logic for SimpleToggleControl.xaml
	/// </summary>
	public partial class SimpleToggleControl : UserControl
	{
		public static DependencyProperty ActiveStateUIElementProperty;
		public static DependencyProperty InactiveStateUIElementProperty;
		public static DependencyProperty AdditionalHitAreaUIElementProperty;
		public static DependencyProperty IsCheckedProperty;
		public static DependencyProperty IsAvailableProperty;
		public static DependencyProperty AlwaysShowActiveImageIfIsAvailableProperty;
		public static DependencyProperty AlwaysShowInactiveImageIfIsNotAvailableProperty;
		public static DependencyProperty AssociatedUIElementProperty;
		public static DependencyProperty GroupIndexProperty;

		public event EventHandler ActiveStateUIElementChanged;
		public event EventHandler InactiveStateUIElementChanged;
		public event EventHandler AdditionalHitAreaUIElementChanged;
		public event EventHandler IsCheckedChanged;
		public event EventHandler IsAvailableChanged;
		public event EventHandler AlwaysShowActiveImageIfIsAvailableChanged;
		public event EventHandler AlwaysShowInactiveImageIfIsNotAvailableChanged;
		public event EventHandler AssociatedUIElementChanged;
		public event EventHandler GroupIndexChanged;

		public ImageSource ActiveImageSource;
		public ImageSource InactiveImageSource;

		public const Int32 UNDEFINED_GROUP_INDEX = -1;

		private static Dictionary<Int32, List<SimpleToggleControl>> _Groups
			= new Dictionary<Int32, List<SimpleToggleControl>>( );

		public static readonly RoutedEvent CheckedEvent;

		static SimpleToggleControl( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( SimpleToggleControl )
			);
			// Registration of propeties dependencies.
			ActiveStateUIElementProperty = DependencyProperty.Register(
				nameof( ActiveStateUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnActiveStateUIElementChanged )
				)
			);
			InactiveStateUIElementProperty = DependencyProperty.Register(
				nameof( InactiveStateUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnInactiveStateUIElementChanged )
				)
			);
			AdditionalHitAreaUIElementProperty = DependencyProperty.Register(
				nameof( AdditionalHitAreaUIElement ),
				typeof( UIElement ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAdditionalHitAreaUIElementChanged )
				)
			);
			IsCheckedProperty = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			IsAvailableProperty = DependencyProperty.Register(
				nameof( IsAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsAvailableChanged )
				)
			);
			AlwaysShowActiveImageIfIsAvailableProperty
					= DependencyProperty.Register(
				nameof( AlwaysShowActiveImageIfIsAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback
						( OnAlwaysShowActiveImageIfIsAvailableChanged )
				)
			);
			AlwaysShowInactiveImageIfIsNotAvailableProperty
					= DependencyProperty.Register(
				nameof( AlwaysShowInactiveImageIfIsNotAvailable ),
				typeof( Boolean ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback
						( OnAlwaysShowInactiveImageIfIsNotAvailableChanged )
				)
			);
			AssociatedUIElementProperty = DependencyProperty.Register(
					nameof( AssociatedUIElement ),
					typeof( UIElement ),
					typeof( SimpleToggleControl ),
					new FrameworkPropertyMetadata(
						null,
						new PropertyChangedCallback( OnAssociatedUIElementChanged )
					)
				);
			GroupIndexProperty = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( SimpleToggleControl ),
				new FrameworkPropertyMetadata(
					UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
		} // static SimpleToggleControl

		public SimpleToggleControl( )
		{
			InitializeComponent( );
		} // SimpleToggleControl

		private void SetActiveImageSourceFromLabeledImageDataControl
			( LabeledImageDataControl parHavingActiveImageControl )
		{
			if ( parHavingActiveImageControl == null )
				return;

			/*this.ActiveImageSource = parHavingActiveImageControl.ImageSource;*/

			if ( this.IsAvailable )
			{
				if ( this.AlwaysShowActiveImageIfIsAvailable )
				{
					LabeledImageDataControl havingInactiveImageControl
						= this.InactiveStateUIElement as LabeledImageDataControl;
					if ( havingInactiveImageControl != null )
						havingInactiveImageControl.ImageSource = this.ActiveImageSource;
				}
			} // if ( this.IsAvailable )
			else
				if ( ( this.InactiveImageSource != null )
						&& this.AlwaysShowInactiveImageIfIsNotAvailable )
				parHavingActiveImageControl.ImageSource = this.InactiveImageSource;
		} // SetActiveImageSourceFromLabeledImageDataControl

		private void SetInactiveImageSourceFromLabeledImageDataControl
			( LabeledImageDataControl parHavingInactiveImageControl )
		{
			if ( parHavingInactiveImageControl == null )
				return;

			/*this.InactiveImageSource = parHavingInactiveImageControl.ImageSource;*/

			if ( !this.IsAvailable )
			{
				if ( this.AlwaysShowInactiveImageIfIsNotAvailable )
				{
					LabeledImageDataControl havingActiveImageControl
						= this.ActiveStateUIElement as LabeledImageDataControl;
					if ( havingActiveImageControl != null )
						havingActiveImageControl.ImageSource = this.InactiveImageSource;
				}
			} // if ( !this.IsAvailable )
			else
				if ( ( this.ActiveImageSource != null )
						&& this.AlwaysShowActiveImageIfIsAvailable )
					parHavingInactiveImageControl.ImageSource = this.ActiveImageSource;
		} // SetInactiveImageSourceFromLabeledImageDataControl

		private void OnActiveStateUIElementImageSourceChanged
			( Object parSender, EventArgs parArgs )
		{
			LabeledImageDataControl havingActiveImageControl
				= parSender as LabeledImageDataControl;
			if ( parSender != null )
				this.SetActiveImageSourceFromLabeledImageDataControl
					( havingActiveImageControl );
		} // OnActiveStateUIElementImageSourceChanged

		private void OnInactiveStateUIElementImageSourceChanged
			( Object parSender, EventArgs parArgs )
		{
			LabeledImageDataControl havingInactiveImageControl
				= parSender as LabeledImageDataControl;
			if ( parSender != null )
				this.SetInactiveImageSourceFromLabeledImageDataControl
					( havingInactiveImageControl );
		} // OnActiveStateUIElementImageSourceChanged


		private static void OnActiveStateUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newActiveStateUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			if ( control.ActiveStateUIElement != null )
			{
				control.ActiveStateUIElement.MouseDown -= control.OnMouseDown;
				LabeledImageDataControl oldHavingActiveImageControl
					= control.ActiveStateUIElement as LabeledImageDataControl;
				if ( oldHavingActiveImageControl != null )
					oldHavingActiveImageControl.ImageSourceChanged
						-= control.OnActiveStateUIElementImageSourceChanged;
			}
			control.ActiveStateUIElement = newActiveStateUIElement;
			control.ActiveStateUIElement.MouseDown += control.OnMouseDown;

			LabeledImageDataControl havingActiveImageControl
				= control.ActiveStateUIElement as LabeledImageDataControl;
			if ( havingActiveImageControl != null )
			{
				havingActiveImageControl.ImageSourceChanged
						+= control.OnActiveStateUIElementImageSourceChanged;
				control.SetActiveImageSourceFromLabeledImageDataControl
					( havingActiveImageControl );
				control.ActiveImageSource = havingActiveImageControl.ImageSource;
			}
			else
				control.ActiveImageSource = null;

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
			control.ActiveStateUIElementChanged?.Invoke( control, EventArgs.Empty );
		} // OnActiveStateUIElementChanged

		private static void OnInactiveStateUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newInactiveStateUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			if ( control.InactiveStateUIElement != null )
			{
				control.InactiveStateUIElement.MouseDown -= control.OnMouseDown;
				LabeledImageDataControl oldHavingInactiveImageControl
					= control.InactiveStateUIElement as LabeledImageDataControl;
				if ( oldHavingInactiveImageControl != null )
					oldHavingInactiveImageControl.ImageSourceChanged
						-= control.OnInactiveStateUIElementImageSourceChanged;
			}
			control.InactiveStateUIElement = newInactiveStateUIElement;
			control.InactiveStateUIElement.MouseDown += control.OnMouseDown;

			LabeledImageDataControl havingInactiveImageControl
				= control.InactiveStateUIElement as LabeledImageDataControl;
			if ( havingInactiveImageControl != null )
			{
				havingInactiveImageControl.ImageSourceChanged
					+= control.OnInactiveStateUIElementImageSourceChanged;
				control.SetInactiveImageSourceFromLabeledImageDataControl
					( havingInactiveImageControl );
				control.InactiveImageSource = havingInactiveImageControl.ImageSource;
			} // if ( havingInactiveImageControl != null )
			else
				control.InactiveImageSource = null;

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
			control.InactiveStateUIElementChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnInactiveStateUIElementChanged

		private static void OnAdditionalHitAreaUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAdditionalHitAreaUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			if ( control.AdditionalHitAreaUIElement != null )
				control.AdditionalHitAreaUIElement.MouseDown -= control.OnMouseDown;
			control.AdditionalHitAreaUIElement = newAdditionalHitAreaUIElement;
			control.AdditionalHitAreaUIElement.MouseDown += control.OnMouseDown;
			control.AdditionalHitAreaUIElementChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAdditionalHitAreaUIElementChanged

		private void OnMouseDown( Object parSender, MouseEventArgs parArgs )
		{
			this.IsChecked = true;
		} // OnMouseDown

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
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
				List<SimpleToggleControl> currentGroupControls
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
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			control.IsAvailable = newIsAvailable;

			ImageSource newActiveImageSource;
			ImageSource newInactiveImageSource;
			if ( control.IsAvailable )
			{
				newActiveImageSource = control.ActiveImageSource;
				newInactiveImageSource = control.AlwaysShowActiveImageIfIsAvailable
					? control.ActiveImageSource : control.InactiveImageSource;
			}
			// if ( ! control.IsAvailable )
			else
			{
				newInactiveImageSource = control.InactiveImageSource;
				newActiveImageSource = control.AlwaysShowInactiveImageIfIsNotAvailable
					? control.InactiveImageSource : control.ActiveImageSource;
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

		private static void OnAlwaysShowActiveImageIfIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowActiveImageIfIsAvailable
				= ( Boolean ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			control.AlwaysShowActiveImageIfIsAvailable
				= newAlwaysShowActiveImageIfIsAvailable;

			if ( control.IsAvailable )
			{
				ImageSource newInactiveImageSource
					= control.AlwaysShowActiveImageIfIsAvailable
					? control.ActiveImageSource : control.InactiveImageSource;
				if ( newInactiveImageSource != null )
				{
					LabeledImageDataControl havingInactiveImageControl
						= control.InactiveStateUIElement as LabeledImageDataControl;
					if ( havingInactiveImageControl != null )
						havingInactiveImageControl.ImageSource = newInactiveImageSource;
				} // if ( newInactiveImageSource != null )
			} // if ( control.IsAvailable )
			control.AlwaysShowActiveImageIfIsAvailableChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAlwaysShowActiveImageIfIsAvailableChanged

		private static void OnAlwaysShowInactiveImageIfIsNotAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowInactiveImageIfIsNotAvailable
				= ( Boolean ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			control.AlwaysShowInactiveImageIfIsNotAvailable
				= newAlwaysShowInactiveImageIfIsNotAvailable;

			if ( !control.IsAvailable )
			{
				ImageSource newActiveImageSource
					= control.AlwaysShowInactiveImageIfIsNotAvailable
					? control.InactiveImageSource : control.ActiveImageSource;
				if ( newActiveImageSource != null )
				{
					LabeledImageDataControl havingActiveImageControl
						= control.ActiveStateUIElement as LabeledImageDataControl;
					if ( havingActiveImageControl != null )
						havingActiveImageControl.ImageSource = newActiveImageSource;
				} // if ( newInactiveImageSource != null )
			} // if ( !control.IsAvailable )
			control.AlwaysShowInactiveImageIfIsNotAvailableChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnAlwaysShowInactiveImageIfIsNotAvailableChanged

		private static void OnAssociatedUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			SimpleToggleControl control = ( SimpleToggleControl ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.ResetAssociatedUIElementVisibility( );
			control.AssociatedUIElementChanged?.Invoke( control, EventArgs.Empty );
		} // OnAssociatedUIElementChanged

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
				_Groups = new Dictionary<Int32, List<SimpleToggleControl>>( );
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

			KeyValuePair<Int32, List<SimpleToggleControl>>? group =
				_Groups.FirstOrDefault( ( KeyValuePair<Int32,
				List<SimpleToggleControl>> currentGroup )
				=> currentGroup.Key == parGroupIndex );
			if ( ( group == null ) || ( group.Value.Value == null ) )
				return;
			List<SimpleToggleControl> groupControls = _Groups[ parGroupIndex ];
			if ( groupControls == null )
				return;
			groupControls.Remove( this );
			if ( this.IsChecked && groupControls.Any( ) )
			{
				groupControls[ 0 ].IsChecked = true;
				this.IsChecked = false;
			}
			/*groupControls.RemoveAt(
				groupControls.FindIndex( ( SimpleToggleControl currentControl )
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

			foreach ( KeyValuePair<Int32, List<SimpleToggleControl>>
				group in _Groups )
			{
				List<SimpleToggleControl> controls = group.Value;
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

		private List<SimpleToggleControl>
			GetCurrentGroupControlsAndAddToItIfItIsEmpty( )
		{
			List<SimpleToggleControl> currentGroupControls = null;

			KeyValuePair<Int32, List<SimpleToggleControl>>? group =
				_Groups.FirstOrDefault( ( KeyValuePair<Int32,
				List<SimpleToggleControl>> currentGroup )
				=> currentGroup.Key == this.GroupIndex );
			if ( ( group == null ) || ( group.Value.Value == null ) )
			{
				currentGroupControls = new List<SimpleToggleControl> { this };
				_Groups.Add( this.GroupIndex, currentGroupControls );
			}
			else
			{
				currentGroupControls = _Groups[ this.GroupIndex ];
				if ( currentGroupControls == null )
				{
					currentGroupControls = new List<SimpleToggleControl> { this };
					_Groups[ this.GroupIndex ] = currentGroupControls;
				}
			}
			return currentGroupControls;
		} // GetCurrentGroupControlsAndAddToItIfItIsEmpty

		private static void UncheckGroupOtherControlsIfDefinedIsChecked
			( SimpleToggleControl parDefinedControl,
			List<SimpleToggleControl> parGroupControls )
		{
			if ( ( !parDefinedControl.IsChecked )
					|| ( parGroupControls == null )
					|| ( parGroupControls.Count( ) == 0 ) )
				return;

			foreach ( SimpleToggleControl control in parGroupControls )
				if ( !control.Equals( parDefinedControl ) )
					control.IsChecked = false;
		} // UncheckGroupOtherControlsIfDefinedIsChecked

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			SimpleToggleControl control
				= ( SimpleToggleControl ) parSender;
			control.DeleteFromGroup( ( Int32 ) parArgs.OldValue );

			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			control.GroupIndex = newGroupIndex;

			if ( !control.GroupIndexIsUndefined( ) )
			{

				List<SimpleToggleControl> currentGroupControls = control
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
			this.RaiseEvent( new RoutedEventArgs( SimpleToggleControl.CheckedEvent ) );
		} // RaiseCheckedEvent

		#region Properties

		public UIElement ActiveStateUIElement
		{
			get
			{
				return ( UIElement ) GetValue( ActiveStateUIElementProperty );
			}
			set
			{
				SetValue( ActiveStateUIElementProperty, value );
			}
		} // ActiveStateUIElement

		public UIElement InactiveStateUIElement
		{
			get
			{
				return ( UIElement ) GetValue( InactiveStateUIElementProperty );
			}
			set
			{
				SetValue( InactiveStateUIElementProperty, value );
			}
		} // ActiveStateUIElement

		public UIElement AdditionalHitAreaUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AdditionalHitAreaUIElementProperty );
			}
			set
			{
				SetValue( AdditionalHitAreaUIElementProperty, value );
			}
		} // ActiveStateUIElement

		public Boolean IsChecked
		{
			get
			{
				return ( Boolean ) GetValue( IsCheckedProperty );
			}
			set
			{
				SetValue( IsCheckedProperty, value );
			}
		} // IsChecked

		public Boolean IsAvailable
		{
			get
			{
				return ( Boolean ) GetValue( IsAvailableProperty );
			}
			set
			{
				SetValue( IsAvailableProperty, value );
			}
		} // IsAvailable

		public Boolean AlwaysShowActiveImageIfIsAvailable
		{
			get
			{
				return ( Boolean ) GetValue
					( AlwaysShowActiveImageIfIsAvailableProperty );
			}
			set
			{
				SetValue( AlwaysShowActiveImageIfIsAvailableProperty, value );
			}
		} // AlwaysShowActiveImageIfIsAvailable

		public Boolean AlwaysShowInactiveImageIfIsNotAvailable
		{
			get
			{
				return ( Boolean ) GetValue
					( AlwaysShowInactiveImageIfIsNotAvailableProperty );
			}
			set
			{
				SetValue( AlwaysShowInactiveImageIfIsNotAvailableProperty, value );
			}
		} // AlwaysShowInactiveImageIfIsNotAvailable

		public UIElement AssociatedUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AssociatedUIElementProperty );
			}
			set
			{
				SetValue( AssociatedUIElementProperty, value );
			}
		} // IsChAssociatedUIElementecked

		public Int32 GroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( GroupIndexProperty );
			}
			set
			{
				SetValue( GroupIndexProperty, value );
			}
		} // GroupIndex

		#endregion Properties
	} // SimpleToggleControl
} // BeATrueCowboy.Controls