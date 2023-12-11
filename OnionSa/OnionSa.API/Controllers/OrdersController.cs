using Microsoft.AspNetCore.Mvc;
using OnionSa.Application.Services.Interfaces;


namespace OnionSa.API.Controllers;

[Route("api/pedidos")]
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

    [HttpPost("upload")]
    public async Task<ActionResult> Upload([FromForm] ICollection<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("Arquivo vazio");
        }
        try
        {
            var dataAsList = await _spreadsheetService.ProcessExcelFiles(files);

            var listClients = _clientService.CreateList(dataAsList);
            List<string> listErrosClients = new List<string>();
            foreach (var client in listClients)
            {
                bool created = _clientService.Create(client);
                if (created == false)
                {
                    listErrosClients.Add(client.Document);
                }
            }

            var listOrders = _orderService.CreateList(dataAsList);
            List<int> listErrosOrders = new List<int>();
            foreach (var order in listOrders)
            {
                bool created = _orderService.Create(order);
                if (created == false)
                {
                    listErrosOrders.Add(order.OrderId);
                }
            }

            var orders = _orderService.GetAll();

            return Ok(orders);
        }
        catch (InvalidOperationException ex) // Pega o erro da formatação do excel
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
        }
    }
}