using System;
using System.Collections.Generic;
using System.Globalization;

namespace TimeManagerLib
{
    public class StudentSelfStudyWeeks
    {

        //class constructor
        public StudentSelfStudyWeeks(List<StudentSelfStudy> studentSelfStudyDays)
        {
            this.studentSelfStudyDays = studentSelfStudyDays;
        }

        //attribute for list studentSelfStudyDays
        public List<StudentSelfStudy> studentSelfStudyDays { get; set; }

        //method to calc prev week and current week self study hours
        public Tuple<double, double> ReturnSelfStudyData(string moduleCode) 
        {

            //Initialize tuple for output, first value = previous weeks values, second value = current week value
            Tuple<double, double> output = new Tuple<double, double>(0, 0); 

            //load the Calendar instance associated with South African local CultureInfo.
            CultureInfo myCultureInfo = new CultureInfo("en-ZA");
            Calendar studyCal = myCultureInfo.Calendar;

            //Load the DTFI properties required by GetWeekOfYear method.
            CalendarWeekRule CWR = myCultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek FirstDOW = myCultureInfo.DateTimeFormat.FirstDayOfWeek;

            int thisWeek = studyCal.GetWeekOfYear(DateTime.Now, CWR, FirstDOW);//gets the current week relative to the year

            //Initialize variables for previous and current week self study hours
            double hoursThisWeek = 0;
            double hoursWorked = 0;

            //use LINQ to get all self study days for specified module
            List<StudentSelfStudy> temp = studentSelfStudyDays.FindAll(x => x.selfStudyModule.code == moduleCode);

            foreach (StudentSelfStudy item in temp)
            {
                //if true, self study was done in a previous week, add hours to prev week hours
                if ((studyCal.GetWeekOfYear(item.selfStudyDate, CWR, FirstDOW) < thisWeek))
                {
                    hoursWorked += item.selfStudyHours;                  
                }

                //if true, self study was done in current week, add hours to current week
                if (studyCal.GetWeekOfYear(item.selfStudyDate, CWR, FirstDOW) == thisWeek) 
                {
                    hoursThisWeek += item.selfStudyHours;//add hours done to this weeks hours                  
                }
            }

            //set output variables
            output = new Tuple<double, double>(hoursWorked, hoursThisWeek);

            //get output
            return output;
        }

        //method to add a self study day to the list
        public void AddSelfStudyDay(StudentSelfStudy input) 
        {
            studentSelfStudyDays.Add(input);
        }


        //method that uses LINQ to display all self study days of a module
        public string DisplaySelfStudyData(string moduleCode) 
        {
            //output string initialized with header
            string output = "Date(DD/MM/YYYY)\tHours";

            //use LINQ FindALL function
            List<StudentSelfStudy> temp = studentSelfStudyDays.FindAll(x => x.selfStudyModule.code == moduleCode);

            //if self study has being done, return values | else prompt no days studied yet
            if (temp.Count > 0)
            {
                foreach (StudentSelfStudy item in temp)
                {
                    output += $"\n{item.selfStudyDate.ToString("dd / MM / yyyy")}\t\t{item.selfStudyHours}";
                }
            }
            else
            {
                output = "No Self Study Has Being Done For This Module";
            }

            return output;
        }

    }
}
