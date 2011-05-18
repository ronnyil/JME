using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MostlyMusic;
using ReTaggin;

namespace formTest
{
    public partial class albumform : Form
    {
        bool buttonsEnabled;
        private MostlyMusicAdapter _adapter;
        private ReTag _module;
        
        public albumform()
        {
            InitializeComponent();
            buttonsEnabled = true;
            
        }
        public void reinitialize(MostlyMusicAdapter ad, ReTag re)
        {
            this.Text = ad.Query;
            _adapter = ad;
            _module = re;
        }
        private void albumform_Shown(object sender, EventArgs e)
        {
            listBox1.DataSource = _adapter.Responses;      
        }
        private void toggleButtons()
        {
            buttonsEnabled = !buttonsEnabled;
            listBox1.Enabled = button2.Enabled = button1.Enabled = buttonsEnabled;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            
                if (listBox1.SelectedItem is MostlyMusicQueryResponse)
                {                    
                    try
                    {
                        _adapter.TrackDownloadFinished += new EventHandler(_adapter_TrackDownloadFinished);
                        _module.ResetAlbumName(((MostlyMusicQueryResponse)listBox1.SelectedItem).AlbumTitle);
                        progressBar1.Visible = true;
                        waitLabel.Visible = true;
                        toggleButtons();
                        _adapter.Album = listBox1.SelectedItem as MostlyMusicQueryResponse; 
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show((ex as MMAdapterException).problem.ToString());
                    }
                }
            
        }

        void _adapter_TrackDownloadFinished(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(()=>
                {
                    listBox1.DataSource = _adapter.TrackList;
                    listBox1.DoubleClick -= (listBox1_DoubleClick);
                    button1.Visible = true;
                    progressBar1.Visible = false;
                    waitLabel.Visible = false;
                    toggleButtons();
                    _adapter.TrackDownloadFinished -= _adapter_TrackDownloadFinished;
                    pictureBox1.Visible = false;
                    pictureBox1.Image = _adapter.AlbumArt.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, new Image.GetThumbnailImageAbort(() => { return true; }), new IntPtr(20));
                    pictureBox1.Visible = true;
                    checkBox1.Visible = true;
                    /* pictureBox1.LoadAsync(_adapter.ImageLink);
                    pictureBox1.LoadCompleted += new AsyncCompletedEventHandler((obj, ev) =>
                        {
                            pictureBox1.Image = pictureBox1.Image.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, new Image.GetThumbnailImageAbort(() => { return true; }), new IntPtr(20));
                            pictureBox1.Visible = true;
                        });*/
                 }));
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you share you wish to proceed?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    /*BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += new DoWorkEventHandler((obj, args) => 
                    {
                        ((ReTag)args.Argument).ProgressUpdate+=new EventHandler((ob,arg)=>
                        {
                            bgw.ReportProgress(0, ((ProgressEventArgs)arg).File);
                        });
                        ((ReTag)args.Argument).saveChangesByTrackList();
                    });
                    bgw.ProgressChanged+=new ProgressChangedEventHandler((ob,ar)=>{
                        
                        Invoke(new MethodInvoker(()=>
                            {
                                progressBar1.Visible = true;
                                waitLabel.Text = "Updating " + (string)ar.UserState;
                            }));
                    });
                    bgw.RunWorkerCompleted+=new RunWorkerCompletedEventHandler((o,a)=>{
                        Invoke(new MethodInvoker(() => { this.Close(); }));
                    });*/
                    _module.prep(_adapter.TrackList, checkBox1.Checked, _adapter.AlbumArt); 
                    _module.saveChangesByTrackList();
                    waitLabel.Visible = true;
                    waitLabel.Text = "Done!";                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("oops...something not so good happened :(\n" + ex.Message);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            searchForm s = new searchForm(_adapter);
            s.FormClosed += new FormClosedEventHandler(s_FormClosed);
            s.ShowDialog();
        }

        void s_FormClosed(object sender, FormClosedEventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = _adapter.Responses;
            listBox1.DoubleClick += (listBox1_DoubleClick);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
