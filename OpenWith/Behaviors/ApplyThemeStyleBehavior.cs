using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace OpenWith.Behaviors
{
    public class ApplyThemeStyleBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty StyleProperty =
                DependencyProperty.Register("Style", typeof(Style), typeof(ApplyThemeStyleBehavior),
                                            new PropertyMetadata(null, new PropertyChangedCallback(OnStyleChanged)));

        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Style style = e.NewValue as Style;
            if (style == null) {
                return;
            }
            FrameworkElement element = d as FrameworkElement;
        }
    }
}
