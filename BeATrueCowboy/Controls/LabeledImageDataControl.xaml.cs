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
	/// Interaction logic for LabeledImageDataControl.xaml
	/// </summary>
	public partial class LabeledImageDataControl : UserControl
	{
		public static DependencyProperty ImageSourceProperty;
		public static DependencyProperty LabelContentProperty;
		public static DependencyProperty ImageStyleProperty;
		public static DependencyProperty LabelStyleProperty;

		public event EventHandler ImageSourceChanged;
		public event EventHandler LabelContentChanged;
		public event EventHandler ImageStyleChanged;
		public event EventHandler LabelStyleChanged;

		public LabeledImageDataControl( )
		{
			InitializeComponent( );
		} // LabeledImageDataControl

		static LabeledImageDataControl( )
		{
			// Registration of propeties dependencies.
			ImageSourceProperty = DependencyProperty.Register(
				nameof( ImageSource ),
				typeof( ImageSource ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImageSourceChanged )
				)
			);
			LabelContentProperty = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLabelContentChanged )
				)
			);

			ImageStyleProperty = DependencyProperty.Register(
				nameof( ImageStyle ),
				typeof( Style ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImageStyleChanged )
				)
			);
			LabelStyleProperty = DependencyProperty.Register(
				nameof( LabelStyle ),
				typeof( Style ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLabelStyleChanged )
				)
			);
		} // LabeledImageDataControl

		private static void OnImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newImageSource = ( ImageSource ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.ImageSource = newImageSource;
			control.ImageSourceChanged?.Invoke( control, EventArgs.Empty );
		} // OnImageSourceChanged

		private static void OnLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.LabelContent = newLabelContent;
			control.LabelContentChanged?.Invoke( control, EventArgs.Empty );
		} // OnLabelContentChanged

		private static void OnImageStyleChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Style newImageStyle = ( Style ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.ImageStyle = newImageStyle;
			control.ImageStyleChanged?.Invoke( control, EventArgs.Empty );
		} // OnImageStyleChanged

		private static void OnLabelStyleChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Style newLabelStyle = ( Style ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.LabelStyle = newLabelStyle;
			control.LabelStyleChanged?.Invoke( control, EventArgs.Empty );
		} // OnLabelStyleChanged

		public ImageSource ImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ImageSourceProperty );
			}
			set
			{
				SetValue( ImageSourceProperty, value );
			}
		} // ImageSource

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

		public Style ImageStyle
		{
			get
			{
				return ( Style ) GetValue( ImageStyleProperty );
			}
			set
			{
				SetValue( ImageStyleProperty, value );
			}
		} // ImageStyle

		public Style LabelStyle
		{
			get
			{
				return ( Style ) GetValue( LabelStyleProperty );
			}
			set
			{
				SetValue( LabelStyleProperty, value );
			}
		} // LabelStyle

	} // LabeledImageDataControl
} // BeATrueCowboy.Controls
