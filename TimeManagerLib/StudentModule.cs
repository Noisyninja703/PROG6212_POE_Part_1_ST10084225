using System;

namespace TimeManagerLib
{
    public class StudentModule
    {
        //StudentModule object constructor
        public StudentModule(string moduleCode, string moduleName, int moduleCredits, int moduleClassHours) 
        {
            code = moduleCode;
            name = moduleName;
            credits = moduleCredits;
            classHours = moduleClassHours;
        }

        //attributes of class
        public string code { get; set; }
        public string name { get; set; }
        public int credits { get; set; }
        public int classHours { get; set; }

        //method to return self study hours required per week for a module, formula based on given formula in POE Part 1
        public double SelfStudyTotal(int semWeeks) 
        {
            double output = ((credits * 10) / semWeeks) - classHours;

            return output;
        }

        //method to display a module's data
        public string DisplayModule()
        {
            return $"{code}\t{name}\t{credits}\t{classHours}";
        }
    }
}
