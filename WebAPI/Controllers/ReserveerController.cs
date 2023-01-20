using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("reserveringen")]
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
        
        [HttpPost("reserveer/{id}")]
        public Pakket ReserveerPakket(int id, Student s)
        {
            Pakket pakket = pakketRepository.ReadPakket(id);
            Student student = studentRepository.ReadStudent(s.Email);
            return pakketRepository.ReserveerPakket(pakket, student);
        }

        [HttpGet]
        public List<Pakket> GetReserveringen()
        {
            return pakketRepository.Pakketten.Where(p => p.GereserveerdDoor != null).ToList();
        }
        
        [HttpDelete("{id}")]
        public Pakket VerwijderReservering(int id)
        {
            Pakket pakket = pakketRepository.ReadPakket(id);
            pakket.GereserveerdDoor = null;
            return pakketRepository.UpdatePakket(pakket, id);
        }
        
        [HttpPut("{id}")]
        public Pakket UpdateReservering(int id, Student s)
        {
            Pakket pakket = pakketRepository.ReadPakket(id);
            Student student = studentRepository.ReadStudent(s.Email);
            pakket.GereserveerdDoor = student;
            return pakketRepository.UpdatePakket(pakket, id);
        }
    }
}
