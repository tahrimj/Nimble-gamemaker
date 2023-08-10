using Nimble_UI;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nimble_Core
{
    public class Game
    {
        public Game(String projectName, String BufferStartPath, String BufferCollisionPath, String BufferEndPath, String headerPath, String footerPath, String playerPath, String obstaclePath, String splashPath, String backgroundPath, bool obs1Checked, bool obs2Checked, bool obs3Checked, bool obs4Checked, bool obs5Checked, bool headercheckbox, bool footercheckbox,double fixSecond, int startX, int startY, CoordinatesPoint[] obsStartPoisiton, CoordinatesPoint[] obsEndPoisiton, byte[] keyPressedClock, bool grav_no) 
        {
            var mode = new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);

            RenderWindow window = new RenderWindow(mode, projectName, Styles.Default); // ProjectName

            SFML.Graphics.Color windowColour = new SFML.Graphics.Color(0, 0, 0);
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();

            // Sound
            SoundBuffer bufferstart = new SoundBuffer(BufferStartPath); // BufferStartPath
            Sound startsound = new Sound(bufferstart); 

            SoundBuffer buffercollision = new SoundBuffer(BufferCollisionPath); // BufferCollisionPath
            Sound collisionsound = new Sound(buffercollision);
            

            SoundBuffer bufferend = new SoundBuffer(BufferEndPath);
            Sound endsound = new Sound(bufferend); // BufferEndPath

            SFML.Graphics.Font font1 = new SFML.Graphics.Font(@".\fonts\Arimo-Regular.ttf");


            // Load the game assets
            Texture backgroundTexture = new Texture(backgroundPath); // backgroundPath
            Texture playerTexture = new Texture(playerPath); // playerPath
            Texture footerTexture = new Texture(footerPath); // footerPath
            Texture obsTexture1 = new Texture(obstaclePath); // obstaclePath
            Texture obsTexture2 = new Texture(obstaclePath); // obstaclePath
            Texture obsTexture3 = new Texture(obstaclePath); // obstaclePath
            Texture obsTexture4 = new Texture(obstaclePath); // obstaclePath
            Texture obsTexture5 = new Texture(obstaclePath); // obstaclePath
            Texture headerTexture = new Texture(headerPath); // headerPath
            Texture splashBlack = new Texture(splashPath); // splashPath

            // Create the game objects
            Sprite background = new Sprite(backgroundTexture);
            Sprite player = new Sprite(playerTexture);
            Sprite footer = new Sprite(footerTexture);
            Sprite obs1 = new Sprite(obsTexture1);
            Sprite obs2 = new Sprite(obsTexture2);
            Sprite obs3 = new Sprite(obsTexture3);
            Sprite obs4 = new Sprite(obsTexture4);
            Sprite obs5 = new Sprite(obsTexture5);
            Sprite header = new Sprite(headerTexture);
            Sprite black1 = new Sprite(splashBlack);

            //header height and width
            float header_scale_width = (float)(VideoMode.DesktopMode.Width) / (float)(header.Texture.Size.X);
            float header_scale_height = (float)(VideoMode.DesktopMode.Height) / (float)(header.Texture.Size.Y);

            //footer height and width
            float footer_scale_width = (float)(VideoMode.DesktopMode.Width) / (float)(footer.Texture.Size.X);
            float footer_scale_height = (float)(VideoMode.DesktopMode.Height) / (float)(footer.Texture.Size.Y);

            //header & footer size
            //footer.Scale = new Vector2f(2.2f, 1f);
            footer.Scale = new Vector2f((float)(footer_scale_width), (float)(footer_scale_height * 0.15));
            header.Scale = new Vector2f((float)(header_scale_width), (float)(header_scale_height * 0.15));

            //footer position on bottom of y
            float f_pos_y = (float)VideoMode.DesktopMode.Height - (float)(footer.Texture.Size.Y) + 100f;

            //game entities position
            footer.Position = new Vector2f(0f, f_pos_y);
            player.Position = new Vector2f((float)startX, (float)startY);

            obs1.Position = new Vector2f(obsStartPoisiton[0].x, obsStartPoisiton[0].y);
            obs2.Position = new Vector2f(obsStartPoisiton[1].x, obsStartPoisiton[1].y);
            obs3.Position = new Vector2f(obsStartPoisiton[2].x, obsStartPoisiton[2].y);
            obs4.Position = new Vector2f(obsStartPoisiton[3].x, obsStartPoisiton[3].y);
            obs5.Position = new Vector2f(obsStartPoisiton[4].x, obsStartPoisiton[4].y);


            // Clock Obst for x axis 
            Clock clock = new Clock();
            float duration = 0.15f;

            Clock clock_gravity = new Clock();
            float gravity_value = 9.81f;



            //window close event
            window.Closed += HandleClose;


            // Calculate the progress of the animation based on the elapsed time
            float elapsed = clock.Restart().AsSeconds();
            stopwatch.Start();
            stopwatch1.Start();
            double countSeconds = 0;
            Vector2f currentPosition = player.Position;

            startsound.Play();

            RenderWindow splash_window1 = new RenderWindow(new VideoMode(800, 600), "Splash Screen");

            while (startsound.Status == SoundStatus.Playing)
            {
                // Create a new font object
                string Lose = "Starting the game";

                stopwatch.Restart();
                // Create a new Text object
                SFML.Graphics.Text text = new SFML.Graphics.Text(Lose, font1);

                // Set the font size and color of the text
                text.CharacterSize = 50;
                text.FillColor = SFML.Graphics.Color.Red;

                // Set the position of the text
                text.Position = new Vector2f(100, 100);

                // Draw the text to the window
                splash_window1.Draw(black1);
                splash_window1.Draw(text);
                splash_window1.Display();
            }

            splash_window1.Close();

            while (window.IsOpen)
            {
                if (grav_no == false)
                {
                    float deltaTime_gravity = clock_gravity.Restart().AsSeconds();
                    float gravity_Y = (gravity_value * deltaTime_gravity) + player.Position.Y;
                    player.Position = new Vector2f(player.Position.X, gravity_Y);
                }

                // Handle events
                window.DispatchEvents();

                if ((Keyboard.IsKeyPressed(Keyboard.Key.Left) && keyPressedClock[3] == 37))
                {
                    if (player.Position.X > 0)
                    {
                        player.Position += new Vector2f(-2f, 0);
                    }
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.A) && keyPressedClock[3] == 97)
                {
                    if (player.Position.X > 0)
                    {
                        player.Position += new Vector2f(-2f, 0);
                    }
                }

                if ((Keyboard.IsKeyPressed(Keyboard.Key.Right) && keyPressedClock[1] == 39))
                {
                    if (player.Position.X < (VideoMode.DesktopMode.Width - player.Texture.Size.X))
                    {
                        player.Position += new Vector2f(2f, 0);
                    }
                }
                else if(Keyboard.IsKeyPressed(Keyboard.Key.D) && keyPressedClock[1] == 100)
                {
                    if (player.Position.X < (VideoMode.DesktopMode.Width - player.Texture.Size.X))
                    {
                        player.Position += new Vector2f(2f, 0);
                    }
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && keyPressedClock[0] == 38)
                {
                    if (player.Position.Y > 0)
                    {
                        player.Position += new Vector2f(0,  -2f);
                    }
                }
                else if(Keyboard.IsKeyPressed(Keyboard.Key.W) && keyPressedClock[0] == 119)
                {
                    if (player.Position.Y > 0)
                    {
                        player.Position += new Vector2f(0, -2f);
                    }
                }
                
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && keyPressedClock[2] == 40)
                {
                    if (player.Position.Y < (VideoMode.DesktopMode.Height - player.Texture.Size.Y))
                    {
                        player.Position += new Vector2f(0, 2f);
                    }
                }
                else if(Keyboard.IsKeyPressed(Keyboard.Key.S) && keyPressedClock[2] == 115)
                {
                    if (player.Position.Y < (VideoMode.DesktopMode.Height - player.Texture.Size.Y))
                    {
                        player.Position += new Vector2f(0, 2f);
                    }
                }

                //float progress = Math.Min(1f, elapsed / duration);
                float progress = (float)elapsed / (float)duration;

                // Calculate the progress of the animation based on the elapsed time
                Vector2f startPos1 = obs1.Position;
                Vector2f startPos2 = obs2.Position;
                Vector2f startPos3 = obs3.Position;
                Vector2f startPos4 = obs4.Position;
                Vector2f startPos5 = obs5.Position;

                Vector2f endPos1 = new Vector2f(obsEndPoisiton[0].x, obsEndPoisiton[0].y);
                Vector2f endPos2 = new Vector2f(obsEndPoisiton[1].x, obsEndPoisiton[1].y);
                Vector2f endPos3 = new Vector2f(obsEndPoisiton[2].x, obsEndPoisiton[2].y);
                Vector2f endPos4 = new Vector2f(obsEndPoisiton[3].x, obsEndPoisiton[3].y);
                Vector2f endPos5 = new Vector2f(obsEndPoisiton[4].x, obsEndPoisiton[4].y);


                // Calculate the new position of the object
                Vector2f newPos1 = startPos1 + (endPos1 - startPos1) * progress * (float)fixSecond;
                Vector2f newPos2 = startPos2 + (endPos2 - startPos2) * progress * (float)fixSecond;
                Vector2f newPos3 = startPos3 + (endPos3 - startPos3) * progress * (float)fixSecond;
                Vector2f newPos4 = startPos4 + (endPos4 - startPos4) * progress * (float)fixSecond;
                Vector2f newPos5 = startPos5 + (endPos5 - startPos5) * progress * (float)fixSecond;

                //assigning obstacle new position 
                obs1.Position = newPos1;
                obs2.Position = newPos2;
                obs3.Position = newPos3;
                obs4.Position = newPos4;
                obs5.Position = newPos5;

                // Clear the window
                window.Clear(SFML.Graphics.Color.Black);
                window.Draw(background);
                window.Draw(footer);
                window.Draw(player);

                if (obs1Checked)
                    window.Draw(obs1);
                
                if(obs2Checked)
                    window.Draw(obs2);
                
                if (obs3Checked)
                    window.Draw(obs3);

                if (obs4Checked)
                    window.Draw(obs4);

                if (obs5Checked)
                    window.Draw(obs5);

                window.Draw(header);


                if (newPos1.X < -1000)
                {
                    obs1.Position = new Vector2f(2600f, 500f);
                    //Console.WriteLine("Obs 1: " + obs1.Position);
                }
                if (newPos2.X < -1000)
                {
                    // Move the sprite back to the right side of the screen
                    obs2.Position = new Vector2f(2600f, 500f);

                }
                if (newPos3.X < -1000)
                {
                    // Move the sprite back to the right side of the screen
                    obs3.Position = new Vector2f(2600f, 500f);

                }
                if (newPos4.X < -1000)
                {
                    // Move the sprite back to the right side of the screen
                    obs4.Position = new Vector2f(2600f, 500f);

                }
                if (newPos5.X < -1000)
                {
                    // Move the sprite back to the right side of the screen
                    obs5.Position = new Vector2f(2600f, 500f);

                }

                // Collision detection
                FloatRect obs_rect1 = obs1.GetGlobalBounds();
                FloatRect obs_rect2 = obs2.GetGlobalBounds();
                FloatRect obs_rect3 = obs3.GetGlobalBounds();
                FloatRect obs_rect4 = obs4.GetGlobalBounds();
                FloatRect obs_rect5 = obs5.GetGlobalBounds();
                FloatRect player_rect = player.GetGlobalBounds();
                FloatRect header_rect = header.GetGlobalBounds();
                FloatRect footer_rect = footer.GetGlobalBounds();

                // ********** obstacle 1 collision bounds *************

                float obs_rect_width1 = obs1.Texture.Size.X / 2;
                obs_rect_width1 = (obs_rect_width1 / 2) + obs_rect_width1;
                obs_rect1.Width = obs_rect_width1;
                // Console.WriteLine(obs_rect1.Width + " = "  +  obs1.Texture.Size);

                float obs_rect_height1 = obs1.Texture.Size.Y / 2;
                obs_rect_height1 = (obs_rect_height1 / 2) + obs_rect_height1;
                obs_rect1.Height = obs_rect_height1;

                // Console.WriteLine(obs_rect1.Width);


                //obstacle 2 collision bounds
                float obs_rect_height2 = obs2.Texture.Size.Y / 2;
                obs_rect_height2 = (obs_rect_height2 / 2) + obs_rect_height2;
                obs_rect2.Height = obs_rect_height2;

                float obs_rect_width2 = obs2.Texture.Size.X / 2;
                obs_rect_width2 = (obs_rect_width2 / 2) + obs_rect_width2;
                obs_rect2.Width = obs_rect_width2;


                //obstacle 3 collision bounds
                float obs_rect_height3 = obs3.Texture.Size.Y / 2;
                obs_rect_height3 = (obs_rect_height3 / 2) + obs_rect_height3;
                obs_rect3.Height = obs_rect_height3;

                float obs_rect_width3 = obs3.Texture.Size.X / 2;
                obs_rect_width3 = (obs_rect_width3 / 2) + obs_rect_width3;
                obs_rect3.Width = obs_rect_width3;

                //obstacle 4 collision bounds
                float obs_rect_height4 = obs4.Texture.Size.Y / 2;
                obs_rect_height4 = (obs_rect_height4 / 2) + obs_rect_height4;
                obs_rect4.Height = obs_rect_height4;

                float obs_rect_width4 = obs4.Texture.Size.X / 2;
                obs_rect_width4 = (obs_rect_width4 / 2) + obs_rect_width4;
                obs_rect4.Width = obs_rect_width4;

                //obstacle 5 collision bounds
                float obs_rect_height5 = obs5.Texture.Size.Y / 2;
                obs_rect_height5 = (obs_rect_height5 / 2) + obs_rect_height5;
                obs_rect5.Height = obs_rect_height5;

                float obs_rect_width5 = obs5.Texture.Size.X / 2;
                obs_rect_width5 = (obs_rect_width5 / 2) + obs_rect_width5;
                obs_rect5.Width = obs_rect_width5;


                // ********** player collision bounds **********
                float player_rect_height = (player.Texture.Size.Y / 2);
                player_rect_height = (player_rect_height / 2) + player_rect_height;
                player_rect.Height = player_rect_height;


                float player_rect_width = (player.Texture.Size.X / 2);
                player_rect_width = (player_rect_width / 2) + player_rect_width;
                player_rect.Width = player_rect_width;


                // header collision
                float header_rect_width = (header.Texture.Size.X / 2);
                header_rect_width = (header_rect_width / 2) + header_rect_width;
                header_rect.Width = header_rect_width;

                float header_rect_height = (header.Texture.Size.Y / 2);
                header_rect_height = (header_rect_height / 2) + header_rect_height;
                header_rect.Height = header_rect_height;

                // footer collisiom
                float footer_rect_width = (footer.Texture.Size.X / 2);
                footer_rect_width = (footer_rect_width / 2) + footer_rect_width;
                footer_rect.Width = footer_rect_width;

                float footer_rect_height = (footer.Texture.Size.Y / 2);
                footer_rect_height = (footer_rect_height / 2) + footer_rect_height;
                footer_rect.Height = footer_rect_height;


                if ((obs_rect1.Intersects(player_rect) && obs1Checked) || 
                    (obs_rect2.Intersects(player_rect) && obs2Checked) || 
                    (obs_rect3.Intersects(player_rect) && obs3Checked) || 
                    (obs_rect4.Intersects(player_rect) && obs4Checked) || 
                    (obs_rect5.Intersects(player_rect) && obs5Checked) ||
                    (header_rect.Intersects(player_rect) && headercheckbox) ||
                    (footer_rect.Intersects(player_rect) && footercheckbox))
                {

                    RenderWindow splash_window = new RenderWindow(new VideoMode(800, 600), "Splash Screen");

                    collisionsound.Play();
                    if (splash_window.IsOpen)
                    {
                        Database_Sqlite database_Sqlite = new Database_Sqlite();

                        database_Sqlite.CreateConnection("Game_Score");
                        database_Sqlite.CreateTable();

                        List<long> read_score = database_Sqlite.ReadData();

                        if (read_score.Count > 0)
                        {
                            if (countSeconds > read_score[1])
                            {
                                database_Sqlite.UpdateData(Convert.ToInt32(read_score[0]), Convert.ToInt32(countSeconds));
                            }
                        }
                        else
                        {
                            database_Sqlite.InsertData(Convert.ToInt32(countSeconds));
                        }

                        read_score = database_Sqlite.ReadData();

                        // Create a new font object
                        SFML.Graphics.Font font = new SFML.Graphics.Font(@".\fonts\Arimo-Regular.ttf");
                        string Lose = "     Game Over! \n Press 'Space' To Restart.. \n Press 'Esc' to close all window \n " + "Current Score : "  + countSeconds + " \n Highest Score : " + read_score[1];
               

                        stopwatch.Restart();

                        // Create a new Text object
                        SFML.Graphics.Text text = new SFML.Graphics.Text(Lose, font);

                        // Set the font size and color of the text
                        text.CharacterSize = 50;
                        text.FillColor = SFML.Graphics.Color.Red;

                        // Set the position of the text
                        text.Position = new Vector2f(100, 100);

                        // Draw the text to the window
                        splash_window.Draw(black1);
                        splash_window.Draw(text);
                        splash_window.Display();

                        while (true)
                        {
                            // create window
                            // splash_window.Clear(Color.Black);

                            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                            {
                                splash_window.Close();

                                footer.Position = new Vector2f(0f, f_pos_y);
                                player.Position = new Vector2f(1f, 300f);


                                obs1.Position = new Vector2f(obsStartPoisiton[0].x, obsStartPoisiton[0].y);
                                obs2.Position = new Vector2f(obsStartPoisiton[1].x, obsStartPoisiton[1].y);
                                obs3.Position = new Vector2f(obsStartPoisiton[2].x, obsStartPoisiton[2].y);
                                obs4.Position = new Vector2f(obsStartPoisiton[3].x, obsStartPoisiton[3].y);
                                obs5.Position = new Vector2f(obsStartPoisiton[4].x, obsStartPoisiton[4].y);


                                break;
                            }
                            else if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                            {
                                endsound.Play();

                                while (true)
                                {
                                    if (endsound.Status != SoundStatus.Playing)
                                    {
                                        break;
                                    }
                                }
                                //window close event
                                splash_window.Close();
                                window.Close();
                                Application.Exit();
                            }
                        }
                    }

                }

                if (stopwatch.IsRunning)
                {
                    countSeconds = stopwatch.Elapsed.Seconds;
                }

                // Display the window
                window.Display();
            }

            void HandleClose(object sender, EventArgs e)
            {
                window.Close();
            }
        }






        public static void SFML_Sound(Sound startsound, Stopwatch stopwatch, SFML.Graphics.Font font1, RenderWindow splash_window1, Sprite black1)
        {
            while (startsound.Status == SoundStatus.Playing)
            {
                // Create a new font object
                string Lose = "Starting the game";

                stopwatch.Restart();
                // Create a new Text object
                SFML.Graphics.Text text = new SFML.Graphics.Text(Lose, font1);


                // Set the font size and color of the text
                text.CharacterSize = 50;
                text.FillColor = SFML.Graphics.Color.Red;

                // Set the position of the text
                text.Position = new Vector2f(100, 100);

                // Draw the text to the window
                splash_window1.Draw(black1);
                splash_window1.Draw(text);
                splash_window1.Display();
            }
        }
    }
}
