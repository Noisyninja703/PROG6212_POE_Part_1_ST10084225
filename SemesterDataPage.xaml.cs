using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace TimeManagementApp
{

    public partial class SemesterDataPage : Page
    {
        public SemesterDataPage()
        {
            InitializeComponent();

            //Check if the user has entered weeks
            if (SelectedModule.ModuleDetails.studentSem.noWeeks > 0) 
            {
                txbData.Text = SelectedModule.ModuleDetails.studentSem.SemesterData();
            }
            else
            {
                txbData.Text = "";
            }
            datePickerSemStart.Loaded += delegate
            {
                //Date picker background
                var textBox1 = (TextBox)datePickerSemStart.Template.FindName("PART_TextBox", datePickerSemStart);
                textBox1.Background = datePickerSemStart.Background;
            }; 

        }

        //set rgb value of backcolour page theme to a brush
        SolidColorBrush backColour = new SolidColorBrush(Color.FromRgb(77, 91, 85));

        bool weeks = false;

        //Reset input fields
        private void resetInputs()
        {
            txbWeeks.Text = "Weeks in Semester";
            canWeeks.Background = backColour;
            datePickerSemStart.SelectedDate = null;
        }

        //UI
        private void txbWeeks_LostFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prompt
            if (txbWeeks.Text == "")
            {
                txbWeeks.Text = "Weeks in Semester";
                txbWeeks.FontStyle = FontStyles.Italic;
            }
        }

        //text changed events will run when text is entered, then validates text
        private void txbWeeks_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded) 
            {
                weeks = Validation.ValidateNumWeeks(canWeeks, txbWeeks.Text, backColour);
            }
        }

        private void txbWeeks_GotFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prep for input
            if (txbWeeks.Text == "Weeks in Semester")
            {
                txbWeeks.Text = "";
                txbWeeks.FontStyle = FontStyles.Normal;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //if Validate data is entered, populate | else error messagebox
            if (weeks == true && !(datePickerSemStart.SelectedDate == null))
            {
                int weeks = Int32.Parse(Validation.AlterTxtBlock(txbWeeks.Text));
                DateTime semStart = (DateTime)datePickerSemStart.SelectedDate;
                SelectedModule.ModuleDetails.studentSem.noWeeks = weeks;
                SelectedModule.ModuleDetails.studentSem.semStartDate = semStart;
                txbData.Text = SelectedModule.ModuleDetails.studentSem.SemesterData();
                resetInputs();
            }
            else
            {
                MessageBox.Show("Incorrect format for number of weeks or no values inputted", "Input incorrect", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
