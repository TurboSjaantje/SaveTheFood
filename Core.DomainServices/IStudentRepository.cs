using Domain;

namespace Core.DomainServices
{
    public interface IStudentRepository
    {
        IEnumerable<Student> Studenten { get; }

        Student? CreateStudent(Student Student);
        Student? ReadStudent(string email);
        Student? UpdateStudent(Student Student);
        Student? DeleteStudent(Student Student);
    }
}