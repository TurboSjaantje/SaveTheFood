namespace Domain.Extensions
{
    public static class StudentExtensions 
    {
        public static int GetAge(this Student student)
        {
            return DateTime.Now.Year - DateTime.Parse(student.GeboorteDatum).Year;
        }
    }
}

