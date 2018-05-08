using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Bai1_Tuan8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                mailclient.EnableSsl = true;
                mailclient.Credentials = new NetworkCredential(textBox1.Text, textBox2.Text);
                MailMessage message = new MailMessage(textBox1.Text, textBox3.Text);
                message.Subject = textBox4.Text;
                message.Body = textBox5.Text;
                mailclient.Send(message);
                MessageBox.Show("Mail Sent!");
            }
            catch
            {
                MessageBox.Show("Unable to send mail!");
            }

        }
    }
}
