using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MostlyMusic;
using System.IO;
using ReTaggin;

namespace formTest
{
    public partial class Form1 : Form
    {
        bool buttonsEnabled;
        albumform a;
        private MostlyMusicAdapter _adapter;
        private ReTag _reTagger;
        public Form1()
        {
            InitializeComponent();
            this.MaximumSize = this.MinimumSize = this.Size;
            buttonsEnabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog a = new FolderBrowserDialog();
            if (a.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                try
                {
                    _reTagger = new ReTag(a.SelectedPath);
                    albumBox.DataSource = _reTagger.AlbumSet.ToList();
                    foundlabel.Text = "I have found the following albums in that folder,\nSelect the one you would like to edit and press Go ";
                    albumBox.Visible = true;
                    goButton.Visible = true;    
                }
                catch (Exception retagEx)
                {
                    MessageBox.Show(retagEx.Message);
                }
                      

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            _adapter = new MostlyMusicAdapter();
            a = new albumform();
            _reTagger.CurrentAlbum = (string)albumBox.SelectedItem;
            a.FormClosed+=new FormClosedEventHandler(a_FormClosed);
            BW.RunWorkerAsync(albumBox.SelectedItem); 
            progressBar1.Visible = true;
            waitLabel.Visible = true;
            toggleButtons();           
        }
        /// <summary>
        /// Shows that we're working.
        /// </summary>
        private void toggleButtons()
        {

            buttonsEnabled = !buttonsEnabled;
            button1.Enabled = goButton.Enabled  = albumBox.Enabled = buttonsEnabled;           
        }

        void a_FormClosed(object sender, FormClosedEventArgs e)
        {
            toggleButtons();
            this.Show();

        }

        private void BW_DoWork(object sender, DoWorkEventArgs e)
        {            
                MostlyMusicAdapter mm = new MostlyMusicAdapter();
                mm.InitializeComponent((string)e.Argument);
                e.Result = mm.Responses;        
        }

        private void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            Invoke(new MethodInvoker(() =>
                {
                    _adapter.Responses = e.Result as List<MostlyMusicQueryResponse>;
                    a.reinitialize(_adapter, _reTagger);
                    progressBar1.Visible = false;
                    waitLabel.Visible = false;
                    this.Hide();
                    a.ShowDialog();
                }));
        }

   
      
     
    }
}
