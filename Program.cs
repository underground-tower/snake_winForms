using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace snake_big{
    public class Program : Form{
        [STAThread]//set main thread 
       static void Main(){Application.Run(new Program());}
        public class coordinat{public int X, Y;public coordinat(int x, int y){  X = x; Y = y;} }//cooddinat class

       Timer timer = new Timer();//tic-tak
       Random rand = new Random();//srand
        //  size - , size |  , pixel sixe
        int W = 22, H = 22, S = 17;

        List<coordinat> ZZmei = new List<coordinat>();//snake new list(parts of snake)header and body parts
        coordinat apple; // aple class coordinates
        
        int direction = 0; //direction 0up 1right, 2down, 3left
        int apples = 0; // how much apless
        Program() {
            this.Text = "ЗМЕЯ НИКИТЫ"; // lable of for
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false; 
            this.DoubleBuffered = true;             
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.Size = new Size(W * S + 2 * SystemInformation.FrameBorderSize.Height, H * S +
                SystemInformation.CaptionHeight + 2 * SystemInformation.FrameBorderSize.Height); // set form size
            this.Paint += new PaintEventHandler(_Paint); // bind form handler paint graphic
            this.KeyDown += new KeyEventHandler(_KeyDown); // bind form handler keySet funtion  
            timer.Tick += new EventHandler(_Tick); // bind timer
            timer.Interval = 170; // optimal timing or game
            timer.Start();      
            for(int i =3; i>0;i--)ZZmei.Add(new coordinat(W /2, H - i));// set start 3 parts of snake
            apple = new coordinat(rand.Next(1,W), rand.Next(1,H)); // apple place
          }
        void _KeyDown(object sender, KeyEventArgs ABC) { 
            switch (ABC.KeyData){
                case Keys.Up:if (direction != 2)direction = 0;
                    break;
                case Keys.Right:if (direction != 3)direction = 1;
                    break;
                case Keys.Down:if (direction != 0) direction = 2;
                    break;
                case Keys.Left:if (direction != 1)direction = 3;
                    break;}}
        void _Tick(object sender, EventArgs NoUsing){//Go GO snake fuction
            int x = ZZmei[0].X, y = ZZmei[0].Y;// save coordinat of snake
            switch (direction){//the location for drawing snake parts
                case 0: y--;
                    if (y < 0)y = H - 1;break;
                case 1:x++;
                    if (x >= W) x = 0;break;
                case 2:y++;
                    if (y >= H) y = 0; break;
                case 3: x--;
                    if (x < 0)x = W - 1;break;}

            coordinat c = new coordinat(x, y); // new head coordinate
            ZZmei.Insert(0, c); // add new zero element
            if (ZZmei[0].X == apple.X && ZZmei[0].Y == apple.Y) {//head=apple
                apple = new coordinat(rand.Next(W-1), rand.Next(H-1));//new apple position
                apples++;}
            else ZZmei.RemoveAt(ZZmei.Count - 1);//delite old sigment because set head is it new part of snake
           Invalidate(); //calling _Paint func (redrawing)       
        }
        // _Paint all elements
        void _Paint(object sender, PaintEventArgs ABC){
            ABC.Graphics.FillEllipse(Brushes.Purple, new Rectangle(ZZmei[0].X * S, ZZmei[0].Y * S, S, S));
            for (int i = 1; i < ZZmei.Count; i++) ABC.Graphics.FillEllipse(Brushes.Green, new Rectangle(ZZmei[i].X * S, ZZmei[i].Y * S, S, S));
            ABC.Graphics.FillRectangle(Brushes.Red, new Rectangle(apple.X * S, apple.Y * S, S, S));
            string state = "Apples:" + apples.ToString() ;    
            ABC.Graphics.DrawString(state, new Font("Arial", 10, FontStyle.Italic), Brushes.Black, new Point(5, 5));
        }
    }
}//make tg @Gm_hum
