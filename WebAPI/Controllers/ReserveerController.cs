using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReserveerController : Controller
    {
        private readonly IPakketRepository pakketRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ILogger<ReserveerController> logger;

        public ReserveerController(IPakketRepository pakketRepository, IStudentRepository studentRepository, ILogger<ReserveerController> logger)
        {
            this.pakketRepository = pakketRepository;
            this.studentRepository = studentRepository;
            this.logger = logger;
        }

        [HttpPut("{id}")]
        public Pakket ReserveerPakket(int id, Student s)
        {
            Pakket pakket = pakketRepository.ReadPakket(id);
            Student student = studentRepository.ReadStudent(s.Email);
            return pakketRepository.ReserveerPakket(pakket, student);
        }
    }
}
