using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2.UserControls
{
    public enum InputType
    {
        Text,
        Numeric
    }
    public partial class MyTextBox : UserControl
    {
        public MyTextBox()
        {
            InitializeComponent();
            InputType = InputType.Text;
        }
        public InputType InputType { get; set; }

        private void InputHandler(object sender, TextCompositionEventArgs e)
        {
            if (InputType == InputType.Numeric)
            {
                // Allow only numeric input
                if (!char.IsDigit(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            }
            // Add additional conditions for other input types if needed
        }

        public string TextValue
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }


        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(MyTextBox));


        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

 

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MyTextBox));

       
    }
}