using Microsoft.Maui.Controls.Shapes;

namespace SelectableOptionControl;

public class SelectableOption : ContentView
{
 		public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SelectableOption), default(string));

        public static readonly BindableProperty CheckedBackgroundColorProperty =
            BindableProperty.Create(nameof(CheckedBackgroundColor), typeof(Color), typeof(SelectableOption), Colors.Blue);

        public static readonly BindableProperty UncheckedBackgroundColorProperty =
            BindableProperty.Create(nameof(UncheckedBackgroundColor), typeof(Color), typeof(SelectableOption), Colors.Gray);

        public static readonly BindableProperty CheckedTextColorProperty =
            BindableProperty.Create(nameof(CheckedTextColor), typeof(Color), typeof(SelectableOption), Colors.White);

        public static readonly BindableProperty UncheckedTextColorProperty =
            BindableProperty.Create(nameof(UncheckedTextColor), typeof(Color), typeof(SelectableOption), Colors.Black);

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SelectableOption), false, propertyChanged: OnIsCheckedChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color CheckedBackgroundColor
        {
            get => (Color)GetValue(CheckedBackgroundColorProperty);
            set => SetValue(CheckedBackgroundColorProperty, value);
        }

        public Color UncheckedBackgroundColor
        {
            get => (Color)GetValue(UncheckedBackgroundColorProperty);
            set => SetValue(UncheckedBackgroundColorProperty, value);
        }

        public Color CheckedTextColor
        {
            get => (Color)GetValue(CheckedTextColorProperty);
            set => SetValue(CheckedTextColorProperty, value);
        }

        public Color UncheckedTextColor
        {
            get => (Color)GetValue(UncheckedTextColorProperty);
            set => SetValue(UncheckedTextColorProperty, value);
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public SelectableOption()
        {
            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18,
            };

            var border = new Border
            {
                WidthRequest = 120,
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) },
                StrokeThickness = 0,
                Content = label
            };

            label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));

            Content = border;

            VisualStateManager.SetVisualStateGroups(this, CreateVisualStateGroup());

            // Esta es la acción inicial que pone el control en su estado correcto basado en `IsChecked`
            UpdateVisualState();
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SelectableOption)bindable;
            control.UpdateVisualState();
        }

        // Método que actualiza los estados visuales
        private void UpdateVisualState()
        {
            // Cambia de estado dependiendo de IsChecked
            VisualStateManager.GoToState(this, IsChecked ? "Checked" : "Unchecked");
        }

        private VisualStateGroupList CreateVisualStateGroup()
        {
            var visualStateGroupList = new VisualStateGroupList();

            var checkedStates = new VisualStateGroup
            {
                Name = "CheckedStates",
                States =
                {
                    new VisualState
                    {
                        Name = "Checked",
                        Setters =
                        {
                            new Setter { Property = BackgroundColorProperty, Value = CheckedBackgroundColor },
                            new Setter { TargetName = "label", Property = Label.TextColorProperty, Value = CheckedTextColor }
                        }
                    },
                    new VisualState
                    {
                        Name = "Unchecked",
                        Setters =
                        {
                            new Setter { Property = BackgroundColorProperty, Value = UncheckedBackgroundColor },
                            new Setter { TargetName = "label", Property = Label.TextColorProperty, Value = UncheckedTextColor }
                        }
                    }
                }
            };

            visualStateGroupList.Add(checkedStates);
            return visualStateGroupList;
        }
}