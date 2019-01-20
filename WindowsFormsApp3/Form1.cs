using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Napravite igru križić-kružić(iks-oks) korištenjem znanja stečenih na ovoj laboratorijskoj vježbi.
//Omogućiti pokretanje igre, unos imena dvaju igrača, ispis koji igrač je trenutno na potezu, 
//igranje igre s iscrtavanjem križića i kružića na odgovarajućim mjestima te ispis dijaloga s porukom o pobjedi,
//odnosno neriješenom rezultatu kao i praćenje ukupnog rezultata.

namespace WindowsFormsApp3
{
    
    public partial class Form1 : Form
    {
        Boolean[,] odabrano = new Boolean[3,3];
        Boolean[,] odabrano_x = new Boolean[3, 3];
        Boolean[,] odabrano_y = new Boolean[3, 3];
        Boolean zavrsilo = false;
        Random rng = new Random();
        Boolean player1_igra;
        string player1_name;
        string player2_name;
        int player1_wins=0;
        int player2_wins=0;
        public int width=379;
        int height= 372;
        int draw = 0;
        int draws = 0;
        Graphics g;
        Pen pen_blue = new Pen(Color.Blue, 1.5f);
        Pen pen_green = new Pen(Color.Green, 1.5f);
        Pen pen_black = new Pen(Color.Black, 2.5f);
        public Form1()
        {                    
                InitializeComponent();
                g = pictureBox1.CreateGraphics();           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 3; i++)
           {

                for(int j = 0; j < 3; j++)
              {
                   odabrano[i,j] = false;
                   odabrano_x[i, j] = false;
                   odabrano_y[i, j] = false;
                }
           }
            int a = rng.Next(100);
            player1_name = textBox1.Text.ToString();
            player2_name = textBox2.Text.ToString();
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                player1_name = "Player 1";
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                player2_name = "Player 2";
            }
            if (a > 50)
            {
                label7.Text = player1_name;
                player1_igra = true;
            }
            else
            {
                label7.Text = player2_name;
                player1_igra = false;
            }
           Graphics_pb gpb = new Graphics_pb();
           gpb.redraw(g);
           gpb.draw_lines(g, pen_black);   
           label3.Text = player1_name;
           label4.Text = player2_name;
           label5.Text = "Wins: " + player1_wins;
           label6.Text = "Wins: " + player2_wins;
           textBox1.Enabled = false;
           textBox2.Enabled = false;
           label9.Text = "Draws: " + draws;
           zavrsilo = false;
           draw = 0;
        }
        public class Graphics_pb: Form1
        {

           private int r = 100;
           public void draw_lines(Graphics g, Pen p)
           {
               g.DrawLine(p, width/3, 0, width/3, height);
               g.DrawLine(p, width*2 / 3, 0, width *2/ 3, height);
               g.DrawLine(p, 5, height/3,width , height/3);
               g.DrawLine(p, 5 , height *2 /3, width, height*2/3);
            }
           public void draw_circle(Graphics g,Pen p,int x, int y)
            {
                g.DrawEllipse(p, x, y, r, r);
            }
           public void draw_x(Graphics g,Pen p,int x, int y)
            {
                g.DrawLine(p, x, y, x+100,y+100);
                g.DrawLine(p,x+100, y, x, y+100);
            }
           public void redraw(Graphics g)
            {
                g.Clear(Color.White);             
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics_pb gpb = new Graphics_pb();
            int i=0, j=0;
            int mousex = e.X;
            int mousey = e.Y;
            int pos_x=0;
            int pos_y=0;
            if(mousex < width / 3)
            {
                i = 0;
                pos_x = 10;
            }
            else if(mousex < width *2 / 3 && mousex > width / 3)
            {
                i = 1;
                pos_x = width / 3 + 10;
            }
            else if(mousex > width * 2 / 3){
                i = 2;
                pos_x = width * 2 / 3 + 10;
            }
            if (mousey < height / 3)
            {
                j = 0;
                pos_y = 10;
            }
            else if (mousey < height * 2 / 3 && mousey > height / 3)
            {
                j = 1;
                pos_y = height / 3 + 10;
            }
            else if (mousey > height * 2 / 3)
            {
                j = 2;
                pos_y = height * 2 / 3 + 10;
            }
            if (player1_igra == true && odabrano[i,j]==false && zavrsilo == false)
            {
                odabrano_x[i, j] = true;
                label7.Text = player2_name;
                gpb.draw_x(g, pen_blue, pos_x, pos_y);
            }
            if (player1_igra == false && odabrano[i, j] == false && zavrsilo == false)
            {
                odabrano_y[i, j] = true;
                label7.Text = player1_name;
                gpb.draw_circle(g, pen_green, pos_x, pos_y);
            }
            if(odabrano[i, j] == false)
            {
               player1_igra = !player1_igra;
            }
            if (odabrano[i, j] == false)
            {
                draw++;            
            }
            odabrano[i, j] = true;
            pobjeda();
            label5.Text = "Wins: " + player1_wins;
            label6.Text = "Wins: " + player2_wins;
            label9.Text = "Draws: " + draws;
              
        }
        void pobjeda()
        {
            Boolean pobjeda=false;
            Boolean pobjeda2 = false;
            int x = 0;
            int y = 0;
            int xy = 0;
            int yx = 0;
            int x_o = 0;
            int y_o = 0;
            int xy_o = 0;
            int yx_o = 0;
            for (int i=0; i < 3; i++)
            {
                x_o = 0;
                x = 0;
                for (int j=0; j < 3; j++)
                {


                    if (odabrano_y[i, j] == true && i + j == 2)
                    {
                        yx_o++;
                    }
                    if (odabrano_y[i, j] == true && i == j)
                    {
                        xy_o++;
                    }
                    if (odabrano_y[i, j] == true)
                    {
                        x_o++;
                    }
                    if (odabrano_x[i, j] == true && i+j==2)
                    {
                        yx++;
                    }
                    if (odabrano_x[i, j] == true && i == j)
                    {
                        xy++;
                    }
                    if (odabrano_x[i, j] == true)
                    {
                        x++;
                    }                   
                }
                if (x == 3 || xy==3 || yx ==3)
                {
                    pobjeda = true;
                }
                if (x_o == 3 || xy_o == 3 || yx_o == 3)
                {
                    pobjeda2 = true;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                y_o = 0;
                y = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (odabrano_x[i, j] == true)
                    {
                        y++;
                    }
                    if (odabrano_y[i, j] == true)
                    {
                        y_o++;
                    }
                }
                if (y == 3)
                {
                    pobjeda = true;
                }
                if (y_o == 3)
                {
                    pobjeda2 = true;
                }
            }
            if (pobjeda==true && zavrsilo==false)
            {
                zavrsilo = true;
                player1_wins++;
                MessageBox.Show(player1_name +" Wins!!!!");
            }
            if (pobjeda2 == true && zavrsilo == false)
            {
                zavrsilo = true;
                player2_wins++;
                MessageBox.Show(player2_name + " Wins!!!!");
            }
            if (draw == 9 && pobjeda==false && pobjeda2==false)
            {
                draws++;
                MessageBox.Show("Its a Draw!!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            draws = 0;
            player1_wins = 0;
            player2_wins = 0;
            label5.Text = "Wins: " + player1_wins;
            label6.Text = "Wins: " + player2_wins;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            draws = 0;
            player1_wins = 0;
            player2_wins = 0;
            label5.Text = "Wins: " + player1_wins;
            label6.Text = "Wins: " + player2_wins;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }
    }
}
