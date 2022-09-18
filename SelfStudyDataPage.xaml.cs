using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeManagerLib;


namespace TimeManagementApp
{
    public partial class SelfStudyDataPage : Page
    {
        public SelfStudyDataPage()
        {
            InitializeComponent();

            //textbox default values
            txbData.Text = "";
            txbSelfStudyHours.Text = "";

            datePickerSelfStudyDay.DisplayDateStart = SelectedModule.ModuleDetails.studentSem.semStartDate;

            //load data into combo box
            populateCMB();

            datePickerSelfStudyDay.Loaded += delegate

            {
                //Date picker background
                var textBox1 = (TextBox)datePickerSelfStudyDay.Template.FindName("PART_TextBox", datePickerSelfStudyDay);
                textBox1.Background = datePickerSelfStudyDay.Background;
            };
        }

        //set rgb value of backcolour page theme to a brush
        SolidColorBrush backColour = new SolidColorBrush(Color.FromRgb(77, 91, 85));

        bool hours = false;

        //UI
        private void txbHours_GotFocus(object sender, RoutedEventArgs e)
        {
            //if blank, prep for input
            if (txbHours.Text == "Hours Worked")
            {
                txbHours.Text = "";
                txbHours.FontStyle = FontStyles.Normal;
            }
        }

        //method to load data into combo box
        public void populateCMB()
        {
            cmbModules.Items.Clear();
            List<StudentModule> temp = SelectedModule.ModuleDetails.studentSem.semModules.semModules;
            temp.ForEach(x => cmbModules.Items.Add(x.code));

        }

        //reset input fields
        private void resetInputs()
        {
            txbHours.Text = "Hours Worked";
            canHours.Background = backColour;
            datePickerSelfStudyDay.SelectedDate = null;

        }

        private void txbHours_LostFocus(object sender, RoutedEventArgs e)
        {
            //if blank, prompt
            if (txbHours.Text == "")
            {
                txbHours.Text = "Hours Worked";
                txbHours.FontStyle = FontStyles.Italic;
            }
        }

        //text changed events will run when text is entered, then validates text
        private void txbHours_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded) 
            {
                hours = Validation.ValidateNumWeeks(canHours, txbHours.Text, backColour);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //if input has neccesary fields entered, populate | else error messagebox
            if (cmbModules.SelectedItem != null)
            {
                if (hours == true && !(datePickerSelfStudyDay.SelectedDate == null))
                {
                    int hours = Int32.Parse(txbHours.Text);
                    DateTime newSelfStudyDay = (DateTime)datePickerSelfStudyDay.SelectedDate;
                    string moduleCode = cmbModules.SelectedItem + "";
                    StudentSelfStudy newWorkDay = new StudentSelfStudy(newSelfStudyDay, hours, SelectedModule.ModuleDetails.studentSem.semModules.ReturnModule(moduleCode));
                    SelectedModule.ModuleDetails.studentSelfStudyWeeks.AddSelfStudyDay(newWorkDay);
                    txbData.Text = SelectedModule.ModuleDetails.studentSelfStudyWeeks.DisplaySelfStudyData(moduleCode);
                    txbSelfStudyHours.Text = SelectedModule.ModuleDetails.ReturnSelfStudyCalc(moduleCode);

                    resetInputs();

                }
                else
                {
                    MessageBox.Show("Incorrect format for number of weeks or no values inputted", "Input incorrect", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not selected a module. Please select a Module to add work for", "No Module Selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Dev Testing info to help lecturer understand how to test my program
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("My program was designed to function in a real world scernario. \n\n So when inputting study hours, make sure that your system date is set to a day in the week corresponding to the week of the date selected from the date picker. \n\n Otherwise the data will be counted as invalid and it won't be factored into calculations until your system date reaches a date that is in the same week or a week after your selected date", "How to Enter Test Data", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("All calculations are based on the difference in weeks between the system start date and the current week as well as the amount of weeks in the semester. \n\n So the remaining hours to study for the semester are dynamically calculated using how many hours are left over after a week is complete. \n\n This in turn means that it'll automatically set the total required weekly study time, based on how many weeks are left and how many hours of study have being completed so far. \n\n It also rounds off the hours required for the week, so the user has an estimate of how much more they'd need to study to catch up if they hadn't completed the previous weeks required hours", "How Are Calculations Done?", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("My system for calculations allowes for scernarios in which the user hasn't studied enough for the week and scernarios where the user has studied for more than the hours required for the week. \n\n It does this by dynacmically changing the study hours required per week based on all the previously stated data.\n\n This also means that my system accounts for scernarios where the user forgot to enter data for a previous day studied. \n\n It does this by allowing the user to enter data for previous dates and then factors those into the current calculations.", "What does this mean for the user?", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //repopulate data when module selection changed
        private void cmbModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string moduleCode = cmbModules.SelectedItem + "";
            txbSelfStudyHours.Text = SelectedModule.ModuleDetails.ReturnSelfStudyCalc(moduleCode);
            txbData.Text = SelectedModule.ModuleDetails.studentSelfStudyWeeks.DisplaySelfStudyData(moduleCode);
        }
    }
}
