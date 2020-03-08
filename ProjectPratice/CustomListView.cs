using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPratice
{
    class CustomListView : ListView
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush br = new LinearGradientBrush(new Rectangle(0, 0, 776, 452), Color.Black, Color.Black, 0, true);
            ColorBlend cb = new ColorBlend();
            cb.Positions = new[] { 0, 2.0f, 1.0f };
            cb.Colors = new[] { Color.SteelBlue, Color.SpringGreen, Color.DarkBlue };
            br.InterpolationColors = cb;

            // rotate
            br.RotateTransform(0);
            // paint
            e.Graphics.FillRectangle(br, this.ClientRectangle);


            base.OnPaint(e);
        }

        public CustomListView()
        {
            ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();
            ColumnHeader columnHeader3 = new System.Windows.Forms.ColumnHeader();
            ColumnHeader columnHeader4 = new System.Windows.Forms.ColumnHeader();
            AutoArrange = false;
            CheckBoxes = true;
            Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
           columnHeader4});
            Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FullRowSelect = true;
            GridLines = true;

            columnHeader1.Text = "Student ID";
            columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 107;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "State";
            columnHeader3.Width = 116;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Class";
            columnHeader4.Width = 77;
        }
    }
}
