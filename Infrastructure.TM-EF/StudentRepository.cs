using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TM_EF
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SaveTheFoodDbContext _dbcontext;

        public StudentRepository(SaveTheFoodDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Student> Studenten => _dbcontext.Studenten.ToList();

        public Student? CreateStudent(Student student)
        {
            _dbcontext.Studenten.Add(student);
            _dbcontext.SaveChanges();

            return student;
        }

        public Student? ReadStudent(string email)
        {
            return _dbcontext.Studenten.FirstOrDefault(s => s.Email == email);
        }
        public Student? UpdateStudent(Student student)
        {
            var entityToUpdate = _dbcontext.Studenten.FirstOrDefault(s => s.Email == student.Email);   
            if (entityToUpdate != null)
            {
                entityToUpdate.Naam = student.Naam;
                entityToUpdate.GeboorteDatum = student.GeboorteDatum;
                entityToUpdate.StudentNummer = student.StudentNummer;
                entityToUpdate.StudieStad = student.StudieStad;
                entityToUpdate.TelefoonNummer = student.TelefoonNummer;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }

        public Student? DeleteStudent(Student student)
        {
            var entityToRemove = _dbcontext.Studenten.FirstOrDefault(s => s.Email == student.Email);
            if (entityToRemove != null)
            {
                _dbcontext.Studenten.Remove(entityToRemove);
                _dbcontext.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
