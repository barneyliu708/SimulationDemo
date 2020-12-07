namespace Simulation.Presentation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startbutton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.arrivalRateTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.numOfCashierTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfSelfCheckoutTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numOfSelfCheckMachineTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.maxIterationTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startbutton
            // 
            this.startbutton.Location = new System.Drawing.Point(45, 296);
            this.startbutton.Name = "startbutton";
            this.startbutton.Size = new System.Drawing.Size(131, 40);
            this.startbutton.TabIndex = 0;
            this.startbutton.Text = "Start";
            this.startbutton.UseVisualStyleBackColor = true;
            this.startbutton.Click += new System.EventHandler(this.startAsyncButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(436, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UpdateArrivalRate_Click);
            // 
            // arrivalRateTextBox
            // 
            this.arrivalRateTextBox.Location = new System.Drawing.Point(236, 372);
            this.arrivalRateTextBox.Name = "arrivalRateTextBox";
            this.arrivalRateTextBox.Size = new System.Drawing.Size(175, 35);
            this.arrivalRateTextBox.TabIndex = 2;
            this.arrivalRateTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 377);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Arrival Rate";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(43, 454);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "+1 Cashier";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.AddOneCashier_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(236, 296);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 40);
            this.button4.TabIndex = 5;
            this.button4.Text = "Speed Up";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SpeedUp_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(422, 296);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "Slow Down";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.SlowDown_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(236, 454);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(131, 40);
            this.button6.TabIndex = 7;
            this.button6.Text = "-1 Cashier";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.CloseOneCashier_Click);
            // 
            // numOfCashierTextBox
            // 
            this.numOfCashierTextBox.Location = new System.Drawing.Point(330, 29);
            this.numOfCashierTextBox.Name = "numOfCashierTextBox";
            this.numOfCashierTextBox.Size = new System.Drawing.Size(175, 35);
            this.numOfCashierTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "# of Cashiers";
            // 
            // numOfSelfCheckoutTextBox
            // 
            this.numOfSelfCheckoutTextBox.Location = new System.Drawing.Point(330, 85);
            this.numOfSelfCheckoutTextBox.Name = "numOfSelfCheckoutTextBox";
            this.numOfSelfCheckoutTextBox.Size = new System.Drawing.Size(175, 35);
            this.numOfSelfCheckoutTextBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 30);
            this.label3.TabIndex = 11;
            this.label3.Text = "# of Self Checkout Area";
            // 
            // numOfSelfCheckMachineTextBox
            // 
            this.numOfSelfCheckMachineTextBox.Location = new System.Drawing.Point(330, 146);
            this.numOfSelfCheckMachineTextBox.Name = "numOfSelfCheckMachineTextBox";
            this.numOfSelfCheckMachineTextBox.Size = new System.Drawing.Size(175, 35);
            this.numOfSelfCheckMachineTextBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 30);
            this.label4.TabIndex = 13;
            this.label4.Text = "# of Machines per Area";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(606, 296);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(131, 40);
            this.stopButton.TabIndex = 14;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 30);
            this.label5.TabIndex = 15;
            this.label5.Text = "Max Iteration";
            // 
            // textBox1
            // 
            this.maxIterationTextBox.Location = new System.Drawing.Point(330, 211);
            this.maxIterationTextBox.Name = "textBox1";
            this.maxIterationTextBox.Size = new System.Drawing.Size(175, 35);
            this.maxIterationTextBox.TabIndex = 16;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 566);
            this.Controls.Add(this.maxIterationTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numOfSelfCheckMachineTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numOfSelfCheckoutTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numOfCashierTextBox);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.arrivalRateTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startbutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox arrivalRateTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox numOfCashierTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numOfSelfCheckoutTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numOfSelfCheckMachineTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox maxIterationTextBox;
    }
}

