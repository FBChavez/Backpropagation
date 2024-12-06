using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backprop;

namespace Backpropagation_Activity
{
    public partial class Form1 : Form
    {
        NeuralNet nn;
        int trainCounter = 0; // Epoch for OR Gate

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;  // Disable the maximize button
            this.MinimizeBox = false;  // Disable the minimize button

            panel1.Visible = false; // AND GATE
            panel2.Visible = true; // OR GATE
        }
        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inputOrGateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        // Note: I swapped the panels accidentally and couldn't be bothered swapping them again.
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nn = new NeuralNet(2, 100, 1);
            label6.Text = "Created BPNN";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nn == null)
            {
                label7.Text = "Create a BPNN first";
            }
            else
            {
                trainCounter++;

                for (int x = 0; x < 100; x++)
                {
                    nn.setInputs(0, 0.0);
                    nn.setInputs(1, 0.0);
                    nn.setDesiredOutput(0, 0.0);
                    nn.learn();

                    nn.setInputs(0, 0.0);
                    nn.setInputs(1, 1.0);
                    nn.setDesiredOutput(0, 1.0);
                    nn.learn();

                    nn.setInputs(0, 1.0);
                    nn.setInputs(1, 0.0);
                    nn.setDesiredOutput(0, 1.0);
                    nn.learn();

                    nn.setInputs(0, 1.0);
                    nn.setInputs(1, 1.0);
                    nn.setDesiredOutput(0, 1.0);
                    nn.learn();
                }

                label7.Text = $"Train Counter: {trainCounter}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nn == null)
            {
                label8.Text = "Create a BPNN first";
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text) || (textBox1.Text != "0" && textBox1.Text != "1") ||
            string.IsNullOrWhiteSpace(textBox2.Text) || (textBox2.Text != "0" && textBox2.Text != "1"))
            {
                label8.Text = "Incorrect inputs!";
            }
            else
            {
                nn.setInputs(0, Convert.ToDouble(textBox1.Text));
                nn.setInputs(1, Convert.ToDouble(textBox2.Text));
                nn.run();

                textBox3.Text = "" + nn.getOuputData(0);

                label8.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        NeuralNet nn2;
        int trainCounter2 = 0; // Epoch for AND Gate
        int hNeuron = 100; // Hidden Neurons

        double[,] andData = new double[16, 5]
        {
            {0.0, 0.0, 0.0, 0.0, 0.0},
            {0.0, 0.0, 0.0, 1.0, 0.0},
            {0.0, 0.0, 1.0, 0.0, 0.0},
            {0.0, 0.0, 1.0, 1.0, 0.0},
            {0.0, 1.0, 0.0, 0.0, 0.0},
            {0.0, 1.0, 1.0, 1.0, 0.0},
            {0.0, 1.0, 1.0, 0.0, 0.0},
            {0.0, 1.0, 0.0, 1.0, 0.0},
            {1.0, 0.0, 1.0, 0.0, 0.0},
            {1.0, 0.0, 1.0, 1.0, 0.0},
            {1.0, 0.0, 0.0, 0.0, 0.0},
            {1.0, 0.0, 1.0, 1.0, 0.0},
            {1.0, 1.0, 1.0, 0.0, 0.0},
            {1.0, 1.0, 0.0, 1.0, 0.0},
            {1.0, 1.0, 1.0, 0.0, 0.0},
            {1.0, 1.0, 1.0, 1.0, 1.0}
        };

        private bool fitTest()
        {
            if (nn2 == null)
                return false;

            for (int i = 0; i < 16; i++)
            {
                nn2.setInputs(0, andData[i, 0]);
                nn2.setInputs(1, andData[i, 1]);
                nn2.setInputs(2, andData[i, 2]);
                nn2.setInputs(3, andData[i, 3]);
                nn2.run();

                //if (Math.Round(nn2.getOuputData(0)) != andData[i, 4])
                //    return false;

                double output = nn2.getOuputData(0);

                if (andData[i, 4] == 1)
                {
                    if (output < 1 - 0.2 || output > 1)
                        return false;
                }
                else if (andData[i, 4] == 0)
                {
                    if (output > 0.2 || output < 0)
                        return false;
                }
            }

            return true;
        }

        bool checkInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || (textBox4.Text != "0" && textBox4.Text != "1") ||
            string.IsNullOrWhiteSpace(textBox5.Text) || (textBox5.Text != "0" && textBox5.Text != "1") ||
            string.IsNullOrWhiteSpace(textBox6.Text) || (textBox6.Text != "0" && textBox6.Text != "1") ||
            string.IsNullOrWhiteSpace(textBox7.Text) || (textBox7.Text != "0" && textBox7.Text != "1"))
            {
                return false;
            }
            return true; 
        }

        private void train()
        {
            if (nn2 == null)
                return;

            for (int i = 0; i < 16; i++)
            {
                nn2.setInputs(0, andData[i, 0]);
                nn2.setInputs(1, andData[i, 1]);
                nn2.setInputs(2, andData[i, 2]);
                nn2.setInputs(3, andData[i, 3]);
                nn2.setDesiredOutput(0, andData[i, 4]);
                nn2.learn();
            }
        }

        private void trainUntilFit()
        {
            textBox10.Text = "0";
            trainCounter2 = 0;

            while (!fitTest())
            {
                train();
                trainCounter2++;
                textBox10.Text = trainCounter2.ToString();
            }
        }

        private void inputAndGateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            nn2 = new NeuralNet(4, hNeuron, 1);
            textBox9.Text = hNeuron.ToString();
            label17.Text = "Message: Created BPNN";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (nn2 == null)
            {
                label17.Text = "Message: Create a BPNN first";
                return;
            }
            else
            {
                trainCounter2++;
                for (int i = 0; i < 16; i++)
                {
                    nn2.setInputs(0, andData[i, 0]);
                    nn2.setInputs(1, andData[i, 1]);
                    nn2.setInputs(2, andData[i, 2]);
                    nn2.setInputs(3, andData[i, 3]);
                    nn2.setDesiredOutput(0, andData[i, 4]);
                    nn2.learn();
                }
                label18.Text = $"Counter: {trainCounter2}";
                textBox10.Text = "" + trainCounter2;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (nn2 == null)
            {
                label17.Text = "Message: Create a BPNN first";
            }
            else if (!checkInputs())
            {
                label17.Text = "Message: Incorrect inputs!";
            }
            else
            {
                nn2.setInputs(0, Convert.ToDouble(textBox4.Text));
                nn2.setInputs(1, Convert.ToDouble(textBox5.Text));
                nn2.setInputs(1, Convert.ToDouble(textBox6.Text));
                nn2.setInputs(1, Convert.ToDouble(textBox7.Text));
                nn2.run();

                textBox8.Text = "" + nn2.getOuputData(0);
                label17.Text = "Message:";
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 100; i++) // starts at HN = 1
            {
                textBox9.Text = i.ToString();

                nn2 = new NeuralNet(4, i, 1);

                trainUntilFit();

                if (fitTest())
                {
                    label17.Text = $"Message: Training successful with {i} hidden neurons";
                    break;
                }
                else
                {
                    label17.Text = $"Failed with {i} hidden neurons, trying next.";
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int minimumEpoch = int.MaxValue;
            int attempts = 0;
            int maxAttempts = 100;

            for (int i = 1; i <= 100; i++)
            {
                textBox9.Text = i.ToString();

                nn2 = new NeuralNet(4, i, 1);

                trainUntilFit();

                if (trainCounter2 < minimumEpoch)
                {
                    minimumEpoch = trainCounter2;
                    attempts = 0;
                }
                else
                {
                    attempts++;

                    if (attempts >= maxAttempts)
                    {
                        label17.Text = $"Stopped after {maxAttempts} attempts without improvement.";
                        break;
                    }
                }
            }

            label17.Text = $"Minimum Epochs: {minimumEpoch}";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void label16_Click(object sender, EventArgs e)
        {

        }

        // Message Label
        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}
