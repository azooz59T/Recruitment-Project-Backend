using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public CompanyController(ApplicationDBContext context, IMapper mapper)
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

        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto dto)
        {
            Company newCompany = _mapper.Map<Company>(dto);
            await _context.Companys.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return Ok("Company Created Succesfully");
        }

        //READ

        //UPDATE

        //DELETE
    }

}
