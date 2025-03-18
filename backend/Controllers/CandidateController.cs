using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Company;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Runtime.ExceptionServices;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        public CandidateController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApplicationDBContext _context { get; }
        public IMapper _mapper { get; }

        //CRUD

        //CREATE
        [HttpPost]
        [Route("Create")]

       public async Task<ActionResult> CreateCandidate([FromForm] CandidateCreateDto dto, IFormFile pdfFile)
        {
            // First => Save pdf to Server
            // Then => Save url into our entity

            var fiveMegaByte = 5 * 1024 * 1024;
            var pdfMimeType = "application/pdf";

            if (pdfFile.Length > fiveMegaByte || pdfFile.ContentType != pdfMimeType)
            {
                return BadRequest("file is not valid");
            }

            var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeUrl);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            Candidate newcandidate = _mapper.Map<Candidate>(dto);
            newcandidate.ResumeUrl = resumeUrl;
            await _context.Candidates.AddAsync(newcandidate);
            await _context.SaveChangesAsync();
            return Ok("Candidate Created Succesfully");

        }

        //READ
        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
        {
            var candidate = await _context.Candidates.Include(candidate => candidate.job).ToListAsync();
            var candidatedto = _mapper.Map<IEnumerable<CandidateGetDto>>(candidate);

            return Ok(candidatedto);
        }

        //UPDATE

        //DELETE
    }
}
