using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //default page
            MainFrame.Content = new SemesterDataPage();
        }

        //check if weeks have been entered
        public bool checkCurrentDetailsNull()
        {
            bool output = true;
            if (SelectedModule.ModuleDetails.studentSem.noWeeks > 0)
            {
                output = false;
            }
            return output;
        }

        //exit
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SemesterButton_Click(object sender, RoutedEventArgs e)
        {
            //Change to semester page
            MainFrame.Content = new SemesterDataPage();
        }

        private void ModuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkCurrentDetailsNull() == false)
            {
                //Change to module page
                MainFrame.Content = new ModuleDataPage();
            }
            else
            {
                MessageBox.Show("Please enter your Semester Information before adding the module information for the semester", "Semester info not entered", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void SelfStudyButton_Click(object sender, RoutedEventArgs e)
        {

            if (checkCurrentDetailsNull() == false)
            {
                //Change to self study page
                MainFrame.Content = new SelfStudyDataPage();
            }
            else
            {
                MessageBox.Show("Please enter your Semester Information before adding the work information for the semester", "Semester info not entered", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

    }
}

