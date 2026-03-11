using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.Runtime.InteropServices;

namespace NetSpeedWidget
{
    public partial class Form1 : Form
    {

        // Constantes de Windows
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        public Form1()
        {
            InitializeComponent();

            //no muestro icno en la barra de tareas
            this.ShowInTaskbar = false;

            //Siempre visinle
            this.TopMost = true;

            // Ajuste automático de tamaño
            this.Resize += Form1_Resize;

            // Captura el mouse dentro de WebView2
            pictureBox1.MouseDown += Form1_MouseDown;


            //elimino elementos
            webView21.NavigationCompleted += async (s, e) =>
            {
                await EliminarHostSelect();
            };
        }

        private void PosicionarAbajoDerecha()
        {
            // Asegura que el Form no se muestre centrado
            this.StartPosition = FormStartPosition.Manual;

            // Obtiene el tamaño de la pantalla
            var screen = Screen.PrimaryScreen.WorkingArea;

            // Posiciona el Form abajo a la derecha
            this.Location = new System.Drawing.Point(
                screen.Right - this.Width,   // X: derecha
                screen.Bottom - this.Height  // Y: abajo
            );
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //elimino los logos de antel
            try
            {
                await webView21.EnsureCoreWebView2Async(null);

                // Script que se ejecuta antes de que la página cargue
                string script = @"
            function eliminarHostSelect(){
                document.querySelectorAll('.host-select').forEach(e => e.remove());
            }

            // eliminar si aparece
            const observer = new MutationObserver(() => {
                eliminarHostSelect();
            });

            document.addEventListener('DOMContentLoaded', () => {
                eliminarHostSelect();
                observer.observe(document.documentElement, {
                    childList: true,
                    subtree: true
                });
            });
        ";

                await webView21.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);

                webView21.ZoomFactor = 0.50;
                webView21.Source = new Uri("http://antel.dualstack.speedtestcustom.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error WebView2: " + ex.Message);
            }


            //posisiono el form abajo a la derecha
            PosicionarAbajoDerecha();


            //ajusto el tamaño del control al del form
            try
            {
                // Inicializa WebView2
                await webView21.EnsureCoreWebView2Async(null);

                // Ajusta zoom al 50%
                webView21.ZoomFactor = 0.50;

                // Carga la página
                webView21.Source = new Uri("http://antel.dualstack.speedtestcustom.com/");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error WebView2: " + ex.Message);
            }
        }

        private async System.Threading.Tasks.Task EliminarHostSelect()
        {
            if (webView21.CoreWebView2 != null)
            {
                string script = @"
            var elements = document.querySelectorAll('.host-select');
            elements.forEach(function(el) { el.remove(); });
        ";
                await webView21.ExecuteScriptAsync(script);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Ajusta tamaño del control al Form
            webView21.Width = this.ClientSize.Width;
            webView21.Height = this.ClientSize.Height;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}