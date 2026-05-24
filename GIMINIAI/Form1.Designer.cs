namespace GIMINIAI
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
            answerLBL = new Label();
            chatDisplay = new RichTextBox();
            questionTXT = new TextBox();
            sendButton = new Button();
            SuspendLayout();
            // 
            // answerLBL
            // 
            answerLBL.Location = new Point(78, 46);
            answerLBL.Name = "answerLBL";
            answerLBL.RightToLeft = RightToLeft.Yes;
            answerLBL.Size = new Size(297, 86);
            answerLBL.TabIndex = 2;
            answerLBL.Text = "בא ונתחיל שיחה";
            // 
            // chatDisplay
            // 
            chatDisplay.BackColor = Color.PapayaWhip;
            chatDisplay.Font = new Font("Segoe UI", 14F);
            chatDisplay.Location = new Point(54, 34);
            chatDisplay.Name = "chatDisplay";
            chatDisplay.ReadOnly = true;
            chatDisplay.RightToLeft = RightToLeft.Yes;
            chatDisplay.ScrollBars = RichTextBoxScrollBars.Vertical;
            chatDisplay.Size = new Size(684, 590);
            chatDisplay.TabIndex = 3;
            chatDisplay.Text = "";
            chatDisplay.TextChanged += chatDisplay_TextChanged_1;
            // 
            // questionTXT
            // 
            questionTXT.Location = new Point(119, 560);
            questionTXT.Name = "questionTXT";
            questionTXT.RightToLeft = RightToLeft.Yes;
            questionTXT.Size = new Size(403, 27);
            questionTXT.TabIndex = 5;
            // 
            // sendButton
            // 
            sendButton.BackColor = Color.IndianRed;
            sendButton.Location = new Point(586, 549);
            sendButton.Name = "sendButton";
            sendButton.RightToLeft = RightToLeft.Yes;
            sendButton.Size = new Size(137, 46);
            sendButton.TabIndex = 4;
            sendButton.Text = "send";
            sendButton.UseVisualStyleBackColor = false;
            sendButton.Click += sendButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RosyBrown;
            ClientSize = new Size(769, 646);
            Controls.Add(questionTXT);
            Controls.Add(sendButton);
            Controls.Add(chatDisplay);
            Controls.Add(answerLBL);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label answerLBL;
        private RichTextBox chatDisplay;
        private TextBox questionTXT;
        private Button sendButton;
    }
}
