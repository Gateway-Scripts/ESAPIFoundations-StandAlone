using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace DataMiner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using (Application app = Application.CreateApplication())
                {
                    Execute(app);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
        }
        static void Execute(Application app)
        {
            // TODO: Add your code here.
            foreach (var ps in app.PatientSummaries.Take(20))
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Patient Id: {ps.Id}");
                Console.WriteLine($"Patient First Name: {ps.FirstName}");
                Console.WriteLine($"Patient Last Name: {ps.LastName}");
                Patient patient = app.OpenPatient(ps);
                foreach(var course in patient.Courses)
                {
                    Console.WriteLine($"\tCourse Id: {course.Id}");
                    foreach(var plan in course.PlanSetups)
                    {
                        if (plan.IsDoseValid)
                        {
                            Console.WriteLine($"\tPlan ID:{plan.Id}\tMax Dose = {plan.Dose.DoseMax3D}");
                        }
                    }
                }
                //must remember to close patient before opening a new patient.
                app.ClosePatient();
            }
        }
    }
}
