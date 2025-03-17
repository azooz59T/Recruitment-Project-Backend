using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public JobController(ApplicationDBContext context, IMapper mapper)
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

        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto dto)
        {
            Job newJob = _mapper.Map<Job>(dto);
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return Ok("Job Created Succesfully");
        }

        //READ
        [HttpGet]
        [Route("GET")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJob()
        {
            var jobs = await _context.Jobs.Include(job => job.Company).ToListAsync();
            var mappedJob = _mapper.Map<IEnumerable<JobGetDto>>(jobs);
            return  Ok(mappedJob);
        }
    }
}
