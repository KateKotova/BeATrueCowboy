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
	/// Interaction logic for ServiceSimpleToggle.xaml
	/// </summary>
	public partial class ServiceSimpleToggle : UserControl
	{
		public static DependencyProperty ActiveImgSourceDP;
		public static DependencyProperty InactiveImgSourceDP;
		public static DependencyProperty LblContentDP;
		public static DependencyProperty SecondLblContentDP;
		public static DependencyProperty LblIsTightDP;
		public static DependencyProperty IsCheckedDP;
		public static DependencyProperty IsAvailableDP;
		public static DependencyProperty AssociatedUIElemDP;
		public static DependencyProperty GroupIndexDP;
		public static DependencyProperty HasBottomBarWhileInctiveProperty;

		public const String SERVICE_TOGGLE_TIGHT_LABEL_STYLE_NAME
			= "ServiceToggleTightLabelStyle";
		public const String SERVICE_TOGGLE_LABEL_STYLE_NAME
			= "ServiceToggleLabelStyle";
		public const String SERVICE_TOGGLE_TIGHT_SECOND_LABEL_STYLE_NAME
			= "ServiceToggleTightSecondLabelStyle";
		public const String SERVICE_TOGGLE_SECOND_LABEL_STYLE_NAME
			= "ServiceToggleSecondLabelStyle";
		public const String SERVICE_TOGGLE_IMAGE_STYLE_NAME
			= "ServiceToggleImageStyle";
		public const String
			SERVICE_TOGGLE_IMAGE_WHILE_SECOND_LABEL_IS_VISIBLE_STYLE_NAME
			= "ServiceToggleImageStyleWhileSecondLabelIsVisible";
		public const String INCTIVE_SERVICE_TOGGLE_TEMPLATE_STYLE_NAME
			= "InctiveServiceToggleTemplate";
		public const String
			INCTIVE_SERVICE_TOGGLE_WITHOUT_BOTTOM_BAR_TEMPLATE_STYLE_NAME
			= "InctiveServiceToggleWithoutBottomBarTemplate";

		public static readonly RoutedEvent CheckedEvent;

		static ServiceSimpleToggle( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( ServiceSimpleToggle )
			);
			// Registration of propeties dependencies.
			ActiveImgSourceDP = DependencyProperty.Register(
				nameof( ActiveImageSource ),
				typeof( ImageSource ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnActiveImgSourceChanged )
				)
			);
			InactiveImgSourceDP = DependencyProperty.Register(
				nameof( InactiveImageSource ),
				typeof( ImageSource ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnInactiveImgSourceChanged )
				)
			);
			LblContentDP = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLblContentChanged )
				)
			);
			SecondLblContentDP = DependencyProperty.Register(
				nameof( SecondLabelContent ),
				typeof( Object ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnSecondLblContentChanged )
				)
			);
			LblIsTightDP = DependencyProperty.Register(
				nameof( LabelIsTight ),
				typeof( Boolean ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnLabelIsTightChanged )
				)
			);

			IsCheckedDP = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			IsAvailableDP = DependencyProperty.Register(
				nameof( IsAvailable ),
				typeof( Boolean ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsAvailableChanged )
				)
			);
			AssociatedUIElemDP = DependencyProperty.Register(
				nameof( AssociatedUIElement ),
				typeof( UIElement ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAssociatedUIElemChanged )
				)
			);
			GroupIndexDP = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					SimpleToggle.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
			HasBottomBarWhileInctiveProperty = DependencyProperty.Register(
				nameof( HasBottomBarWhileInctive ),
				typeof( Boolean ),
				typeof( ServiceSimpleToggle ),
				new FrameworkPropertyMetadata(
					true,
					new PropertyChangedCallback( OnHasBottomBarWhileInctiveChanged )
				)
			);
	} // ServiceSimpleToggle

		public ServiceSimpleToggle( )
		{
			InitializeComponent( );
		} // ServiceSimpleToggle

		private static void OnActiveImgSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newActiveImageSource = ( ImageSource ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.ActiveImageSource = newActiveImageSource;
			control.ActiveServiceToggle.ImageSource = newActiveImageSource;
		} // OnActiveImgSourceChanged

		private static void OnInactiveImgSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newInactiveImageSource = ( ImageSource ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.InactiveImageSource = newInactiveImageSource;
			control.InactiveServiceToggle.ImageSource = newInactiveImageSource;
			//gridInInactiveServiceToggleTemplate.RowDefinitions[ 0 ].Height;
		} // OnInactiveImgSourceChanged

		private static void OnLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.LabelContent = newLabelContent;
			control.ActiveServiceToggle.LabelContent = newLabelContent;
			control.InactiveServiceToggle.LabelContent = newLabelContent;
		} // OnLblContentChanged

		private static void OnSecondLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newSecondLabelContent = ( Object ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.SecondLabelContent = newSecondLabelContent;
			control.ActiveServiceToggle.SecondLabelContent
				= newSecondLabelContent;
			control.InactiveServiceToggle.SecondLabelContent
				= newSecondLabelContent;

			String newSecondLabelContentAsString = newSecondLabelContent as String;
			Visibility newSecondLabelVisibility
				= ( ( newSecondLabelContent == null )
					|| String.IsNullOrWhiteSpace( newSecondLabelContentAsString ) )
				? Visibility.Collapsed : Visibility.Visible;
			control.ActiveServiceToggle.SecondLabelVisability
				= newSecondLabelVisibility;
			control.InactiveServiceToggle.SecondLabelVisability
				= newSecondLabelVisibility;

			String imageStyleName
				= ( newSecondLabelVisibility == Visibility.Visible )
				? SERVICE_TOGGLE_IMAGE_WHILE_SECOND_LABEL_IS_VISIBLE_STYLE_NAME
				: SERVICE_TOGGLE_IMAGE_STYLE_NAME;
			Style imageStyle = ( Style ) control.Resources
				[ imageStyleName ];
			control.ActiveServiceToggle.ImageStyle = imageStyle;
			control.InactiveServiceToggle.ImageStyle = imageStyle;
		} // OnLblContentChanged

		private static void OnLabelIsTightChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newLabelIsTight = ( Boolean ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.LabelIsTight = newLabelIsTight;

			String labelStyleName = control.LabelIsTight
				? SERVICE_TOGGLE_TIGHT_LABEL_STYLE_NAME
				: SERVICE_TOGGLE_LABEL_STYLE_NAME;
			Style labelStyle = ( Style ) control.Resources[ labelStyleName ];

			String secondLabelStyleName = control.LabelIsTight
				? SERVICE_TOGGLE_TIGHT_SECOND_LABEL_STYLE_NAME
				: SERVICE_TOGGLE_SECOND_LABEL_STYLE_NAME;
			Style secondLabelStyle = ( Style ) control.Resources
				[ secondLabelStyleName ];

			control.ActiveServiceToggle.LabelStyle = labelStyle;
			control.InactiveServiceToggle.LabelStyle = labelStyle;
			control.ActiveServiceToggle.SecondLabelStyle = secondLabelStyle;
			control.InactiveServiceToggle.SecondLabelStyle = secondLabelStyle;
		} // OnLabelIsTightChanged

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.IsChecked = newIsChecked;
			control.ServiceToggle.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		private static void OnIsAvailableChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsAvailable = ( Boolean ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.IsAvailable = newIsAvailable;
			control.ServiceToggle.IsAvailable = newIsAvailable;
		} // OnIsAvailableChanged
		private static void OnAssociatedUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.ServiceToggle.AssociatedUIElement = newAssociatedUIElement;
		} // OnAssociatedUIElemChanged

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.GroupIndex = newGroupIndex;
			control.ServiceToggle.GroupIndex = newGroupIndex;
		} // OnGroupIndexChanged

		private static void OnHasBottomBarWhileInctiveChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newHasBottomBarWhileInctive = ( Boolean ) parArgs.NewValue;
			ServiceSimpleToggle control = ( ServiceSimpleToggle ) parSender;
			control.HasBottomBarWhileInctive = newHasBottomBarWhileInctive;

			String templateName = newHasBottomBarWhileInctive
				? INCTIVE_SERVICE_TOGGLE_TEMPLATE_STYLE_NAME
				: INCTIVE_SERVICE_TOGGLE_WITHOUT_BOTTOM_BAR_TEMPLATE_STYLE_NAME;
			control.InactiveServiceToggle.Template = ( ControlTemplate )
				control.Resources[ templateName ];
		} // OnHasBottomBarWhileInctiveChanged


		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( ServiceSimpleToggle.CheckedEvent ) );
		} // RaiseCheckedEvent

		private void OnSimpleToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseCheckedEvent( );
		} // OnSimpleToggleChecked

		public ImageSource ActiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ActiveImgSourceDP );
			}
			set
			{
				SetValue( ActiveImgSourceDP, value );
			}
		} // ActiveImageSource

		public ImageSource InactiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( InactiveImgSourceDP );
			}
			set
			{
				SetValue( InactiveImgSourceDP, value );
			}
		} // InactiveImageSource

		public Object LabelContent
		{
			get
			{
				return ( Object ) GetValue( LblContentDP );
			}
			set
			{
				SetValue( LblContentDP, value );
			}
		} // LabelContent

		public Object SecondLabelContent
		{
			get
			{
				return ( Object ) GetValue( SecondLblContentDP );
			}
			set
			{
				SetValue( SecondLblContentDP, value );
			}
		} // SecondLabelContent

		public Boolean LabelIsTight
		{
			get
			{
				return ( Boolean ) GetValue( LblIsTightDP );
			}
			set
			{
				SetValue( LblIsTightDP, value );
			}
		} // LabelIsTight

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

		public Boolean HasBottomBarWhileInctive
		{
			get
			{
				return ( Boolean ) GetValue( HasBottomBarWhileInctiveProperty );
			}
			set
			{
				SetValue( HasBottomBarWhileInctiveProperty, value );
			}
		} // HasBottomBarWhileInctive
	} // ServiceSimpleToggle
} // Controls
