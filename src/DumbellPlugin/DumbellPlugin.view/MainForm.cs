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
    using DumbellPlugin.Wrapper;

    /// <summary>
    /// ����.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// ��������� ������ ����������.
        /// </summary>
        private readonly Parameters _parameters = new Parameters();

        /// <summary>
        /// ��������� ������ ���������.
        /// </summary>
        private readonly Builder _builder = new Builder();

        /// <summary>
        /// �������, ���������� �������� ���������� ����� ��� ������� ����
        /// ���������.
        /// </summary>
        private readonly Dictionary<ParameterType, Dictionary<string, Control>>
            _parameterFormElements = new Dictionary<ParameterType, Dictionary<string, Control>>();

        /// <summary>
        /// ���� �� ��������� ��� ��������� �����.
        /// </summary>
        private readonly Color _defaultColor = Color.White;

        /// <summary>
        /// ���� ��� ����������� ������ �����.
        /// </summary>
        private readonly Color _errorColor =
            Color.FromArgb(255, 192, 192);

        /// <summary>
        /// ������ ������������ _textBox.
        /// </summary>
        private readonly string _textBox = "textBox";

        /// <summary>
        /// ������ ������������ _label.
        /// </summary>
        private readonly string _label = "label";

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
            this._parameterFormElements.Add(
                ParameterType.LengthHandle,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.LengthHandleTextBox },
                    { this._label, this.LengthHandleLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.DiameterHandle,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.DiameterHandleTextBox },
                    { this._label, this.DiameterHandleLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.WidthFasten,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.WidthFastenTextBox },
                    { this._label, this.WidthFastenLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.DiameterFasten,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.DiameterFastenTextBox },
                    { this._label, this.DiameterFastenLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.AmountDisk,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.AmountDiskTextBox },
                    { this._label, this.AmountDiskLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.OuterDiameterDisk,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.OuterDiameterDiskTextBox },
                    { this._label, this.OuterDiameterDiskLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.InnerDiameterDisk,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.InnerDiameterDiskTextBox },
                    { this._label, this.InnerDiameterDiskLabel },
                });
            this._parameterFormElements.Add(
                ParameterType.WidthDisk,
                new Dictionary<string, Control>
                {
                    { this._textBox, this.WidthDiskTextBox },
                    { this._label, this.WidthDiskLabel },
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
                ParameterType? parameterType = null;

                var parameterTypeStr =
                    textBox.Name.Remove(textBoxName.Length - "TextBox".Length);

                foreach (var item in this._parameterFormElements.Keys)
                {
                    if (item.ToString() == parameterTypeStr)
                    {
                        parameterType = item;
                        break;
                    }
                }

                if (parameterType.HasValue)
                {
                    try
                    {
                        this._parameters.AssertParameter(
                            parameterType.Value,
                            this._parameters.ParametersDict[parameterType.Value],
                            Convert.ToDouble(textBox.Text));
                        this.SetTextFormElements();
                        this._parameterFormElements[parameterType.Value][this._textBox].BackColor = this._defaultColor;
                        this.BuildButton.Enabled = true;
                    }
                    catch (ArgumentException ex)
                    {
                        var parameter = this._parameters.ParametersDict[parameterType.Value];
                        var minValue = parameter.MinValue;
                        var maxValue = parameter.MaxValue;
                        var message = $"{ex.Message}\n������� ����� �� {minValue} �� {maxValue}";
                        this._parameterFormElements[parameterType.Value][this._label].Text = message;
                        this._parameterFormElements[parameterType.Value][this._textBox].BackColor = this._errorColor;
                        this.BuildButton.Enabled = false;
                    }
                    catch (FormatException)
                    {
                        // ��������� ������, ���� ��������� �������� �� ����� ���� ������������� � double
                        // ��������:
                        this._parameterFormElements[parameterType.Value][this._label].Text = "������������ ������ �����";
                        this._parameterFormElements[parameterType.Value][this._textBox].BackColor = this._errorColor;
                        this.BuildButton.Enabled = false;
                    }
                    catch (OverflowException)
                    {
                        // ��������� ������, ���� ��������� �������� ������� �� ������� ����������� ��������� double
                        // ��������:
                        this._parameterFormElements[parameterType.Value][this._label].Text = "�������� ��� ����������� ���������";
                        this._parameterFormElements[parameterType.Value][this._textBox].BackColor = this._errorColor;
                        this.BuildButton.Enabled = false;
                    }
                    catch (Exception)
                    {
                        // ��������� ��������� ����������, �� �������� ����
                        // ��������:
                        this._parameterFormElements[parameterType.Value][this._label].Text = "������ �����";
                        this._parameterFormElements[parameterType.Value][this._textBox].BackColor = this._errorColor;
                        this.BuildButton.Enabled = false;
                    }
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
                in this._parameters.ParametersDict)
            {
                var key = item.Key;
                var value = item.Value;

                this._parameterFormElements[key][this._textBox].Text =
                    value.CurrentValue.ToString();
                this._parameterFormElements[key][this._label].Text =
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
            if (Ladder30DegreeRadioButton.Checked)
            {
                _builder.Degrees = 30;
            }
            else if (Ladder45DegreeRadioButton.Checked)
            {
                _builder.Degrees = 45;
            }
            else
            {
                _builder.Degrees = 90;
            }

            this._builder.BuildDetail(_parameters);
        }
    }
}