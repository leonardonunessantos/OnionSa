using Microsoft.AspNetCore.Mvc;
using OnionSa.Application.Services.Interfaces;


namespace OnionSa.API.Controllers;

[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISpreadsheetService _spreadsheetService;
    private readonly IOrderService _orderService;
    private readonly IClientService _clientService;

    public OrdersController(
        ISpreadsheetService spreadsheetService,
        IOrderService orderService,
        IClientService clientService)
    {
        _spreadsheetService = spreadsheetService;
        _orderService = orderService;
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<ActionResult> Upload([FromForm] ICollection<IFormFile> file)
    {
         if (file == null || file.Count == 0)
        {
            return BadRequest("Arquivo vazio");
        }
        try
        {
            var dataAsList = await _spreadsheetService.ProcessExcelFiles(file);
            
            var createClients = _clientService.CreateAll(dataAsList);

            var createOrders = _orderService.CreateAll(dataAsList);

            return Ok(true);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
        }
    }
}