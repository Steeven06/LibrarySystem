using LibrarySystem.Application.DTOs.Loans;
using LibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoanController(ILoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetById(Guid id)
        {
            var loan = await _service.GetByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateLoanDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnLoan(Guid id)
        {
            await _service.ReturnBookAsync(id);
            return NoContent();
        }
    }
}
