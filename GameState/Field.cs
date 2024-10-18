using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2;



internal class Field : Label
{
    
    Size gridSize;
    Label grid;
    Label showLog;

    internal Character guy = new();
    internal string log = "";

    //non important paramaters
    const int xoffset = 5;
    const int yoffset = 5;
    const int boxsize = 300;


    internal Field (Size gridsize, Size size)
    {
        showLog = new();
        grid = new();
        

        Reset(gridsize, size);
        
        Controls.Add(showLog);
        Controls.Add(grid);
        this.Invalidated += (object o, InvalidateEventArgs e) => {
            grid.Invalidate();
            showLog.Invalidate();
        };
        grid.Paint += Draw;
        this.gridSize = gridsize; 
    }

    internal void Reset(Size gridsize, Size size)
    {
        this.Size = size;
        grid.BackColor = Color.AliceBlue;
        grid.Size = new Size(size.Width, size.Height - boxsize);
        guy = new();

        showLog.BorderStyle = BorderStyle.FixedSingle;
        showLog.ForeColor = Color.Red;
        showLog.Size = new Size(size.Width, boxsize);
        showLog.Location = new Point(0, size.Height - boxsize);
        showLog.Text = "";
        log = "";

        this.gridSize = gridsize;
    }

    void Draw(object o, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Size tileSize = new Size(grid.Size.Width / gridSize.Width, grid.Size.Height / gridSize.Height);

        for (int x = 0; x < gridSize.Width; x++)
        {
            g.DrawLine(Pens.Black, (x * grid.Size.Width) / gridSize.Width, 0, (x * grid.Size.Width) / gridSize.Width, grid.Size.Height);
        }
        g.DrawLine(Pens.Black, grid.Size.Width-1, 0, grid.Size.Width - 1, grid.Size.Height);

        for (int y = 0; y < gridSize.Height; y++)
        {
            g.DrawLine(Pens.Black, 0, (y * grid.Size.Height) / gridSize.Height, grid.Size.Width, (y * grid.Size.Height) / gridSize.Height);
        }
        g.DrawLine(Pens.Black, 0, grid.Size.Height-1, grid.Size.Width, grid.Size.Height-1);

        int start = guy.direction * 90 + 30;

        


        g.DrawArc(
            new Pen(Color.Blue, 3), 
            new(transform(guy.p), 
            tileSize),
            start,
            300
            );

        Point transform(Point p)
        {
            return new Point(((p.X+xoffset) * Size.Width) / gridSize.Width, ((p.Y+yoffset) * Size.Height) / gridSize.Height);
        }


        showLog.Text = log;

    }
}
