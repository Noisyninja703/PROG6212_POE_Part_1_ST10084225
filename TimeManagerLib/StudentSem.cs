using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerLib
{
    public class StudentSem
    {
        //class constructor
        public StudentSem(int noWeeks, DateTime semStartDate, StudentModuleLists semModules)
        {
            this.noWeeks = noWeeks;
            this.semStartDate = semStartDate;
            this.semModules = semModules;
        }

        //atributes of clas
        public int noWeeks { get; set; }
        public DateTime semStartDate { get; set; }
        public StudentModuleLists semModules { get; set; }

        //method to return semester data
        public string SemesterData()
        {
            string date = semStartDate.ToString("dd.MM.yyyy");
            return $"Semester Weeks: {noWeeks}\nSemester Start Date: {date}";
        }

    }
}
