using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Nimble_UI
{
    public partial class Nimble_UI : Form
    {
        private static String BufferStartStr = "";
        private static String BufferCollisionStr = "";
        private static String BufferEndStr = "";
        private static bool obsChecked1 = false;
        private static bool obsChecked2 = false;
        private static bool obsChecked3 = false;
        private static bool obsChecked4 = false;
        private static bool obsChecked5 = false;
        private static bool headercheck = false;
        private static bool footercheck = false;
        private static bool grav_yes = false;   
        private static bool grav_no = true;   
        private static String playerPath = "";
        private static String headerPath = "";
        private static String footerPath = "";
        private static String backgroundPath = "";
        private static String splashPath = "";
        private static String obsPath = "";
        private static int X = 0;
        private static int Y = 0;
        private static byte[] keyPressed = new byte[4] {0, 0, 0, 0};

        public Nimble_UI()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void coordinate_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void field_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void field_panel_MouseMove(object sender, MouseEventArgs e)
        {
            coordinates.Text = "X: " + e.X + " Y: " + e.Y;
            X = e.X; 
            Y = e.Y;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(gravno.Checked)
            {
                gravyes.Checked = false;

                grav_no = true;
                grav_yes = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(gravyes.Checked)
            {
                gravno.Checked = false;

                grav_yes = true;
                grav_no = false;
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            CoordinatesPoint[] obsStartPoisiton = new CoordinatesPoint[5];
            CoordinatesPoint[] obsEndPoisiton = new CoordinatesPoint[5];



            if(obs1ix.TextLength == 0 && obs1iy.TextLength == 0)
            {
                obs1ix.Text = "0";
                obs1iy.Text = "0";
            }
            
            if (obs2ix.TextLength == 0 && obs2iy.TextLength == 0)
            {
                obs2ix.Text = "0";
                obs2iy.Text = "0";
            }

            if (obs3ix.TextLength == 0 && obs3iy.TextLength == 0)
            {
                obs3ix.Text = "0";
                obs3iy.Text = "0";
            }

            if (obs4ix.TextLength == 0 && obs4iy.TextLength == 0)
            {
                obs4ix.Text = "0";
                obs4iy.Text = "0";
            }

            if (obs5ix.TextLength == 0 && obs5iy.TextLength == 0)
            {
                obs5ix.Text = "0";
                obs5iy.Text = "0";
            }



            if (obs1gx.TextLength == 0 && obs1gy.TextLength == 0)
            {
                obs1gx.Text = "0";
                obs1gy.Text = "0";
            }

            if (obs2gx.TextLength == 0 && obs2gy.TextLength == 0)
            {
                obs2gx.Text = "0";
                obs2gy.Text = "0";
            }

            if (obs3gx.TextLength == 0 && obs3gy.TextLength == 0)
            {
                obs3gx.Text = "0";
                obs3gy.Text = "0";
            }

            if (obs4gx.TextLength == 0 && obs4gy.TextLength == 0)
            {
                obs4gx.Text = "0";
                obs4gy.Text = "0";
            }

            if (obs5gx.TextLength == 0 && obs5gy.TextLength == 0)
            {
                obs5gx.Text = "0";
                obs5gy.Text = "0";
            }

            obsStartPoisiton[0] = new CoordinatesPoint(Convert.ToInt32(obs1ix.Text), Convert.ToInt32(obs1iy.Text));
            obsStartPoisiton[1] = new CoordinatesPoint(Convert.ToInt32(obs2ix.Text), Convert.ToInt32(obs2iy.Text));
            obsStartPoisiton[2] = new CoordinatesPoint(Convert.ToInt32(obs3ix.Text), Convert.ToInt32(obs3iy.Text));
            obsStartPoisiton[3] = new CoordinatesPoint(Convert.ToInt32(obs4ix.Text), Convert.ToInt32(obs4iy.Text));
            obsStartPoisiton[4] = new CoordinatesPoint(Convert.ToInt32(obs5ix.Text), Convert.ToInt32(obs5iy.Text));


            obsEndPoisiton[0] = new CoordinatesPoint(Convert.ToInt32(obs1gx.Text), Convert.ToInt32(obs1gy.Text));
            obsEndPoisiton[1] = new CoordinatesPoint(Convert.ToInt32(obs2gx.Text), Convert.ToInt32(obs2gy.Text));
            obsEndPoisiton[2] = new CoordinatesPoint(Convert.ToInt32(obs3gx.Text), Convert.ToInt32(obs3gy.Text));
            obsEndPoisiton[3] = new CoordinatesPoint(Convert.ToInt32(obs4gx.Text), Convert.ToInt32(obs4gy.Text));
            obsEndPoisiton[4] = new CoordinatesPoint(Convert.ToInt32(obs5gx.Text), Convert.ToInt32(obs5gy.Text));

            bool keyPressedChecked = false;
            for (int i = 0; i < keyPressed.Length; i++)
            {
                if (keyPressed[i] == 0)
                {
                    keyPressedChecked = true;
                }
            }

            if (BufferStartStr != "" && BufferCollisionStr != "" && BufferEndStr != "" && fixsec.Text != "" && fixsec != null && obsPath != "" && headerPath != "" && footerPath != "" && backgroundPath != "" && playerPath != "" && splashPath != "" && startx.Text != "" && starty.Text != ""  && startx.Text != null && starty.Text != null && keyPressedChecked == false)
            {
                double fixSecond = Convert.ToDouble(fixsec.Text.ToString());

                Nimble_Core.Game game = new Nimble_Core.Game("Nimble", BufferStartStr, BufferCollisionStr, BufferEndStr, headerPath, footerPath, playerPath, obsPath, splashPath, backgroundPath, obsChecked1, obsChecked2, obsChecked3, obsChecked4, obsChecked5, headercheck, footercheck, fixSecond, Convert.ToInt32(startx.Text), Convert.ToInt32(starty.Text), obsStartPoisiton, obsEndPoisiton, keyPressed, grav_no);
            }
            else
            {
                MessageBox.Show("Please Input the values first");
            }
        }

        private void Nimble_UI_Load(object sender, EventArgs e)
        {

        }

        private void startaud_Click(object sender, EventArgs e)
        {
            bufferStartDialog.ShowDialog();
            BufferStartStr = bufferStartDialog.FileName;
        }

        private void endaud_Click(object sender, EventArgs e)
        {
            bufferEndDialog.ShowDialog();
            BufferEndStr = bufferEndDialog.FileName;
        }

        private void collisionaud_Click(object sender, EventArgs e)
        {
            bufferCollisionDialog.ShowDialog();
            BufferCollisionStr = bufferCollisionDialog.FileName;
        }

        private void obs1check_CheckedChanged(object sender, EventArgs e)
        {
            if (obs1check.Checked)
            {
                obsChecked1 = true;
            }
            else 
            { 
                obsChecked1 = false; 
            }
        }

        private void obs2check_CheckedChanged(object sender, EventArgs e)
        {
            if (obs2check.Checked)
            {
                obsChecked2 = true;
            }
            else
            {
                obsChecked2 = false;
            }
        }

        private void obs3check_CheckedChanged(object sender, EventArgs e)
        {
            if (obs3check.Checked)
            {
                obsChecked3 = true;
            }
            else
            {
                obsChecked3 = false;
            }
        }

        private void obs4check_CheckedChanged(object sender, EventArgs e)
        {
            if (obs4check.Checked)
            {
                obsChecked4 = true;
            }
            else
            {
                obsChecked4 = false;
            }
        }

        private void obs5check_CheckedChanged(object sender, EventArgs e)
        {
            if (obs5check.Checked)
            {
                obsChecked5 = true;
            }
            else
            {
                obsChecked5 = false;
            }
        }

        private void fixseclbl_Click(object sender, EventArgs e)
        {

        }

        private void obsglbl_Click(object sender, EventArgs e)
        {

        }

        private void obsimp_Click(object sender, EventArgs e)
        {
            obsDialog.ShowDialog();
            obsPath = obsDialog.FileName;

            Image image = new Bitmap(obsPath);

            obsPicture.Image = image;
            obsPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void playerimp_Click(object sender, EventArgs e)
        {
            playerDialog.ShowDialog();
            playerPath = playerDialog.FileName;

            Image image = new Bitmap(playerPath);

            playerPicture.Image = image;
            playerPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void headerimp_Click(object sender, EventArgs e)
        {
            headerDialog.ShowDialog();
            headerPath = headerDialog.FileName;

            Image image = new Bitmap(headerPath);

            headerPic.Image = image;
            headerPic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void footerimp_Click(object sender, EventArgs e)
        {
            footerDialog.ShowDialog();
            footerPath = footerDialog.FileName;

            Image image = new Bitmap(footerPath);

            footerPic.Image = image;
            footerPic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void backimp_Click(object sender, EventArgs e)
        {
            backgroundDialog.ShowDialog();
            backgroundPath = backgroundDialog.FileName;

            Image image = new Bitmap(backgroundPath);

            backgroundPic.Image = image;
            backgroundPic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void splashbtn_Click(object sender, EventArgs e)
        {
            splashDialog.ShowDialog();
            splashPath = splashDialog.FileName;
        }

        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void backgroundPic_MouseMove(object sender, MouseEventArgs e)
        {
            coordinates.Text = "X: " + e.X + " Y: " + e.Y;
            X = e.X;
            Y = e.Y;
        }

        private void headerPic_MouseMove(object sender, MouseEventArgs e)
        {
            coordinates.Text = "X: " + e.X + " Y: " + e.Y;
            X = e.X;
            Y = e.Y;
        }

        private void footerPic_MouseMove(object sender, MouseEventArgs e)
        {
            coordinates.Text = "X: " + e.X + " Y: " + e.Y;
            X = e.X;
            Y = e.Y;
        }

        private void upkeybox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (upkeybox.SelectedIndex == 0)
            {
                keyPressed[0] = 38;
            }
            else if (upkeybox.SelectedIndex == 1)
            {
                keyPressed[0] = 119;
            }
        }

        private void downkeybox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downkeybox.SelectedIndex == 0)
            {
                keyPressed[2] = 40;
            }
            else if (downkeybox.SelectedIndex == 1)
            {
                keyPressed[2] = 115;
            }
        }

        private void leftkeybox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (leftkeybox.SelectedIndex == 0)
            {
                keyPressed[3] = 37;
            }
            else if (leftkeybox.SelectedIndex == 1)
            {
                keyPressed[3] = 97;
            }
        }

        private void rightkeybox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rightkeybox.SelectedIndex == 0)
            {
                keyPressed[1] = 39;
            }
            else if (rightkeybox.SelectedIndex == 1)
            {
                keyPressed[1] = 100;
            }
        }

        private void upcheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void headercheck_CheckedChanged(object sender, EventArgs e)
        {
            if (headercheckbox.Checked)
            {
                headercheck = true;
            }
            else
            {
                headercheck = false;
            }
        }

        private void footercheck_CheckedChanged(object sender, EventArgs e)
        {
            if (footercheckbox.Checked)
            {
                footercheck = true;
            }
            else
            {
                footercheck = false;
            }
        }

        private void resetMenu_Click(object sender, EventArgs e)
        {
            startx.Text = String.Empty; 
            starty.Text = String.Empty; 
            
            gravyes.Checked = false;
            gravno.Checked = false;

            upkeybox.SelectedIndex = 0;
            downkeybox.SelectedIndex = 0;
            leftkeybox.SelectedIndex = 0;
            rightkeybox.SelectedIndex = 0;

            obs1check.Checked = false;
            obs2check.Checked = false;
            obs3check.Checked = false;
            obs4check.Checked = false;
            obs5check.Checked = false;

            obs1ix.Text = String.Empty;
            obs1iy.Text = String.Empty;

            obs2ix.Text = String.Empty;
            obs2iy.Text = String.Empty;

            obs3ix.Text = String.Empty;
            obs3iy.Text = String.Empty;

            obs4ix.Text = String.Empty;
            obs4iy.Text = String.Empty;

            obs5ix.Text = String.Empty;
            obs5iy.Text = String.Empty;

            obs1gx.Text = String.Empty;
            obs1gy.Text = String.Empty;

            obs2gx.Text = String.Empty;
            obs2gy.Text = String.Empty;

            obs3gx.Text = String.Empty;
            obs3gy.Text = String.Empty;

            obs4gx.Text = String.Empty;
            obs4gy.Text = String.Empty;

            obs5gx.Text = String.Empty;
            obs5gy.Text = String.Empty;

            fixsec.Text = String.Empty;
            headercheckbox.Checked = false;
            footercheckbox.Checked = false;
        }

        private void deleteMenu_Click(object sender, EventArgs e)
        {
            string databaseFileName = "Game_Score.db";

            if (File.Exists(databaseFileName))
            {
                File.Delete(databaseFileName);                       
            }
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
