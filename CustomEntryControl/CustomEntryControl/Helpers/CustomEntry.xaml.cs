using FFImageLoading.Svg.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomEntryControl.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : ContentView
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public string Text { get; set; }
        public string IconSvg
        {
            get { return base.GetValue(IconProperty).ToString(); }
            set { base.SetValue(IconProperty, value); }
        }
        private static string iconPath { get; set; }
        public Color BorderColor { get; set; }
        public Thickness ContentPadding { get; set; }
        public Color BackgroundColor { get; set; }
        public string CornerRadius { get; set; }
        public Color FocusedBorderColor { get; set; }
        public Keyboard Keyboard { get; set; }
        public Thickness BorderWidth { get; set; }
        public bool Shadow { get; set; }
        public EventHandler Completed {
            get { return (EventHandler)base.GetValue(CompletedProperty); }
            set { base.SetValue(CompletedProperty, value); }
        }
        public ReturnType ReturnType { get; set; }
        public Color InvalidColor { get; set; }
        private static Color invalidColor;
        private static Color focusedValidColor;
        public bool IsValid { get; set; } = true;
        private static bool isValid;

        public static readonly BindableProperty InvalidColorProperty = BindableProperty.Create(
                                                         propertyName: "InvalidColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: Color.Red,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: InvalidColorPropertyChanged);
        private static void InvalidColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            invalidColor = (Color)newValue;
        }
        //TODO Fill svg icon with focused/unfocused colors

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
                                                         propertyName: "IsValid",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: IsValidPropertyChanged);
        private static void IsValidPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            isValid = (bool)newValue;
            control.BorderFrame.BackgroundColor = isValid ? focusedValidColor : invalidColor;
        }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
                                                         propertyName: "ReturnType",
                                                         returnType: typeof(ReturnType),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ReturnTypePropertyChanged);
        private static void ReturnTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.ReturnType = (ReturnType)newValue;
        }

        public static readonly BindableProperty CompletedProperty = BindableProperty.Create(
                                                         propertyName: "Completed",
                                                         returnType: typeof(EventHandler),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: CompletedPropertyChanged);
        private static void CompletedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.Completed += (EventHandler)newValue;
        }

        public static readonly BindableProperty ShadowProperty = BindableProperty.Create(
                                                         propertyName: "Shadow",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ShadowPropertyChanged);
        private static void ShadowPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.BorderFrame.HasShadow = (bool)newValue;
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
                                                         propertyName: "BorderWidth",
                                                         returnType: typeof(Thickness),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BorderWidthPropertyChanged);
        private static void BorderWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.BorderFrame.Padding = (Thickness)newValue;
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
                                                         propertyName: "Keyboard",
                                                         returnType: typeof(Keyboard),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: Keyboard.Default,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: KeyboardPropertyChanged);
        private static void KeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.Keyboard = (Keyboard)newValue;
        }

        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(
                                                         propertyName: "FocusedBorderColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: FocusedBorderColorPropertyChanged);
        private static void FocusedBorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.FocusedBorderColor = (Color)newValue;
            focusedValidColor = (Color)newValue;
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
                                                         propertyName: "CornerRadius",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: "30",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: CornerRadiusPropertyChanged);

        private static void CornerRadiusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.FrameLayout.CornerRadius = float.Parse(newValue.ToString());
            control.BorderFrame.CornerRadius = float.Parse(newValue.ToString());
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                         propertyName: "Text",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TextPropertyChanged);

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.Entry.Text = (string)newValue;
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
                                                         propertyName: "IconSvg",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: IconPropertyChanged);

        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            iconPath = newValue.ToString();
            control.Icon.Source = SvgImageSource.FromResource(newValue.ToString());
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
                                                         propertyName: "BorderColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: Color.Transparent,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BorderColorPropertyChanged);
        
        private static void BorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.BorderFrame.BackgroundColor = (Color)newValue;
            control.BorderColor = (Color)newValue;
        }

        public static readonly BindableProperty ContentPaddingProperty = BindableProperty.Create(
                                                         propertyName: "ContentPadding",
                                                         returnType: typeof(Thickness),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: new Thickness(10, 0),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ContentPaddingPropertyChanged);

        private static void ContentPaddingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.FrameLayout.Padding = (Thickness)newValue;
        }

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
                                                         propertyName: "BackgroundColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: Color.White,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BackgroundColorPropertyChanged);

        private static void BackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomEntry)bindable;
            control.FrameLayout.BackgroundColor = (Color)newValue;
        }
        public CustomEntry()
        {
            invalidColor = Color.Red;
            isValid = true;
            InitializeComponent();
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            BorderFrame.BackgroundColor = isValid ? FocusedBorderColor : invalidColor;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            BorderFrame.BackgroundColor = isValid ? BorderColor : invalidColor;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Entry.Focus();
        }

        public Dictionary<string, string> ReplaceSvgColor(Color color)
        {
            dict.Clear();
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            var rgbFill = $"rgb({red},{green},{blue}) ";
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CustomEntry)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(iconPath);
            string text = "";
            var rgbString = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
                string[] strings = text.Split('\"');
                foreach (var s in strings)
                {
                    if (s[0] == 'r' && s[1] == 'g' && s[2] == 'b')
                    {
                        rgbString = s;
                    }
                }
                if (string.IsNullOrEmpty(rgbString))
                {
                    rgbFill = "path " + rgbFill;
                    rgbString = "path";
                }
            }
            dict.Add(rgbString, rgbFill);
            return dict;
        }

    }
}