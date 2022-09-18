using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerLib
{
    public class StudentSelfStudy
    {
        //class constructor
        public StudentSelfStudy(DateTime selfStudyDate, double selfStudyHours, StudentModule selfStudyModule) 
        {
            this.selfStudyDate = selfStudyDate;
            this.selfStudyHours = selfStudyHours;
            this.selfStudyModule = selfStudyModule;
        }

        //attributes of class
        public DateTime selfStudyDate { get; set; }
        public double selfStudyHours { get; set; }
        public StudentModule selfStudyModule { get; set; }

    }
}
