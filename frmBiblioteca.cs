using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryGordillo3
{
    public partial class frmBiblioteca : Form
    {
        public frmBiblioteca()
        {
            InitializeComponent();
        }

        public Datos[,] matLibros = new Datos[21, 5];
        public int contador = 0;

        public struct Datos
        {
            public int Codigo;
            public string NombreLibro;
            public string NombreEditorial;
            public int CodigoAutor;
            public string NombreDisitribuidor;
        }

        private void frmBiblioteca_Load(object sender, EventArgs e)
        {
            char separador = Convert.ToChar(",");
            int i = 0;

            StreamReader srLibros = new StreamReader("./LIBRO.txt");

            while (!srLibros.EndOfStream && i < 21)
            {
                string[] vecLibro = srLibros.ReadLine().Split(separador);
                //Borramos los espacios en blanco
                for (int indice = 0; indice < vecLibro.Length; indice++)
                {
                    vecLibro[indice] = Regex.Replace(vecLibro[indice], @"\t", "");
                }

                matLibros[i, 0].Codigo = Convert.ToInt32(vecLibro[0]);
                matLibros[i, 1].NombreLibro = vecLibro[1];
                matLibros[i, 2].NombreEditorial = vecLibro[2];
                matLibros[i, 3].CodigoAutor = Convert.ToInt32(vecLibro[3]);
                matLibros[i, 4].NombreDisitribuidor = vecLibro[4];
                //Asignamos los nombres de la editorial por nombre
                StreamReader srEditorial = new StreamReader("./EDITORIAL.txt");

                while (!srEditorial.EndOfStream)
                {
                    string[] vecEditorial = srEditorial.ReadLine().Split(separador);
                    //Borramos los espacio en blanco
                    for (int indice = 0; indice < vecEditorial.Length; indice++)
                    {
                        //Reemplazamos una expresion por una cadena 
                        vecEditorial[indice] = Regex.Replace(vecEditorial[indice], @"\t", "");
                    }
                    //Asignamos nombres
                    if (vecEditorial[0] == matLibros[i, 2].NombreEditorial)
                    {
                        matLibros[i, 2].NombreEditorial = vecEditorial[1];
                    }
                }

                srEditorial.Close();

                StreamReader srDistribuidora = new StreamReader("./DISTRIBUIDORA.txt");

                while (!srDistribuidora.EndOfStream)
                {
                    string[] vecDistribuidora = srDistribuidora.ReadLine().Split(separador);
                    //Borramos espacio en blanco
                    for (int indice = 0; indice < vecDistribuidora.Length; indice++)
                    {
                        //estamos remplazando una expresion por una cadena 
                        vecDistribuidora[indice] = Regex.Replace(vecDistribuidora[indice], @"\t", "");
                    }
                    //Asignamos nombre 
                    if (vecDistribuidora[0] == matLibros[i, 4].NombreDisitribuidor)
                    {
                        matLibros[i, 4].NombreDisitribuidor = vecDistribuidora[1];
                    }
                }

                srDistribuidora.Close();

                i++;
            }

            srLibros.Close();

            txtCodigo.Text = Convert.ToString(matLibros[0, 0].Codigo);
            txtNombreLibro.Text = matLibros[0, 1].NombreLibro;
            txtNombreEdit.Text = matLibros[0, 2].NombreEditorial;
            txtCodigoAutor.Text = Convert.ToString(matLibros[0, 3].CodigoAutor);
            txtNombreDistribuidor.Text = matLibros[0, 4].NombreDisitribuidor;

            cmdAtras.Enabled = false;
        }

        private void cmdAdelante_Click(object sender, EventArgs e)
        {
            contador++;

            txtCodigo.Text = Convert.ToString(matLibros[contador, 0].Codigo);
            txtNombreLibro.Text = matLibros[contador, 1].NombreLibro;
            txtNombreEdit.Text = matLibros[contador, 2].NombreEditorial;
            txtCodigoAutor.Text = Convert.ToString(matLibros[contador, 3].CodigoAutor);
            txtNombreDistribuidor.Text = matLibros[contador, 4].NombreDisitribuidor;

            cmdAdelante.Enabled = true;
            cmdAtras.Enabled = true;

            if (contador == matLibros.GetLength(0) - 1)
            {
                cmdAdelante.Enabled = false;
            }
            else
            {
                cmdAdelante.Enabled = true;
            }
        }

        private void cmdAtras_Click(object sender, EventArgs e)
        {
            contador--;

            if (contador >= 0)
            {
                txtCodigo.Text = Convert.ToString(matLibros[contador, 0].Codigo);
                txtNombreLibro.Text = matLibros[contador, 1].NombreLibro;
                txtNombreEdit.Text = matLibros[contador, 2].NombreEditorial;
                txtCodigoAutor.Text = Convert.ToString(matLibros[contador, 3].CodigoAutor);
                txtNombreDistribuidor.Text = matLibros[contador, 4].NombreDisitribuidor;
            }
            else
            {
                cmdAtras.Enabled = false;
            }

            if (contador == 0)
            {
                cmdAtras.Enabled = false;
            }
            else
            {
                cmdAdelante.Enabled = true;
            }
        }
    }
}

