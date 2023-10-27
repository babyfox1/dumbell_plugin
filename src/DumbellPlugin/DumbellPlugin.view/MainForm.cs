namespace DumbellPlugin.view
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int parsedValue;

            if (!int.TryParse(textBox1.Text, out parsedValue) || parsedValue > 400 || parsedValue < 320)
            {
                textBox1.BackColor = Color.Pink;
                label17.Text = "Ошибка! Длина рукоятки.";
                label17.BackColor = Color.Pink;
            }
            else
            {
                textBox1.BackColor = SystemColors.Window; // Восстановить стандартный цвет фона
                label17.Text = ""; // Очистить текст в label17
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}