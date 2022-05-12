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
        bool hasCount=false ;

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
            Button[] btnCount = new Button[5] { button_plus, button_minus, button_times, button_divided, button_square };
            foreach (var item in btnCount)
            {
                item.Click += Btn_count;
            }
            button_ac.Click += Button_ac_Click;
            button_back.Click += Button_back_Click;
            button_equals.Click += Button_equals_Click;

        }
        //輸入數字
        private string TextValue(string boxValue, string buttonText)
        {
            if (boxValue == "0" || boxValue == "")
            {
                return buttonText == "." ? "0." : buttonText;
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
        //歸零
        private void Clear()
        {
            textBox1.Text = "0";
            label1.Text = "";
            countText = "";
            beforeValue = "";
        }
        //倒退鍵
        static string Back(string boxValue)
        {
            return boxValue.Length > 1 ? boxValue.Remove(boxValue.Length - 1, 1) : "0";
        }
        //鍵盤數字鍵
        static string KeyNum(string boxValue, int key, int num)
        {
            return boxValue == "0" || boxValue == "" ? (key - num).ToString() : boxValue += (key - num).ToString();
        }
        //鍵盤運算鍵
        private void KeyCount(string boxValue, string KeyCountText)
        {
            if (!hasCount)
            {
                beforeValue = boxValue;
            }
            countText = KeyCountText;
            //beforeValue = boxValue;
            hasCount = true;

            if (KeyCountText == "√")
            {
                textBox1.Text = IC.等於(計算法.開根號, Convert.ToDouble(beforeValue)).ToString();
                label1.Text = countText + " " + beforeValue;
            }
            else
            {
                label1.Text = beforeValue + " " + countText;
                textBox1.Text = "";
            }
        }
        //等於運算
        private string Equals(string boxValue, string a)
        {
            hasCount = false;
            switch (countText)
            {
                case "+":
                    label1.Text = a + " " + countText + " " + boxValue + " = ";
                    boxValue = IC.等於(計算法.加法, Convert.ToDouble(a), Convert.ToDouble(boxValue)).ToString();

                    break;
                case "-":
                    label1.Text = a + " " + countText + " " + boxValue + " = ";
                    boxValue = IC.等於(計算法.減法, Convert.ToDouble(a), Convert.ToDouble(boxValue)).ToString();

                    break;
                case "×":
                    label1.Text = a + " " + countText + " " + boxValue + " = ";
                    boxValue = IC.等於(計算法.乘法, Convert.ToDouble(a), Convert.ToDouble(boxValue)).ToString();

                    break;
                case "÷":
                    label1.Text = a + " " + countText + " " + boxValue + " = ";
                    if (boxValue != "0" && a != "0")
                    {

                        boxValue = IC.等於(計算法.除法, Convert.ToDouble(a), Convert.ToDouble(boxValue)).ToString();
                    }
                    else
                    {
                        boxValue = "0";
                    }

                    break;
                case "√":
                    label1.Text = a + " " + countText;
                    boxValue = IC.等於(計算法.開根號, Convert.ToDouble(beforeValue)).ToString();

                    break;
                default:
                    boxValue = "0";
                    break;
            }
            return boxValue;
        }
        private void Button_ac_Click(object sender, EventArgs e)
        {
            Clear();

        }
        private void Button_back_Click(object sender, EventArgs e)
        {
            textBox1.Text = Back(textBox1.Text);
        }


        private void Button_equals_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                textBox1.Text = Equals(textBox1.Text, beforeValue);
            }
        }

        private void Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            textBox1.Text = TextValue(textBox1.Text, btn.Text);

        }

        private void Btn_count(object sender, EventArgs e)
        {
            Button btnCount = (Button)sender;
            
            KeyCount(textBox1.Text, btnCount.Text);

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int intKey = (int)keyData;
            if (intKey >= 96 && intKey <= 105)
            {
                textBox1.Text = KeyNum(textBox1.Text, intKey, 96);

            }
            else if (intKey >= 48 && intKey <= 57)
            {
                textBox1.Text = KeyNum(textBox1.Text, intKey, 48);
            }
            switch (intKey)
            {
                case 110:
                case 190:
                    textBox1.Text = TextValue(textBox1.Text, ".");
                    break;
                case 8:
                    textBox1.Text = Back(textBox1.Text);
                    break;
                case 27:
                    Clear();
                    break;
                case 187:
                case 107:
                    KeyCount(textBox1.Text, "+");
                    break;
                case 189:
                case 109:
                    KeyCount(textBox1.Text, "-");
                    break;
                case 106:
                    KeyCount(textBox1.Text, "×");
                    break;
                case 111:
                    KeyCount(textBox1.Text, "÷");
                    break;
                case 13:
                    label1.Focus();
                    if (textBox1.Text!="")
                    {
                        textBox1.Text = Equals(textBox1.Text, beforeValue);
                    }
                    break;
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
