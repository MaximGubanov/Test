using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace GasNetwork.Behaviors
{
    public class ButtonBehavior : AvaloniaObject
    {
        public static readonly AttachedProperty<string> MyNameProperty =
            AvaloniaProperty.RegisterAttached<ButtonBehavior, Control, string>
                ("MyName", string.Empty, false, BindingMode.OneTime, null, FocusType);

        public static void SetMyName(Control element, string value)
        {
            element.SetValue(MyNameProperty, value);
        }

        public static string GetMyName(Control element)
        {
            return element.GetValue(MyNameProperty);
        }

        private static string FocusType(IAvaloniaObject element, string name)
        {
            return name;
        }
    }
}
