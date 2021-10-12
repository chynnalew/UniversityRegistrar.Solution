using System;
using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityRegistrarContext _db;

    public StudentsController(UniversityRegistrarContext db) 
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Student> model = _db.Students.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student, int CourseId)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      if (CourseId != 0)
      {
        _db.Enrollments.Add(new Enrollments() {CourseId = CourseId, StudentId = student.StudentId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCourses(int id)
    {
      var thisStudent = _db.Students
        .Include(student => student.JoinEntities)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(student => student.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourses(Student student, int CourseId)
    {
      if (CourseId != 0)
      {
        _db.Enrollments.Add(new Enrollments() {CourseId = CourseId, StudentId = student.StudentId});
      }
      _db.SaveChanges();
      return RedirectToAction("AddCourses");
    }

   [HttpPost]
    public ActionResult DeleteCourse(int joinId, int id)
    {
      var joinEntry = _db.Enrollments.FirstOrDefault(entry => entry.EnrollmentsId == joinId);
      _db.Enrollments.Remove(joinEntry);
      _db.SaveChanges();
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View("Details", thisStudent);
    } 

    public ActionResult Details(int id) {
      var thisStudent = _db.Students
        .Include(student => student.JoinEntities)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    public ActionResult Delete(int id) 
      {
        var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
        return View(thisStudent);
      }

      [HttpPost, ActionName("Delete")]
      public ActionResult Destroy(int id)
        {
          var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
          _db.Students.Remove(thisStudent);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
    
    // public ActionResult AddCourse(int id)
    // {
    //     var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
    //     ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
    //     return View(thisStudent);
    // }

  }
}
