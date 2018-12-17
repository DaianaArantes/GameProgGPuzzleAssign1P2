/*
 * Q-Puzzle Design Application
 * Assignment 2
 * Revision history
 * Daiana Arantes, 2018-10-23 : created
 * Daiana Arantes, 2018-10-30 : finished
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
using static DArantesAssignment2.DesignForm;

namespace DArantesAssignment2
{
    /// <summary>
    /// Class that set QPuzzle to play mode
    /// </summary>
    public partial class PlayForm : Form
    {
        // Declarations
        private const int WIDTH = 62;
        private const int LEFT = WIDTH;
        private const int HEIGHT = 62;
        private const int TOP = HEIGHT;
        private const int VGAP = 1;
        int countMoves = 0;
        Tile selectedTile;
        Tile[,] gridArray;

        public enum ImageColor
        {
            None,
            Red,
            Blue,
            Yellow,
            Green
        }

        /// <summary>
        /// The defaut constructor of the class.
        /// </summary>
        public PlayForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click event hander of the saveToolStripMenuItem_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear grid if another game is selected without the first one is finished
            if (gridArray != null)
            {
                foreach (Tile item in gridArray)
                {
                    this.pnlGrid.Controls.Remove(item);
                }
                gridArray = null;
            }

            try
            {
                string fileName = "";

                // Get file from folders
                openFileDialog1.ShowDialog();

                // Check if file name is not empty
                if (openFileDialog1.FileName != "")
                {
                    fileName = openFileDialog1.FileName;
                }

                StreamReader streamReader = new StreamReader(fileName);

                int row = 0;
                int colunm = 0;

                Tile tile;

                row = Convert.ToInt16(streamReader.ReadLine());
                colunm = Convert.ToInt16(streamReader.ReadLine());

                gridArray = new Tile[row, colunm];

                // Goes through file and reads lines
                while (!streamReader.EndOfStream)
                {
                    int rowPosition = Convert.ToInt16(streamReader.ReadLine());
                    int colunmPosition = Convert.ToInt16(streamReader.ReadLine());
                    ImageType imageType = (ImageType)Convert.ToInt16(streamReader.ReadLine());

                    switch (imageType)
                    {

                        case ImageType.Wall:
                            tile = new Wall();
                            tile.ImageColor = ImageColor.None;
                            break;
                        case ImageType.RedDoor:
                            tile = new Door();
                            tile.ImageColor = ImageColor.Red;
                            break;
                        case ImageType.BlueDoor:
                            tile = new Door();
                            tile.ImageColor = ImageColor.Blue;
                            break;
                        case ImageType.YellowDoor:
                            tile = new Door();
                            tile.ImageColor = ImageColor.Yellow;
                            break;
                        case ImageType.GreenDoor:
                            tile = new Door();
                            tile.ImageColor = ImageColor.Green;
                            break;
                        case ImageType.RedBox:
                            tile = new Box();
                            tile.ImageColor = ImageColor.Red;
                            break;
                        case ImageType.BlueBox:
                            tile = new Box();
                            tile.ImageColor = ImageColor.Blue;
                            break;
                        case ImageType.YellowBox:
                            tile = new Box();
                            tile.ImageColor = ImageColor.Yellow;
                            break;
                        case ImageType.GreenBox:
                            tile = new Box();
                            tile.ImageColor = ImageColor.Green;
                            break;
                        default:
                            continue;
                    }

                    // Defines properties of the tile
                    tile.Row = rowPosition;
                    tile.Colunm = colunmPosition;
                    tile.ImageType = imageType;
                    tile.Left = LEFT + colunmPosition * WIDTH + VGAP;
                    tile.Width = WIDTH;
                    tile.Height = HEIGHT;
                    tile.Top = TOP + rowPosition * HEIGHT + VGAP;
                    tile.BorderStyle = BorderStyle.Fixed3D;
                    tile.Click += PicBox_Click;

                    // Attributes tile to gridArray 
                    gridArray[rowPosition, colunmPosition] = tile;

                    // Attributes image to selected picture box
                    getPicture(tile);

                    // Adds tile to panel grid control
                    this.pnlGrid.Controls.Add(tile);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error with file, please select again", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Enable Buttons
            btnDown.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;
            btnUp.Enabled = true;
        }

        private void getPicture (Tile tile)
        {
            //Attributes image to selected picture box
            switch (tile.ImageType)
            {
                case ImageType.None:
                    tile.Image = null;
                    break;
                case ImageType.Wall:
                    tile.Image = DArantesAssignment2.Properties.Resources.wall;
                    break;
                case ImageType.RedDoor:
                    tile.Image = DArantesAssignment2.Properties.Resources.redDoor;
                    break;
                case ImageType.BlueDoor:
                    tile.Image = DArantesAssignment2.Properties.Resources.blueDoor;
                    break;
                case ImageType.YellowDoor:
                    tile.Image = DArantesAssignment2.Properties.Resources.yellowDoor;
                    break;
                case ImageType.GreenDoor:
                    tile.Image = DArantesAssignment2.Properties.Resources.greenDoor;
                    break;
                case ImageType.RedBox:
                    tile.Image = DArantesAssignment2.Properties.Resources.redBox;
                    break;
                case ImageType.BlueBox:
                    tile.Image = DArantesAssignment2.Properties.Resources.blueBox;
                    break;
                case ImageType.YellowBox:
                    tile.Image = DArantesAssignment2.Properties.Resources.yellowBox;
                    break;
                case ImageType.GreenBox:
                    tile.Image = DArantesAssignment2.Properties.Resources.greenBox;
                    break;
            }
        }
        /// <summary>
        /// Click event hander of the PicBox_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_Click(object sender, EventArgs e)
        {
            if (selectedTile != null)
            {
                selectedTile.BorderStyle = BorderStyle.Fixed3D;
            }

            selectedTile = (Tile)sender;
            ImageType imageType = selectedTile.ImageType;

            if (imageType == ImageType.RedBox ||
                imageType == ImageType.BlueBox ||
                imageType == ImageType.YellowBox ||
                imageType == ImageType.GreenBox)
            {
                selectedTile.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        /// <summary>
        /// Click event hander of the btnLeft_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            // tile must me selected
            if (selectedTile != null)
            {
                // Handles only Box movements
                if (selectedTile is Box)
                {

                    bool canMove;
                    bool hasMoved = false;
                    do
                    {
                        // Verifies if the tile to be moved to is an empty tile
                        if (gridArray[selectedTile.Row, selectedTile.Colunm - 1] == null)
                        {
                            // Moving the box in the array
                            gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                            selectedTile.Colunm--;
                            gridArray[selectedTile.Row, selectedTile.Colunm] = selectedTile;

                            // Move the box on the screen
                            selectedTile.Left -= WIDTH;

                            canMove = true;
                            hasMoved = true;
                        }
                        // Verifies if the tile to be moved to is a door
                        else if (gridArray[selectedTile.Row, selectedTile.Colunm - 1] is Door)
                        {
                            // Verifies if the door is the same color as the box
                            if (selectedTile.ImageColor == gridArray[selectedTile.Row, selectedTile.Colunm - 1].ImageColor)
                            {
                                // makes the box pass through the door and removes it from the screen
                                gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                                this.pnlGrid.Controls.Remove(selectedTile);
                                selectedTile = null;
                                canMove = false;
                                hasMoved = true;
                               
                            }
                            // Door is not the same color as the box, so no movement is done
                            else
                            {
                                canMove = false;
                            }
                        }
                        // Next tile is a wall or another box
                        else
                        {
                            canMove = false;
                        }
                    } while (canMove);

                    if (hasMoved)
                    {
                        // Add to move made to count and display
                        countMoves++;
                        txtNumberOfMoves.Text = countMoves.ToString();
                        ValidateGameStatus();
                    }
                    
                }
                
            }
            else
            {
                MessageBox.Show("Click to select", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Click event hander of the btnDown_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            // tile must me selected
            if (selectedTile != null)
            {
                // Handles only Box movements
                if (selectedTile is Box)
                {

                    bool canMove;
                    bool hasMoved = false;
                    do
                    {
                        // Verifies if the tile to be moved to is an empty tile
                        if (gridArray[selectedTile.Row + 1, selectedTile.Colunm] == null)
                        {
                            // Moving the box in the array
                            gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                            selectedTile.Row++;
                            gridArray[selectedTile.Row, selectedTile.Colunm] = selectedTile;

                            // Move the box on the screen
                            selectedTile.Top += HEIGHT;

                            canMove = true;
                            hasMoved = true;
                        }
                        // Verifies if the tile to be moved to is a door
                        else if (gridArray[selectedTile.Row + 1, selectedTile.Colunm] is Door)
                        {
                            // Verifies if the door is the same color as the box
                            if (selectedTile.ImageColor == gridArray[selectedTile.Row + 1, selectedTile.Colunm].ImageColor)
                            {
                                // makes the box pass through the door and removes it from the screen
                                gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                                this.pnlGrid.Controls.Remove(selectedTile);
                                selectedTile = null;
                                canMove = false;
                                hasMoved = true;
                                
                            }
                            // Door is not the same color as the box, so no movement is done
                            else
                            {
                                canMove = false;
                            }

                        }
                        // Next tile is a wall or another box
                        else
                        {
                            canMove = false;
                        }
                    } while (canMove);

                    if (hasMoved)
                    {
                        // Add to move made to count and display
                        countMoves++;
                        txtNumberOfMoves.Text = countMoves.ToString();
                        ValidateGameStatus();
                    }
                }

            }
            else
            {
                MessageBox.Show("Click to select", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Click event hander of the btnUp_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            // tile must me selected
            if (selectedTile != null)
            {
                // Handles only Box movements
                if (selectedTile is Box)
                {
                    bool canMove;
                    bool hasMoved = false;
                    do
                    {
                        // Verifies if the tile to be moved to is an empty tile
                        if (gridArray[selectedTile.Row - 1, selectedTile.Colunm] == null)
                        {
                            // Moving the box in the array
                            gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                            selectedTile.Row--;
                            gridArray[selectedTile.Row, selectedTile.Colunm] = selectedTile;

                            // Move the box on the screen
                            selectedTile.Top -= HEIGHT;

                            canMove = true;
                            hasMoved = true;
                        }
                        // Verifies if the tile to be moved to is a door
                        else if (gridArray[selectedTile.Row - 1, selectedTile.Colunm] is Door)
                        {
                            // Verifies if the door is the same color as the box
                            if (selectedTile.ImageColor == gridArray[selectedTile.Row - 1, selectedTile.Colunm].ImageColor)
                            {
                                // makes the box pass through the door and removes it from the screen
                                gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                                this.pnlGrid.Controls.Remove(selectedTile);
                                selectedTile = null;
                                canMove = false;
                                hasMoved = true;
                                
                            }
                            // Door is not the same color as the box, so no movement is done
                            else
                            {
                                canMove = false;
                            }
                        }
                        // Next tile is a wall or another box
                        else
                        {
                            canMove = false;
                        }
                    } while (canMove);

                    if (hasMoved)
                    {
                        // Add to move made to count and display
                        countMoves++;
                        txtNumberOfMoves.Text = countMoves.ToString();
                        ValidateGameStatus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Click to select", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Click event hander of the btnRight_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            // tile must me selected
            if (selectedTile != null)
            {
                // Handles only Box movements
                if (selectedTile is Box)
                {
                    bool canMove;
                    bool hasMoved = false;
                    do
                    {
                        // Verifies if the tile to be moved to is an empty tile
                        if (gridArray[selectedTile.Row, selectedTile.Colunm + 1] == null)
                        {
                            // Moving the box in the array
                            gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                            selectedTile.Colunm++;
                            gridArray[selectedTile.Row, selectedTile.Colunm] = selectedTile;

                            // Move the box on the screen
                            selectedTile.Left += WIDTH;

                            canMove = true;
                            hasMoved = true;
                        }
                        // Verifies if the tile to be moved to is a door
                        else if (gridArray[selectedTile.Row, selectedTile.Colunm + 1] is Door)
                        {
                            // Verifies if the door is the same color as the box
                            if (selectedTile.ImageColor == gridArray[selectedTile.Row, selectedTile.Colunm + 1].ImageColor)
                            {
                                // makes the box pass through the door and removes it from the screen
                                gridArray[selectedTile.Row, selectedTile.Colunm] = null;
                                this.pnlGrid.Controls.Remove(selectedTile);
                                selectedTile = null;
                                canMove = false;
                                hasMoved = true;
                                
                            }
                            // Door is not the same color as the box, so no movement is done
                            else
                            {
                                canMove = false;
                            }
                        }
                        // Next tile is a wall or another box
                        else
                        {
                            canMove = false;
                        }
                    } while (canMove);

                    if (hasMoved)
                    {
                        // Add to move made to count and display
                        countMoves++;
                        txtNumberOfMoves.Text = countMoves.ToString();
                        ValidateGameStatus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Click to select", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool IsGameFinished()
        {
            // verifies if there is any box on the grid
            foreach (Tile item in gridArray)
            {
                if (item is Box)
                {
                    return false;
                }
            }

            return true;
        }

        public void ValidateGameStatus()
        {
            // When Game is finished send a message
            if (IsGameFinished())
            {
                MessageBox.Show("Congratulations\nGame Finished!", "QGame", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Removes all items from grid so another game can be played
                foreach (Tile item in gridArray)
                {
                    this.pnlGrid.Controls.Remove(item);
                }

                gridArray = null;
                countMoves = 0;
                txtNumberOfMoves.Text = "";
            }
        }
        /// <summary>
        /// Click event hander of the closeToolStripMenuItem_Click button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Closes form
            this.Close();
        }
    }

    
}
