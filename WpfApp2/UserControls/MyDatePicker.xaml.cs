// MyDatePicker.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2.UserControls
{
    public enum DatePickerInputType
    {
        Numeric
    }

    public partial class MyDatePicker : UserControl
    {
        public MyDatePicker()
        {
            
            InitializeComponent();
        }
        public string TextValue
        {
            get { return datePicker.Text; }
            set { datePicker.Text = value; }
        }

        public DatePickerInputType InputType { get; set; }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputType == DatePickerInputType.Numeric && datePicker.SelectedDate.HasValue)
            {
                // Set the Text property to a numeric representation if needed
                // For example, you can use a specific format or extract the numeric value.
                 datePicker.Text = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            }
            // Handle additional conditions for other input types if needed
        }
        public string DCaption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("DCaption", typeof(string), typeof(MyTextBox));

    }
}
