using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EPSILab.SolarSystem.Saturn.Windows8.Command
{
    class ListViewBaseItemClickCommand
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ListViewBaseItemClickCommand), new PropertyMetadata(null, CommandPropertyChanged));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(ListViewBaseItemClickCommand), new PropertyMetadata(null));

        public static void SetCommand(DependencyObject attached, ICommand value)
        {
            attached.SetValue(CommandProperty, value);
        }

        public static void SetCommandParameter(DependencyObject attached, object value)
        {
            attached.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject attached)
        {
            return (ICommand)attached.GetValue(CommandProperty);
        }

        public static object GetCommandParameter(DependencyObject attached)
        {
            return attached.GetValue(CommandProperty);
        }

        private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Attach click handler
            ((ListViewBase)d).ItemClick += ListViewBase_ItemClick;
        }

        private static void ListViewBase_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Get ListViewBase
            ListViewBase listViewBase = (sender as ListViewBase);

            // Get command
            ICommand command = GetCommand(listViewBase);

            // Execute command
            command.Execute(e.ClickedItem);
        }
    }
}