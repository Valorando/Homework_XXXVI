using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_27_10_II
{
    public partial class Form1 : Form
    {
        private Process[] processes = Process.GetProcesses();
        public Form1()
        {
            InitializeComponent();
            LoadProcesses();

            int updateInterval = 3000;
            timer1.Interval = updateInterval;
            timer1.Tick += (sender, e) => LoadProcesses(); 
            timer1.Start();

            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog1.Title = "Сохранить в файл";
        }


        private void LoadProcesses()
        {
            
            listView1.Items.Clear();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                ListViewItem item = new ListViewItem(process.ProcessName); 
                item.SubItems.Add(process.Id.ToString()); 
                item.SubItems.Add(process.Threads.Count.ToString()); 
                item.SubItems.Add(process.HandleCount.ToString()); 
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
             if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {
                    
                 using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                 {
                     foreach (ListViewItem item in listView1.Items)
                     {
                          string line = string.Join(",", item.SubItems.OfType<ListViewItem.ListViewSubItem>().Select(subItem => subItem.Text));
                          sw.WriteLine(line);
                     }
                 }
            }
        }
    }
}
