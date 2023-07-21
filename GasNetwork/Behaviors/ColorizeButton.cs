using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Xaml.Interactivity;
using System.Linq;

namespace GasNetwork.Behaviors
{
    public class ColorizeButton : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.Click -= OnClick;
        }

        private void OnClick(object? sender, RoutedEventArgs e)
        {
            var ancestors = ((Button)sender!).GetLogicalAncestors();
            var itemsControl = (ItemsControl?)ancestors.FirstOrDefault(x => x.GetType().Name == "ItemsControl");

            if (itemsControl != null)
            {
                foreach (var descendant in itemsControl.GetLogicalChildren())
                {
                    if (descendant is Button)
                    {
                        ((Button)descendant).Background = new SolidColorBrush(Colors.CornflowerBlue);
                    }
                    else
                    {
                        ((Button)descendant.LogicalChildren[0]).Background = new SolidColorBrush(Colors.CornflowerBlue);
                    }
                }

                ((Button)sender!).Background = new SolidColorBrush(Colors.LimeGreen);
            }
        }
    }
}