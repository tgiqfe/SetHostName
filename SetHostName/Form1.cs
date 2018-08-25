using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;

namespace SetHostName
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            textBox1.Focus();
        }

        //  変更
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = 
                        $"/c wmic computersystem where name=\"%computername%\" call rename name=\"{textBox1.Text}\"";
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.Start();
                    proc.WaitForExit();
                }
                label1.Visible = true;
                label2.Visible = true;
                label2.Text = "新ホスト名：" + textBox1.Text;
                textBox1.Text = "";
            }
        }

        //  閉じる
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //  再起動
        private void button3_Click(object sender, EventArgs e)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments =
                    $"/c shutdown /r /t 0";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.Enter:
                    button1_Click(sender, e);
                    break;
            }
        }
    }
}
