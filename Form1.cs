﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scribble
{
    public partial class Scribble : Form
    {
        Stroke current;
        List<Stroke> strokes = new List<Stroke>();
        List<Stroke> undo = new List<Stroke>();
        Color currentColor = Color.Black;
        int currentSize = 10;
        bool isFilledChecked;
        ScribbleTool currentTool = new ScribbleTool(false);

        public Scribble()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Stroke stroke in strokes)
            {
                stroke.DrawThyself(e);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (current != null)
            {
                current.mouseTrack.Add(e.Location);
                Invalidate();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            current = new Stroke();
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                current.tool = currentTool;
            strokes.Add(current);
            Form1_MouseMove(sender, e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo();
            }
        }

        private void Undo()
        {
            if (strokes.Count > 0)
            {
                undo.Add(strokes[strokes.Count - 1]);
                strokes.RemoveAt(strokes.Count - 1);
                Invalidate();
            }
        }

        private void Redo()
        {
            if (undo.Count > 0)
            {
                strokes.Add(undo[undo.Count - 1]);
                undo.RemoveAt(undo.Count - 1);
                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            current = null;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            var colorPicker = new ColorDialog();
            colorPicker.AllowFullOpen = false;
            if (colorPicker.ShowDialog() == DialogResult.OK)
                currentTool.color = colorPicker.Color;
        }

        private void SizeSelect_ValueChanged(object sender, EventArgs e)
        {
            currentTool.size = (int)SizeSelect.Value;
        }

        private void FillCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cursive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Circle_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
