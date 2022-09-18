using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerLib
{
    public class StudentModuleLists
    {
        //class contructor
        public StudentModuleLists(List<StudentModule> semModules)
        {
            this.semModules = semModules;
        }

        //attribute for list of StudentModule
        public List<StudentModule> semModules { get; set; } 

        //method to check if module exists
        public bool ModuleExists() 
        {
            return (semModules.Count > 0);
        }

        //method to delete a module using LINQ
        public void DeleteModule(string code)
        {
            //use LINQ RemoveAll function to remove a module
            semModules.RemoveAll(x => x.code == code);
        }

        //method to add a module
        public void AddModule(StudentModule input)
        {
            semModules.Add(input);
        }

        //method that uses LINQ to return a module based on its module code
        public StudentModule ReturnModule(string code)
        {
            //Use LINQ Find function
            return semModules.Find(x => x.code == code);
        }

        //method that uses LINQ to display all modules
        public string DisplayModules()
        {
            //string for output initialized with headers based on campus code length, eg: PROGxxxx, CLDVxxxx, etc where x is a number
            string output = "Code\t\tName\t\tCredits\tClass Hrs/Week";

            //Use LINQ ForEach function
            semModules.ForEach(x => output += "\n" + x.DisplayModule());
            return output;
        }

    }
}
