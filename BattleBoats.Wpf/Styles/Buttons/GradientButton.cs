using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;


namespace BattleBoats.Wpf.Styles.Buttons
{
    public class GradientButton : Button
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public Color GradientColor0
        {
            get { return (Color)GetValue(GradientColor0Property); }
            set { SetValue(GradientColor0Property, value); }
        }
        public Color GradientColor1
        {
            get { return (Color)GetValue(GradientColor1Property); }
            set { SetValue(GradientColor1Property, value); }
        }
        public Color GradientColor2
        {
            get { return (Color)GetValue(GradientColor2Property); }
            set { SetValue(GradientColor2Property, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(GradientButton));
        public static readonly DependencyProperty GradientColor0Property = DependencyProperty.Register(
            nameof(GradientColor0),
            typeof(Color),
            typeof(GradientButton));
        public static readonly DependencyProperty GradientColor1Property = DependencyProperty.Register(
            nameof(GradientColor1),
            typeof(Color),
            typeof(GradientButton));
        public static readonly DependencyProperty GradientColor2Property = DependencyProperty.Register(
            nameof(GradientColor2),
            typeof(Color),
            typeof(GradientButton));

        public GradientButton() : base()
        {
            CornerRadius = new CornerRadius(50);
            this.DefaultStyleKey = typeof(GradientButton);
        }
    }
}
