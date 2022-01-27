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

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    /// <summary>
    /// این فرم قبل از همه ی فرمهای برنامه اجرا می شود 
    /// و شما می توانید کنترلهای اولیه را در این فرم انجام دهید
    /// مانند چک کردن اتصال اینترنت ، فعال بود قفل سخت افزاری
    /// اتصال به بانک اطلاعاتی ، خواندن داده ها از بانک اطلاعات و غیره
    /// </summary>
    public partial class SplashForm : Form
    {
        Bitmap bmpFrmBack =
            new Bitmap(Properties.Resources.SportSplash_910508);
        
        public SplashForm()
        {
            InitializeComponent();

            backgroundWorker.WorkerReportsProgress = true;

            backgroundWorker.WorkerSupportsCancellation = true;
            
            BitmapRegion.CreateControlRegion(this, bmpFrmBack);
        }

        public bool IsInternetAccess { get; set; }

        private Thread _internetThread;
        private ThreadStart _internetThreadStart;
        delegate void internetTaskDelegate(bool blnExist);

        private void SplashForm_Load(object sender, EventArgs e)
        {
            _internetThreadStart =
                new ThreadStart(CheckConnection);

            _internetThread =
                new Thread(_internetThreadStart);

            _internetThread
                .SetApartmentState(ApartmentState.STA);

             _internetThread.Start();

            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// //عملیات کنترل وصل بودن اینترنت
        /// </summary>
        private void CheckConnection()
        {
            try
            {
                IsInternetAccess =
                    GetInternetConnectionStatus.IsInternetConnectionAvailable();
            }
            catch (System.Net.WebException)
            {
                IsInternetAccess = false;
            }

            EndCheckConnection(false);
        }

        private void EndCheckConnection(bool blnVisible)
        {
            if (this.InvokeRequired)
            {
                internetTaskDelegate oCallBack =
                    new internetTaskDelegate(EndCheckConnection);

                Invoke(oCallBack, new object[]
                {
                    blnVisible
                });
            }
            else
            {
                //تمام شده است و هر کاری که لازم است می توانیم انجام دهیم  thread اینجا کار 
                if (IsInternetAccess == true)
                {
                    MessageBox.Show("InternetConnection is connected!");
                   // AuthenticatedUser.Instance.IsInternetAccess = true;
                }
                else
                {
                    MessageBox.Show("InternetConnection is disconnected!");
                    //AuthenticatedUser.Instance.IsInternetAccess = false;
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker =
                          sender as BackgroundWorker;

            //عملیات کنترل وصل بودن اینترنت
            //CheckConnection();

            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                Thread.Sleep(100);

                // Report progress.
                backgroundWorker.ReportProgress(i);

                if (backgroundWorker.CancellationPending)
                {
                    // this is important as it set the cancelled property of RunWorkerCompletedEventArgs to true
                    e.Cancel = true;
                    break;
                }
            }

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = e.ProgressPercentage;

            System.Threading.Thread.Sleep(70);

            myProgressBar.ForeColor =
                Color.FromArgb(index + 155, 2 * index, index);//(255, 0, 0);

            myProgressBar.Value = index;

            myProgressBar.Refresh();

            statusLabel.ForeColor =
                Color.FromArgb(index * 1 + 50, index * 1 + 90, index * 2 + 55);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

       
        protected override void OnLoad(EventArgs e)
        {

            //Some Effect are :
            // CollapseInward_Effect = AW_ACTIVATE | AW_CENTER;
            //  FadeEffect = AW_ACTIVATE | AW_BLEND;
            //   BottomToTop = AW_ACTIVATE | AW_VER_NEGATIVE | AW_SLIDE;
            //  TopToBottom = AW_ACTIVATE | AW_VER_POSITIVE | AW_SLIDE;
            //  RightToLeft = AW_ACTIVATE | AW_HOR_NEGATIVE | AW_SLIDE;
            //  LeftToRight = AW_ACTIVATE | AW_HOR_POSITIVE | AW_SLIDE;


            API.AnimateWindow(
                this.Handle,
                2000,
                ConstValues.AW_ACTIVATE |
                ConstValues.AW_VER_POSITIVE |
                ConstValues.AW_SLIDE
                );

            base.OnLoad(e);
        }
    }
}
