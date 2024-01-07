using System.Net.Http.Json;
using MyBank.Lib;

namespace MyBank.ATM;

public partial class Form1 : Form
{
    private readonly HttpClient _client;
    public Form1()
    {
        InitializeComponent();
        _client = new();
        _client.BaseAddress = new Uri("http://localhost:5162/");
    }

    public async Task Withdraw(WithdrawModel model)
    {
        await _client.PostAsJsonAsync("/api/withdraw", model);
    }

    public async Task Deposit(DepositModel model)
    {
        await _client.PostAsJsonAsync("/api/deposit", model);
    }

    private void _btnDeposit_Click(object sender, EventArgs e)
    {
        int input = int.Parse(_txtInput.Text);
        Guid bankAccount = new(_txtBankAccount.Text);

        DepositModel model = new()
        {
            BankAccount = bankAccount,
            MoneyCount = input,
        };

        Deposit(model);
        _txtInput.Text = string.Empty;
        _txtBankAccount.Text = string.Empty;
    }

    private void _btnWithdraw_Click(object sender, EventArgs e)
    {
        int input = int.Parse(_txtInput.Text);
        Guid bankAccount = new(_txtBankAccount.Text);

        WithdrawModel model = new()
        {
            BankAccount = bankAccount,
            MoneyCount = input,
        };

        Withdraw(model);
        _txtInput.Text = string.Empty;
        _txtBankAccount.Text = string.Empty;
    }
}