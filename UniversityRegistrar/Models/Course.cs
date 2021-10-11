using System;
using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    public Course()
    {
      this.JoinEntities = new HashSet<Enrollments>();
    }
    public int CourseId {get; set;}
    public string CourseName {get; set;}
    public string CourseNumber {get; set;}

    public virtual ICollection<Enrollments> JoinEntities {get;}
  }
}
