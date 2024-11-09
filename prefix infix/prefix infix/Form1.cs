using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prefix_infix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int comparsion(char a)
        {
            if (a == '-' || a == '+')
                return 1;
            else if (a == '*' || a == '/')
                return 2;
            else if (a == '^')
                return 3;
            else
                return -1;
        }
        bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '_';
        }

        bool IsInfix(string expression)
        {
            
            int a = 0;
            bool exp = true;
            bool first = false;
            Stack<char> stack = new Stack<char>();
            
                foreach (char c in expression)
                {
                if (!first)
                {
                    if (IsOperator(c))
                    {
                        return false;
                    }
                    first = true;
                }
                if (char.IsLetterOrDigit(c))
                    {
                        a++;
                        exp = false;
                    }
                    else if (IsOperator(c))
                    {
                        a--;
                        exp = true;
                    }
                    else if (c == '(')
                        stack.Push(c);
                    else if (c == ')')
                    {
                        if (stack.Count == 0)
                        {
                            return false;
                        }
                        stack.Pop();
                    }
                    else
                        return false;

                }
               return a == 1 && stack.Count == 0 && !exp;
            
        }
        bool isPrefix(string expression)
        {
            int letter = 0;
            int oper = 0;
            bool first = false;
            
            foreach (char c in expression)
            {
                if (!first)
                {
                    if (!IsOperator(c))
                    { 
                        return false;
                    }
                    first = true;  
                }
                if (IsOperator(c))
                    oper++;
                else if (char.IsLetterOrDigit(c))
                    letter++;
                else
                    return false;
                if (letter>oper+1)
                    return false;
            }
            return letter == oper + 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = "";
            try 
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("عبارت را وارد کنید");
                }
                else
                {
                    string infix = textBox1.Text.ToString().Trim();
                    bool isInfix = IsInfix(infix);
                    if (isInfix == false)

                        MessageBox.Show("عبارت وارد شده میانوندی نمیباشد");
                    else
                    {
                        Stack<char> Operator = new Stack<char>();
                        Stack<string> Operand = new Stack<string>();
                        foreach (char i in infix)
                        {
                            if (char.IsLetterOrDigit(i))
                                Operand.Push(i.ToString());
                            else if (i == '(')
                                Operator.Push(i);
                            else if (i == ')')
                            {
                                while (Operator.Count > 0 && Operator.Peek() != '(')
                                {
                                    string ope = Operator.Pop().ToString();
                                    string pre1 = Operand.Pop();
                                    string pre2 = Operand.Pop();
                                    Operand.Push(ope + pre2 + pre1);
                                }
                                if(Operator.Count > 0 && Operator.Peek()=='(')
                                Operator.Pop();
                            }
                            else
                            {
                                while (Operator.Count > 0 && comparsion(i) <= comparsion(Operator.Peek()))
                                {
                                    string ope = Operator.Pop().ToString();
                                    string pre1 = Operand.Pop();
                                    string pre2 = Operand.Pop();
                                    Operand.Push(ope + pre2 + pre1);
                                }
                                Operator.Push(i);
                            }
                           
                        }
                        while (Operator.Count > 0)
                        {
                            string ope = Operator.Pop().ToString();
                            string pre1 = Operand.Pop();
                            string pre2 = Operand.Pop();
                            Operand.Push(ope + pre2 + pre1);
                        }
                        textBox2.Text = Operand.Pop();
                    }

                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erorr:"+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            try {



                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("عبارت را وارد کنید");
                }
                else
                {
                    string prefix = textBox1.Text.ToString().Trim();
                    bool ispre = isPrefix(prefix);
                    if (ispre == false)

                        MessageBox.Show("عبارت وارد شده پیشوندی نمیباشد");
                    else
                    {
                        


                        char[] prefixRev = prefix.ToArray();
                        Array.Reverse(prefixRev);
                        Stack<string> stack = new Stack<string>();
                        foreach (char i in prefixRev)
                        {
                            if (char.IsLetterOrDigit(i))
                            {
                                stack.Push(i.ToString());
                            }
                            else
                            {
                                string prefix1 = stack.Pop().ToString();
                                string prefix2 = stack.Pop().ToString();
                                stack.Push(prefix1 + i + prefix2);
                            }
                        }
                        textBox2.Text = stack.Pop();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
            }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = textBox2.Text = "";
                button2.Hide();
                button1.Show();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = textBox2.Text = "";
                button1.Hide();
                button2.Show();
            }
        }
    }
}
