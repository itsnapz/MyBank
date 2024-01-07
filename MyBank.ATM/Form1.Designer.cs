namespace MyBank.ATM;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _lblHeading = new Label();
        _txtInput = new TextBox();
        _btnDeposit = new Button();
        _btnWithdraw = new Button();
        _txtBankAccount = new TextBox();
        SuspendLayout();
        // 
        // _lblHeading
        // 
        _lblHeading.AutoSize = true;
        _lblHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
        _lblHeading.Location = new Point(330, 9);
        _lblHeading.Name = "_lblHeading";
        _lblHeading.Size = new Size(157, 41);
        _lblHeading.TabIndex = 0;
        _lblHeading.Text = "Bankomat";
        // 
        // _txtInput
        // 
        _txtInput.Location = new Point(306, 209);
        _txtInput.Name = "_txtInput";
        _txtInput.PlaceholderText = "Částka";
        _txtInput.Size = new Size(203, 27);
        _txtInput.TabIndex = 1;
        // 
        // _btnDeposit
        // 
        _btnDeposit.Location = new Point(52, 194);
        _btnDeposit.Name = "_btnDeposit";
        _btnDeposit.Size = new Size(185, 56);
        _btnDeposit.TabIndex = 2;
        _btnDeposit.Text = "Vložit";
        _btnDeposit.UseVisualStyleBackColor = true;
        _btnDeposit.Click += _btnDeposit_Click;
        // 
        // _btnWithdraw
        // 
        _btnWithdraw.Location = new Point(577, 194);
        _btnWithdraw.Name = "_btnWithdraw";
        _btnWithdraw.Size = new Size(185, 56);
        _btnWithdraw.TabIndex = 3;
        _btnWithdraw.Text = "Vybrat";
        _btnWithdraw.UseVisualStyleBackColor = true;
        _btnWithdraw.Click += _btnWithdraw_Click;
        // 
        // _txtBankAccount
        // 
        _txtBankAccount.Location = new Point(306, 144);
        _txtBankAccount.Name = "_txtBankAccount";
        _txtBankAccount.PlaceholderText = "Číslo účtu";
        _txtBankAccount.Size = new Size(203, 27);
        _txtBankAccount.TabIndex = 4;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(_txtBankAccount);
        Controls.Add(_btnWithdraw);
        Controls.Add(_btnDeposit);
        Controls.Add(_txtInput);
        Controls.Add(_lblHeading);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label _lblHeading;
    private TextBox _txtInput;
    private Button _btnDeposit;
    private Button _btnWithdraw;
    private TextBox _txtBankAccount;
}