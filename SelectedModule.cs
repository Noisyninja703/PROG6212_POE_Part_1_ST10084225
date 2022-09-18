using System;
using System.Collections.Generic;
using TimeManagerLib;

namespace TimeManagementApp
{

    //Static object creation so data can persist through pages on load.
    class SelectedModule
    {
        public static WorkForSemester ModuleDetails = new WorkForSemester(new StudentSem(0, DateTime.Now, new StudentModuleLists(new List<StudentModule>())), new StudentSelfStudyWeeks(new List<StudentSelfStudy>()));//create static object for student details and set to default values
    }
}
