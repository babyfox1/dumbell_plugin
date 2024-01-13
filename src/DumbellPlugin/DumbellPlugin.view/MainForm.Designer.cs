namespace DumbellPlugin.View
{
    partial class MainForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            LengthHandleLabel = new Label();
            LengthHandleTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            DiameterHandleTextBox = new TextBox();
            DiameterHandleLabel = new Label();
            label5 = new Label();
            DiameterFastenTextBox = new TextBox();
            DiameterFastenLabel = new Label();
            label7 = new Label();
            WidthFastenTextBox = new TextBox();
            WidthFastenLabel = new Label();
            label9 = new Label();
            WidthDiskTextBox = new TextBox();
            WidthDiskLabel = new Label();
            label11 = new Label();
            InnerDiameterDiskTextBox = new TextBox();
            InnerDiameterDiskLabel = new Label();
            label13 = new Label();
            OuterDiameterDiskTextBox = new TextBox();
            OuterDiameterDiskLabel = new Label();
            label15 = new Label();
            AmountDiskTextBox = new TextBox();
            AmountDiskLabel = new Label();
            BuildButton = new Button();
            Ladder45DegreeRadioButton = new RadioButton();
            Ladder30DegreeRadioButton = new RadioButton();
            label1 = new Label();
            NormRadioButton = new RadioButton();
            SuspendLayout();
            // 
            // LengthHandleLabel
            // 
            LengthHandleLabel.AutoSize = true;
            LengthHandleLabel.Location = new Point(414, 88);
            LengthHandleLabel.Name = "LengthHandleLabel";
            LengthHandleLabel.Size = new Size(98, 15);
            LengthHandleLabel.TabIndex = 0;
            LengthHandleLabel.Text = "Длина рукоятки:";
            // 
            // LengthHandleTextBox
            // 
            LengthHandleTextBox.Location = new Point(290, 80);
            LengthHandleTextBox.Name = "LengthHandleTextBox";
            LengthHandleTextBox.Size = new Size(100, 23);
            LengthHandleTextBox.TabIndex = 1;
            LengthHandleTextBox.Text = "340";
            LengthHandleTextBox.TextChanged += TextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(115, 88);
            label2.Name = "label2";
            label2.Size = new Size(98, 15);
            label2.TabIndex = 2;
            label2.Text = "Длина рукоятки:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(115, 118);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 5;
            label3.Text = "Диаметр рукоятки:";
            // 
            // DiameterHandleTextBox
            // 
            DiameterHandleTextBox.Location = new Point(290, 110);
            DiameterHandleTextBox.Name = "DiameterHandleTextBox";
            DiameterHandleTextBox.Size = new Size(100, 23);
            DiameterHandleTextBox.TabIndex = 4;
            DiameterHandleTextBox.Text = "25";
            DiameterHandleTextBox.TextChanged += TextBox_TextChanged;
            // 
            // DiameterHandleLabel
            // 
            DiameterHandleLabel.AutoSize = true;
            DiameterHandleLabel.Location = new Point(414, 118);
            DiameterHandleLabel.Name = "DiameterHandleLabel";
            DiameterHandleLabel.Size = new Size(111, 15);
            DiameterHandleLabel.TabIndex = 3;
            DiameterHandleLabel.Text = "Диаметр рукоятки:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(115, 177);
            label5.Name = "label5";
            label5.Size = new Size(120, 15);
            label5.TabIndex = 11;
            label5.Text = "Диаметр крепления:";
            // 
            // DiameterFastenTextBox
            // 
            DiameterFastenTextBox.Location = new Point(290, 169);
            DiameterFastenTextBox.Name = "DiameterFastenTextBox";
            DiameterFastenTextBox.Size = new Size(100, 23);
            DiameterFastenTextBox.TabIndex = 10;
            DiameterFastenTextBox.Text = "60";
            DiameterFastenTextBox.TextChanged += TextBox_TextChanged;
            // 
            // DiameterFastenLabel
            // 
            DiameterFastenLabel.AutoSize = true;
            DiameterFastenLabel.Location = new Point(414, 177);
            DiameterFastenLabel.Name = "DiameterFastenLabel";
            DiameterFastenLabel.Size = new Size(120, 15);
            DiameterFastenLabel.TabIndex = 9;
            DiameterFastenLabel.Text = "Диаметр крепления:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(115, 147);
            label7.Name = "label7";
            label7.Size = new Size(117, 15);
            label7.TabIndex = 8;
            label7.Text = "Ширина крепления:";
            // 
            // WidthFastenTextBox
            // 
            WidthFastenTextBox.Location = new Point(290, 139);
            WidthFastenTextBox.Name = "WidthFastenTextBox";
            WidthFastenTextBox.Size = new Size(100, 23);
            WidthFastenTextBox.TabIndex = 7;
            WidthFastenTextBox.Text = "100";
            WidthFastenTextBox.TextChanged += TextBox_TextChanged;
            // 
            // WidthFastenLabel
            // 
            WidthFastenLabel.AutoSize = true;
            WidthFastenLabel.Location = new Point(414, 147);
            WidthFastenLabel.Name = "WidthFastenLabel";
            WidthFastenLabel.Size = new Size(117, 15);
            WidthFastenLabel.TabIndex = 6;
            WidthFastenLabel.Text = "Ширина крепления:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(115, 295);
            label9.Name = "label9";
            label9.Size = new Size(89, 15);
            label9.TabIndex = 23;
            label9.Text = "Ширина диска:";
            // 
            // WidthDiskTextBox
            // 
            WidthDiskTextBox.Location = new Point(290, 287);
            WidthDiskTextBox.Name = "WidthDiskTextBox";
            WidthDiskTextBox.Size = new Size(100, 23);
            WidthDiskTextBox.TabIndex = 22;
            WidthDiskTextBox.Text = "20";
            WidthDiskTextBox.TextChanged += TextBox_TextChanged;
            // 
            // WidthDiskLabel
            // 
            WidthDiskLabel.AutoSize = true;
            WidthDiskLabel.Location = new Point(414, 295);
            WidthDiskLabel.Name = "WidthDiskLabel";
            WidthDiskLabel.Size = new Size(92, 15);
            WidthDiskLabel.TabIndex = 21;
            WidthDiskLabel.Text = "Ширина диска: ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(115, 265);
            label11.Name = "label11";
            label11.Size = new Size(166, 15);
            label11.TabIndex = 20;
            label11.Text = "Внутренний диаметр дисков:";
            // 
            // InnerDiameterDiskTextBox
            // 
            InnerDiameterDiskTextBox.Location = new Point(290, 257);
            InnerDiameterDiskTextBox.Name = "InnerDiameterDiskTextBox";
            InnerDiameterDiskTextBox.Size = new Size(100, 23);
            InnerDiameterDiskTextBox.TabIndex = 19;
            InnerDiameterDiskTextBox.Text = "50";
            InnerDiameterDiskTextBox.TextChanged += TextBox_TextChanged;
            // 
            // InnerDiameterDiskLabel
            // 
            InnerDiameterDiskLabel.AutoSize = true;
            InnerDiameterDiskLabel.Location = new Point(414, 265);
            InnerDiameterDiskLabel.Name = "InnerDiameterDiskLabel";
            InnerDiameterDiskLabel.Size = new Size(166, 15);
            InnerDiameterDiskLabel.TabIndex = 18;
            InnerDiameterDiskLabel.Text = "Внутренний диаметр дисков:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(115, 236);
            label13.Name = "label13";
            label13.Size = new Size(152, 15);
            label13.TabIndex = 17;
            label13.Text = "Внешний диаметр дисков:";
            // 
            // OuterDiameterDiskTextBox
            // 
            OuterDiameterDiskTextBox.Location = new Point(290, 228);
            OuterDiameterDiskTextBox.Name = "OuterDiameterDiskTextBox";
            OuterDiameterDiskTextBox.Size = new Size(100, 23);
            OuterDiameterDiskTextBox.TabIndex = 16;
            OuterDiameterDiskTextBox.Text = "250";
            OuterDiameterDiskTextBox.TextChanged += TextBox_TextChanged;
            // 
            // OuterDiameterDiskLabel
            // 
            OuterDiameterDiskLabel.AutoSize = true;
            OuterDiameterDiskLabel.Location = new Point(414, 236);
            OuterDiameterDiskLabel.Name = "OuterDiameterDiskLabel";
            OuterDiameterDiskLabel.Size = new Size(152, 15);
            OuterDiameterDiskLabel.TabIndex = 15;
            OuterDiameterDiskLabel.Text = "Внешний диаметр дисков:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(115, 206);
            label15.Name = "label15";
            label15.Size = new Size(116, 15);
            label15.TabIndex = 14;
            label15.Text = "Количество дисков:";
            // 
            // AmountDiskTextBox
            // 
            AmountDiskTextBox.Location = new Point(290, 198);
            AmountDiskTextBox.Name = "AmountDiskTextBox";
            AmountDiskTextBox.Size = new Size(100, 23);
            AmountDiskTextBox.TabIndex = 13;
            AmountDiskTextBox.Text = "3";
            AmountDiskTextBox.TextChanged += TextBox_TextChanged;
            // 
            // AmountDiskLabel
            // 
            AmountDiskLabel.AutoSize = true;
            AmountDiskLabel.Location = new Point(414, 206);
            AmountDiskLabel.Name = "AmountDiskLabel";
            AmountDiskLabel.Size = new Size(116, 15);
            AmountDiskLabel.TabIndex = 12;
            AmountDiskLabel.Text = "Количество дисков:";
            // 
            // BuildButton
            // 
            BuildButton.Location = new Point(290, 316);
            BuildButton.Name = "BuildButton";
            BuildButton.Size = new Size(100, 23);
            BuildButton.TabIndex = 24;
            BuildButton.Text = "Построить";
            BuildButton.UseVisualStyleBackColor = true;
            BuildButton.Click += BuildButton_Click;
            // 
            // Ladder45DegreeRadioButton
            // 
            Ladder45DegreeRadioButton.AutoSize = true;
            Ladder45DegreeRadioButton.Location = new Point(385, 374);
            Ladder45DegreeRadioButton.Name = "Ladder45DegreeRadioButton";
            Ladder45DegreeRadioButton.Size = new Size(89, 19);
            Ladder45DegreeRadioButton.TabIndex = 25;
            Ladder45DegreeRadioButton.TabStop = true;
            Ladder45DegreeRadioButton.Text = "45 градусов";
            Ladder45DegreeRadioButton.UseVisualStyleBackColor = true;
            // 
            // Ladder30DegreeRadioButton
            // 
            Ladder30DegreeRadioButton.AutoSize = true;
            Ladder30DegreeRadioButton.Location = new Point(290, 374);
            Ladder30DegreeRadioButton.Name = "Ladder30DegreeRadioButton";
            Ladder30DegreeRadioButton.Size = new Size(89, 19);
            Ladder30DegreeRadioButton.TabIndex = 26;
            Ladder30DegreeRadioButton.TabStop = true;
            Ladder30DegreeRadioButton.Text = "30 градусов";
            Ladder30DegreeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(115, 376);
            label1.Name = "label1";
            label1.Size = new Size(164, 15);
            label1.TabIndex = 27;
            label1.Text = "Построить диски под углом:";
            // 
            // NormRadioButton
            // 
            NormRadioButton.AutoSize = true;
            NormRadioButton.Location = new Point(477, 374);
            NormRadioButton.Name = "NormRadioButton";
            NormRadioButton.Size = new Size(91, 19);
            NormRadioButton.TabIndex = 28;
            NormRadioButton.TabStop = true;
            NormRadioButton.Text = "Убрать угол";
            NormRadioButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 450);
            Controls.Add(NormRadioButton);
            Controls.Add(label1);
            Controls.Add(Ladder30DegreeRadioButton);
            Controls.Add(Ladder45DegreeRadioButton);
            Controls.Add(BuildButton);
            Controls.Add(label9);
            Controls.Add(WidthDiskTextBox);
            Controls.Add(WidthDiskLabel);
            Controls.Add(label11);
            Controls.Add(InnerDiameterDiskTextBox);
            Controls.Add(InnerDiameterDiskLabel);
            Controls.Add(label13);
            Controls.Add(OuterDiameterDiskTextBox);
            Controls.Add(OuterDiameterDiskLabel);
            Controls.Add(label15);
            Controls.Add(AmountDiskTextBox);
            Controls.Add(AmountDiskLabel);
            Controls.Add(label5);
            Controls.Add(DiameterFastenTextBox);
            Controls.Add(DiameterFastenLabel);
            Controls.Add(label7);
            Controls.Add(WidthFastenTextBox);
            Controls.Add(WidthFastenLabel);
            Controls.Add(label3);
            Controls.Add(DiameterHandleTextBox);
            Controls.Add(DiameterHandleLabel);
            Controls.Add(label2);
            Controls.Add(LengthHandleTextBox);
            Controls.Add(LengthHandleLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "DumbellPlugin";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LengthHandleLabel;
        private TextBox LengthHandleTextBox;
        private Label label2;
        private Label label3;
        private TextBox DiameterHandleTextBox;
        private Label DiameterHandleLabel;
        private Label label5;
        private TextBox DiameterFastenTextBox;
        private Label DiameterFastenLabel;
        private Label label7;
        private TextBox WidthFastenTextBox;
        private Label WidthFastenLabel;
        private Label label9;
        private TextBox WidthDiskTextBox;
        private Label WidthDiskLabel;
        private Label label11;
        private TextBox InnerDiameterDiskTextBox;
        private Label InnerDiameterDiskLabel;
        private Label label13;
        private TextBox OuterDiameterDiskTextBox;
        private Label OuterDiameterDiskLabel;
        private Label label15;
        private TextBox AmountDiskTextBox;
        private Label AmountDiskLabel;
        private Button BuildButton;
        private RadioButton Ladder45DegreeRadioButton;
        private RadioButton Ladder30DegreeRadioButton;
        private Label label1;
        private RadioButton NormRadioButton;
    }
}