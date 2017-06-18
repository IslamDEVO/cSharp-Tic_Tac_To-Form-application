using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace Tic_Tac_To
{
    public class TicTacTo : Form
    {
        delegate void del(string s);
        private Container components;
        private FlowLayoutPanel _panel;
        Button[] buttons = new Button[9];
        Font square_font = new Font("Times New Roman", 40);
        public void fun(string s)
        {
            Console.WriteLine(s);
        }
        public TicTacTo()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new Container();
            _panel = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();

            _panel.Location = new System.Drawing.Point(10, 10);
            _panel.Name = "_panel";
            _panel.Size = new System.Drawing.Size(262, 240);
            _panel.TabIndex = 0;

            
            // Add menus
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem File = new ToolStripMenuItem("File");
            ToolStripMenuItem About = new ToolStripMenuItem("About", null, new EventHandler(aboutHandler));
            ToolStripMenuItem NewGame = new ToolStripMenuItem("New", null, new EventHandler(newHandler));
            ToolStripMenuItem Exit = new ToolStripMenuItem("Exit", null, new EventHandler(exitHandler));
            File.DropDownItems.Add(NewGame);
            File.DropDownItems.Add(Exit);
            ms.Items.Add(File);
            ms.Items.Add(About);
            ms.Dock = DockStyle.Top;
            Controls.Add(ms);
            

           AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(284, 262);
            Controls.Add(_panel);
            Name = "TicTacTo";
            Text = "Form1";
           // Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Load += new System.EventHandler(OnLoad);
            ResumeLayout(false);
        }

        //بناء رقعة اللعبة (9 ازرار
        private void OnLoad(object sender, EventArgs e)
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var button = new Button();
                button.BackColor = System.Drawing.Color.DarkCyan;
                button.Location = new System.Drawing.Point(28, 48);
                button.Size = new System.Drawing.Size(75, 75);
                button.Click += square_Click;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Font = square_font;
                buttons[i] = button;
            }
            this._panel.Controls.AddRange(buttons);
        }

        bool isaret = true;
        int isaret_tie = 0;
        //الدالة التى سوف يقوم الحاسب بتنفيذها عند عمل كليك على اى من ازرار اللعبة
        private void square_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (isaret)
            {
                b.Text = "X";
                b.BackColor = Color.Yellow;
            }
            else
            {
                b.Text = "O";
                b.BackColor = Color.Red;
            }
            checkWinner();
            isaret = !isaret;
            b.Enabled = false;
            isaret_tie++;
           
        }

        //التحقق من شرط اللعبة قى الصفوف و الأعمدة و الأقطار
        private bool checkRCD(int A1, int A2, int A3)
        {
            if ((buttons[A1].Text == buttons[A2].Text) &&
                (buttons[A2].Text == buttons[A3].Text) &&
                (!buttons[A1].Enabled))
                return true;
            else
                return false;

        }
        //التحقق من الفائز
        private void checkWinner()
        {
            bool winner = false;

            // Rows
            if (checkRCD(0, 1, 2))
                winner = true;
            if (checkRCD(3, 4, 5))
                winner = true;
            if (checkRCD(6, 7, 8))
                winner = true;

            //Cols
            else if (checkRCD(0, 3, 6))
                winner = true;
            if (checkRCD(1, 4, 7))
                winner = true;
            if (checkRCD(2, 5, 8))
                winner = true;

            //Digonal
            else if (checkRCD(0, 4, 8))
                winner = true;
            if (checkRCD(2, 4, 6))
                winner = true;
            if (winner)
            {
                btnDisable();
                string letter = "";
                if (isaret)
                    letter = "O";
                else
                    letter = "X";
                MessageBox.Show(letter + " is a winner", "Tic Tac Toe");
            }
            else
            {
                if (isaret_tie == 9)
                    MessageBox.Show(" Tie!!!!!!", "Tic Tac Toe");
            }
        }

        //تعطيل الأزرار
        private void btnDisable()
        {
            try
            {
                foreach (Control c in _panel.Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }

        }

        private void aboutHandler(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void newHandler(object sender, EventArgs e)
        {
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].Text = "";
                buttons[i].BackColor = System.Drawing.Color.DarkCyan;
                buttons[i].Enabled = true;
            }
            isaret_tie = 0;
        }

        private void exitHandler(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static void Main()
        {
            Application.Run(new TicTacTo());
        }

    }
}
