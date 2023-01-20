using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("pakketten")]
    public class PakketController : Controller
    {
    
        private readonly IPakketRepository pakketRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ILogger<ReserveerController> logger;
        
        public PakketController(IPakketRepository pakketRepository, IStudentRepository studentRepository, ILogger<ReserveerController> logger)
        {
            this.pakketRepository = pakketRepository;
            this.studentRepository = studentRepository;
            this.logger = logger;
        }
        
        [HttpGet]
        public List<Pakket> GetPakketten()
        {
            return pakketRepository.Pakketten.ToList();
        }
        
    }
}


