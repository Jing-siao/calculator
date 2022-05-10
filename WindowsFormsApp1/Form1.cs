using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Calculator.Core.CalculatorCore calculatorCore = new Calculator.Core.CalculatorCore();
            Calculator.UI.CalculatorUI calculatorUI = new Calculator.UI.CalculatorUI(calculatorCore);
            this.Controls.Add(calculatorUI);
            InitializeComponent();
        }
    }
}
