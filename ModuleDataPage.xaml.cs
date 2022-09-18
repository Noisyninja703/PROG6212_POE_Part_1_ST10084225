using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeManagerLib;


namespace TimeManagementApp
{

    public partial class ModuleDataPage : Page
    {
        public ModuleDataPage()
        {
            InitializeComponent();

            //populate combo box
            PopulateCMB();

            //if modules exist, display module | else prompt
            if (SelectedModule.ModuleDetails.studentSem.semModules.ModuleExists() == true)
            {
                txbData.Text = SelectedModule.ModuleDetails.studentSem.semModules.DisplayModules();
            }
            else
            {
                txbData.Text = "No Modules added yet";
            }
        }

        //set rgb value of backcolour page theme to a brush
        SolidColorBrush backColour = new SolidColorBrush(Color.FromRgb(77, 91, 85));

        //bools for validation
        bool code = false;
        bool name = false;
        bool credits = false;
        bool classHrs = false;

        //method to populate combo box
        public void PopulateCMB()
        {
            cmbModules.Items.Clear();
            List<StudentModule> temp = SelectedModule.ModuleDetails.studentSem.semModules.semModules;
            temp.ForEach(x => cmbModules.Items.Add(x.code));

        }

        //method to validate input
        private bool ValidateInput()
        {
            bool output = true;
            List<bool> allInput = new List<bool> { code, name, credits, classHrs };
            foreach (bool item in allInput)
            {
                if (item == false)
                {
                    output = false;
                    break;
                }
            }
            return output;

        }

        //method to reset input fields
        private void ResetInputs()
        {
            txbCode.Text = "Module Code";
            canCode.Background = backColour;
            txbName.Text = "Module Name";
            canName.Background = backColour;
            txbCredits.Text = "Module Credits";
            canCredits.Background = backColour;
            txbClassHrs.Text = "Class Hours per week";
            canClassHrs.Background = backColour;
        }

        //UI
        private void txbCode_GotFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prep for input
            if (txbCode.Text == "Module Code")
            {
                txbCode.Text = "";
                txbCode.FontStyle = FontStyles.Normal;
            }
        }

        private void txbCode_LostFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prompt
            if (txbCode.Text == "")
            {
                txbCode.Text = "Module Code";
                txbCode.FontStyle = FontStyles.Italic;
            }
        }

        private void txbName_GotFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prep for input
            if (txbName.Text == "Module Name")
            {
                txbName.Text = "";
                txbName.FontStyle = FontStyles.Normal;
            }
        }

        private void txbName_LostFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prompt
            if (txbName.Text == "")
            {
                txbName.Text = "Module Name";
                txbName.FontStyle = FontStyles.Italic;
            }
        }

        private void txbCredits_GotFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prep for input
            if (txbCredits.Text == "Module Credits")
            {
                txbCredits.Text = "";
                txbCredits.FontStyle = FontStyles.Normal;
            }
        }

        private void txbCredits_LostFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prompt
            if (txbCredits.Text == "")
            {
                txbCredits.Text = "Module Credits";
                txbCredits.FontStyle = FontStyles.Italic;
            }

        }

        private void txbClassHrs_GotFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prep for input
            if (txbClassHrs.Text == "Class Hours per week")
            {
                txbClassHrs.Text = "";
                txbClassHrs.FontStyle = FontStyles.Normal;
            }

        }
        private void txbClassHrs_LostFocus(object sender, RoutedEventArgs e)
        {
            //if textbox blank, prompt
            if (txbClassHrs.Text == "")
            {
                txbClassHrs.Text = "Class Hours per week";
                txbClassHrs.FontStyle = FontStyles.Italic;
            }

        }

        private void txbClassHrs_TextChanged(object sender, TextChangedEventArgs e)
        {
            //text changed events will run when text is entered, then validates text
            if (this.IsLoaded) 
            {
                classHrs = Validation.ValidateNumWeeks(canClassHrs, txbClassHrs.Text, backColour);
            }
        }



        //Remove module button
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //if module exists, remove module | else, prompt | else, error messagebox, module not selected | else, error messagebox, no modules exist
            if (SelectedModule.ModuleDetails.studentSem.semModules.ModuleExists() == true)
            {
                if (cmbModules.SelectedIndex > -1)
                {
                    string code = cmbModules.Text;
                    SelectedModule.ModuleDetails.studentSem.semModules.DeleteModule(code);
                    PopulateCMB();
                    cmbModules.SelectedIndex = -1;
                    if (SelectedModule.ModuleDetails.studentSem.semModules.ModuleExists() == true)
                    {
                        txbData.Text = SelectedModule.ModuleDetails.studentSem.semModules.DisplayModules();

                    }
                    else
                    {
                        txbData.Text = "No Modules added yet";
                        cmbModules.Items.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please select the module code of the module you wish to delete", "No module Selected", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please create a module before attempting to remove one", "No Modules", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //populate
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //if input valid, populate, reset input fields | else, error messagebox
            if (ValidateInput() == true)
            {
                string code = txbCode.Text.Trim();
                string name = txbName.Text.Trim();
                int credit = Int32.Parse(Validation.AlterTxtBlock(txbCredits.Text));
                int weeks = Int32.Parse(Validation.AlterTxtBlock(txbClassHrs.Text));
                StudentModule newMod = new StudentModule(code, name, credit, weeks);
                SelectedModule.ModuleDetails.studentSem.semModules.AddModule(newMod);
                txbData.Text = SelectedModule.ModuleDetails.studentSem.semModules.DisplayModules();
                PopulateCMB();
                ResetInputs();
            }
            else
            {
                MessageBox.Show("Incorrect format for number of weeks or no values inputted", "Input incorrect", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txbCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            //text changed events will run when text is entered, then validates text
            if (this.IsLoaded)
            {
                if (txbCode.Text == "")
                {
                    code = false;
                }
                else
                {
                    code = true;
                }
            }
        }

        private void txbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //text changed events will run when text is entered, then validates text
            if (this.IsLoaded)
            {
                if (txbName.Text == "")
                {
                    name = false;
                }
                else
                {
                    name = true;
                }
            }
        }

        private void txbCredits_TextChanged(object sender, TextChangedEventArgs e)
        {
            //text changed events will run when text is entered, then validates text
            if (this.IsLoaded)
            {
                credits = Validation.ValidateNumWeeks(canCredits, txbCredits.Text, backColour);
            }
        }
    }
}
