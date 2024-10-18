using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;

internal class Game : Form
{
    Field field;
    Program program;
    Label stats = new();
    Label code = new();


    internal Game()
    {
        stats.Location = new Point(850, 50);
        stats.Size = new(200, 100);
        Controls.Add(stats);
        stats.BorderStyle = BorderStyle.FixedSingle;

        code.Location = new Point(850, 150);
        code.Size = new(200, 600);
        Controls.Add(code);
        code.BorderStyle = BorderStyle.FixedSingle;


        this.Size = new Size(1100, 850);
        GenerateToolStrip();
        InitializeField();

        program = new(new(field));
    }

    void GenerateToolStrip()
    {
        ToolStrip ToolStrip = new();
        ToolStripButton t1 = new();
        t1.Text = "open";
        t1.Click += (object o, EventArgs e) =>
        {
            OpenFileDialog fd = new();
            fd.Filter = "Text files (*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                LoadProgram(fd.FileName);
            }

        };
        ToolStrip.Items.Add(t1);

        ToolStripButton t2 = new();
        t2.Text = "basic";
        t2.Click += (object o, EventArgs e) =>
        {
            
            LoadProgram("..\\..\\..\\Examples\\Example1.txt");

        };
        ToolStrip.Items.Add(t2);

        ToolStripButton t3 = new();
        t3.Text = "advanced";
        t3.Click += (object o, EventArgs e) =>
        {

            LoadProgram("..\\..\\..\\Examples\\Example2.txt");

        };
        ToolStrip.Items.Add(t3);

        ToolStripButton t4 = new();
        t4.Text = "expert";
        t4.Click += (object o, EventArgs e) =>
        {

            LoadProgram("..\\..\\..\\Examples\\Example3.txt");

        };
        ToolStrip.Items.Add(t4);

        ToolStripButton t5 = new();
        t5.Text = "Run";
        t5.Click += (object o, EventArgs e) =>
        {
            program.Start();
            field.Invalidate();

        };
        ToolStrip.Items.Add(t5);

        ToolStripButton t6 = new();
        t6.Text = "resetField";
        t6.Click += (object o, EventArgs e) =>
        {

            ResetField();
            field.Invalidate();

        };
        ToolStrip.Items.Add(t6);


        Controls.Add(ToolStrip);
    }

    void LoadProgram(string filename)
    {
        string codetxt = File.ReadAllText(filename);
        program.Parse(codetxt);
        code.Text = codetxt.Replace('\t', '_');
        stats.Text = program.GetStatistics();

        field.Invalidate();

    }

    void InitializeField()
    {
        field = new Field(new(),new ());
        ResetField();
        field.Location = new Point(50, 50);

        Controls.Add(field);
    }

    void ResetField()
    {
        field.Reset(new Size(60, 30), new Size(800, 700));
    }


}
