using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBank.Web.Data;
using MyBank.Web.Models;
using OfficeOpenXml;

namespace MyBank.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return View(new LoginModel());
        }

        return RedirectToAction("Home");
    }

    [HttpPost]
    public IActionResult Index(LoginModel model)
    {
        var client = _db.Clients.FirstOrDefault(x => x.ClientId == model.Id && x.Password == model.Password);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        Response.Cookies.Append("id", client.ClientId.ToString());
        return RedirectToAction("Home");
    }

    public IActionResult Home()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        return View(client);
    }

    public IActionResult Transactions()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new(token);

        var transactions = _db.Transactions.ToList().FindAll(x => x.SenderId == g || x.TargetId == g);

        foreach (var transaction in transactions)
        {
            if (transaction.SenderId == g)
            {
                transaction.Sent = true;
            }
        }
        
        return View(transactions);
    }

    [HttpGet]
    public IActionResult CreateTransaction()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.CardId == null)
        {
            return RedirectToAction("Home");
        }
        
        return View(new Transaction());
    }

    [HttpPost]
    public IActionResult CreateTransaction(Transaction model)
    {
        string token = Request.Cookies["id"];

        Guid myId = new(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == myId);
        var target = _db.Clients.FirstOrDefault(x => x.ClientId == model.TargetId);

        if (target == null)
        {
            return RedirectToAction("Home");
        }

        model.SenderId = myId;
        model.SenderBankCode = client.BankCode;
        model.Date = DateTime.Today.ToShortDateString();

        client.Money = client.Money - model.MoneyCount;
        target.Money = target.Money + model.MoneyCount;

        _db.Transactions.Add(model);
        _db.SaveChanges();

        return RedirectToAction("Home");
    }

    public IActionResult Admin()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }

        return View();
    }

    [HttpGet]
    public IActionResult AddClient()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }


        return View(new Client());
    }

    [HttpPost]
    public IActionResult AddClient(Client model)
    {
        int length = 10;
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        var random = new Random();
        string password = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        model.ClientId = Guid.NewGuid();
        model.Password = password;
        model.Money = 0;
        model.IsAdmin = false;


        _db.Clients.Add(model);
        _db.SaveChanges();
        return RedirectToAction("Admin");
    }
    
    [HttpGet]
    public IActionResult DeleteClient()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }


        return View(new RemoveModel());
    }

    [HttpPost]
    public IActionResult DeleteClient(RemoveModel model)
    {
        var client = _db.Clients.FirstOrDefault(x => x.ClientId == model.Id);

        if (client == null)
        {
            return RedirectToAction("Admin");
        }

        _db.Clients.Remove(client);
        _db.SaveChanges();

        return RedirectToAction("Admin");
    }
    
    [HttpGet]
    public IActionResult CreateCard()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }


        return View(new CardModel());
    }
    
    [HttpPost]
    public IActionResult CreateCard(CardModel model)
    {
        var client = _db.Clients.FirstOrDefault(x => x.ClientId == model.Owner);

        if (client == null)
        {
            return RedirectToAction("Admin");
        }

        if (client.CardId != null)
        {
            return RedirectToAction("Admin");
        }

        long number = GenerateCardNumber();

        var check = _db.Cards.FirstOrDefault(x => x.CardId == number);

        if (check == null)
        {
            Card c = new Card()
            {
                Pin = model.Pin,
                CardId = number
            };

            client.CardId = number;
            _db.Cards.Add(c);
            _db.SaveChanges();

            return RedirectToAction("Admin");
        }

        return RedirectToAction("Admin");
    }
    
    
    [HttpGet]
    public IActionResult DeleteCard()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }


        return View(new CardRemoveModel());
    }
    
    [HttpPost]
    public IActionResult DeleteCard(CardRemoveModel model)
    {
        var card = _db.Cards.FirstOrDefault(x => x.CardId == model.CardId);

        var client = _db.Clients.FirstOrDefault(x => x.CardId == model.CardId);

        if (card == null)
        {
            return RedirectToAction("Admin");
        }

        client.CardId = null;
        _db.Cards.Remove(card);
        _db.SaveChanges();

        return RedirectToAction("Admin");
    }
    
    [HttpGet]
    public IActionResult Generate()
    {
        string token = Request.Cookies["id"];

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index");
        }

        Guid g = new Guid(token);

        var client = _db.Clients.FirstOrDefault(x => x.ClientId == g);

        if (client == null)
        {
            return RedirectToAction("Index");
        }

        if (client.IsAdmin == false)
        {
            return RedirectToAction("Home");
        }

        return View(new GenerateModel());
    }
    
    [HttpPost]
    public IActionResult Generate(GenerateModel model)
    {
        var transactions = _db.Transactions.ToList().FindAll(x => x.SenderBankCode == model.BankCode || x.TargetBankCode == model.BankCode);

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Transactions");

            // Add headers
            worksheet.Cells["A1"].Value = "TransactionId";
            worksheet.Cells["B1"].Value = "MoneyCount";
            worksheet.Cells["C1"].Value = "Sender";
            worksheet.Cells["D1"].Value = "Target";

            // Add data
            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                worksheet.Cells[i + 2, 1].Value = transaction.TransactionId;
                worksheet.Cells[i + 2, 2].Value = transaction.MoneyCount;
                worksheet.Cells[i + 2, 3].Value = transaction.SenderId;
                worksheet.Cells[i + 2, 4].Value = transaction.TargetId;
            }

            // Save the Excel package to a file
            byte[] fileBytes = package.GetAsByteArray();
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "transakce.xlsx");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public long GenerateCardNumber()
    {
        int length = 16;
        string chars = "0123456789";
        var random = new Random();
        string number = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        return long.Parse(number);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}