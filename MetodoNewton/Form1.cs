using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodoNewton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int numeroPuntos = int.Parse(inputNumPuntos.Text);
            int puntoMover = 262;
            
            for (int i = 0; i < numeroPuntos; i++)
            {
                TextBox puntoX = new TextBox();
                puntoX.Name = "puntoX" + i.ToString();
                puntoX.Size = new Size(66, 22);
                puntoX.Location = new Point(48,puntoMover);

                TextBox puntoFx = new TextBox();
                puntoFx.Name = "puntoFx" + i.ToString();
                puntoFx.Size = new Size(66, 22);
                puntoFx.Location = new Point(125, puntoMover);
                puntoFx.Enabled = true;

                puntoMover += 42;

                this.Controls.Add(puntoX);
                this.Controls.Add(puntoFx);
            }

            /*Creacion del boton para resolver*/
            Button botonResolver = new Button();
            botonResolver.Text = "Resolver";
            botonResolver.Name = "btnResolver";
            botonResolver.Location = new Point(76,puntoMover);
            botonResolver.Size = new Size(72, 19);
            this.Controls.Add(botonResolver);

            // Asociar el evento Click al método manejador
            botonResolver.Click += new EventHandler(botonResolver_Click);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void contenedorPuntos_Enter(object sender, EventArgs e)
        {

        }
        // Método manejador del evento Click para el botón "Resolver"
        private void botonResolver_Click(object sender, EventArgs e)
        {
            // Limpiar el DataGridView antes de configurarlo
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            /*INICIO DE LLENADO DE LAS MATRIZES*/
            double[] matrizPuntosX = new double[int.Parse(inputNumPuntos.Text)];
            double[] matrizPuntosY = new double[int.Parse(inputNumPuntos.Text)];


            // Agregar columnas
            dataGridView1.Columns.Add("x", "x");
            dataGridView1.Columns.Add("f(x)", "f(x)");
            for (int i = 0; i < int.Parse(inputNumPuntos.Text); i++)
            {
                // Buscar el TextBox por su nombre
                TextBox txtPuntos = this.Controls.Find("puntoX" + i.ToString(), true).FirstOrDefault() as TextBox;
                matrizPuntosX[i] = int.Parse(txtPuntos.Text);
                // Buscar el TextBox por su nombre
                TextBox txtPuntos2 = this.Controls.Find("puntoFx" + i.ToString(), true).FirstOrDefault() as TextBox;
                matrizPuntosY[i] = int.Parse(txtPuntos2.Text);

                dataGridView1.Rows.Add(matrizPuntosX[i], matrizPuntosY[i]);
            }
            /*FIN DE LLENADO DE LAS MATRIZES*/

            /*RESOLUCION DE TODAS LAS Y1*/
            int aumento = 0;
            int reducirYveces = 1;
            for (int i = 0; i < int.Parse(inputNumPuntos.Text) - 1; i++) {
                
                dataGridView1.Columns.Add("y"+(i+1), "y" + (i + 1));
                for (int j = 0; j < int.Parse(inputNumPuntos.Text) - reducirYveces; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0) /*con esto vamos agregando el polinomio*/
                        {
                            textPolinomio.Text += matrizPuntosY[0].ToString();
                        }
                        
                        /*Formula para calcular cada Y*/
                        matrizPuntosY[j] = (matrizPuntosY[j + 1] - matrizPuntosY[j]) / (matrizPuntosX[j + 1] - matrizPuntosX[j]);

                        /*ahora llenaremos el datagridview*/
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                                // Verificar si el valor de la fila actual es el que buscamos
                            if (fila.Cells["x"].Value != null && fila.Cells["x"].Value.ToString() == matrizPuntosX[j].ToString())
                            {
                               // agregar la y1
                                fila.Cells["y"+(i+1)].Value = matrizPuntosY[j].ToString();
                                break; // Salir del bucle después de encontrar y actualizar
                            }
                        }
                        if (j == 0) /*con esto vamos agregando el polinomio*/
                        {
                            textPolinomio.Text += " + ("+ matrizPuntosY[0].ToString() + ")*(x - (" + matrizPuntosX[0] +"))";
                        }

                    }
                    else
                    {
                        if(i == 1) /*esto se usa para la formula la cual aqui el objetivo es lograr llegar a la ultima posicion de las x q se usan en la formula*/
                        {          /*por ej si se tiene x0 con este 2 logramos llegar a la x2 para agarrar su dato ahora bien*/
                                   /*si ahora se necesita calcular el otro punto q es x1 para la y2 ahora necesitamos la x3 en la formula con lo cual necesito aumentar 2 posiciones para llegar a la x3 las y*/
                            aumento = 2;
                        }

                        matrizPuntosY[j] = (matrizPuntosY[j + 1] - matrizPuntosY[j])/ (matrizPuntosX[j + aumento] - matrizPuntosX[j]);
                        
                        /*ahora llenaremos el datagridview*/
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            // Verificar si el valor de la fila actual es el que buscamos
                            if (fila.Cells["x"].Value != null && fila.Cells["x"].Value.ToString() == matrizPuntosX[j].ToString())
                            {
                                // agregar la y1
                                fila.Cells["y" + (i + 1)].Value = matrizPuntosY[j].ToString();
                                break; // Salir del bucle después de encontrar y actualizar
                            }
                        }

                        textPolinomio.Text += " + (" + matrizPuntosY[0].ToString() + ")*(x - (" + matrizPuntosX[0] + "))(x -("+ matrizPuntosX[1] + "))";
                    }
                }
                aumento += 1; /*aqui ya se hace un aumento de a 1 dad que despues solo va aumentando de 1 en 1 para llegar por ej si estamos en y3*/
                reducirYveces += 1; /*y estamos en x0 ahora la primera vez queremos llegar al valor de x3 entonces cuando le aumentamo 1 q anteriormente tenia 2 pues sera capaz de llegar a ese valor*/
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
