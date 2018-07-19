using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Negocios;


namespace ContezaAdmin.Atendimento
{
    public partial class frmAssinatura : Form
    {
        public Int64 TB012_id { get; set; }

        Graphics g;
        //define a largura e altura para serem iguais a da tela
        int TelaLargura = Screen.PrimaryScreen.Bounds.Width;
        int TelaAltura = Screen.PrimaryScreen.Bounds.Height;

        public frmAssinatura(Int64 vTB012_id)
        {
            TB012_id = vTB012_id;
            InitializeComponent();
        }

        private void frmAssinatura_Load(object sender, EventArgs e)
        {
            // faça seu cursor como um cursor da mão ao pintar no formulário
            Cursor = Cursors.Hand;
        }

        // crie uma variável booleana chamada mustPaint e tenha um valor False na primeira execução do programa
        bool _mustPaint = false;

        public void MouseEvent_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Faça a variável mustPaint ser true
            _mustPaint = true;
        }

        public void MouseEvent_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Este código é para mover o mouse, ao fazer a variável mustPaint em True

            if (_mustPaint)
            {
                // This Graphics class delivers methods for drawing objects to the display form.
                Graphics graphic = CreateGraphics();
                // especificado por um par de coordenadas (x e y em nosso programa), uma largura (que é 10) e uma altura (5).
                // A classe SolidBrush define um pincel de uma única cor na qual usamos cor verde.
                graphic.FillEllipse(new SolidBrush(Color.Black), e.X, e.Y, 5, 6);
            }
        }

        public void MouseEvent_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Faça a variável mustPaint ser falsa
            _mustPaint = false;
        }

        private void mnuAssinaturaCapturarImagem_Click(object sender, EventArgs e)
        {
            mnuAssinatura.Visible = false;
           

            Bitmap b = new Bitmap(TelaLargura, TelaAltura);
            //copia  a tela no bitmap
            g = Graphics.FromImage(b);
            g.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);
            //atribui a imagem ao picturebox exibindo-a
            picTela.Image = b;
            picTela.Visible = true;
       

            

            _originalImage = LoadBitmapUnlocked(b);
            _croppedImage = _originalImage.Clone() as Bitmap;
            _displayImage = _croppedImage.Clone() as Bitmap;
            if (_displayImage != null) _displayGraphics = Graphics.FromImage(_displayImage);
            this.picTela.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.picTela.Location = new System.Drawing.Point(0, 0);
            //this.picTela.Size = new System.Drawing.Size(TelaLargura, TelaAltura);
            mnuAssinatura.Visible = true;
           // picTela.Image.Save(@"V:\\teste.jpg", picTela.Image.RawFormat);
        }


        private bool _drawing = false;
        private Point _startPoint, _endPoint;

        private Bitmap _displayImage;
        private Graphics _displayGraphics;
        private Bitmap _croppedImage;
        private Bitmap _originalImage;

      

        private void picTela_MouseDown(object sender, MouseEventArgs e)
        {
            _drawing = true;
            _startPoint = e.Location;

            // Draw the area selected.
            DrawSelectionBox(e.Location);
        }

        private void picTela_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_drawing) return;

            // Draw the area selected.
            DrawSelectionBox(e.Location);
        }

        private void picTela_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_drawing) return;
            _drawing = false;

            // Crop.
            // Get the selected area's dimensions.
            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);
            Rectangle sourceRect = new Rectangle(x, y, width, height);
            Rectangle destRect = new Rectangle(0, 0, width, height);

            // Copy that part of the image to a new bitmap.
            _displayImage = new Bitmap(width, height);
            _displayGraphics = Graphics.FromImage(_displayImage);
            _displayGraphics.DrawImage(_croppedImage, destRect, sourceRect, GraphicsUnit.Pixel);

            // Display the new bitmap.
            _croppedImage = _displayImage;
            _displayImage = _croppedImage.Clone() as Bitmap;
            _displayGraphics = Graphics.FromImage(_displayImage);
            picTela.Image = _displayImage;
            picTela.Refresh();
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DrawSelectionBox(Point end_point)
        {
            // Save the end point.
            _endPoint = end_point;
            if (_endPoint.X < 0) _endPoint.X = 0;
            if (_endPoint.X >= _croppedImage.Width) _endPoint.X = _croppedImage.Width - 1;
            if (_endPoint.Y < 0) _endPoint.Y = 0;
            if (_endPoint.Y >= _croppedImage.Height) _endPoint.Y = _croppedImage.Height - 1;

            // Reset the image.
            _displayGraphics.DrawImageUnscaled(_croppedImage, 0, 0);

            // Draw the selection area.
            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);
            _displayGraphics.DrawRectangle(Pens.Red, x, y, width, height);
            picTela.Refresh();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Arquivo = @"C:\\Temp\\" + TB012_id + ".jpg";
            picTela.Image.Save(Arquivo);

            byte[] vetorImagens;
            long tamanhoArquivoImagem;

            try
            {

                FileInfo arqImagem = new FileInfo(Arquivo);
                tamanhoArquivoImagem = arqImagem.Length;
                FileStream fs = new FileStream(Arquivo, FileMode.Open, FileAccess.Read, FileShare.Read);
                vetorImagens = new byte[Convert.ToInt32(tamanhoArquivoImagem)];
                int iBytesRead = fs.Read(vetorImagens, 0, Convert.ToInt32(tamanhoArquivoImagem));
                fs.Close();

                //if(new ContratoNegocios().Contratoincluirassinatura(TB012_id, vetorImagens,ParametrosInterface.objUsuarioLogado.TB011_Id))
                //{
                    TB012_id = 0;
                    MessageBox.Show(MensagensDoSistema._0018, @"Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private Bitmap LoadBitmapUnlocked(Bitmap file_name)
        {
            using (Bitmap bm = new Bitmap(file_name))
            {
                Bitmap new_bitmap = new Bitmap(bm.Width, bm.Height);
                using (Graphics gr = Graphics.FromImage(new_bitmap))
                {
                    gr.DrawImage(bm, 0, 0);
                }
                return new_bitmap;
            }
        }
    }
}
