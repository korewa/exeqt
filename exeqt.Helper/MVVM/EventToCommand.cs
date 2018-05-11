using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace exeqt.Helper.MVVM
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        #region Properties

        private CultureInfo _culture;

        public CultureInfo Culture => _culture;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command),
            typeof(ICommand),
            typeof(EventToCommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public IValueConverter Converter { get; set; }

        public static readonly DependencyProperty ConverterParameterProperty = DependencyProperty.Register(nameof(ConverterParameter),
            typeof(object),
            typeof(EventToCommand));

        public object ConverterParameter
        {
            get => GetValue(ConverterParameterProperty);
            set => SetValue(ConverterParameterProperty, value);
        }

        #endregion Properties

        #region Constructors

        public EventToCommand() : this(CultureInfo.CurrentCulture)
        {
        }

        public EventToCommand(CultureInfo culture) => _culture = culture;

        #endregion Constructors

        #region TriggerAction<T> Methods

        protected override void Invoke(object parameter)
        {
            if (Command == null)
                return;

            var param = parameter;

            if (Converter != null)
                param = Converter.Convert(parameter, typeof(object), ConverterParameter, Culture);

            if (Command.CanExecute(param))
                Command.Execute(param);
        }

        #endregion TriggerAction<T> Methods
    }
}