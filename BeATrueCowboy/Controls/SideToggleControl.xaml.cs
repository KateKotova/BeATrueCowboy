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
	/// Interaction logic for SideToggleControl.xaml
	/// </summary>
	public partial class SideToggleControl : UserControl
	{
		public static DependencyProperty ActiveImageSourceProperty;
		public static DependencyProperty InactiveImageSourceProperty;
		public static DependencyProperty LabelContentProperty;
		public static DependencyProperty LabelIsTightProperty;
		public static DependencyProperty IsCheckedProperty;
		public static DependencyProperty IsAvailableProperty;
		public static DependencyProperty AssociatedUIElementProperty;
		public static DependencyProperty GroupIndexProperty;
		public static DependencyProperty AlwaysShowActiveImageIfIsAvailableProperty;
		public static DependencyProperty AlwaysShowInactiveImageIfIsNotAvailableProperty;

		public const String TIGHT_LABEL_STYLE_NAME = "TightLabelStyle";
		public const String NORMAL_LABEL_STYLE_NAME = "NormalLabelStyle";
		public const String IMAGE_STYLE_NAME = "ImageStyle";

		public static readonly RoutedEvent CheckedEvent;

		static SideToggleControl( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( SideToggleControl )
			);
			// Registration of propeties dependencies.
			ActiveImageSourceProperty = DependencyProperty.Register(
				nameof( ActiveImageSource ),
				typeof( ImageSource ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnActiveImageSourceChanged )
				)
			);
			InactiveImageSourceProperty = DependencyProperty.Register(
				nameof( InactiveImageSource ),
				typeof( ImageSource ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnInactiveImageSourceChanged )
				)
			);
			LabelContentProperty = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLabelContentChanged )
				)
			);
			LabelIsTightProperty = DependencyProperty.Register(
				nameof( LabelIsTight ),
				typeof( Boolean ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnLabelIsTightChanged )
				)
			);

			IsCheckedProperty = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			IsAvailableProperty = DependencyProperty.Register(
				nameof( IsAvailable ),
				typeof( Boolean ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsAvailableChanged )
				)
			);
			AssociatedUIElementProperty = DependencyProperty.Register(
				nameof( AssociatedUIElement ),
				typeof( UIElement ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAssociatedUIElementChanged )
				)
			);
			GroupIndexProperty = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					SimpleToggleControl.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
			AlwaysShowActiveImageIfIsAvailableProperty
					= DependencyProperty.Register(
				nameof( AlwaysShowActiveImageIfIsAvailable ),
				typeof( Boolean ),
				typeof( SideToggleControl ),
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
				typeof( SideToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback
						( OnAlwaysShowInactiveImageIfIsNotAvailableChanged )
				)
			);
		} // SideToggleControl

		public SideToggleControl( )
		{
			InitializeComponent( );
		} // SideToggleControl

		private static void OnActiveImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newActiveImageSource = ( ImageSource ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.ActiveImageSource = newActiveImageSource;
			control.ToggleObject.ActiveImageSource = newActiveImageSource;
			control.ActiveToggle.ImageSource = newActiveImageSource;
		} // OnActiveImageSourceChanged

		private static void OnInactiveImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newInactiveImageSource = ( ImageSource ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.InactiveImageSource = newInactiveImageSource;
			control.ToggleObject.InactiveImageSource = newInactiveImageSource;
			control.InactiveToggle.ImageSource = newInactiveImageSource;
			//gridInInactiveToggleTemplate.RowDefinitions[ 0 ].Height;
		} // OnInactiveImageSourceChanged

		private static void OnLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.LabelContent = newLabelContent;
			control.ActiveToggle.LabelContent = newLabelContent;
			control.InactiveToggle.LabelContent = newLabelContent;
		} // OnLabelContentChanged

		private static void OnLabelIsTightChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newLabelIsTight = ( Boolean ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.LabelIsTight = newLabelIsTight;

			String labelStyleName = control.LabelIsTight
				? TIGHT_LABEL_STYLE_NAME
				: NORMAL_LABEL_STYLE_NAME;
			Style labelStyle = ( Style ) control.Resources[ labelStyleName ];

			control.ActiveToggle.LabelStyle = labelStyle;
			control.InactiveToggle.LabelStyle = labelStyle;
		} // OnLabelIsTightChanged

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.IsChecked = newIsChecked;
			control.ToggleObject.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		private static void OnIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsAvailable = ( Boolean ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.IsAvailable = newIsAvailable;
			control.ToggleObject.IsAvailable = newIsAvailable;
		} // OnIsAvailableChanged
		private static void OnAssociatedUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.ToggleObject.AssociatedUIElement = newAssociatedUIElement;
		} // OnAssociatedUIElementChanged

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.GroupIndex = newGroupIndex;
			control.ToggleObject.GroupIndex = newGroupIndex;
		} // OnGroupIndexChanged

		private static void OnAlwaysShowActiveImageIfIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowActiveImageIfIsAvailable
				= ( Boolean ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.AlwaysShowActiveImageIfIsAvailable
				= newAlwaysShowActiveImageIfIsAvailable;
			control.ToggleObject.AlwaysShowActiveImageIfIsAvailable
				= newAlwaysShowActiveImageIfIsAvailable;
		} // OnAlwaysShowActiveImageIfIsAvailableChanged

		private static void OnAlwaysShowInactiveImageIfIsNotAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newAlwaysShowInactiveImageIfIsNotAvailable
				= ( Boolean ) parArgs.NewValue;
			SideToggleControl control = ( SideToggleControl ) parSender;
			control.AlwaysShowInactiveImageIfIsNotAvailable
				= newAlwaysShowInactiveImageIfIsNotAvailable;
			control.ToggleObject.AlwaysShowInactiveImageIfIsNotAvailable
				= newAlwaysShowInactiveImageIfIsNotAvailable;
		} // OnAlwaysShowInactiveImageIfIsNotAvailableChanged

		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( SideToggleControl.CheckedEvent ) );
		} // RaiseCheckedEvent

		private void OnToggleObjectChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseCheckedEvent( );
		} // OnToggleObjectChecked

		public ImageSource ActiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ActiveImageSourceProperty );
			}
			set
			{
				SetValue( ActiveImageSourceProperty, value );
			}
		} // ActiveImageSource

		public ImageSource InactiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( InactiveImageSourceProperty );
			}
			set
			{
				SetValue( InactiveImageSourceProperty, value );
			}
		} // InactiveImageSource

		public Object LabelContent
		{
			get
			{
				return ( Object ) GetValue( LabelContentProperty );
			}
			set
			{
				SetValue( LabelContentProperty, value );
			}
		} // LabelContent

		public Boolean LabelIsTight
		{
			get
			{
				return ( Boolean ) GetValue( LabelIsTightProperty );
			}
			set
			{
				SetValue( LabelIsTightProperty, value );
			}
		} // LabelIsTight

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
	} // SideToggleControl
} // BeATrueCowboy.Controls