using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APD.Parallel
{
    public partial class FrmPrincipalParallel : Form
    {
        Bitmap image = null;
        public FrmPrincipalParallel()
        {
            InitializeComponent();
        }

        public Bitmap CaminhoResources(int i)
        {
            return ((Bitmap)Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\resources\img_" + i + ".png"));
        }

        public void Animacao1()
        {
            int i = 1;
            bool pos = true;
            while (true)
            {
                Bitmap[] vetorBitmap = new Bitmap[7];
                vetorBitmap[i] = CaminhoResources(i);

                gpbImagens.Invoke((Action)delegate
                {
                    image = vetorBitmap[i];
                    pcbImg1.Image = image;
                    Thread.Sleep(50);
                    this.pcbImg1.Refresh();
                });

                if (pos)
                {
                    if (i < 6)
                    {
                        i++;
                    }
                    else
                    {
                        pos = false;
                    }
                }
                else
                {
                    if (i > 1)
                    {
                        i--;
                    }
                    else
                    {
                        pos = true;
                    }
                }
            }
        }
        public void Animacao2()
        {
            int i = 1;
            bool pos = true;
            while (true)
            {
                Bitmap[] vetorBitmap = new Bitmap[7];
                vetorBitmap[i] = CaminhoResources(i);

                gpbImagens.Invoke((Action)delegate
                {
                    image = vetorBitmap[i];
                    pcbImg2.Image = image;
                    Thread.Sleep(50);
                    this.pcbImg2.Refresh();
                });

                if (pos)
                {
                    if (i < 6)
                    {
                        i++;
                    }
                    else
                    {
                        pos = false;
                    }
                }
                else
                {
                    if (i > 1)
                    {
                        i--;
                    }
                    else
                    {
                        pos = true;
                    }
                }
            }
        }
        public void Animacao3()
        {
            int i = 1;
            bool pos = true;
            while (true)
            {
                Bitmap[] vetorBitmap = new Bitmap[7];
                vetorBitmap[i] = CaminhoResources(i);

                gpbImagens.Invoke((Action)delegate
                {
                    image = vetorBitmap[i];
                    pcbImg3.Image = image;
                    Thread.Sleep(50);
                    this.pcbImg3.Refresh();
                });

                if (pos)
                {
                    if (i < 6)
                    {
                        i++;
                    }
                    else
                    {
                        pos = false;
                    }
                }
                else
                {
                    if (i > 1)
                    {
                        i--;
                    }
                    else
                    {
                        pos = true;
                    }
                }
            }
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            pcbImg1.Image = null;
            pcbImg2.Image = null;
            pcbImg3.Image = null;

            System.Threading.Tasks.Parallel.Invoke(
                  new Action(() => { Animacao1(); }),
                  new Action(() => { Animacao2(); }),
                  new Action(() => { Animacao3(); })
          );

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            pcbImg1.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbImg2.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbImg3.SizeMode = PictureBoxSizeMode.StretchImage;

            pcbImg1.BackColor = Color.Transparent;
            pcbImg2.BackColor = Color.Transparent;
            pcbImg3.BackColor = Color.Transparent;
        }
    }
}
