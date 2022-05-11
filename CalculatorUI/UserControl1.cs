using Calculator.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Calculator.UI
{
    public partial class CalculatorUI : UserControl
    {
        ICalculator IC;
        string countText;
        string beforeValue;
        
        public CalculatorUI(ICalculator ic)
        {
            InitializeComponent();
            IC = ic;
            label1.Text = "";
            textBox1.Text = "0";
            Button[] btnNum = new Button[11] { button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button_dot };
            foreach (var item in btnNum)
            {
                item.Click += Btn_click;
            }
            Button[] btnCount = new Button[5] { button_plus,button_minus,button_times,button_divided,button_square };
            foreach (var item in btnCount)
            {
                item.Click += Btn_count;
            }
            button_ac.Click += Button_ac_Click;
            button_back.Click += Button_back_Click;
            button_equals.Click += Button_equals_Click;

        }

        private void Button_ac_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            label1.Text = "";
           
        }
        private void Button_back_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 1)
            {
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
            else
            {
                textBox1.Text = "0";
            }
        }


        private void Button_equals_Click(object sender, EventArgs e)
        {
            switch (countText)
            {
                case "+":
                    textBox1.Text =IC.等於(計算法.加法, Convert.ToDouble(beforeValue) , Convert.ToDouble(textBox1.Text)).ToString();

                    break;
                case "-":
                    textBox1.Text = IC.等於(計算法.減法, Convert.ToDouble(beforeValue), Convert.ToDouble(textBox1.Text)).ToString();

                    break;
                case "×":
                    textBox1.Text = IC.等於(計算法.乘法, Convert.ToDouble(beforeValue), Convert.ToDouble(textBox1.Text)).ToString();

                    break;
                case "÷":
                    if (textBox1.Text!="0" && beforeValue!="0")
                    {

                    textBox1.Text = IC.等於(計算法.除法, Convert.ToDouble(beforeValue), Convert.ToDouble(textBox1.Text)).ToString();
                    }
                    else
                    {
                        textBox1.Text = "0";
                    }

                    break;
                case "√":
                    textBox1.Text = IC.等於(計算法.開根號, Convert.ToDouble(beforeValue)).ToString();

                    break;
                default:
                    textBox1.Text = "0";
                    break;
            }
        }
        static string TextValue(string boxValue, string buttonText)
        {
            if (boxValue == "0" || boxValue == "")
            {
                boxValue = buttonText == "." ? "0." : buttonText;
                return boxValue;
            }
            else
            {
                //檢查小數點，如果已經有了就不能再次輸入
                if ((buttonText != ".") || (!boxValue.Contains(".")))
                {
                    return boxValue += buttonText;
                }
                return boxValue;
            }
        }
        private void Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            textBox1.Text=TextValue(textBox1.Text, btn.Text);
            
        }
      
        private void Btn_count(object sender, EventArgs e)
        {
            Button btnCount = (Button)sender;
            countText = btnCount.Text;
            beforeValue = textBox1.Text;
            label1.Text = btnCount.Text== "√"? countText+ beforeValue: beforeValue + countText;
            textBox1.Text = "";
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int pressed_key = (int)KeyInterop.VirtualKeyFromKey((Key)keyData);
            int intKey = (int)keyData;
            if (intKey >= 96 && intKey <= 105)
            {
                textBox1.Text += (intKey - 96).ToString();
                return true;

            }
            else if (intKey == (int)Keys.Decimal)
            {
                textBox1.Text=TextValue(textBox1.Text, ".");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        //private void CalculatorUI_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    char ch = e.KeyChar;
        //        textBox1.Text += ch;
        //    if ((ch < '0' || ch > '9') && ch != '\b' && ch != '.')
        //    {
        //        if ((byte)ch == 13)
        //        {
        //            button_equals.Focus();
        //        }
        //        else
        //        {  
        //             e.Handled = true;

        //        }
        //    }
        //}


    }
}
