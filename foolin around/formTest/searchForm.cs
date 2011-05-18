using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace formTest
{
    public partial class searchForm : Form
    {
        private MostlyMusic.MostlyMusicAdapter _adapter;
        public searchForm(MostlyMusic.MostlyMusicAdapter adapter)
        {
            InitializeComponent();
            _adapter = adapter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (queryBox.Text != "")
            {
                _adapter.Query = queryBox.Text;
                this.Close();
            }
        }
    }
}
