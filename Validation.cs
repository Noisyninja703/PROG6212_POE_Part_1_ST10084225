using System;
using System.Windows.Controls;
using System.Windows.Media;
namespace TimeManagementApp
{
    class Validation
    {

        //method to alter textblock so it can be parsed
        public static string AlterTxtBlock(string input)
        {
            string output = input.Replace(".", ",");
            output.Replace(" ", "");
            return output;
        }



        public static bool ValidateNumWeeks(Canvas output, string input, SolidColorBrush valid)
        {
            bool value = false;

            //Set error prompt colour
            SolidColorBrush error = new SolidColorBrush(Color.FromRgb(255, 173, 81));
            SolidColorBrush validEntry = new SolidColorBrush(Color.FromRgb(246, 250, 248));

            int temp;

            //removes spaces, allowes for parsing to double.
            input.Replace(" ", "");

            //replaces . with , | allowes for parsing to double
            string edited = input.Replace(".", ",");

            if (edited != "")
            {
                //confirm if value is a number
                if (Int32.TryParse(edited, out temp)) 
                {
                    //confirm value is non negative
                    if (temp > 0) 
                    {
                        //confirms that number of weeks is not greater than 1 year worth of weeks 
                        if (temp <= 52) 
                        {
                            //textbox tool tip to prompt invalid input
                            output.Background = validEntry;
                            ToolTipService.SetToolTip(output, "All correct!");                                                                            
                            value = true;
                        }
                        else
                        {
                            //textbox tool tip to prompt invalid input
                            output.Background = error;
                            ToolTipService.SetToolTip(output, "Value must be less than or equal to 52.");
                        }
                    }
                    else
                    {
                        //textbox tool tip to prompt invalid input
                        output.Background = error;
                        ToolTipService.SetToolTip(output, "Value must be greater than 0.");
                    }
                }
                else
                {
                    //textbox tool tip to prompt invalid input
                    output.Background = error;
                    ToolTipService.SetToolTip(output, "Value is not a number.");
                }
            }
            else
            {
                //textbox tool tip to prompt invalid input
                output.Background = error;
                ToolTipService.SetToolTip(output, "No value entered. Please enter a value.");
            }

            return value;
        }


    }
}
