using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2024._12._11.javító_dolgozat_megoldása
{
    public partial class Form1 : Form
    {
        int voltage = 12;
        int batteryVoltage = 24;
        int count = 0;
        Timer moveTimer = new Timer();
        Timer createTimer = new Timer();
        //List<Label> electrons = new List<Label>();
        //
        // minden Label-ből Electron lett
        // minden TabIndex-ből speed lett
        List<Electron> electrons = new List<Electron>();
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            button1.Click += IncreaseVoltage;
            button2.Click += DecreaseVoltage;
            StartTimers();
        }
        void StartTimers()
        {
            moveTimer.Interval = 16;
            createTimer.Interval = 500;

            moveTimer.Tick += MoveEvent;
            createTimer.Tick += CreateEvent;

            moveTimer.Start();
            createTimer.Start();
        }
        void MoveEvent(Object s, EventArgs e)
        {
            int divider = 4;
            List<Electron> toBeDeleted = new List<Electron>();
            foreach (Electron item in electrons)
            {
                if (item.Bounds.IntersectsWith(wire1.Bounds) && (item.Top + batteryVoltage / divider) > (wire2.Top - wire2.Height / 2 + item.Height / 2))
                {
                    item.Top -= batteryVoltage / divider;
                }
                else if (item.Bounds.IntersectsWith(wire2.Bounds))
                {
                    item.Left -= batteryVoltage / divider;
                    if (item.Left <= voltageRegulator.Right)
                    {
                        item.Hide();
                    }
                }
                else if (item.Bounds.IntersectsWith(voltageRegulator.Bounds))
                {
                    item.Left -= batteryVoltage / divider;
                    if (item.Right < voltageRegulator.Left)
                    {
                        item.Show();
                        item.speed = voltage / divider;
                    }
                }
                else if (item.Bounds.IntersectsWith(wire3.Bounds))
                {
                    item.Left -= item.speed;
                    if (item.Left <= light.Right)
                    {
                        item.Hide();
                    }
                }
                else if (item.Bounds.IntersectsWith(light.Bounds))
                {
                    if (item.Left >= wire4.Left)
                    {
                        item.Left -= item.speed;
                    }
                    else
                    {
                        item.Top += item.speed;
                        if (item.Top >= light.Bottom)
                        {
                            item.Show();
                        }
                    }
                }
                else if (item.Bounds.IntersectsWith(wire4.Bounds))
                {
                    item.Top += item.speed;
                }
                else if (item.Bounds.IntersectsWith(wire5.Bounds))
                {
                    item.Left += item.speed;
                }
                else if (item.Bounds.IntersectsWith(end.Bounds))
                {
                    if (item.Visible)
                    {
                        item.Hide();
                        count++;
                        UpdateCount();
                        toBeDeleted.Add(item);
                    }
                }
            }
            //  ezzel töröljük az electrons lista elemeit
            foreach (Electron item in toBeDeleted)
            {
                electrons.Remove(item);
                //kiir();
            }
        }
        void CreateEvent(object s, EventArgs e)
        {
            Electron electron = new Electron();
            this.Controls.Add(electron);
            electrons.Add(electron);
            electron.AutoSize = false;
            electron.Width = 6;
            electron.Height = 6;
            electron.BackColor = Color.Lime;
            electron.Top = wire1.Bottom - electron.Height;
            electron.Left = wire1.Left + wire1.Width / 2 - electron.Width / 2;
            electron.BringToFront();
            //kiir();
        }
        void IncreaseVoltage(Object s, EventArgs e)
        {
            if (voltage < batteryVoltage)
                voltage++;
            UpdateVoltage();
        }
        void DecreaseVoltage(Object s, EventArgs e)
        {
            if (voltage > 4)
                voltage--;
            UpdateVoltage();
        }
        void UpdateCount()
        {
            counterLabel.Text = $"Electrons passed: {count}";
        }
        void UpdateVoltage()
        {
            voltageLabel.Text = voltage + "V";
        }
        /*void kiir()
        {
            this.Text = electrons.Count.ToString();
        }*/
    }
}