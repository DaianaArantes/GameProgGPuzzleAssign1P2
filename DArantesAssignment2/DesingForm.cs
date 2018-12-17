/*
 * Q-Puzzle Design Application
 * Assignment 1
 * Revision history
 * Daiana Arantes, 2018-9-23 : created
 * Daiana Arantes, 2018-9-30 : finished
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DArantesAssignment2
{
    /// <summary>
    /// Class that designs the QPuzzle game
    /// </summary>
    public partial class DesignForm : Form
    {
        private const int LEFT = 250;
        private const int WIDTH = 62;
        private const int HEIGHT = 62;
        private const int TOP = 120;
        private const int VGAP = 1;
        int rows = 0;
        int colunms = 0;
        int x = LEFT;
        int y = TOP;
        PictureBox[,] gridArray;

        public enum ImageType
        {
            None,
            Wall,
            RedDoor,
            BlueDoor,
            YellowDoor,
            GreenDoor,
            RedBox,
            BlueBox,
            YellowBox,
            GreenBox
        }

        ImageType imageType = new ImageType();

        /// <summary>
        /// The defaut constructor of the class.
        /// </summary>
        public DesignForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click event hander of the btnGenerate_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // check input before saving
                if (int.Parse(txtRow.Text) <= 0 || int.Parse(txtColumns.Text) <= 0)
                {
                    MessageBox.Show("No grid generated, please insert number grater than 0");
                }
                else
                {
                    //Clear grid first, than instanciate array
                    ClearGrid();
                    rows = int.Parse(txtRow.Text);
                    colunms = int.Parse(txtColumns.Text);
                    gridArray = new PictureBox[rows, colunms];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Not a valid input, please insert only integers");
            }

            //Creates picture boxes and set properties
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colunms; j++)
                {
                    PictureBox picBox = new PictureBox();
                    picBox.Left = x;
                    picBox.Width = WIDTH;
                    picBox.Height = HEIGHT;
                    picBox.Top = y;
                    picBox.BorderStyle = BorderStyle.Fixed3D;
                    picBox.Click += PicBox_Click;
                    picBox.Name = ((int)ImageType.None).ToString();

                    gridArray[i, j] = picBox;

                    this.Controls.Add(picBox);

                    x += VGAP + WIDTH;
                }
                x = LEFT;
                y += VGAP + HEIGHT;
            }
        }
        /// <summary>
        /// Clear grid before creating a new one
        /// In case user wants to create another
        /// grid without leaving the form
        /// </summary>
        private void ClearGrid()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colunms; j++)
                {
                    this.Controls.Remove(gridArray[i, j]);
                }

            }
            //Reset y to TOP to generate grid on the same place
            y = TOP;
        }

        /// <summary>
        /// Click event hander of the btnGenerate_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            //Attributes image to selected picture box
            switch (imageType)
            {
                case ImageType.None:
                    pictureBox.Image = null;
                    break;
                case ImageType.Wall:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.wall;
                    break;
                case ImageType.RedDoor:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.redDoor;
                    break;
                case ImageType.BlueDoor:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.blueDoor;
                    break;
                case ImageType.YellowDoor:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.yellowDoor;
                    break;
                case ImageType.GreenDoor:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.greenDoor;
                    break;
                case ImageType.RedBox:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.redBox;
                    break;
                case ImageType.BlueBox:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.blueBox;
                    break;
                case ImageType.YellowBox:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.yellowBox;
                    break;
                case ImageType.GreenBox:
                    pictureBox.Image = DArantesAssignment2.Properties.Resources.greenBox;
                    break;
            }
            // Attributes the enumerator value as the picture box name
            pictureBox.Name = ((int)imageType).ToString();
            
        }
        /// <summary>
        /// Click event hander of the btnNone_Click button
        /// Atributtes none image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNone_Click(object sender, EventArgs e)
        {
            imageType = ImageType.None;
        }
        /// <summary>
        /// Click event hander of the btnWall_Click button
        /// Atributtes wall image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWall_Click(object sender, EventArgs e)
        {
            imageType = ImageType.Wall;
        }
        /// <summary>
        /// Click event hander of the btnRedDoor_Click button
        /// Atributtes redDoor image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRedDoor_Click(object sender, EventArgs e)
        {
            imageType = ImageType.RedDoor;
        }
        /// <summary>
        /// Click event hander of the btnBlueDoor_Click button
        /// Atributtes blueDoor image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBlueDoor_Click(object sender, EventArgs e)
        {
            imageType = ImageType.BlueDoor;
        }
        /// <summary>
        /// Click event hander of the btnYellowDoor_Click button
        /// Atributtes yellowDoor image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYellowDoor_Click(object sender, EventArgs e)
        {
            imageType = ImageType.YellowDoor;
        }
        /// <summary>
        /// Click event hander of the btnGreenDoor_Click button
        /// Atributtes greenDoor image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGreenDoor_Click(object sender, EventArgs e)
        {
            imageType = ImageType.GreenDoor;
        }
        /// <summary>
        /// Click event hander of the btnRedBox_Click button
        /// Atributtes redBox image to imageType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRedBox_Click(object sender, EventArgs e)
        {
            imageType = ImageType.RedBox;
        }
        /// <summary>
        /// Click event hander of the btnBlueBox_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBlueBox_Click(object sender, EventArgs e)
        {
            imageType = ImageType.BlueBox;
        }
        /// <summary>
        /// Click event hander of the btnYellowBox_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYellowBox_Click(object sender, EventArgs e)
        {
            imageType = ImageType.YellowBox;
        }
        /// <summary>
        /// Click event hander of the btnGreenBox_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGreenBox_Click(object sender, EventArgs e)
        {
            imageType = ImageType.GreenBox;
        }
        /// <summary>
        /// Click event hander of the closeToolStripMenuItem_Click button
        /// Leaves DesignForm and retoun to ControlPanelForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Click event hander of the saveToolStripMenuItem_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show dialog to save file
            saveFileDialog.ShowDialog();

            StreamWriter streamWriter;

            try
            {
                // Save file name and start writing file
                string fileName = saveFileDialog.FileName;
                streamWriter = new StreamWriter(fileName);
                streamWriter.WriteLine(rows);
                streamWriter.WriteLine(colunms);

                // Write to file position of array
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < colunms; j++)
                    {
                        streamWriter.WriteLine(i);
                        streamWriter.WriteLine(j);
                        streamWriter.WriteLine(gridArray[i,j].Name);
                    }
                }
                
                MessageBox.Show("Grid values saved to file: " + fileName);

                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please insert a file name");
            }
        }
    }
}
