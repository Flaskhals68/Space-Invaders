
namespace Kalle_Johansson___Space_Invaders
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bulletMove = new System.Windows.Forms.Timer(this.components);
            this.movement = new System.Windows.Forms.Timer(this.components);
            this.alienMove = new System.Windows.Forms.Timer(this.components);
            this.createLaser = new System.Windows.Forms.Timer(this.components);
            this.laserMove = new System.Windows.Forms.Timer(this.components);
            this.startStageDelay = new System.Windows.Forms.Timer(this.components);
            this.textLabel = new System.Windows.Forms.Label();
            this.menuBackround = new System.Windows.Forms.Panel();
            this.muteTextLabel = new System.Windows.Forms.Label();
            this.createUfo = new System.Windows.Forms.Timer(this.components);
            this.ufoMove = new System.Windows.Forms.Timer(this.components);
            this.menuBackround.SuspendLayout();
            this.SuspendLayout();
            // 
            // bulletMove
            // 
            this.bulletMove.Interval = 30;
            this.bulletMove.Tick += new System.EventHandler(this.bulletMoveTick);
            // 
            // movement
            // 
            this.movement.Interval = 20;
            this.movement.Tick += new System.EventHandler(this.playerMoveTick);
            // 
            // alienMove
            // 
            this.alienMove.Interval = 30;
            this.alienMove.Tick += new System.EventHandler(this.alienMoveTick);
            // 
            // createLaser
            // 
            this.createLaser.Interval = 250;
            this.createLaser.Tick += new System.EventHandler(this.createLaserTick);
            // 
            // laserMove
            // 
            this.laserMove.Interval = 30;
            this.laserMove.Tick += new System.EventHandler(this.laserMoveTick);
            // 
            // startStageDelay
            // 
            this.startStageDelay.Interval = 1000;
            this.startStageDelay.Tick += new System.EventHandler(this.startStageDelayTick);
            // 
            // textLabel
            // 
            this.textLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(88)))), ((int)(((byte)(175)))));
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 52F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel.Location = new System.Drawing.Point(215, 35);
            this.textLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(607, 96);
            this.textLabel.TabIndex = 3;
            this.textLabel.Text = "Space Invaders";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuBackround
            // 
            this.menuBackround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(88)))), ((int)(((byte)(175)))));
            this.menuBackround.Controls.Add(this.muteTextLabel);
            this.menuBackround.Controls.Add(this.textLabel);
            this.menuBackround.Location = new System.Drawing.Point(460, 200);
            this.menuBackround.Name = "menuBackround";
            this.menuBackround.Size = new System.Drawing.Size(1000, 500);
            this.menuBackround.TabIndex = 1;
            // 
            // muteTextLabel
            // 
            this.muteTextLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(88)))), ((int)(((byte)(175)))));
            this.muteTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 52F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.muteTextLabel.Location = new System.Drawing.Point(171, 131);
            this.muteTextLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.muteTextLabel.Name = "muteTextLabel";
            this.muteTextLabel.Size = new System.Drawing.Size(704, 96);
            this.muteTextLabel.TabIndex = 4;
            this.muteTextLabel.Text = "Press \'M\' To Mute";
            this.muteTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createUfo
            // 
            this.createUfo.Interval = 1000;
            this.createUfo.Tick += new System.EventHandler(this.createUfoTick);
            // 
            // ufoMove
            // 
            this.ufoMove.Interval = 30;
            this.ufoMove.Tick += new System.EventHandler(this.ufoMoveTick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1462, 958);
            this.Controls.Add(this.menuBackround);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Invaders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
            this.menuBackround.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer movement;
        private System.Windows.Forms.Timer alienMove;
        private System.Windows.Forms.Timer createLaser;
        private System.Windows.Forms.Timer laserMove;
        private System.Windows.Forms.Timer startStageDelay;
        private System.Windows.Forms.Timer bulletMove;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Panel menuBackround;
        private System.Windows.Forms.Timer createUfo;
        private System.Windows.Forms.Label muteTextLabel;
        private System.Windows.Forms.Timer ufoMove;
    }
}

