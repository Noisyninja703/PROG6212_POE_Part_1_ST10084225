using System;
using System.Globalization;


namespace TimeManagerLib
{
    //class to store semester, module and self study data
    public class WorkForSemester 
    {
        //attributes of class
        public StudentSem studentSem { get; set; }
        public StudentSelfStudyWeeks studentSelfStudyWeeks { get; set; }

        //class constructor
        public WorkForSemester(StudentSem studentSem, StudentSelfStudyWeeks studentSelfStudyDays)
        {
            this.studentSem = studentSem;
            this.studentSelfStudyWeeks = studentSelfStudyDays;
        }

        //method to dynamically calculate total hours of study more and required weekly self study hour
        //if the user has a surplus or deficit of hours studied for a week, it will account for that and adjust the values as needed
        public string ReturnSelfStudyCalc(string moduleCode) 
        {
            //initialize output string
            string output = "";

            //return the module that self study is being calced for
            StudentModule temp = studentSem.semModules.ReturnModule(moduleCode);

            //calc the self study hours done in prev and current weeks
            Tuple<double, double> selfStudyCalc = studentSelfStudyWeeks.ReturnSelfStudyData(moduleCode); 

            //calc the self study hours done this week
            double hoursThisWeek = (temp.SelfStudyTotal(studentSem.noWeeks) * studentSem.noWeeks - selfStudyCalc.Item1) / SemesterWeeksLeft();

            //calc the remaining hours for the week
            double hoursleftThisWeek = hoursThisWeek - selfStudyCalc.Item2;

            //concatinate the values in this order to the output string            
            output += "Total Hours Needed:\t" + Math.Round(hoursThisWeek);
            output += "\nCurrent Hours Remaining:\t" + Math.Round(hoursleftThisWeek);
            output += "\nCurrent Hours Studied:\t" + selfStudyCalc.Item2;
            output += "\nPrev Weeks Hours:\t" + (selfStudyCalc.Item1);


            return output;
        }


        //method to calc the weeks left in the semester
        public int SemesterWeeksLeft()
        {
            //load the Calendar instance associated with South African local CultureInfo.
            CultureInfo myCultureInfo = new CultureInfo("en-ZA");
            Calendar selfStudyCal = myCultureInfo.Calendar;

            //Load the DTFI properties required by GetWeekOfYear method.
            CalendarWeekRule CWR = myCultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek FirstDOW = myCultureInfo.DateTimeFormat.FirstDayOfWeek;

            //return the current week relevant to the year
            int thisWeek = selfStudyCal.GetWeekOfYear(DateTime.Now, CWR, FirstDOW);

            //return the semester start date week
            int semStartWeek = selfStudyCal.GetWeekOfYear(studentSem.semStartDate, CWR, FirstDOW);
            int semWeeksLeft = 0;

            //if the current week > semester start week, find the difference | else number of weeks remains the same
            if (thisWeek > semStartWeek)
            {
                semWeeksLeft = studentSem.noWeeks - (thisWeek - semStartWeek);
            }
            else
            {
                semWeeksLeft = studentSem.noWeeks;
            }

            return semWeeksLeft;
        }

    }
}
