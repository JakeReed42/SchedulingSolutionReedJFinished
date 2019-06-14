using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Services
{
     public static class SelectedCoursesExtensions
     {
            private const string SelectedCoursesSessionKey = "SelectedCourseList";

            public static List<int> GetSelectedCourses(this ISession session)
            {
                var value = session.GetString(SelectedCoursesSessionKey);
                if (value == null)
                {
                    return new List<int>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<int>>(value);
                }
            }

            public static void SetSelectedCourses(this ISession session, List<int> selectedCoursesList)
            {
                session.SetString(SelectedCoursesSessionKey, JsonConvert.SerializeObject(selectedCoursesList));
            }
     }
}

