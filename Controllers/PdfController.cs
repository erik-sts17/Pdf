using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using Pdf.Services;


namespace Pdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;

        public PdfController(ILogger<PdfController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var generator = new GeradorPdf();
                byte[] pdf = generator.GeneratePdf("<h1>Teste PDF</h1>");
                return File(pdf, "application/pdf", "file.pdf");
            }
            catch (Exception e)
            {
                return BadRequest("Invalid Base64 string.");
            }
        }
    }
}
