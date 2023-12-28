// <copyright file="MainForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using DumbellPlugin.Model;

    /// <summary>
    /// ����.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// ��������� ������ ����������.
        /// </summary>
        private readonly Parameters parameters = new Parameters();

        /// <summary>
        /// ��������� ������ ���������.
        /// </summary>
        private readonly Builder builder = new Builder();

        /// <summary>
        /// �������, ���������� �������� ���������� ����� ��� ������� ����
        /// ���������.
        /// </summary>
        private readonly Dictionary<ParameterType, Dictionary<string, Control>>
            parameterFormElements = new Dictionary<ParameterType, Dictionary<string, Control>>();

        /// <summary>
        /// ���� �� ��������� ��� ��������� �����.
        /// </summary>
        private readonly Color defaultColor = Color.White;

        /// <summary>
        /// ���� ��� ����������� ������ �����.
        /// </summary>
        private readonly Color errorColor =
            Color.FromArgb(255, 192, 192);

        /// <summary>
        /// ������ ������������ textBox.
        /// </summary>
        private readonly string textBox = "textBox";

        /// <summary>
        /// ������ ������������ label.
        /// </summary>
        private readonly string label = "label";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// ���������� ������� �������� �����.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitializeParameterFormElements();
            this.SetTextFormElements();
        }

        /// <summary>
        /// �������������� �������� ���������� ����� ��� ������� ����
        /// ���������.
        /// </summary>
        private void InitializeParameterFormElements()
        {
            this.parameterFormElements.Add(
                ParameterType.LengthHandle,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.LengthHandleTextBox },
                    { this.label, this.LengthHandleLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.DiameterHandle,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.DiameterHandleTextBox },
                    { this.label, this.DiameterHandleLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.WidthFasten,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.WidthFastenTextBox },
                    { this.label, this.WidthFastenLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.DiameterFasten,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.DiameterFastenTextBox },
                    { this.label, this.DiameterFastenLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.AmountDisk,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.AmountDiskTextBox },
                    { this.label, this.AmountDiskLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.OuterDiameterDisk,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.OuterDiameterDiskTextBox },
                    { this.label, this.OuterDiameterDiskLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.InnerDiameterDisk,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.InnerDiameterDiskTextBox },
                    { this.label, this.InnerDiameterDiskLabel },
                });
            this.parameterFormElements.Add(
                ParameterType.WidthDisk,
                new Dictionary<string, Control>
                {
                    { this.textBox, this.WidthDiskTextBox },
                    { this.label, this.WidthDiskLabel },
                });
            this.SetTextFormElements();
        }

        /// <summary>
        /// ���������� ������� ��������� ������ � ��������� ����.
        /// </summary>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var textBoxName = textBox.Name;
                var parameterType = ParameterType.Unknown;

                var parameterTypeStr =
                    textBoxName.Split('_')[1];

                foreach (var item in this.parameterFormElements.Keys)
                {
                    if (item.ToString() == parameterTypeStr)
                    {
                        parameterType = item;
                        break;
                    }
                }

                try
                {
                    this.parameters.AssertParameter(
                        parameterType,
                        this.parameters.ParametersDict[parameterType],
                        Convert.ToDouble(textBox.Text));
                    this.SetTextFormElements();
                    this.parameterFormElements[parameterType][this.textBox].BackColor = this.defaultColor;
                    this.BuildButton.Enabled = true;
                }
                catch (ArgumentException ex)
                {
                    var parameter = this.parameters.ParametersDict[parameterType];
                    var minValue = parameter.MinValue;
                    var maxValue = parameter.MaxValue;
                    var message = $"{ex.Message}\n������� ����� �� {minValue} �� {maxValue}";
                    this.parameterFormElements[parameterType][this.label].Text = message;
                    this.parameterFormElements[parameterType][this.textBox].BackColor = this.errorColor;
                    this.BuildButton.Enabled = false;
                }
                catch (FormatException)
                {
                    // ��������� ������, ���� ��������� �������� �� ����� ���� ������������� � double
                    // ��������:
                    this.parameterFormElements[parameterType][this.label].Text = "������������ ������ �����";
                    this.parameterFormElements[parameterType][this.textBox].BackColor = this.errorColor;
                    this.BuildButton.Enabled = false;
                }
                catch (OverflowException)
                {
                    // ��������� ������, ���� ��������� �������� ������� �� ������� ����������� ��������� double
                    // ��������:
                    this.parameterFormElements[parameterType][this.label].Text = "�������� ��� ����������� ���������";
                    this.parameterFormElements[parameterType][this.textBox].BackColor = this.errorColor;
                    this.BuildButton.Enabled = false;
                }
                catch (Exception)
                {
                    // ��������� ��������� ����������, �� �������� ����
                    // ��������:
                    this.parameterFormElements[parameterType][this.label].Text = "������ �����";
                    this.parameterFormElements[parameterType][this.textBox].BackColor = this.errorColor;
                    this.BuildButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// ������������� ����� � �������� ��������� ���������� ����� ��
        /// ������ ������� ����������.
        /// </summary>
        private void SetTextFormElements()
        {
            foreach (var item
                in this.parameters.ParametersDict)
            {
                var key = item.Key;
                var value = item.Value;

                this.parameterFormElements[key][this.textBox].Text =
                    value.CurrentValue.ToString();
                this.parameterFormElements[key][this.label].Text =
                    $"�� {value.MinValue} �� {value.MaxValue}";
            }
        }

        /// <summary>
        /// ������ ���������.
        /// </summary>
        /// <param name="sender">��������.</param>
        /// <param name="e">��������2.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            this.builder.BuildDetail(this.parameters);
        }
    }
}