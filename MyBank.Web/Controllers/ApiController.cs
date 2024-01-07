using Microsoft.AspNetCore.Mvc;
using MyBank.Lib;
using MyBank.Web.Data;

namespace MyBank.Web.Controllers;

[ApiController]
public class ApiController : Controller
{
    private readonly ApplicationDbContext _db;
    public ApiController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost("/api/deposit")]
    public IActionResult Deposit([FromBody] DepositModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == model.BankAccount);

        client.Money = client.Money + model.MoneyCount;
        _db.SaveChanges();

        return Ok();
    }

    [HttpPost("/api/withdraw")]
    public IActionResult Withdraw([FromBody] WithdrawModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == model.BankAccount);

        if (client.Money >= model.MoneyCount)
        {
            client.Money = client.Money - model.MoneyCount;
            _db.SaveChanges();

            return Ok();
        }

        return BadRequest();
    }
}